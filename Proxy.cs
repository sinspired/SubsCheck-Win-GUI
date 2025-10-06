using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace subs_check.win.gui
{
    internal class Proxy
    {
        // 常见代理端口
        private static readonly List<string> CommonProxies = new List<string>
        {
            "http://127.0.0.1:7890",
            "http://127.0.0.1:7891",
            "http://127.0.0.1:1080",
            "http://127.0.0.1:8080",
            "http://127.0.0.1:10808",
            "http://127.0.0.1:10809"
        };

        public class SysProxyResult
        {
            public bool IsAvailable { get; set; }
            public string Address { get; set; }
        }

        /// <summary>
        /// 检测系统代理是否可用，并设置环境变量
        /// </summary>
        public static Task<SysProxyResult> GetSysProxyAsync(string configProxy)
        {
            return FindAvailableSysProxyAsync(configProxy, CommonProxies)
                .ContinueWith(t =>
                {
                    string proxy = t.Result;
                    if (!string.IsNullOrEmpty(proxy))
                    {
                        Environment.SetEnvironmentVariable("HTTP_PROXY", proxy);
                        Environment.SetEnvironmentVariable("HTTPS_PROXY", proxy);

                        Console.WriteLine("系统代理可用: " + proxy);
                        return new SysProxyResult
                        {
                            IsAvailable = true,
                            Address = proxy
                        };
                    }

                    Console.WriteLine("未找到可用代理，将不设置代理");
                    return new SysProxyResult
                    {
                        IsAvailable = false,
                        Address = string.Empty
                    };
                });
        }


        /// <summary>
        /// 检测代理是否可用（要求 Google 204 和 GitHub Raw 都成功）
        /// </summary>
        private static async Task<bool> IsSysProxyAvailableAsync(string proxy)
        {
            try
            {
                var proxyUri = new Uri(proxy);
                var handler = new HttpClientHandler
                {
                    Proxy = new WebProxy(proxyUri),
                    UseProxy = true
                };

                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var testUrls = new List<Tuple<string, HttpStatusCode>>
                    {
                        Tuple.Create("https://www.google.com/generate_204", HttpStatusCode.NoContent),
                        Tuple.Create("https://raw.githubusercontent.com/github/gitignore/main/Go.gitignore", HttpStatusCode.OK)
                    };

                    var tasks = new List<Task<bool>>();
                    foreach (var t in testUrls)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            try
                            {
                                var resp = await client.GetAsync(t.Item1);
                                return resp.StatusCode == t.Item2;
                            }
                            catch
                            {
                                return false;
                            }
                        }));
                    }

                    var results = await Task.WhenAll(tasks);
                    foreach (var ok in results)
                    {
                        if (!ok) return false;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 优先检测配置文件中的代理，不可用则并发检测常见端口
        /// </summary>
        private static async Task<string> FindAvailableSysProxyAsync(string configProxy, List<string> candidates)
        {
            // Step 1: 优先检测配置文件中的代理
            if (!string.IsNullOrEmpty(configProxy) && await IsSysProxyAvailableAsync(configProxy))
            {
                return configProxy;
            }

            // Step 2: 并发检测候选代理
            var cts = new CancellationTokenSource();
            var tasks = new List<Task<string>>();

            foreach (var p in candidates)
            {
                tasks.Add(Task.Run(async () =>
                {
                    if (await IsSysProxyAvailableAsync(p))
                    {
                        cts.Cancel(); // 找到一个就取消其他任务
                        return p;
                    }
                    return null;
                }, cts.Token));
            }

            var allTasks = Task.WhenAll(tasks);
            var completed = await Task.WhenAny(allTasks, Task.Delay(5000));
            if (completed == allTasks)
            {
                foreach (var t in tasks)
                {
                    if (!string.IsNullOrEmpty(t.Result))
                        return t.Result;
                }
            }

            return string.Empty;
        }
    }
}
