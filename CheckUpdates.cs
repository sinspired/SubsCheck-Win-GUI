using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

using AutoUpdaterDotNET;

using Newtonsoft.Json.Linq;

using subs_check.win.gui.Properties;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace subs_check.win.gui
{
    public partial class CheckUpdates : Form
    {
        // 添加一个属性用于存储和传递文本内容
        public string UrlContent { get; set; }
        public System.Windows.Forms.ComboBox.ObjectCollection githubProxys { get; set; }
        public string githubProxy { get; set; }
        string githubProxyURL;
        public string 当前subsCheck版本号 { get; set; }
        public string 当前GUI版本号 { get; set; }
        public string 最新GUI版本号 { get; set; }
        public bool EnableHighConcurrent { get; set; }
        public bool EnableArch64 { get; set; }

        public CheckUpdates()
        {
            InitializeComponent();
            //注册自动更新订阅事件
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        }

        //自定义检查更新事件
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.IsUpdateAvailable)
                {
                    // 如果你想显示标准更新窗口，请取消下面这行的注释
                    AutoUpdater.ShowUpdateForm(args);
                }
            }
            else
            {
                if (args.Error is WebException)
                {
                    MessageBox.Show(
                        @"无法连接到更新服务器。请检查您的网络连接并稍后重试。",
                        @"更新检查失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(args.Error.Message,
                        args.Error.GetType().ToString(), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            timer1.Enabled = true;

            if (githubProxys != null)
            {
                comboBox1.Items.Clear();
                foreach (var item in githubProxys)
                {
                    comboBox1.Items.Add(item);
                }
            }
            if (!string.IsNullOrEmpty(githubProxy)) comboBox1.Text = githubProxy;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label3.Text = 最新GUI版本号;
            label4.Text = 当前GUI版本号;

            label5.Text = 当前subsCheck版本号;

            MainGui mainForm = new MainGui();

            githubProxyURL = await mainForm.GetGithubProxyUrlAsync();

            if (最新GUI版本号 != 当前GUI版本号)
            {
                // 检查当前目录下是否存在 Upgrade.exe
                string upgradeExePath = System.IO.Path.Combine(Application.StartupPath, "Upgrade.exe");
                if (System.IO.File.Exists(upgradeExePath))
                {
                    button1.Text = "立即更新";
                    button1.Enabled = true;
                }
                else
                {
                    button1.Text = "AutoUpdate";
                    button1.Enabled = false;
                    // 使用AutoUpdater进行更新检查
                    //AutoUpdater.Mandatory = true;
                    //AutoUpdater.UpdateMode = Mode.Forced;
                    AutoUpdater.SetOwner(CheckUpdates.ActiveForm);
                    AutoUpdater.Icon = Resources.download;
                    AutoUpdater.ShowRemindLaterButton = false;
                    AutoUpdater.ReportErrors = true;
                    AutoUpdater.HttpUserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";
                    AutoUpdater.Start("https://ghproxy.net/raw.githubusercontent.com/sinspired/subsCheck-Win-GUI/master/update.xml");
                }
            }
            else
            {
                button1.Text = "已是最新版本";
                button1.Enabled = false;
            }

            // 根据并发参数选择仓库
            string repoOwner = EnableHighConcurrent ? "sinspired" : "beck-8";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");
                    client.Timeout = TimeSpan.FromSeconds(30); // 增加超时时间以适应下载需求


                    string url = $"https://api.github.com/repos/{repoOwner}/subs-check/releases/latest";
                    string 备用url = $"https://api.github.cmliussss.net/repos/{repoOwner}/subs-check/releases/latest";

                    HttpResponseMessage response = null;
                    string responseBody = null;
                    JObject json = null;

                    // 先尝试主URL
                    try
                    {
                        response = await client.GetAsync(url);

                        // 如果主URL请求成功返回有效数据
                        if (response.IsSuccessStatusCode)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            json = JObject.Parse(responseBody);
                            Console.WriteLine("成功从主API获取版本信息");
                        }
                        // 如果主URL请求不成功但没有抛出异常
                        else
                        {
                            Console.WriteLine($"主API请求失败 HTTP {(int)response.StatusCode}，尝试备用API...");
                            response = await client.GetAsync(备用url);

                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStringAsync();
                                json = JObject.Parse(responseBody);
                                Console.WriteLine("成功从备用API获取版本信息");
                            }
                            else
                            {
                                Console.WriteLine($"备用API也请求失败: HTTP {(int)response.StatusCode}", true);
                                return; // 两个URL都失败，提前退出
                            }
                        }
                    }
                    // 捕获网络请求异常（如连接超时、无法解析域名等）
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"主API请求出错: {ex.Message}，尝试备用API...");
                        try
                        {
                            response = await client.GetAsync(备用url);
                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStringAsync();
                                json = JObject.Parse(responseBody);
                                Console.WriteLine("成功从备用API获取版本信息");
                            }
                            else
                            {
                                Console.WriteLine($"备用API也请求失败: HTTP {(int)response.StatusCode}", true);
                                return; // 备用URL也失败，提前退出
                            }
                        }
                        catch (Exception backupEx)
                        {
                            Console.WriteLine($"备用API请求也出错: {backupEx.Message}", true);
                            return; // 连备用URL也异常，提前退出
                        }
                    }
                    // 捕获JSON解析异常
                    catch (Newtonsoft.Json.JsonException ex)
                    {
                        Console.WriteLine($"解析JSON数据出错: {ex.Message}", true);
                        try
                        {
                            response = await client.GetAsync(备用url);
                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStringAsync();
                                json = JObject.Parse(responseBody);
                                Console.WriteLine("成功从备用API获取版本信息");
                            }
                        }
                        catch (Exception backupEx)
                        {
                            Console.WriteLine($"备用API请求也出错: {backupEx.Message}", true);
                            return; // 连备用URL也有问题，提前退出
                        }
                    }
                    // 捕获其他所有异常
                    catch (Exception ex)
                    {
                        Console.WriteLine($"获取版本信息时出现未预期的错误: {ex.Message}", true);
                        try
                        {
                            response = await client.GetAsync(备用url);
                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStringAsync();
                                json = JObject.Parse(responseBody);
                                Console.WriteLine("成功从备用URL获取版本信息");
                            }
                        }
                        catch (Exception backupEx)
                        {
                            //控制台打印错误
                            Console.WriteLine($"备用API请求也出错: {backupEx.Message}", true);
                            return; // 连备用URL也有问题，提前退出
                        }
                    }

                    // 如果成功获取了JSON数据，继续处理
                    if (json != null)
                    {
                        string latestVersion = json["tag_name"].ToString();
                        label6.Text = latestVersion;
                        if (当前subsCheck版本号 != latestVersion)
                        {
                            button2.ForeColor = System.Drawing.Color.Green;
                            button2.Text = "立即更新";
                            button2.Enabled = true;
                        }
                        else
                        {
                            button2.Text = "已是最新版本";
                            button2.Enabled = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"下载 subs-check.exe 时出错: {ex.Message}\n\n请前往 https://github.com/{repoOwner}/subs-check/releases 自行下载！",
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 创建专用方法用于异步检测GitHub代理
        private async Task<string> DetectGitHubProxyAsync(List<string> proxyItems)
        {
            string detectedProxyURL = "";

            // 遍历随机排序后的代理列表
            foreach (string proxyItem in proxyItems)
            {
                string checkUrl = $"https://{proxyItem}/https://raw.githubusercontent.com/cmliu/SubsCheck-Win-GUI/master/packages.config";

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(5); // 设置5秒超时
                                                                  // 添加User-Agent头，避免被拒绝访问
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                        // 使用异步方式
                        HttpResponseMessage response = await client.GetAsync(checkUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            // 找到可用代理
                            detectedProxyURL = $"https://{proxyItem}/";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 记录错误但继续尝试下一个
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return detectedProxyURL;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 设置对话框结果为OK，表示用户点击了"立即更新"按钮
            this.DialogResult = DialogResult.OK;

            // 关闭窗口
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //下载链接
            string downloadURL = $"{githubProxyURL}https://github.com/cmliu/SubsCheck-Win-GUI/releases/download/{最新GUI版本号}/SubsCheck_Win_GUI.zip";
            //目标文件
            string downloadEXE = "subs-check.win.gui.exe";

            try
            {
                // 获取应用程序目录
                string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                // 创建 Upgrade.ini 文件路径
                string iniFilePath = System.IO.Path.Combine(executablePath, "Upgrade.ini");

                // 准备 INI 文件内容
                string iniContent =
                    "[Upgrade]\r\n" +
                    $"DownloadURL={downloadURL}\r\n" +
                    $"TargetFile={downloadEXE}\r\n";

                // 写入文件（如果文件已存在会被覆盖）
                System.IO.File.WriteAllText(iniFilePath, iniContent);

                DialogResult result = MessageBox.Show(
                    $"发现新版本: {最新GUI版本号}\n\n" +
                    "· 点击【确定】将下载并安装更新\n" +
                    "· 更新过程中程序会自动关闭并重启\n" +
                    "· 更新完成后所有设置将保持不变\n\n" +
                    "是否立即更新到最新版本？",
                    "发现新版本",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    // 检查目标文件是否存在
                    string targetFilePath = System.IO.Path.Combine(Application.StartupPath, "Upgrade.exe");
                    if (System.IO.File.Exists(targetFilePath))
                    {
                        // 使用Process.Start异步启动应用程序
                        System.Diagnostics.Process.Start(targetFilePath);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("更新程序 Upgrade.exe 不存在！",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"写入更新信息时出错: {ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
