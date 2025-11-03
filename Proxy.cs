using System;
using System.Collections.Generic;
using System.Linq;
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
            "http://127.0.0.1:10809",
            "http://127.0.0.1:3067",
            "http://127.0.0.1:2080",
            "http://127.0.0.1:1194",
            "http://127.0.0.1:1082",
            "http://127.0.0.1:12334",
            "http://127.0.0.1:12335"
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
        private static async Task<bool> IsSysProxyAvailableAsync(string proxy, CancellationToken token = default)
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
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                    var testTasks = new[]
                    {
                        client.GetAsync("https://www.google.com/generate_204", token),
                        client.GetAsync("https://raw.githubusercontent.com/github/gitignore/main/Go.gitignore", token)
                    };

                    var responses = await Task.WhenAll(testTasks);
                    return responses[0].StatusCode == HttpStatusCode.NoContent && responses[1].StatusCode == HttpStatusCode.OK;
                }
            }
            catch (OperationCanceledException)
            {
                return false; // Expected cancellation
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
            var runningTasks = new List<Task<string>>();
            foreach (var p in candidates)
            {
                runningTasks.Add(Task.Run(async () =>
                {
                    if (await IsSysProxyAvailableAsync(p, cts.Token))
                    {
                        return p;
                    }
                    return null;
                }, cts.Token));
            }

            while (runningTasks.Any())
            {
                var completedTask = await Task.WhenAny(runningTasks);
                runningTasks.Remove(completedTask);

                try
                {
                    string result = await completedTask;
                    if (!string.IsNullOrEmpty(result))
                    {
                        cts.Cancel(); // 找到一个就取消其他任务
                        return result; // 立即返回结果
                    }
                }
                catch (OperationCanceledException)
                {
                    // This task was canceled because another one finished first. Ignore.
                }
                // Other exceptions can be logged if needed.
            }

            return string.Empty;
        }
    }
}
