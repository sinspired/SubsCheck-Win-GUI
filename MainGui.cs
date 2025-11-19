using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using AutoUpdaterDotNET;

using Newtonsoft.Json.Linq;

using static subs_check.win.gui.Proxy;

namespace subs_check.win.gui
{
    public partial class MainGui : Form
    {
        //string 版本号;
        string 标题;
        private Process subsCheckProcess = null;
        string nodeInfo;//进度
        private Icon originalNotifyIcon; // 保存原始图标
        private ToolStripMenuItem startMenuItem;
        private ToolStripMenuItem stopMenuItem;
        string githubProxyURL = "";
        string sysProxyURL = "";
        int run = 0;
        string 当前subsCheck版本号 = "未知版本";
        string currentKernel = "原版内核";
        string currentArch = "i386";
        string 当前GUI版本号 = "未知版本";
        string 最新GUI版本号 = "未知版本";
        private string nextCheckTime = null;// 用于存储下次检查时间
        string WebUIapiKey = "CMLiussss";
        int downloading = 0;
        private SysProxyResult SysProxySetting = null;

        private DateTime _lastCheckTime = DateTime.MinValue;

        private static DateTime _lastGetGithubProxyRunTime = DateTime.MinValue;
        private static string _lastGithubProxyUrl = null;

        // ——用于避免无意义的重复 UI 重绘——
        private string _lastLogLabelNodeInfoText = string.Empty;
        private string _lastNotifyText = string.Empty;

        // 存储groupBox原始位置
        private Point _pipeOriginalLocation;
        private Point _enhanceOriginalLocation;
        private bool _originalLocationSaved = false;

        public MainGui()
        {
            InitializeComponent();

            this.Shown += MainGui_Shown;

            originalNotifyIcon = notifyIcon1.Icon;

            // 设置提示信息
            toolTip1.SetToolTip(numericUpDownConcurrent, "并发线程数：推荐 宽带峰值/50M。\n\n如启用高并发而未单独设置分段并发数,将使用该值计算自适应并发数.\n启用高并发后,此值可安全设置,下载速度会被限制在一个较小的值,同时加快检测速度");
            toolTip1.SetToolTip(numericUpDownInterval, "检查间隔时间(分钟)：放置后台的时候，下次自动测速的间隔时间。\n\n 双击切换 使用「cron表达式」");
            toolTip1.SetToolTip(labelInterval, "检查间隔时间(分钟)：放置后台的时候，下次自动测速的间隔时间。\n\n 双击切换 使用「cron表达式」");
            toolTip1.SetToolTip(labelCron, "双击切换 使用「分钟倒计时」");
            toolTip1.SetToolTip(textBoxCron, "支持标准cron表达式，如：\n 0 */2 * * * 表示每2小时的整点执行\n 0 0 */2 * * 表示每2天的0点执行\n 0 0 1 * * 表示每月1日0点执行\n */30 * * * * 表示每30分钟执行一次\n\n 双击切换 使用「分钟倒计时」");
            toolTip1.SetToolTip(numericUpDownTimeout, "超时时间(毫秒)：节点的最大延迟。");
            toolTip1.SetToolTip(numericUpDownMinSpeed, "最低测速结果舍弃(KB/s)。");

            toolTip1.SetToolTip(checkBoxHighConcurrent, "启用流水线分段高并发版本内核。");
            toolTip1.SetToolTip(checkBoxSwitchArch64, "启用64位版本内核。");

            toolTip1.SetToolTip(buttonTriggerCheck, "⏯️开始检测：发送开始检测信号，开始检测；\n⏸️结束检测：发送停止信号，内核保持后台运行。");

            toolTip1.SetToolTip(buttonStartCheck, "启动内核检测进程。");

            toolTip1.SetToolTip(checkBoxPipeAuto, "auto: 切换自适应流水线分段并发模式。");
            toolTip1.SetToolTip(numericUpDownPipeAlive, "测活任务并发数：\n取决于CPU和路由器芯片性能，建议设置 100-1000。\n\n量力而行！");
            toolTip1.SetToolTip(numericUpDownPipeSpeed, "测速任务并发数。\n建议设置 10-32。");
            toolTip1.SetToolTip(numericUpDownPipeMedia, "流媒体检测任务并发数。\n建议设置100-200。");

            toolTip1.SetToolTip(checkBoxEhanceTag, "开启增强位置标签：\n- 无法访问 CF 的 CF 节点: HK⁻¹\r\n- 正常访问 CF: a.出口位置与cdn位置一致: HK¹⁺; b.位置不一致: HK¹-US⁰\r\n- 非 CF 节点,直接显示: HK²\r\n- 未获取到位置: HKˣ (使用原方案)\r\n- 前两位字母是实际浏览网站识别的位置,-US⁰为使用CF CDN服务的网站识别的位置,比如GPT, X等。");
            toolTip1.SetToolTip(checkBoxDropBadCFNodes, "丢弃无法访问CF CDN网站的节点。\r\n- 这类节点可以正常访问YouTube、Google等网站。\r\n- 无法访问cloudflare及使用了CDN服务的网站，比如Twitter、claude等。\r\n- 开启会导致节点数量大幅减少。");


            toolTip1.SetToolTip(comboBoxSubscriptionType, "通用订阅：内置了Sub-Store程序，自适应订阅格式。\nClash订阅：带规则的 Mihomo、Clash 订阅格式。\nSingbox：带规则的singbox订阅，需匹配版本");
            toolTip1.SetToolTip(comboBoxOverwriteUrls, "生成带规则的 Clash 订阅所需的覆写规则文件");

            toolTip1.SetToolTip(checkBoxStartup, "开机启动：勾选后，程序将在Windows启动时自动运行");
            toolTip1.SetToolTip(buttonAdvanceSettings, "高级设置：展开更多设置参数项");

            toolTip1.SetToolTip(buttonMoreSettings, "更多参数: 添加GUI未涵盖的参数项");
            toolTip1.SetToolTip(buttonCheckUpdate, "检查GUI和内核版本更新");

            toolTip1.SetToolTip(numericUpDownDLTimehot, "下载测试时间(s)：与下载链接大小相关，默认最大测试10s。");
            toolTip1.SetToolTip(numericUpDownWebUIPort, "本地监听端口：用于直接返回测速结果的节点信息，方便 Sub-Store 实现订阅转换。");
            toolTip1.SetToolTip(numericUpDownSubStorePort, "Sub-Store监听端口：用于订阅订阅转换。\n注意：除非你知道你在干什么，否则不要将你的 Sub-Store 暴露到公网，否则可能会被滥用");
            toolTip1.SetToolTip(textBoxSubStorePath, "Sub-Store自定义路径\n设置path之后，可以安全暴露到公网，开启订阅分享功能。\r\n# 订阅示例：http://127.0.0.1:8299/{sub-store-path}/api/file/mihomo\r\n# WebUI 支持分享订阅，直接复制订阅链接");

            toolTip1.SetToolTip(numericUpDownDownloadMb, "下载测试限制(MB)：当达到下载数据大小时，停止下载，可节省测速流量，减少测速测死的概率");
            toolTip1.SetToolTip(textBoxSubsUrls, "节点池订阅地址：支持 Link、Base64、Clash 格式的订阅链接。");
            toolTip1.SetToolTip(checkBoxEnableRenameNode, "以节点IP查询位置重命名节点。\n质量差的节点可能造成IP查询失败，造成整体检查速度稍微变慢。");
            toolTip1.SetToolTip(checkBoxEnableMediaCheck, "是否开启流媒体检测，其中IP欺诈依赖'节点地址查询'，内核版本需要 v2.0.8 以上\n\n示例：美国1 | ⬇️ 5.6MB/s |0%|Netflix|Disney|Openai\n风控值：0% (使用ping0.cc标准)\n流媒体解锁：Netflix、Disney、Openai");
            toolTip1.SetToolTip(comboBoxSysProxy, "系统代理设置: 适用于拉取代理、消息推送、文件上传等等。");
            toolTip1.SetToolTip(comboBoxGithubProxyUrl, "GitHub 代理：代理订阅 GitHub raw 节点池。");
            toolTip1.SetToolTip(comboBoxSpeedtestUrl, "测速地址：注意 并发数*节点速度<最大网速 否则测速结果不准确\n尽量不要使用Speedtest，Cloudflare提供的下载链接，因为很多节点屏蔽测速网站。");
            toolTip1.SetToolTip(textBox7, "将测速结果推送到Worker的地址。");
            toolTip1.SetToolTip(textBox6, "Worker令牌。");
            toolTip1.SetToolTip(comboBoxSaveMethod, "测速结果的保存方法。");
            toolTip1.SetToolTip(textBox2, "Gist ID：注意！非Github用户名！");
            toolTip1.SetToolTip(textBox3, "Github TOKEN");

            toolTip1.SetToolTip(checkBoxEnableSuccessLimit, "保存几个成功的节点，不选代表不限制，内核版本需要 v2.1.0 以上\n如果你的并发数量超过这个参数，那么成功的结果可能会大于这个数值");
            toolTip1.SetToolTip(checkBoxTotalBandwidthLimit, "总的下载速度限制,不选代表不限制");
            toolTip1.SetToolTip(numericUpDownSuccessLimit, "保存几个成功的节点，不选代表不限制，内核版本需要 v2.1.0 以上\n如果你的并发数量超过这个参数，那么成功的结果可能会大于这个数值");

            toolTip1.SetToolTip(numericUpDownTotalBandwidthLimit, "总的下载速度限制,不选代表不限制");

            toolTip1.SetToolTip(labelCron, "双击切换 使用「分钟倒计时」");

            toolTip1.SetToolTip(textBoxCron, "支持标准cron表达式，如：\n 0 */2 * * * 表示每2小时的整点执行\n 0 0 */2 * * 表示每2天的0点执行\n 0 0 1 * * 表示每月1日0点执行\n */30 * * * * 表示每30分钟执行一次\n\n 双击切换 使用「分钟倒计时」");

            toolTip1.SetToolTip(checkBoxKeepSucced, "勾选会在内存中保留成功节点以便下次使用（重启后丢失）\n可在订阅链接中添加以下地址作为替代：\n- http://127.0.0.1:8199/all.yaml#KeepSucced\n");
            toolTip1.SetToolTip(checkBoxEnableWebUI, "勾选后启用WebUI管理界面\n建议启用\n建议使用 Cloudflare Tunel隧道 映射主机端口\r\n可使用域名编辑、管理配置,开始、结束检测任务\n本地管理地址: http://127.0.0.1:8199/admin\n");
            toolTip1.SetToolTip(textBoxWebUiAPIKey, "Web控制面板的api-key");
            // 设置通知图标的上下文菜单
            SetupNotifyIconContextMenu();
        }

        private async void MainGui_Shown(object sender, EventArgs e)
        {
            // 先检查系统代理
            await AutoCheckSysProxy();

            // 再初始化 AutoUpdater
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;

            AutoUpdater.Icon = Properties.Resources.download;
            AutoUpdater.ShowRemindLaterButton = false;
            AutoUpdater.ReportErrors = true;
            AutoUpdater.HttpUserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";

            AutoUpdater.Start("https://gh.39.al/raw.githubusercontent.com/sinspired/subsCheck-Win-GUI/master/update.xml");
        }

        // 更新程序退出事件处理器
        private async void AutoUpdater_ApplicationExitEvent()
        {
            StopSubsCheckProcess();
            await KillNodeProcessAsync();
            Application.Exit();
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

        public async Task AutoCheckSysProxy(bool noRepeat = true)
        {
            // 如果10秒内已经执行过，直接返回
            if ((DateTime.Now - _lastCheckTime).TotalSeconds < 10 && noRepeat)
            {
                //Log("10秒内已检测过系统代理，跳过执行", GetRichTextBoxAllLog());
                return;
            }

            _lastCheckTime = DateTime.Now; // 更新执行时间

            // 自动检测系统代理
            string configProxy = comboBoxSysProxy.Text;
            if (configProxy == "自动检测" || string.IsNullOrEmpty(configProxy) || configProxy == "http://" || configProxy == "https://")
            {
                configProxy = "";
            }

            if ((!configProxy.StartsWith("http://") || !configProxy.StartsWith("https://")) && configProxy != "")
            {
                configProxy = "http://" + configProxy;
            }

            SysProxySetting = await Proxy.GetSysProxyAsync(configProxy);

            if (SysProxySetting != null && SysProxySetting.IsAvailable)
            {
                if (comboBoxSysProxy.Text == SysProxySetting.Address)
                {
                    Log("设置系统代理: " + SysProxySetting.Address, GetRichTextBoxAllLog());
                }
                else
                {
                    string input = SysProxySetting.Address?.Trim() ?? string.Empty;

                    // 检查是否存在 "://" 协议部分
                    int protocolIndex = input.IndexOf("://");
                    if (protocolIndex >= 0)
                    {
                        input = input.Substring(protocolIndex + 3);
                    }

                    // 检查是否存在 "/" 路径部分
                    int pathIndex = input.IndexOf('/');
                    if (pathIndex >= 0)
                    {
                        input = input.Substring(0, pathIndex);
                    }

                    comboBoxSysProxy.Text = input;
                    Log("检测到可用系统代理并设置: " + SysProxySetting.Address, GetRichTextBoxAllLog());

                    //await SaveConfig(false);
                }
            }
            else
            {
                Log("未发现系统代理或系统代理不可用", GetRichTextBoxAllLog());
            }
        }

        //临时禁用/恢复控件重绘
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x000B;

        private void SuspendRedraw(Control c)
        {
            if (c != null && c.IsHandleCreated)
                SendMessage(c.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        private void ResumeRedraw(Control c)
        {
            if (c != null && c.IsHandleCreated)
            {
                SendMessage(c.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
                c.Invalidate();
            }
        }

        private void SetupNotifyIconContextMenu()
        {
            // 创建上下文菜单
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // 创建"▶️ 启动"菜单项
            startMenuItem = new ToolStripMenuItem("启动");
            startMenuItem.Click += (sender, e) =>
            {
                if (buttonStartCheck.Text == "▶️ 启动")
                {
                    buttonStartCheck.ForeColor = Color.Black;
                    buttonStartCheck_Click(sender, e);
                }
            };

            // 创建"⏹️ 停止"菜单项
            stopMenuItem = new ToolStripMenuItem("停止");
            stopMenuItem.Click += (sender, e) =>
            {
                if (buttonStartCheck.Text == "⏹️ 停止")
                {
                    buttonStartCheck.ForeColor = Color.Red;
                    buttonStartCheck_Click(sender, e);
                }
            };
            stopMenuItem.Enabled = false; // 初始状态下禁用

            // 创建"退出"菜单项
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("退出");
            exitMenuItem.Click += async (sender, e) =>
            {
                try
                {
                    // 如果程序正在运行，先停止它
                    if (subsCheckProcess != null && !subsCheckProcess.HasExited)
                    {
                        await KillNodeProcessAsync();
                        StopSubsCheckProcess();
                    }

                    // 确保通知图标被移除
                    notifyIcon1.Visible = false;

                    // 使用更直接的退出方式
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"退出程序时发生错误: {ex.Message}", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // 如果仍然失败，尝试强制退出
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            };

            // 将菜单项添加到上下文菜单
            contextMenu.Items.Add(startMenuItem);
            contextMenu.Items.Add(stopMenuItem);
            contextMenu.Items.Add(new ToolStripSeparator()); // 分隔线
            contextMenu.Items.Add(exitMenuItem);

            // 将上下文菜单分配给通知图标
            notifyIcon1.ContextMenuStrip = contextMenu;

            // 确保通知图标可见
            notifyIcon1.Visible = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 取消关闭操作
                e.Cancel = true;
                // 调用隐藏窗口方法
                隐藏窗口();
            }
        }

        private async void timerinitial_Tick(object sender, EventArgs e)//初始化
        {
            timerinitial.Enabled = false;
            if (buttonAdvanceSettings.Text == "高级设置∧") buttonAdvanceSettings_Click(sender, e);
            // 检查并创建config文件夹
            string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string configFolderPath = System.IO.Path.Combine(executablePath, "config");
            if (!System.IO.Directory.Exists(configFolderPath))
            {
                // 文件不存在，可以给用户反馈
                string 免责声明 = "SubsCheck-Win-GUI 项目仅供教育、研究和安全测试目的而设计和开发。本项目旨在为安全研究人员、学术界人士及技术爱好者提供一个探索和实践网络通信技术的工具。\r\n在下载和使用本项目代码时，使用者必须严格遵守其所适用的法律和规定。使用者有责任确保其行为符合所在地区的法律框架、规章制度及其他相关规定。\r\n\r\n使用条款\r\n\r\n教育与研究用途：本软件仅可用于网络技术和编程领域的学习、研究和安全测试。\r\n禁止非法使用：严禁将 SubsCheck-Win-GUI 用于任何非法活动或违反使用者所在地区法律法规的行为。\r\n使用时限：基于学习和研究目的，建议用户在完成研究或学习后，或在安装后的24小时内，删除本软件及所有相关文件。\r\n免责声明：SubsCheck-Win-GUI 的创建者和贡献者不对因使用或滥用本软件而导致的任何损害或法律问题负责。\r\n用户责任：用户对使用本软件的方式以及由此产生的任何后果完全负责。\r\n无技术支持：本软件的创建者不提供任何技术支持或使用协助。\r\n知情同意：使用 SubsCheck-Win-GUI 即表示您已阅读并理解本免责声明，并同意受其条款的约束。\r\n\r\n请记住：本软件的主要目的是促进学习、研究和安全测试。创作者不支持或认可任何其他用途。使用者应当在合法和负责任的前提下使用本工具。\r\n\r\n同意以上条款请点击\"是 / Yes\"，否则程序将退出。";

                // 显示带有 "同意" 和 "拒绝" 选项的对话框
                DialogResult result = MessageBox.Show(免责声明, "SubsCheck-Win-GUI 免责声明", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // 如果用户点击 "拒绝" (对应于 No 按钮)
                if (result == DialogResult.No)
                {
                    // 退出程序
                    Environment.Exit(0); // 立即退出程序
                }
                System.IO.Directory.CreateDirectory(configFolderPath);
            }

            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            当前GUI版本号 = "v" + myFileVersionInfo.FileVersion;
            最新GUI版本号 = 当前GUI版本号;
            标题 = "SubsCheck Win GUI " + 当前GUI版本号;
            this.Text = 标题;// + " TG:CMLiussss BY:CM喂饭 干货满满";
            comboBoxSaveMethod.Text = "本地";
            comboBoxSubscriptionType.Text = "通用订阅";

            ReadConfig();

            if (checkBoxHighConcurrent.Checked)
            {
                groupBoxGist.Location = new Point(groupBoxGist.Location.X, groupBoxGist.Location.Y + groupBoxPipeConcurrent.Height);
                groupBoxR2.Location = groupBoxGist.Location;
                groupBoxWebdav.Location = groupBoxGist.Location;
            }

            if (CheckCommandLineParameter("-auto"))
            {
                Log("检测到开机启动，准备执行任务...", GetRichTextBoxAllLog());
                buttonStartCheck_Click(this, EventArgs.Empty);
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else await CheckGitHubVersionAsync();
        }

        private async Task CheckGitHubVersionAsync()
        {
            try
            {
                // 首先检查是否有网络连接
                if (!IsNetworkAvailable())
                {
                    return; // 静默返回，不显示错误
                }

                var result = await 获取版本号("https://api.github.com/repos/sinspired/SubsCheck-Win-GUI/releases/latest");
                if (result.Item1 != "未知版本")
                {
                    string latestVersionStr = result.Item1;
                    try
                    {
                        // 移除版本号前的 'v' 前缀以便正确解析
                        string latestVersionToParse = latestVersionStr.StartsWith("v") ? latestVersionStr.Substring(1) : latestVersionStr;
                        string currentVersionToParse = 当前GUI版本号.StartsWith("v") ? 当前GUI版本号.Substring(1) : 当前GUI版本号;

                        Version latestVersion = new Version(latestVersionToParse);
                        Version currentVersion = new Version(currentVersionToParse);

                        if (latestVersion > currentVersion)
                        {
                            最新GUI版本号 = latestVersionStr;
                            标题 = "SubsCheck Win GUI " + 当前GUI版本号 + $"  发现新版本: {最新GUI版本号} 请及时更新！";
                            this.Text = 标题;
                        }
                    }
                    catch (Exception)
                    {
                        // 版本号格式解析失败，回退到原始的字符串比较
                        if (latestVersionStr != 当前GUI版本号)
                        {
                            最新GUI版本号 = latestVersionStr;
                            标题 = "SubsCheck Win GUI " + 当前GUI版本号 + $"  发现新版本: {最新GUI版本号} 请及时更新！";
                            this.Text = 标题;
                        }
                    }
                }
            }
            catch
            {
                // 静默处理任何其他异常
                return;
            }
        }

        // 添加检查网络连接的辅助方法
        private bool IsNetworkAvailable()
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
            catch
            {
                return false; // 如果无法检查网络状态，假设网络不可用
            }
        }

        private async void ReadConfig()//读取配置文件
        {
            checkBoxStartup.CheckedChanged -= checkBoxStartup_CheckedChanged;// 临时移除事件处理器，防止触发事件
            try
            {
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string configFilePath = Path.Combine(executablePath, "config", "config.yaml");

                if (File.Exists(configFilePath))
                {
                    // 读取YAML文件内容
                    string yamlContent = File.ReadAllText(configFilePath);

                    // 使用YamlDotNet解析YAML
                    var deserializer = new YamlDotNet.Serialization.Deserializer();
                    var config = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);


                    // 变量放在前面,以防后续读取时未定义
                    string subscheckArch = 读取config字符串(config, "subscheck-arch");
                    if (!string.IsNullOrWhiteSpace(subscheckArch)) currentArch = subscheckArch;

                    string subscheckKernel = 读取config字符串(config, "subscheck-kernel");
                    if (!string.IsNullOrWhiteSpace(subscheckKernel)) currentKernel = subscheckKernel;

                    string githubproxy = 读取config字符串(config, "githubproxy");
                    if (githubproxy != null) comboBoxGithubProxyUrl.Text = githubproxy;

                    const string githubRawPrefix = "https://raw.githubusercontent.com/";

                    // 使用新函数获取整数值并设置UI控件
                    int? concurrentValue = 读取config整数(config, "concurrent");
                    if (concurrentValue.HasValue) numericUpDownConcurrent.Value = concurrentValue.Value;

                    // 临时禁用事件
                    numericUpDownPipeAlive.ValueChanged -= numericUpDownPipeAlive_ValueChanged;
                    numericUpDownPipeSpeed.ValueChanged -= numericUpDownPipeSpeed_ValueChanged;
                    numericUpDownPipeMedia.ValueChanged -= numericUpDownPipeMedia_ValueChanged;

                    // 测活/测速/流媒体阶段并发数（先赋值到控件，再从控件读取最终值，保证非 null）
                    int? aliveConcurrentValue = 读取config整数(config, "alive-concurrent");
                    if (aliveConcurrentValue.HasValue) numericUpDownPipeAlive.Value = aliveConcurrentValue.Value;

                    int? speedConcurrentValue = 读取config整数(config, "speed-concurrent");
                    if (speedConcurrentValue.HasValue) numericUpDownPipeSpeed.Value = speedConcurrentValue.Value;

                    int? mediaConcurrentValue = 读取config整数(config, "media-concurrent");
                    if (mediaConcurrentValue.HasValue) numericUpDownPipeMedia.Value = mediaConcurrentValue.Value;

                    // 根据各阶段并发数切换设置项, 如果任一为0, 则启用自适应高并发
                    switchPipeAutoConcurrent(); // 现在控件已被赋值，函数可以安全读取 numericUpDown 的值

                    // 重新启用事件
                    numericUpDownPipeAlive.ValueChanged += numericUpDownPipeAlive_ValueChanged;
                    numericUpDownPipeSpeed.ValueChanged += numericUpDownPipeSpeed_ValueChanged;
                    numericUpDownPipeMedia.ValueChanged += numericUpDownPipeMedia_ValueChanged;

                    // Enhance-tag 相关设置（稳健解析 "true"/"false"）
                    string enhanceTagRaw = 读取config字符串(config, "enhanced-tag");
                    bool enhanceTagFlag = false;
                    if (!string.IsNullOrWhiteSpace(enhanceTagRaw))
                    {
                        bool.TryParse(enhanceTagRaw.Trim(), out enhanceTagFlag);
                    }
                    checkBoxEhanceTag.Checked = enhanceTagFlag;

                    // 丢弃低质量的 cf 节点
                    string dropBadCFRaw = 读取config字符串(config, "drop-bad-cf-nodes");
                    bool dropBadCFFlag = false;
                    if (!string.IsNullOrWhiteSpace(dropBadCFRaw))
                    {
                        bool.TryParse(dropBadCFRaw.Trim(), out dropBadCFFlag);
                    }
                    checkBoxDropBadCFNodes.Checked = dropBadCFFlag;

                    // 读取 enable-high-concurrent，并解析为 bool
                    string enableHighConcurrentRaw = 读取config字符串(config, "enable-high-concurrent");
                    bool enableHighConcurrentFlag = false;
                    if (!string.IsNullOrWhiteSpace(enableHighConcurrentRaw))
                    {
                        bool.TryParse(enableHighConcurrentRaw.Trim(), out enableHighConcurrentFlag);
                    }

                    // 决定是否启用高并发：只要显式开启 或 drop/enhance 为 true 或 三阶段并发均 > 0
                    bool needHighConcurrent = enableHighConcurrentFlag
                                            || dropBadCFFlag
                                            || enhanceTagFlag
                                            || (aliveConcurrentValue > 0 && speedConcurrentValue > 0 && mediaConcurrentValue > 0);

                    checkBoxHighConcurrent.Checked = needHighConcurrent;

                    if (checkBoxHighConcurrent.Checked)
                    {
                        comboBoxSubscriptionType.Items.AddRange(new object[] { "Singbox1.11", "Singbox1.12" });
                    }

                    // 根据是否启用高并发，调整界面布局
                    string sysproxy;
                    if (!checkBoxHighConcurrent.Checked)
                    {
                        sysproxy = 读取config字符串(config, "proxy");
                        if (sysproxy == null || sysproxy == "")
                        {
                            sysproxy = 读取config字符串(config, "system-proxy");
                        }
                    }
                    else
                    {
                        sysproxy = 读取config字符串(config, "system-proxy");
                        if (sysproxy == null || sysproxy == "")
                        {
                            sysproxy = 读取config字符串(config, "proxy");
                        }
                    }
                    if (sysproxy != null)
                    {
                        string input = sysproxy.Trim();

                        // 检查是否存在 "://" 协议部分
                        int protocolIndex = input.IndexOf("://");
                        if (protocolIndex >= 0)
                        {
                            // 保留 "://" 之后的内容
                            input = input.Substring(protocolIndex + 3);
                        }

                        // 检查是否存在 "/" 路径部分
                        int pathIndex = input.IndexOf('/');
                        if (pathIndex >= 0)
                        {
                            // 只保留 "/" 之前的域名部分
                            input = input.Substring(0, pathIndex);
                        }

                        // 更新 comboBox3 的文本
                        sysproxy = input;
                        comboBoxSysProxy.Text = sysproxy;
                    }
                    else
                    {
                        comboBoxSysProxy.Text = "自动检测";
                    }

                    string switchX64 = 读取config字符串(config, "switch-x64");
                    if (switchX64 != null && switchX64 == "true") checkBoxSwitchArch64.Checked = true;
                    else checkBoxSwitchArch64.Checked = false;


                    int? checkIntervalValue = 读取config整数(config, "check-interval");
                    if (checkIntervalValue.HasValue) numericUpDownInterval.Value = checkIntervalValue.Value;

                    int? timeoutValue = 读取config整数(config, "timeout");
                    if (timeoutValue.HasValue) numericUpDownTimeout.Value = timeoutValue.Value;

                    int? minspeedValue = 读取config整数(config, "min-speed");
                    if (minspeedValue.HasValue) numericUpDownMinSpeed.Value = minspeedValue.Value;

                    int? downloadtimeoutValue = 读取config整数(config, "download-timeout");
                    if (downloadtimeoutValue.HasValue) numericUpDownDLTimehot.Value = downloadtimeoutValue.Value;

                    int? downloadLimitSizeValue = 读取config整数(config, "download-mb");
                    if (downloadLimitSizeValue.HasValue) numericUpDownDownloadMb.Value = downloadLimitSizeValue.Value;

                    int? downloadLimitSpeedValue = 读取config整数(config, "total-speed-limit");
                    if (downloadLimitSpeedValue.HasValue) numericUpDownTotalBandwidthLimit.Value = downloadLimitSpeedValue.Value;

                    string speedTestUrl = 读取config字符串(config, "speed-test-url");
                    if (checkBoxHighConcurrent.Checked && !comboBoxSpeedtestUrl.Items.Contains("random"))
                    {
                        // 只有当列表里至少有1个元素时，才能插在第2个位置（索引1）  
                        // 否则只能插在第1个位置（索引0）  
                        int insertIndex = comboBoxSpeedtestUrl.Items.Count > 0 ? 1 : 0;

                        comboBoxSpeedtestUrl.Items.Insert(insertIndex, "random");
                    }
                    else
                    {
                        comboBoxSpeedtestUrl.Items.Remove("random");
                        if (comboBoxSpeedtestUrl.Text == "random") comboBoxSpeedtestUrl.Text = "不测速";
                    }

                    if (speedTestUrl != null)
                    {
                        if (speedTestUrl == "")
                        {
                            comboBoxSpeedtestUrl.Text = "不测速";

                        }
                        else
                        {
                            comboBoxSpeedtestUrl.Items.Add(speedTestUrl);
                            comboBoxSpeedtestUrl.Text = speedTestUrl;
                        }
                    }
                    else
                    {
                        comboBoxSpeedtestUrl.Text = "不测速";
                    }

                    string listenport = 读取config字符串(config, "listen-port");
                    if (listenport != null)
                    {
                        // 查找最后一个冒号的位置
                        int colonIndex = listenport.LastIndexOf(':');
                        if (colonIndex >= 0 && colonIndex < listenport.Length - 1)
                        {
                            // 提取冒号后面的部分作为端口号
                            string portStr = listenport.Substring(colonIndex + 1);
                            if (decimal.TryParse(portStr, out decimal port))
                            {
                                numericUpDownWebUIPort.Value = port;
                            }
                        }
                    }

                    /*
                    int? substoreport = 读取config整数(config, "sub-store-port");
                    if (substoreport.HasValue) numericUpDown7.Value = substoreport.Value;
                    */

                    string substoreport = 读取config字符串(config, "sub-store-port");
                    if (substoreport != null)
                    {
                        // 查找最后一个冒号的位置
                        int colonIndex = substoreport.LastIndexOf(':');
                        if (colonIndex >= 0 && colonIndex < substoreport.Length - 1)
                        {
                            // 提取冒号后面的部分作为端口号
                            string portStr = substoreport.Substring(colonIndex + 1);
                            if (decimal.TryParse(portStr, out decimal port))
                            {
                                numericUpDownSubStorePort.Value = port;
                            }
                        }
                    }

                    string mihomoOverwriteUrl = 读取config字符串(config, "mihomo-overwrite-url");
                    int mihomoOverwriteUrlIndex = mihomoOverwriteUrl.IndexOf(githubRawPrefix);
                    if (mihomoOverwriteUrl != null)
                    {
                        if (mihomoOverwriteUrl.Contains("http://127.0.0"))
                        {
                            if (mihomoOverwriteUrl.EndsWith("bdg.yaml", StringComparison.OrdinalIgnoreCase))
                            {
                                comboBoxOverwriteUrls.Text = "[内置]布丁狗的订阅转换";
                                await comboBoxOverwriteUrlsSelection();
                            }
                            else if (mihomoOverwriteUrl.EndsWith("ACL4SSR_Online_Full.yaml", StringComparison.OrdinalIgnoreCase))
                            {
                                comboBoxOverwriteUrls.Text = "[内置]ACL4SSR_Online_Full";
                                await comboBoxOverwriteUrlsSelection();
                            }
                        }
                        else if (mihomoOverwriteUrlIndex > 0) comboBoxOverwriteUrls.Text = mihomoOverwriteUrl.Substring(mihomoOverwriteUrlIndex);
                        else comboBoxOverwriteUrls.Text = mihomoOverwriteUrl;
                    }

                    // 处理URLs，检查是否包含GitHub raw链接
                    List<string> subUrls = 读取config列表(config, "sub-urls");
                    if (subUrls != null && subUrls.Count > 0)
                    {
                        // 创建一个新的过滤后的列表
                        var filteredUrls = new List<string>();

                        for (int i = 0; i < subUrls.Count; i++)
                        {
                            // 排除本地URL
                            string localUrlPattern = $"http://127.0.0.1:{numericUpDownWebUIPort.Value}/all.yaml";
                            if (!subUrls[i].Equals(localUrlPattern, StringComparison.OrdinalIgnoreCase))
                            {
                                // 处理GitHub raw链接
                                int index = subUrls[i].IndexOf(githubRawPrefix);
                                if (index > 0) // 如果找到且不在字符串开头
                                {
                                    // 只保留从githubRawPrefix开始的部分
                                    filteredUrls.Add(subUrls[i].Substring(index));
                                }
                                else
                                {
                                    // 如果不是GitHub链接，直接添加
                                    filteredUrls.Add(subUrls[i]);
                                }
                            }
                        }

                        // 将过滤后的列表中的每个URL放在单独的行上
                        textBoxSubsUrls.Text = string.Join(Environment.NewLine, filteredUrls);
                    }

                    string renamenode = 读取config字符串(config, "rename-node");
                    if (renamenode != null && renamenode == "true") checkBoxEnableRenameNode.Checked = true;
                    else checkBoxEnableRenameNode.Checked = false;

                    string mediacheck = 读取config字符串(config, "media-check");
                    if (mediacheck != null && mediacheck == "true") checkBoxEnableMediaCheck.Checked = true;
                    else checkBoxEnableMediaCheck.Checked = false;

                    string githubapimirror = 读取config字符串(config, "github-api-mirror");
                    if (githubapimirror != null) textBox4.Text = githubapimirror;


                    string savemethod = 读取config字符串(config, "save-method");
                    if (savemethod != null)
                    {
                        if (savemethod == "local") comboBoxSaveMethod.Text = "本地";
                        else comboBoxSaveMethod.Text = savemethod;
                    }

                    string githubgistid = 读取config字符串(config, "github-gist-id");
                    if (githubgistid != null) textBox2.Text = githubgistid;
                    string githubtoken = 读取config字符串(config, "github-token");
                    if (githubtoken != null) textBox3.Text = githubtoken;

                    string workerurl = 读取config字符串(config, "worker-url");
                    if (workerurl != null) textBox7.Text = workerurl;
                    string workertoken = 读取config字符串(config, "worker-token");
                    if (workertoken != null) textBox6.Text = workertoken;

                    string webdavusername = 读取config字符串(config, "webdav-username");
                    if (webdavusername != null) textBox9.Text = webdavusername;
                    string webdavpassword = 读取config字符串(config, "webdav-password");
                    if (webdavpassword != null) textBox8.Text = webdavpassword;
                    string webdavurl = 读取config字符串(config, "webdav-url");
                    if (webdavurl != null) textBox5.Text = webdavurl;

                    string subscheckversion = 读取config字符串(config, "subscheck-version");
                    if (subscheckversion != null) 当前subsCheck版本号 = subscheckversion;

                    string keepSucced = 读取config字符串(config, "keep-success-proxies");
                    if (keepSucced != null && keepSucced == "true") checkBoxKeepSucced.Checked = true;
                    else checkBoxKeepSucced.Checked = false;

                    string SubsStats = 读取config字符串(config, "sub-urls-stats");
                    if (SubsStats != null && SubsStats == "true") checkBoxSubsStats.Checked = true;
                    else checkBoxSubsStats.Checked = false;

                    int? successlimit = 读取config整数(config, "success-limit");
                    if (successlimit.HasValue)
                    {
                        if (successlimit.Value == 0)
                        {
                            checkBoxEnableSuccessLimit.Checked = false;
                            numericUpDownSuccessLimit.Enabled = false;
                        }
                        else
                        {
                            checkBoxEnableSuccessLimit.Checked = true;
                            numericUpDownSuccessLimit.Enabled = true;
                            numericUpDownSuccessLimit.Value = successlimit.Value;
                        }
                    }

                    int? totalspeedlimit = 读取config整数(config, "total-speed-limit");
                    if (totalspeedlimit.HasValue)
                    {
                        if (totalspeedlimit.Value == 0)
                        {
                            checkBoxTotalBandwidthLimit.Checked = false;
                            numericUpDownTotalBandwidthLimit.Enabled = false;
                        }
                        else
                        {
                            checkBoxTotalBandwidthLimit.Checked = true;
                            numericUpDownTotalBandwidthLimit.Enabled = true;
                            numericUpDownTotalBandwidthLimit.Value = totalspeedlimit.Value;
                        }
                    }

                    string enablewebui = 读取config字符串(config, "enable-web-ui");
                    if (enablewebui != null && enablewebui == "true") checkBoxEnableWebUI.Checked = true;
                    else checkBoxEnableWebUI.Checked = false;

                    string apikey = 读取config字符串(config, "api-key");
                    if (apikey != null)
                    {
                        if (apikey == GetComputerNameMD5())
                        {
                            checkBoxEnableWebUI.Checked = false;
                            string oldapikey = 读取config字符串(config, "old-api-key");
                            if (oldapikey != null)
                            {
                                textBoxWebUiAPIKey.Text = oldapikey;
                            }
                            else
                            {
                                textBoxWebUiAPIKey.PasswordChar = '\0';
                                textBoxWebUiAPIKey.Text = "请输入密钥";
                                textBoxWebUiAPIKey.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            textBoxWebUiAPIKey.Text = apikey;
                        }
                    }

                    string substorePath = 读取config字符串(config, "sub-store-path");
                    if (!string.IsNullOrEmpty(substorePath))
                    {
                        if (substorePath.StartsWith("/")) substorePath = substorePath.Substring(1);

                        textBoxSubStorePath.Text = substorePath;
                    }

                    string cronexpression = 读取config字符串(config, "cron-expression");
                    if (cronexpression != null)
                    {
                        textBoxCron.Text = cronexpression;
                        string cronDescription = GetCronExpressionDescription(textBoxCron.Text);
                        labelCron.Location = new Point(labelCron.Location.X, labelInterval.Location.Y);
                        textBoxCron.Location = new Point(textBoxCron.Location.X, numericUpDownInterval.Location.Y);
                        labelCron.Visible = true;
                        textBoxCron.Visible = true;
                        labelInterval.Visible = false;
                        numericUpDownInterval.Visible = false;
                    }

                    string guiauto = 读取config字符串(config, "gui-auto");
                    if (guiauto != null && guiauto == "true") checkBoxStartup.Checked = true;
                    else checkBoxStartup.Checked = false;
                }
                else
                {
                    comboBoxGithubProxyUrl.Text = "自动选择";
                    comboBoxSysProxy.Text = "自动检测";

                    comboBoxOverwriteUrls.Text = "[内置]布丁狗的订阅转换";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取配置文件时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            checkBoxStartup.CheckedChanged += checkBoxStartup_CheckedChanged;// 重新绑定事件处理器
        }

        private int? 读取config整数(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                int value;
                if (int.TryParse(config[fieldName].ToString(), out value))
                    return value;
            }
            return null;
        }

        private string 读取config字符串(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                return config[fieldName].ToString();
            }
            return null;
        }

        private List<string> 读取config列表(Dictionary<string, object> config, string fieldName)
        {
            // 检查是否存在指定字段且不为null
            if (config.ContainsKey(fieldName) && config[fieldName] != null)
            {
                // 尝试将对象转换为列表
                if (config[fieldName] is List<object> listItems)
                {
                    return listItems.Select(item => item?.ToString()).Where(s => !string.IsNullOrEmpty(s)).ToList();
                }
            }
            return null;
        }

        private async Task SaveConfig(bool githubProxyCheck = true)//保存配置文件
        {
            try
            {
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string configFilePath = Path.Combine(executablePath, "config", "config.yaml");

                // 创建配置字典
                var config = new Dictionary<string, object>();

                // 从UI控件获取值并添加到字典中
                config["concurrent"] = (int)numericUpDownConcurrent.Value;


                // 测活阶段并发数
                config["alive-concurrent"] = (int)numericUpDownPipeAlive.Value;
                config["speed-concurrent"] = (int)numericUpDownPipeSpeed.Value;
                config["media-concurrent"] = (int)numericUpDownPipeMedia.Value;
                // Enhance-tag相关设置
                config["enhanced-tag"] = checkBoxEhanceTag.Checked;
                // 丢弃低质量的cf节点
                config["drop-bad-cf-nodes"] = checkBoxDropBadCFNodes.Checked;


                config["check-interval"] = (int)numericUpDownInterval.Value;
                if (textBoxCron.Visible) config["cron-expression"] = textBoxCron.Text;
                config["timeout"] = (int)numericUpDownTimeout.Value;
                config["min-speed"] = (int)numericUpDownMinSpeed.Value;
                config["download-timeout"] = (int)numericUpDownDLTimehot.Value;
                config["download-mb"] = (int)numericUpDownDownloadMb.Value;
                config["total-speed-limit"] = (int)numericUpDownTotalBandwidthLimit.Value;


                if (!string.IsNullOrEmpty(comboBoxSpeedtestUrl.Text))
                {
                    string testURL = comboBoxSpeedtestUrl.Text;
                    if (comboBoxSpeedtestUrl.Text == "不测速") testURL = "";
                    config["speed-test-url"] = testURL;
                }
                // 保存save-method，将"本地"转换为"local"
                config["save-method"] = comboBoxSaveMethod.Text == "本地" ? "local" : comboBoxSaveMethod.Text;

                // 保存gist参数
                config["github-gist-id"] = textBox2.Text;
                config["github-token"] = textBox3.Text;

                config["github-api-mirror"] = textBox4.Text;

                // 保存r2参数
                config["worker-url"] = textBox7.Text;
                config["worker-token"] = textBox6.Text;

                // 保存webdav参数
                config["webdav-username"] = textBox9.Text;
                config["webdav-password"] = textBox8.Text;
                config["webdav-url"] = textBox5.Text;

                // 保存enable-web-ui
                config["enable-web-ui"] = true;

                // 保存listen-port
                if (checkBoxEnableWebUI.Checked)
                {
                    WebUIapiKey = textBoxWebUiAPIKey.Text;
                    config["listen-port"] = $@":{numericUpDownWebUIPort.Value}";
                }
                else
                {
                    WebUIapiKey = GetComputerNameMD5();
                    config["listen-port"] = $@":{numericUpDownWebUIPort.Value}";
                    if (textBoxWebUiAPIKey.Text != "请输入密钥") config["old-api-key"] = textBoxWebUiAPIKey.Text;
                }
                config["api-key"] = WebUIapiKey;

                string substorePath = textBoxSubStorePath.Text.Trim();
                // 如果不是以 "/" 开头，则补上
                if (!substorePath.StartsWith("/")) substorePath = "/" + substorePath;
                // 如果是默认提示文字或仅有 "/"，则清空
                if (substorePath == "/请输入路径" || substorePath == "/") substorePath = "";

                config["sub-store-path"] = substorePath;

                // 保存sub-store-port
                config["sub-store-port"] = $@":{numericUpDownSubStorePort.Value}";

                string githubRawPrefix = "https://raw.githubusercontent.com/";
                if (githubProxyCheck)
                {
                    // 检查并处理 GitHub Raw URLs
                    githubProxyURL = await GetGithubProxyUrlAsync();
                }

                if (comboBoxGithubProxyUrl.Text != "自动选择") githubProxyURL = $"https://{comboBoxGithubProxyUrl.Text}/";
                config["githubproxy"] = comboBoxGithubProxyUrl.Text;
                config["github-proxy"] = githubProxyURL;

                if (comboBoxSysProxy.Text != "自动检测") sysProxyURL = $"http://{comboBoxSysProxy.Text}";
                if (checkBoxHighConcurrent.Checked)
                {
                    config["system-proxy"] = sysProxyURL;
                    config["proxy"] = sysProxyURL;
                }
                else
                {
                    config["proxy"] = sysProxyURL;
                    config["system-proxy"] = sysProxyURL;
                }

                // 保存订阅列表
                List<string> subUrls = new List<string>();
                // 使用 HashSet 来快速判重（不区分大小写），只比较主干部分（去掉 fragment）
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                string allyamlFilePath = System.IO.Path.Combine(executablePath, "output", "all.yaml");
                if (System.IO.File.Exists(allyamlFilePath) && checkBoxKeepSucced.Checked && !checkBoxHighConcurrent.Checked)
                {
                    string succedProxiesUrl = $"http://127.0.0.1:{Convert.ToInt32(numericUpDownWebUIPort.Value)}/all.yaml#KeepSucced";
                    string succedProxiesUrlKey = succedProxiesUrl.Split('#')[0];
                    if (seen.Add(succedProxiesUrlKey))
                    {
                        subUrls.Add(succedProxiesUrl);
                    }
                    Log("已加载上次测试结果。", GetRichTextBoxAllLog());
                }
                else
                {
                    Log("不加载上次测试结果。", GetRichTextBoxAllLog());
                }

                if (!string.IsNullOrWhiteSpace(textBoxSubsUrls.Text))
                {
                    var lines = textBoxSubsUrls.Text
                        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s));

                    foreach (var line in lines)
                    {
                        string key = line.Split('#')[0];
                        if (seen.Add(key))
                        {
                            subUrls.Add(line);
                        }
                    }

                    for (int i = 0; i < subUrls.Count; i++)
                    {
                        var url = subUrls[i];
                        if (url.StartsWith(githubRawPrefix, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(githubProxyURL))
                        {
                            subUrls[i] = githubRawPrefix + url.Substring(githubRawPrefix.Length);
                        }
                    }
                }

                config["sub-urls"] = subUrls;

                // 处理配置文件下载与配置
                if (comboBoxOverwriteUrls.Text.Contains("[内置]"))
                {
                    // 确定文件名和下载URL
                    string fileName;
                    string downloadFilePath;
                    string downloadUrl;
                    string displayName;

                    if (comboBoxOverwriteUrls.Text.Contains("[内置]布丁狗"))
                    {
                        fileName = "bdg.yaml";
                        displayName = "[内置]布丁狗的订阅转换";
                        downloadUrl = "https://raw.githubusercontent.com/cmliu/ACL4SSR/main/yaml/bdg.yaml";
                    }
                    else // [内置]ACL4SSR
                    {
                        fileName = "ACL4SSR_Online_Full.yaml";
                        displayName = "[内置]ACL4SSR_Online_Full";
                        downloadUrl = "https://raw.githubusercontent.com/beck-8/override-hub/main/yaml/ACL4SSR_Online_Full.yaml";
                    }

                    // 确保output文件夹存在
                    string outputFolderPath = Path.Combine(executablePath, "output");
                    if (!Directory.Exists(outputFolderPath))
                    {
                        Directory.CreateDirectory(outputFolderPath);
                    }

                    // 确定文件完整路径
                    downloadFilePath = Path.Combine(outputFolderPath, fileName);
                    if (!File.Exists(downloadFilePath)) await comboBoxOverwriteUrlsSelection();

                    // 检查文件是否存在
                    if (!File.Exists(downloadFilePath))
                    {
                        Log($"{displayName} 覆写配置文件 未找到，将使用在线版本。", GetRichTextBoxAllLog());
                        config["mihomo-overwrite-url"] = githubProxyURL + downloadUrl;
                    }
                    else
                    {
                        Log($"{displayName} 覆写配置文件 加载成功。", GetRichTextBoxAllLog());
                        config["mihomo-overwrite-url"] = $"http://127.0.0.1:{numericUpDownWebUIPort.Value}/{fileName}";
                    }

                }
                else if (comboBoxOverwriteUrls.Text.StartsWith(githubRawPrefix)) config["mihomo-overwrite-url"] = githubProxyURL + comboBoxOverwriteUrls.Text;
                else config["mihomo-overwrite-url"] = comboBoxOverwriteUrls.Text != "" ? comboBoxOverwriteUrls.Text : $"http://127.0.0.1:{numericUpDownWebUIPort.Value}/ACL4SSR_Online_Full.yaml";

                config["enable-high-concurrent"] = checkBoxHighConcurrent.Checked;//使用自适应高并发版本
                config["switch-x64"] = checkBoxSwitchArch64.Checked;//是否使用x64内核
                config["rename-node"] = checkBoxEnableRenameNode.Checked;//以节点IP查询位置重命名节点
                config["media-check"] = checkBoxEnableMediaCheck.Checked;//是否开启流媒体检测
                config["keep-success-proxies"] = checkBoxKeepSucced.Checked;//是否保留成功的节点
                config["print-progress"] = false;//是否显示进度
                config["sub-urls-retry"] = 3;//重试次数(获取订阅失败后重试次数)
                config["subscheck-version"] = 当前subsCheck版本号;//当前subsCheck版本号
                config["subscheck-arch"] = currentArch; //当前subsCheck架构
                config["subscheck-kernel"] = currentKernel; //当前内核

                config["gui-auto"] = checkBoxStartup.Checked;//是否开机自启

                //保存几个成功的节点，为0代表不限制 
                if (checkBoxEnableSuccessLimit.Checked) config["success-limit"] = (int)numericUpDownSuccessLimit.Value;
                else config["success-limit"] = 0;

                //下载速度限制,为0代表不限制
                if (checkBoxTotalBandwidthLimit.Checked) config["total-speed-limit"] = (int)numericUpDownTotalBandwidthLimit.Value;
                else config["total-speed-limit"] = 0;

                // 使用YamlDotNet序列化配置
                var serializer = new YamlDotNet.Serialization.SerializerBuilder()
                    .WithIndentedSequences()  // 使序列化结果更易读
                    .Build();


                var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                    .Build();

                string yamlContent = serializer.Serialize(config);

                // 确保配置目录存在
                string configDirPath = Path.GetDirectoryName(configFilePath);
                if (!Directory.Exists(configDirPath))
                    Directory.CreateDirectory(configDirPath);

                string moreYamlPath = Path.Combine(configDirPath, "more.yaml");
                if (File.Exists(moreYamlPath))
                {
                    // 读取more.yaml的内容
                    string moreYamlContent = File.ReadAllText(moreYamlPath);

                    // 解析主配置和补充配置
                    var Config = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
                    var moreConfig = deserializer.Deserialize<Dictionary<string, object>>(moreYamlContent);

                    if (Config == null) Config = new Dictionary<string, object>();
                    if (moreConfig == null) moreConfig = new Dictionary<string, object>();

                    // 检查并记录冲突的键
                    var conflictKeys = new List<string>();
                    var mergedKeys = new List<string>();

                    foreach (var kvp in moreConfig)
                    {
                        if (Config.ContainsKey(kvp.Key))
                        {
                            conflictKeys.Add(kvp.Key);
                            Log($"发现重复键 '{kvp.Key}'，使用GUI配置", GetRichTextBoxAllLog());
                        }
                        else
                        {
                            Config[kvp.Key] = kvp.Value;
                            mergedKeys.Add(kvp.Key);
                        }
                    }

                    // 重新序列化合并后的配置
                    yamlContent = serializer.Serialize(Config);


                    Log($"已将补充参数配置 more.yaml 内容追加到配置文件", GetRichTextBoxAllLog());
                }
                // 写入YAML文件
                File.WriteAllText(configFilePath, yamlContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置文件时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdvanceSettings_Click(object sender, EventArgs e)
        {
            if (!_originalLocationSaved)
            {
                _originalLocationSaved = true;
                _pipeOriginalLocation = groupBoxPipeConcurrent.Location;
                _enhanceOriginalLocation = groupBoxEnhance.Location;
            }
            //展开状态
            if (buttonAdvanceSettings.Text == "高级设置∧")
            {
                buttonAdvanceSettings.Text = "高级设置∨";
                groupBoxAdvanceSettings.Visible = false;
                if (!checkBoxHighConcurrent.Checked)
                {
                    groupBoxPipeConcurrent.Visible = false;
                    groupBoxEnhance.Visible = false;

                    groupBoxGist.Location = _pipeOriginalLocation;
                    groupBoxR2.Location = _pipeOriginalLocation;
                    groupBoxWebdav.Location = _pipeOriginalLocation;
                }
                else
                {
                    groupBoxPipeConcurrent.Visible = true;
                    groupBoxEnhance.Visible = true;

                    groupBoxPipeConcurrent.Location = groupBoxAdvanceSettings.Location;
                    groupBoxEnhance.Location = new Point(groupBoxEnhance.Location.X, groupBoxAdvanceSettings.Location.Y);
                }
            }
            else
            {
                // 收缩状态
                buttonAdvanceSettings.Text = "高级设置∧";
                groupBoxAdvanceSettings.Visible = true;
                if (!checkBoxHighConcurrent.Checked)
                {
                    groupBoxPipeConcurrent.Visible = false;
                    groupBoxEnhance.Visible = false;
                }
                else
                {
                    groupBoxPipeConcurrent.Visible = true;
                    groupBoxEnhance.Visible = true;
                    groupBoxPipeConcurrent.Location = _pipeOriginalLocation;
                    groupBoxEnhance.Location = _enhanceOriginalLocation;
                }
            }
            判断保存类型();
        }

        private async void buttonStartCheck_Click(object sender, EventArgs e)
        {
            buttonStartCheck.Enabled = false;
            if (buttonStartCheck.Text == "▶️ 启动")
            {
                toolTip1.SetToolTip(buttonStartCheck, "启动检测流程!");
                buttonStartCheck.ForeColor = Color.Black;
                if (checkBoxEnableWebUI.Checked && textBoxWebUiAPIKey.Text == "请输入密钥")
                {
                    MessageBox.Show("您已启用WebUI，请设置WebUI API密钥！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                run = 1;
                if (buttonCopySubscriptionUrl.Enabled == false)
                {
                    string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
                    string allyamlFilePath = Path.Combine(executablePath, "output", "all.yaml");
                    buttonCopySubscriptionUrl.Enabled = File.Exists(allyamlFilePath);
                }

                numericUpDownConcurrent.Enabled = false;
                numericUpDownInterval.Enabled = false;
                labelCron.Enabled = false;
                textBoxCron.Enabled = false;
                numericUpDownTimeout.Enabled = false;
                numericUpDownMinSpeed.Enabled = false;
                numericUpDownDLTimehot.Enabled = false;
                numericUpDownDownloadMb.Enabled = false;
                numericUpDownTotalBandwidthLimit.Enabled = false;
                numericUpDownWebUIPort.Enabled = false;
                numericUpDownSubStorePort.Enabled = false;


                // 运行时禁用流水线并发和增强标签相关设置项
                groupBoxPipeConcurrent.Enabled = false;
                groupBoxEnhance.Enabled = false;
                checkBoxHighConcurrent.Enabled = false;
                checkBoxSwitchArch64.Enabled = false;

                comboBoxSaveMethod.Enabled = false;
                textBoxSubsUrls.Enabled = false;
                groupBoxAdvanceSettings.Enabled = false;
                groupBoxGist.Enabled = false;
                groupBoxR2.Enabled = false;
                groupBoxWebdav.Enabled = false;
                if (checkBoxEnableWebUI.Checked) buttonWebUi.Enabled = true;
                buttonStartCheck.Text = "⌛检测代理";
                toolTip1.SetToolTip(buttonStartCheck, "正在检测可用github代理");

                //timer3.Enabled = true;
                // 清空 richTextBox1
                richTextBoxAllLog.Clear();
                await KillNodeProcessAsync();
                await SaveConfig();

                if (run == 1)
                {
                    // 更新菜单项的启用状态
                    startMenuItem.Enabled = false;
                    stopMenuItem.Enabled = true;

                    // 清空 richTextBox1
                    //richTextBox1.Clear();

                    notifyIcon1.Text = "SubsCheck: 已就绪";

                    // 启动 subs-check.exe 程序
                    buttonStartCheck.ForeColor = Color.Red;
                    buttonStartCheck.Text = "⏹️ 停止";
                    toolTip1.SetToolTip(buttonStartCheck, "停止内核检测进程!");

                    await AutoCheckSysProxy();
                    StartSubsCheckProcess();
                }
            }
            else
            {
                run = 0;
                Log("任务停止", GetRichTextBoxAllLog());
                progressBarAll.Value = 0;
                progressBarAll.Visible = false;
                labelLogNodeInfo.Text = "实时日志";
                notifyIcon1.Text = "SubsCheck: 未运行";
                // 停止 subs-check.exe 程序
                StopSubsCheckProcess();
                // 结束 Sub-Store
                await KillNodeProcessAsync();
                if (checkBoxEnableWebUI.Checked) ReadConfig();
                buttonCopySubscriptionUrl.Enabled = false;
                numericUpDownConcurrent.Enabled = true;
                numericUpDownInterval.Enabled = true;
                labelCron.Enabled = true;
                textBoxCron.Enabled = true;
                numericUpDownTimeout.Enabled = true;
                numericUpDownMinSpeed.Enabled = true;
                numericUpDownDLTimehot.Enabled = true;
                numericUpDownDownloadMb.Enabled = true;
                numericUpDownTotalBandwidthLimit.Enabled = true;
                numericUpDownWebUIPort.Enabled = true;
                numericUpDownSubStorePort.Enabled = true;

                // 重新启用
                groupBoxPipeConcurrent.Enabled = true;
                groupBoxEnhance.Enabled = true;
                checkBoxHighConcurrent.Enabled = true;
                checkBoxSwitchArch64.Enabled = true;

                comboBoxSaveMethod.Enabled = true;
                textBoxSubsUrls.Enabled = true;
                groupBoxAdvanceSettings.Enabled = true;
                groupBoxGist.Enabled = true;
                groupBoxR2.Enabled = true;
                groupBoxWebdav.Enabled = true;
                buttonWebUi.Enabled = false;
                buttonStartCheck.Text = "▶️ 启动";
                buttonStartCheck.ForeColor = Color.Black;
                toolTip1.SetToolTip(buttonStartCheck, "启动检测流程!");
                //timer3.Enabled = false;
                // 更新菜单项的启用状态
                startMenuItem.Enabled = true;
                stopMenuItem.Enabled = false;
            }
            if (downloading == 0) buttonStartCheck.Enabled = true;
        }

        public async Task DownloadSubsCheckEXE()
        {
            buttonStartCheck.Enabled = false;
            downloading = 1;
            try
            {
                Log("正在检查网络连接...", GetRichTextBoxAllLog());

                // 动态决定使用哪个仓库（checkBoxHighConcurrent 为 true 时使用 sinspired，否则使用 beck-8）
                string repoOwner = checkBoxHighConcurrent.Checked ? "sinspired" : "beck-8";
                string apiUrl = $"https://api.github.com/repos/{repoOwner}/subs-check/releases/latest";
                string releasesPageUrl = $"https://github.com/{repoOwner}/subs-check/releases";
                // 决定目标资源名称：64位优先 (amd64)，否则 i386
                string desiredArchToken = checkBoxSwitchArch64.Checked ? "x86_64" : "i386";
                string desiredKernel = checkBoxHighConcurrent.Checked ? "高并发内核" : "原版内核";
                string desiredAssetName = $"subs-check_Windows_{desiredArchToken}.zip";

                // 首先检查是否有网络连接
                if (!IsNetworkAvailable())
                {
                    Log("网络连接不可用，无法下载核心文件。", GetRichTextBoxAllLog(), true);
                    MessageBox.Show($"缺少 subs-check.exe 核心文件。\n\n您可以前往 {releasesPageUrl} 自行下载！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                await AutoCheckSysProxy();
                var result = await 获取版本号(apiUrl, true);
                if (result.Item1 == "未知版本")
                {
                    // 无版本信息
                    return;
                }

                // 创建不使用系统代理的 HttpClientHandler
                HttpClientHandler handler = new HttpClientHandler
                {
                    UseProxy = false,
                    Proxy = null
                };

                // 使用自定义 handler 创建 HttpClient
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        string latestVersion = result.Item1;
                        JArray assets = result.Item2;
                        Log($"subs-check.exe 最新版本为: {latestVersion} ", GetRichTextBoxAllLog());

                        // 先尝试精确匹配期望文件名；找不到则回退为任意包含 "Windows" 且包含 arch token 的条目；
                        // 若仍找不到，再回退为任意包含 "Windows" 的资源。
                        string downloadUrl = null;
                        foreach (var asset in assets)
                        {
                            if (asset["name"]?.ToString().Equals(desiredAssetName, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                downloadUrl = asset["browser_download_url"].ToString();
                                break;
                            }
                        }

                        if (downloadUrl == null)
                        {
                            foreach (var asset in assets)
                            {
                                string name = asset["name"]?.ToString() ?? "";
                                if (name.IndexOf("Windows", StringComparison.OrdinalIgnoreCase) >= 0 &&
                                    name.IndexOf(desiredArchToken, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    downloadUrl = asset["browser_download_url"].ToString();
                                    break;
                                }
                            }
                        }

                        if (downloadUrl == null)
                        {
                            // 最后退化：任何 Windows 包
                            foreach (var asset in assets)
                            {
                                string name = asset["name"]?.ToString() ?? "";
                                if (name.IndexOf("Windows", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    downloadUrl = asset["browser_download_url"].ToString();
                                    break;
                                }
                            }
                        }

                        if (downloadUrl == null)
                        {
                            Log("无法找到适用于 Windows 的下载链接。", GetRichTextBoxAllLog(), true);
                            MessageBox.Show($"未能找到适用的 subs-check.exe 下载链接。\n\n可尝试更换 Github Proxy 后，点击「检查更新」>「更新内核」。\n或前往 {releasesPageUrl} 自行下载！",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
                        string zipFilePath = Path.Combine(executablePath, desiredAssetName);

                        // 如果文件已存在，先删除
                        if (File.Exists(zipFilePath)) File.Delete(zipFilePath);

                        Log("开始下载内核文件...", GetRichTextBoxAllLog());


                        // 优先尝试 GitHub 代理，再尝试系统代理,最后使用无代理直连
                        var (downloadSuccess, failureReason) = await TryDownloadWithStrategiesAsync(
                            downloadUrl,
                            zipFilePath,
                            new[] { DownloadStrategy.SystemProxy, DownloadStrategy.GithubProxy, DownloadStrategy.Direct });


                        // 如果所有尝试失败
                        if (!downloadSuccess)
                        {
                            Log($"所有下载尝试均失败，最后错误: {failureReason}", GetRichTextBoxAllLog(), true);
                            MessageBox.Show(
                                $"下载 subs-check.exe 失败，请检查网络连接后重试。\n\n可尝试更换 Github Proxy 后，点击「检查更新」>「更新内核」。\n或前往 {releasesPageUrl} 自行下载！",
                                "下载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            progressBarAll.Value = 0;
                            progressBarAll.Visible = false;
                            return;
                        }


                        // 下载成功 -> 解压并查找 subs-check.exe
                        Log("下载完成，正在解压文件...", GetRichTextBoxAllLog());
                        // 解压文件
                        using (System.IO.Compression.ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zipFilePath))
                        {
                            // 查找subs-check.exe
                            var exeEntry = archive.Entries.FirstOrDefault(
                                entry => entry.Name.Equals("subs-check.exe", StringComparison.OrdinalIgnoreCase));

                            if (exeEntry != null)
                            {
                                string exeFilePath = Path.Combine(executablePath, "subs-check.exe");
                                // 如果文件已存在，先删除
                                if (File.Exists(exeFilePath)) File.Delete(exeFilePath);

                                // 解压文件
                                exeEntry.ExtractToFile(exeFilePath);
                                currentKernel = desiredKernel;
                                currentArch = desiredArchToken;

                                当前subsCheck版本号 = $"{latestVersion}";

                                Log($"{currentKernel}({currentArch}): subs-check.exe {当前subsCheck版本号} 已就绪！", GetRichTextBoxAllLog());

                                await SaveConfig(false);

                                // 可选：删除 zip 文件（注释状态保留原样）
                                //File.Delete(zipFilePath);
                            }
                            else
                            {
                                Log("无法在压缩包中找到 subs-check.exe 文件。", GetRichTextBoxAllLog(), true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"下载过程中出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                        MessageBox.Show($"下载 subs-check.exe 时出错: {ex.Message}\n\n可尝试更换 Github Proxy 后，点击「检查更新」>「更新内核」。\n或前往 {releasesPageUrl} 自行下载！",
                            "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"初始化下载过程出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                MessageBox.Show($"下载准备过程出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonStartCheck.Enabled = true;
                downloading = 0;
            }
        }


        /// <summary>
        /// 获取最新版本号和对应的下载链接
        /// </summary>
        /// <param name="版本号URL">API请求URL</param>
        /// <param name="是否输出log">是否在日志中输出信息</param>
        /// <returns>包含最新版本号和下载链接的元组</returns>
        private async Task<(string LatestVersion, JArray assets)> 获取版本号(string 版本号URL, bool 是否输出log = false)
        {
            string latestVersion = "未知版本";
            JArray assets = null;

            // 创建不使用系统代理的 HttpClientHandler
            HttpClientHandler handler = new HttpClientHandler
            {
                UseProxy = false,
                Proxy = null
            };

            // 使用自定义 handler 创建 HttpClient
            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");
                client.Timeout = TimeSpan.FromSeconds(30); // 增加超时时间以适应下载需求

                if (是否输出log) Log("正在获取最新版本 subs-check.exe 内核下载地址...", GetRichTextBoxAllLog());
                string url = 版本号URL;
                string 备用url = 版本号URL.Replace("api.github.com", "api.github.cmliussss.net");

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
                        if (是否输出log) Log("成功从主API获取版本信息", GetRichTextBoxAllLog());
                    }
                    // 如果主URL请求不成功但没有抛出异常
                    else
                    {
                        if (是否输出log) Log($"主API请求失败 HTTP {(int)response.StatusCode}，尝试备用API...", GetRichTextBoxAllLog());
                        response = await client.GetAsync(备用url);

                        if (response.IsSuccessStatusCode)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            json = JObject.Parse(responseBody);
                            if (是否输出log) Log("成功从备用API获取版本信息", GetRichTextBoxAllLog());
                        }
                        else
                        {
                            if (是否输出log) Log($"备用API也请求失败: HTTP {(int)response.StatusCode}", GetRichTextBoxAllLog(), true);
                            return (latestVersion, assets); // 两个URL都失败，提前退出
                        }
                    }
                }
                // 捕获网络请求异常（如连接超时、无法解析域名等）
                catch (HttpRequestException ex)
                {
                    if (是否输出log) Log($"主API请求出错: {ex.Message}，尝试备用API...", GetRichTextBoxAllLog());
                    try
                    {
                        response = await client.GetAsync(备用url);
                        if (response.IsSuccessStatusCode)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            json = JObject.Parse(responseBody);
                            if (是否输出log) Log("成功从备用API获取版本信息", GetRichTextBoxAllLog());
                        }
                        else
                        {
                            if (是否输出log) Log($"备用API也请求失败: HTTP {(int)response.StatusCode}", GetRichTextBoxAllLog(), true);
                            return (latestVersion, assets); // 备用URL也失败，提前退出
                        }
                    }
                    catch (Exception backupEx)
                    {
                        if (是否输出log) Log($"备用API请求也出错: {backupEx.Message}", GetRichTextBoxAllLog(), true);
                        return (latestVersion, assets); // 连备用URL也异常，提前退出
                    }
                }
                // 捕获JSON解析异常
                catch (Newtonsoft.Json.JsonException ex)
                {
                    if (是否输出log) Log($"解析JSON数据出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                    try
                    {
                        response = await client.GetAsync(备用url);
                        if (response.IsSuccessStatusCode)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            json = JObject.Parse(responseBody);
                            if (是否输出log) Log("成功从备用API获取版本信息", GetRichTextBoxAllLog());
                        }
                    }
                    catch (Exception backupEx)
                    {
                        if (是否输出log) Log($"备用API请求也出错: {backupEx.Message}", GetRichTextBoxAllLog(), true);
                        return (latestVersion, assets); // 连备用URL也有问题，提前退出
                    }
                }
                // 捕获其他所有异常
                catch (Exception ex)
                {
                    if (是否输出log) Log($"获取版本信息时出现未预期的错误: {ex.Message}", GetRichTextBoxAllLog(), true);
                    try
                    {
                        response = await client.GetAsync(备用url);
                        if (response.IsSuccessStatusCode)
                        {
                            responseBody = await response.Content.ReadAsStringAsync();
                            json = JObject.Parse(responseBody);
                            if (是否输出log) Log("成功从备用URL获取版本信息", GetRichTextBoxAllLog());
                        }
                    }
                    catch (Exception backupEx)
                    {
                        if (是否输出log) Log($"备用API请求也出错: {backupEx.Message}", GetRichTextBoxAllLog(), true);
                        return (latestVersion, assets); // 连备用URL也有问题，提前退出
                    }
                }

                // 如果成功获取了JSON数据，继续处理
                if (json != null)
                {
                    latestVersion = json["tag_name"].ToString();
                    assets = (JArray)json["assets"];
                }
            }

            return (latestVersion, assets);
        }


        private async void StartSubsCheckProcess()
        {
            try
            {
                // 重置进度条
                progressBarAll.Value = 0;
                progressBarAll.Visible = true;
                progressBarAll.Enabled = true;
                labelLogNodeInfo.Text = "实时日志";
                using (MemoryStream ms = new MemoryStream(Properties.Resources.going))
                {
                    notifyIcon1.Icon = new Icon(ms);
                }

                // 获取当前应用程序目录
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string subsCheckPath = Path.Combine(executablePath, "subs-check.exe");
                // 设置一个环境变量以表明是有GUI启动的

                Environment.SetEnvironmentVariable("START_FROM_GUI", "true");

                // 检查是否有其他subs-check.exe进程正在运行，并强制结束它们
                try
                {
                    Process[] processes = Process.GetProcessesByName("subs-check");
                    if (processes.Length > 0)
                    {
                        Log("发现正在运行的subs-check.exe进程，正在强制结束...", GetRichTextBoxAllLog());
                        foreach (Process process in processes)
                        {
                            // 确保不是当前应用程序的进程
                            if (process != subsCheckProcess)
                            {
                                try
                                {
                                    process.Kill();
                                    process.WaitForExit();
                                    Log($"成功结束subs-check.exe进程(ID: {process.Id})", GetRichTextBoxAllLog());
                                }
                                catch (Exception ex)
                                {
                                    Log($"结束subs-check.exe进程时出错(ID: {process.Id}): {ex.Message}", GetRichTextBoxAllLog(), true);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"检查运行中的subs-check.exe进程时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                }

                // 检查文件是否存在
                if (!File.Exists(subsCheckPath))
                {
                    Log("没有找到 subs-check.exe 文件。", GetRichTextBoxAllLog(), true);
                    await DownloadSubsCheckEXE(); // 使用异步等待
                }

                // 创建进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = subsCheckPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = executablePath,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // 创建进程
                subsCheckProcess = new Process { StartInfo = startInfo };

                // 设置输出和错误数据接收事件处理
                subsCheckProcess.OutputDataReceived += SubsCheckProcess_OutputDataReceived;
                subsCheckProcess.ErrorDataReceived += SubsCheckProcess_OutputDataReceived;

                // 启动进程
                subsCheckProcess.Start();

                // 开始异步读取输出
                subsCheckProcess.BeginOutputReadLine();
                subsCheckProcess.BeginErrorReadLine();

                // 设置进程退出事件处理
                subsCheckProcess.EnableRaisingEvents = true;
                subsCheckProcess.Exited += SubsCheckProcess_Exited;

                Log($"subs-check.exe {当前subsCheck版本号} 已启动...", GetRichTextBoxAllLog());
                timerRefresh.Enabled = true;
            }
            catch (Exception ex)
            {
                Log($"启动 subs-check.exe 时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                buttonStartCheck.Text = "▶️ 启动";
                buttonStartCheck.ForeColor = Color.Black;
            }
        }


        private void StopSubsCheckProcess()
        {
            timerRefresh.Enabled = false;
            if (subsCheckProcess != null && !subsCheckProcess.HasExited)
            {
                try
                {
                    // 尝试正常关闭进程
                    subsCheckProcess.Kill();
                    subsCheckProcess.WaitForExit();
                    Log("subs-check.exe 已停止", GetRichTextBoxAllLog());
                    notifyIcon1.Icon = originalNotifyIcon;
                    buttonTriggerCheck.Enabled = false;
                    buttonTriggerCheck.Text = "🔀未启动";
                }
                catch (Exception ex)
                {
                    Log($"停止 subs-check.exe 时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                }
                finally
                {
                    subsCheckProcess.Dispose();
                    subsCheckProcess = null;
                }
            }
        }

        private void SubsCheckProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;

            var state = this.Tag as Tuple<System.Collections.Concurrent.ConcurrentQueue<string>, System.Windows.Forms.Timer>;
            if (state == null)
            {
                // 首次初始化在 UI 线程完成
                BeginInvoke(new Action(() =>
                {
                    var st2 = this.Tag as Tuple<System.Collections.Concurrent.ConcurrentQueue<string>, System.Windows.Forms.Timer>;
                    if (st2 != null)
                    {
                        st2.Item1.Enqueue(e.Data);
                        // 启动定时器（如果尚未运行）
                        if (!st2.Item2.Enabled) st2.Item2.Start();
                        return;
                    }

                    var q = new System.Collections.Concurrent.ConcurrentQueue<string>();
                    var t = new System.Windows.Forms.Timer { Interval = 200 }; // 可调：80-200ms
                    t.Tick += (s, ev) =>
                    {
                        // 若无新日志，立即停表避免空转重绘
                        if (q.IsEmpty)
                        {
                            try { if (t.Enabled) t.Stop(); } catch { }
                            return;
                        }

                        var sb = new System.Text.StringBuilder();

                        while (q.TryDequeue(out var rawLine))
                        {
                            string clean = RemoveAnsiEscapeCodes(rawLine);

                            // 过滤掉空白行与 [GIN] 行，避免无意义刷新
                            if (string.IsNullOrWhiteSpace(clean)) continue;
                            if (clean.StartsWith("[GIN]")) continue;

                            // 一次性“下次检查时间”
                            if (clean.Contains("下次检查时间:"))
                            {
                                if (!buttonCopySubscriptionUrl.Enabled)
                                {
                                    string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                    string outputFolderPath = System.IO.Path.Combine(executablePath, "output");
                                    if (System.IO.Directory.Exists(outputFolderPath))
                                    {
                                        string allyamlFilePath = System.IO.Path.Combine(outputFolderPath, "all.yaml");
                                        if (System.IO.File.Exists(allyamlFilePath)) buttonCopySubscriptionUrl.Enabled = true;
                                    }
                                }
                                int startIndex = clean.IndexOf("下次检查时间:");
                                var lineOnly = clean.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)[0];
                                nextCheckTime = lineOnly.Substring(startIndex);
                            }

                            sb.AppendLine(clean);
                        }

                        // 若本轮没有有效输出，停止计时器并退出
                        if (sb.Length == 0)
                        {
                            try { if (q.IsEmpty && t.Enabled) t.Stop(); } catch { }
                            return;
                        }

                        // 批量追加时暂时禁用重绘，减少闪烁
                        SuspendRedraw(richTextBoxAllLog);
                        try
                        {
                            richTextBoxAllLog.AppendText(sb.ToString());
                            richTextBoxAllLog.SelectionStart = richTextBoxAllLog.TextLength;
                            richTextBoxAllLog.ScrollToCaret();
                        }
                        finally
                        {
                            ResumeRedraw(richTextBoxAllLog);
                        }

                        // 若已消费完，停止定时器
                        if (q.IsEmpty)
                        {
                            try { if (t.Enabled) t.Stop(); } catch { }
                        }
                    };

                    t.Start();
                    this.Tag = Tuple.Create(q, t);

                    // 入队并确保定时器在 UI 线程已启动
                    q.Enqueue(e.Data);
                }));

                return;
            }

            // 已初始化：直接入队（非 UI 线程安全）
            state.Item1.Enqueue(e.Data);

            // 确保定时器正在运行（使用 BeginInvoke 在 UI 线程安全地检查/启动）
            BeginInvoke(new Action(() =>
            {
                Tuple<System.Collections.Concurrent.ConcurrentQueue<string>, System.Windows.Forms.Timer> st = this.Tag as Tuple<System.Collections.Concurrent.ConcurrentQueue<string>, System.Windows.Forms.Timer>;
                if (st != null && !st.Item2.Enabled)
                {
                    st.Item2.Start();
                }
            }));
        }


        /*
        // 检查是否是进度信息行
        if (cleanText.StartsWith("进度: ["))
        {
            // 解析百分比
            int percentIndex = cleanText.IndexOf('%');
            if (percentIndex > 0)
            {
                // 查找百分比前面的数字部分
                int startIndex = cleanText.LastIndexOfAny(new char[] { ' ', '>' }, percentIndex) + 1;
                string percentText = cleanText.Substring(startIndex, percentIndex - startIndex);

                if (double.TryParse(percentText, out double percentValue))
                {
                    // 更新进度条，将百分比值（0-100）设置给进度条
                    progressBar1.Value = (int)Math.Round(percentValue);
                }
            }

            // 解析节点信息部分（例如：(12/6167) 可用: 0）
            int infoStartIndex = cleanText.IndexOf('(');
            if (infoStartIndex > 0)
            {
                string fullNodeInfo = cleanText.Substring(infoStartIndex);

                // 提取最重要的信息：节点数量和可用数量
                int endIndex = fullNodeInfo.IndexOf("2025-"); // 查找日期部分开始位置
                if (endIndex > 0)
                {
                    nodeInfo = fullNodeInfo.Substring(0, endIndex).Trim();
                }
                else
                {
                    // 如果找不到日期部分，则取前30个字符
                    nodeInfo = fullNodeInfo.Length > 30 ? fullNodeInfo.Substring(0, 30) + "..." : fullNodeInfo;
                }

                groupBox2.Text = "实时日志 " + nodeInfo;

                // 确保通知图标文本不超过63个字符
                string notifyText = "SubsCheck: " + nodeInfo;
                if (notifyText.Length > 63)
                {
                    notifyText = notifyText.Substring(0, 60) + "...";
                }
                notifyIcon1.Text = notifyText;
            }

            // 更新lastProgressLine，但不向richTextBox添加文本
            lastProgressLine = cleanText;
        }
        else
        {
            // 如果不是进度行，则添加到日志中
            richTextBox1.AppendText(cleanText + "\r\n");
            // 滚动到最底部
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        */


        // 添加一个方法来过滤ANSI转义序列
        private string RemoveAnsiEscapeCodes(string input)
        {
            // 匹配ANSI转义序列的正则表达式
            // 这将匹配类似 "[2m"、"[0m"、"[92m" 等格式的ANSI颜色代码
            return System.Text.RegularExpressions.Regex.Replace(input, @"\x1B\[[0-9;]*[mGK]", string.Empty);
        }

        private void SubsCheckProcess_Exited(object sender, EventArgs e)
        {
            // 进程退出时，在 UI 线程上更新控件
            BeginInvoke(new Action(() =>
            {
                Log("subs-check.exe 已退出", GetRichTextBoxAllLog());
                buttonStartCheck.Text = "▶️ 启动";
                buttonStartCheck.ForeColor = Color.Black;

                // 更新菜单项的启用状态
                startMenuItem.Enabled = true;
                stopMenuItem.Enabled = false;

                // 重新启用控件
                numericUpDownConcurrent.Enabled = true;
                numericUpDownInterval.Enabled = true;
                numericUpDownTimeout.Enabled = true;
                numericUpDownMinSpeed.Enabled = true;
                numericUpDownDLTimehot.Enabled = true;
                numericUpDownDownloadMb.Enabled = true;
                numericUpDownTotalBandwidthLimit.Enabled = true;
                numericUpDownWebUIPort.Enabled = true;
                textBoxSubsUrls.Enabled = true;
                groupBoxAdvanceSettings.Enabled = true;
                // 重新启用
                groupBoxPipeConcurrent.Enabled = true;
                groupBoxEnhance.Enabled = true;
                checkBoxHighConcurrent.Enabled = true;
                checkBoxSwitchArch64.Enabled = true;
            }));
        }

        /// <summary>
        /// 获取本地局域网IP地址，如果有多个则让用户选择
        /// </summary>
        /// <returns>用户选择的IP地址，如果未选择则返回127.0.0.1</returns>
        private string GetLocalLANIP()
        {
            try
            {
                // 获取所有网卡的IP地址
                List<string> lanIPs = new List<string>();

                // 获取所有网络接口
                foreach (System.Net.NetworkInformation.NetworkInterface ni in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    // 排除loopback、虚拟网卡和非活动网卡
                    if (ni.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback &&
                        ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        // 获取该网卡的所有IP地址
                        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            // 只添加IPv4地址且不是回环地址
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                                !System.Net.IPAddress.IsLoopback(ip.Address))
                            {
                                lanIPs.Add(ip.Address.ToString());
                            }
                        }
                    }
                }

                // 如果没有找到任何IP地址，返回本地回环地址
                if (lanIPs.Count == 0)
                {
                    return "127.0.0.1";
                }
                // 如果只找到一个IP地址，直接返回
                else if (lanIPs.Count == 1)
                {
                    return lanIPs[0];
                }
                // 如果有多个IP地址，让用户选择
                else
                {
                    // 创建选择窗口
                    Form selectForm = new Form();
                    selectForm.Text = "选择局域网IP地址";
                    selectForm.StartPosition = FormStartPosition.CenterParent;
                    /*
                    selectForm.Width = 520;  // 保持宽度
                    selectForm.Height = 320; // 增加高度以容纳额外的警告标签
                    selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    */
                    selectForm.AutoSize = true;  // 启用自动大小调整
                    selectForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;  // 根据内容调整大小
                    selectForm.FormBorderStyle = FormBorderStyle.FixedSingle;  // 使用固定但可调整的边框
                    selectForm.ShowIcon = false;
                    selectForm.MaximizeBox = false;
                    selectForm.MinimizeBox = false;

                    // 添加说明标签
                    Label label = new Label();
                    label.Text = "发现多个局域网IP地址：\n\n" +
                                 "· 仅在本机访问：直接点击【取消】，将使用127.0.0.1\n\n" +
                                 "· 局域网内其他设备访问：请在下面列表中选择一个正确的局域网IP";
                    label.Location = new Point(15, 10);
                    label.AutoSize = true;
                    label.MaximumSize = new Size(380, 0); // 设置最大宽度，允许自动换行
                    selectForm.Controls.Add(label);

                    // 计算标签高度以正确放置列表框
                    int labelHeight = label.Height + 20;

                    // 添加IP地址列表框
                    ListBox listBox = new ListBox();
                    listBox.Location = new Point(15, labelHeight);
                    listBox.Width = 380;
                    listBox.Height = 130; // 保持列表框高度
                    foreach (string ip in lanIPs)
                    {
                        listBox.Items.Add(ip);
                    }
                    // 查找非".1"结尾的IP地址，如果所有IP都以".1"结尾，则使用第一个IP
                    int selectedIndex = 0;
                    for (int i = 0; i < lanIPs.Count; i++)
                    {
                        if (!lanIPs[i].EndsWith(".1"))
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    // 设置选中的索引
                    listBox.SelectedIndex = selectedIndex;
                    selectForm.Controls.Add(listBox);

                    // 添加警告标签（放在列表框下方）
                    Label warningLabel = new Label();
                    warningLabel.Text = "注意：选择错误的IP会导致局域网内其他设备无法访问。\n\n　　　推荐您可以先尝试使用非“.1”结尾的IP！";
                    warningLabel.Location = new Point(15, labelHeight + listBox.Height + 10);
                    warningLabel.AutoSize = true;
                    warningLabel.ForeColor = Color.Red; // 警告文本使用红色
                    selectForm.Controls.Add(warningLabel);

                    // 计算按钮位置（居中排布）
                    int buttonY = labelHeight + listBox.Height + warningLabel.Height + 20;
                    int buttonTotalWidth = 75 * 2 + 15; // 两个按钮的宽度加间距
                    int buttonStartX = (selectForm.ClientSize.Width - buttonTotalWidth) / 2;

                    // 添加确定按钮
                    Button okButton = new Button();
                    okButton.Text = "确定";
                    okButton.DialogResult = DialogResult.OK;
                    okButton.Location = new Point(buttonStartX, buttonY);
                    okButton.Width = 75;
                    selectForm.Controls.Add(okButton);
                    selectForm.AcceptButton = okButton;

                    // 添加取消按钮
                    Button cancelButton = new Button();
                    cancelButton.Text = "取消";
                    cancelButton.DialogResult = DialogResult.Cancel;
                    cancelButton.Location = new Point(buttonStartX + 90, buttonY);
                    cancelButton.Width = 75;
                    selectForm.Controls.Add(cancelButton);
                    selectForm.CancelButton = cancelButton;

                    // 显示选择窗口
                    if (selectForm.ShowDialog() == DialogResult.OK)
                    {
                        return listBox.SelectedItem.ToString();
                    }
                    else
                    {
                        return "127.0.0.1"; // 如果用户取消，返回本地回环地址
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取局域网IP地址时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "127.0.0.1";
            }
        }

        private void buttonCopySubscriptionUrl_Click(object sender, EventArgs e)
        {
            string 本地IP = GetLocalLANIP();
            try
            {
                string subPath = textBoxSubStorePath.Text.Trim();
                subPath = !string.IsNullOrEmpty(subPath) && subPath != "请输入路径"
                          ? (subPath.StartsWith("/") ? subPath : "/" + subPath)
                          : string.Empty;

                string baseSubStoreUrl = $"http://{本地IP}:{numericUpDownSubStorePort.Value}{subPath}";
                string url;

                if (comboBoxSubscriptionType.Text == "Clash")
                {
                    url = $"{baseSubStoreUrl}/api/file/mihomo";
                }
                else if (comboBoxSubscriptionType.Text == "Singbox1.11" && checkBoxHighConcurrent.Checked)
                {
                    url = $"{baseSubStoreUrl}/api/file/singbox-1.11";
                }
                else if (comboBoxSubscriptionType.Text == "Singbox1.12" && checkBoxHighConcurrent.Checked)
                {
                    url = $"{baseSubStoreUrl}/api/file/singbox-1.12";
                }
                else
                {
                    url = $"{baseSubStoreUrl}/download/sub";
                }

                // 将URL复制到剪贴板
                Clipboard.SetText(url);
                buttonCopySubscriptionUrl.Text = "复制成功";
                timerCopySubscriptionUrl.Enabled = true;
                // 可选：显示提示消息
                //MessageBox.Show($"URL已复制到剪贴板：\n{url}", "复制成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"复制到剪贴板时出错：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerCopySubscriptionUrl_Tick(object sender, EventArgs e)
        {
            buttonCopySubscriptionUrl.Text = "复制订阅";
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(comboBoxSpeedtestUrl.Text))
            {
                comboBoxSpeedtestUrl.Text = "不测速";
                return;
            }

            string input = comboBoxSpeedtestUrl.Text.Trim();

            // 更新 comboBox2 的文本和选项
            comboBoxSpeedtestUrl.Items.Add(input);
            comboBoxSpeedtestUrl.Text = input;
        }

        private void comboBoxSysProxy_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(comboBoxSysProxy.Text))
            {
                comboBoxSysProxy.Text = "自动检测";
                return;
            }

            string input = comboBoxSysProxy.Text.Trim();

            // 检查是否存在 "://" 协议部分
            int protocolIndex = input.IndexOf("://");
            if (protocolIndex >= 0)
            {
                // 保留 "://" 之后的内容
                input = input.Substring(protocolIndex + 3);
            }

            // 检查是否存在 "/" 路径部分
            int pathIndex = input.IndexOf('/');
            if (pathIndex >= 0)
            {
                // 只保留 "/" 之前的域名部分
                input = input.Substring(0, pathIndex);
            }

            // 更新 comboBox3 的文本
            comboBoxSysProxy.Text = input;
        }

        private void comboBoxGithubProxyUrl_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(comboBoxGithubProxyUrl.Text))
            {
                comboBoxGithubProxyUrl.Text = "自动选择";
                return;
            }

            string input = comboBoxGithubProxyUrl.Text.Trim();

            // 检查是否存在 "://" 协议部分
            int protocolIndex = input.IndexOf("://");
            if (protocolIndex >= 0)
            {
                // 保留 "://" 之后的内容
                input = input.Substring(protocolIndex + 3);
            }

            // 检查是否存在 "/" 路径部分
            int pathIndex = input.IndexOf('/');
            if (pathIndex >= 0)
            {
                // 只保留 "/" 之前的域名部分
                input = input.Substring(0, pathIndex);
            }

            // 更新 comboBox3 的文本
            comboBoxGithubProxyUrl.Text = input;
        }

        private void 判断保存类型()
        {
            if (comboBoxSaveMethod.Text == "本地" || buttonAdvanceSettings.Text == "高级设置∨")
            {
                groupBoxGist.Visible = false;
                groupBoxR2.Visible = false;
                groupBoxWebdav.Visible = false;
            }
            else if (comboBoxSaveMethod.Text == "gist" && buttonAdvanceSettings.Text == "高级设置∧")
            {
                if (!checkBoxHighConcurrent.Checked)
                {
                    groupBoxGist.Location = _pipeOriginalLocation;
                }

                groupBoxGist.Visible = true;

                groupBoxR2.Visible = false;
                groupBoxWebdav.Visible = false;
            }
            else if (comboBoxSaveMethod.Text == "r2" && buttonAdvanceSettings.Text == "高级设置∧")
            {
                if (!checkBoxHighConcurrent.Checked)
                {
                    groupBoxR2.Location = _pipeOriginalLocation;
                }
                groupBoxR2.Location = groupBoxGist.Location;
                groupBoxR2.Visible = true;

                groupBoxGist.Visible = false;
                groupBoxWebdav.Visible = false;
            }
            else if (comboBoxSaveMethod.Text == "webdav" && buttonAdvanceSettings.Text == "高级设置∧")
            {
                if (!checkBoxHighConcurrent.Checked)
                {
                    groupBoxWebdav.Location = _pipeOriginalLocation;
                }
                groupBoxWebdav.Location = groupBoxGist.Location;
                groupBoxWebdav.Visible = true;

                groupBoxGist.Visible = false;
                groupBoxR2.Visible = false;
            }
        }

        private void comboBoxSaveMethod_TextChanged(object sender, EventArgs e)
        {
            判断保存类型();
            if (!(comboBoxSaveMethod.Text == "本地" || comboBoxSaveMethod.Text == "") && buttonAdvanceSettings.Text == "高级设置∨") buttonAdvanceSettings_Click(sender, e);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '\0';
            textBox6.PasswordChar = '\0';
            textBox8.PasswordChar = '\0';
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
            textBox6.PasswordChar = '*';
            textBox8.PasswordChar = '*';
        }

        private void textBoxWebUiAPIKey_Enter(object sender, EventArgs e)
        {
            textBoxWebUiAPIKey.PasswordChar = '\0';
            if (textBoxWebUiAPIKey.Text == "请输入密钥")
            {
                textBoxWebUiAPIKey.Text = "";
                textBoxWebUiAPIKey.ForeColor = Color.Black;
            }
        }

        private void textBoxWebUiAPIKey_Leave(object sender, EventArgs e)
        {

            if (textBoxWebUiAPIKey.Text == "")
            {
                textBoxWebUiAPIKey.PasswordChar = '\0';
                textBoxWebUiAPIKey.Text = "请输入密钥";
                textBoxWebUiAPIKey.ForeColor = Color.Gray;
            }
            else
            {
                textBoxWebUiAPIKey.ForeColor = Color.Black;
                textBoxWebUiAPIKey.PasswordChar = '*';
            }
        }

        private void textBoxSubStorePath_Enter(object sender, EventArgs e)
        {
            textBoxSubStorePath.PasswordChar = '\0';
            if (textBoxSubStorePath.Text == "请输入路径")
            {
                textBoxSubStorePath.Text = "";
                textBoxSubStorePath.ForeColor = Color.Black;
            }
        }

        private void textBoxSubStorePath_Leave(object sender, EventArgs e)
        {

            if (textBoxSubStorePath.Text == "")
            {
                textBoxSubStorePath.PasswordChar = '\0';
                textBoxSubStorePath.Text = "请输入路径";
                textBoxSubStorePath.ForeColor = Color.Gray;
            }
            else
            {
                textBoxSubStorePath.ForeColor = Color.Black;
                textBoxSubStorePath.PasswordChar = '*';
            }
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {
            // 检查是否有内容
            if (string.IsNullOrWhiteSpace(textBox7.Text))
                return;

            string input = textBox7.Text.Trim();

            try
            {
                // 尝试解析为 URI
                Uri uri = new Uri(input);

                // 构建基础 URL (scheme + authority)
                string baseUrl = $"{uri.Scheme}://{uri.Authority}";

                // 更新 textBox7 的文本为基础 URL
                textBox7.Text = baseUrl;
            }
            catch (UriFormatException)
            {
                // 如果输入的不是有效 URI，尝试使用简单的字符串处理
                // 查找双斜杠后的第一个斜杠
                int schemeIndex = input.IndexOf("://");
                if (schemeIndex >= 0)
                {
                    int pathStartIndex = input.IndexOf('/', schemeIndex + 3);
                    if (pathStartIndex >= 0)
                    {
                        // 截取到路径开始之前
                        textBox7.Text = input.Substring(0, pathStartIndex);
                    }
                }
            }
        }

        private RichTextBox GetRichTextBoxAllLog()
        {
            return richTextBoxAllLog;
        }

        public void Log(string message, RichTextBox richTextBoxAllLog, bool isError = false)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logType = isError ? "ERR" : "INF";
            richTextBoxAllLog.AppendText($"{timestamp} {logType} {message}\r\n");

            if (richTextBoxAllLog.IsHandleCreated)
            {
                richTextBoxAllLog.BeginInvoke((MethodInvoker)(() =>
                {
                    // 滚动到最底部
                    richTextBoxAllLog.SelectionStart = richTextBoxAllLog.Text.Length;
                    richTextBoxAllLog.ScrollToCaret();
                }));
            }
        }

        private void 恢复窗口()
        {
            // 首先显示窗体
            this.Show();

            // 强制停止当前布局逻辑
            this.SuspendLayout();

            // 恢复窗口状态
            this.WindowState = FormWindowState.Normal;

            // 强制重新布局
            this.ResumeLayout(true); // 参数true表示立即执行布局

            // 调用刷新布局的方法
            this.PerformLayout();

            // 处理WindowsForms消息队列中的所有挂起消息
            Application.DoEvents();

            // 激活窗口（使其获得焦点）
            this.Activate();
        }

        private void 隐藏窗口()
        {
            // 隐藏窗体（从任务栏消失）
            this.Hide();

            // 确保通知图标可见
            notifyIcon1.Visible = true;

            // 可选：显示气泡提示
            notifyIcon1.ShowBalloonTip(1000, "SubsCheck", "程序已最小化到系统托盘", ToolTipIcon.Info);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 检查窗口是否可见
                if (this.Visible)
                {
                    // 如果窗口当前可见，则隐藏窗口
                    隐藏窗口();
                }
                else
                {
                    // 如果窗口当前不可见，则恢复窗口
                    恢复窗口();
                }
            }
        }

        // 创建专用方法用于异步检测GitHub代理
        private async Task<string> DetectGitHubProxyAsync(List<string> proxyItems)
        {
            bool proxyFound = false;
            string detectedProxyURL = "";

            Log("检测可用 GitHub 代理...", GetRichTextBoxAllLog());

            // 遍历随机排序后的代理列表
            foreach (string proxyItem in proxyItems)
            {
                string checkUrl = $"https://{proxyItem}/https://raw.githubusercontent.com/sinspired/SubsCheck-Win-GUI/master/packages.config";
                Log($"正在测试 GitHub 代理: {proxyItem}", GetRichTextBoxAllLog());
                richTextBoxAllLog.Refresh();

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
                            //richTextBoxAllLog.Clear(); 暂时禁用
                            Log($"找到可用 GitHub 代理: {proxyItem}", GetRichTextBoxAllLog());
                            proxyFound = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // 记录错误但继续尝试下一个
                    Log($"代理 {proxyItem} 测试失败: {ex.Message}", GetRichTextBoxAllLog(), true);
                    richTextBoxAllLog.Refresh();
                }
            }

            // 如果没有找到可用的代理
            if (!proxyFound)
            {
                Log("未找到可用的 GitHub 代理，请在高级设置中手动设置。", GetRichTextBoxAllLog(), true);
                MessageBox.Show("未找到可用的 GitHub 代理。\n\n请打开高级设置手动填入一个可用的Github Proxy，或检查您的网络连接。",
                    "代理检测失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return detectedProxyURL;
        }

        private async void buttonUpdateKernel_Click(object sender, EventArgs e)
        {
            try
            {
                buttonUpdateKernel.Enabled = false;
                buttonStartCheck.Enabled = false;
                // 清空日志
                richTextBoxAllLog.Clear();
                Log("开始检查和下载最新版本的 subs-check.exe...", GetRichTextBoxAllLog());

                // 获取当前应用程序目录
                string executablePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string subsCheckPath = Path.Combine(executablePath, "subs-check.exe");

                // 检查文件是否存在
                if (File.Exists(subsCheckPath))
                {
                    Log($"发现 subs-check.exe，正在删除...", GetRichTextBoxAllLog());

                    try
                    {
                        // 首先检查是否有进程正在运行
                        Process[] processes = Process.GetProcessesByName("subs-check");
                        if (processes.Length > 0)
                        {
                            Log("发现正在运行的 subs-check.exe 进程，正在强制结束...", GetRichTextBoxAllLog());
                            foreach (Process process in processes)
                            {
                                try
                                {
                                    process.Kill();
                                    process.WaitForExit();
                                    Log($"成功结束 subs-check.exe 进程(ID: {process.Id})", GetRichTextBoxAllLog());
                                }
                                catch (Exception ex)
                                {
                                    Log($"结束进程时出错(ID: {process.Id}): {ex.Message}", GetRichTextBoxAllLog(), true);
                                }
                            }
                        }

                        // 删除文件
                        File.Delete(subsCheckPath);
                        Log("成功删除旧版本 subs-check.exe", GetRichTextBoxAllLog());
                    }
                    catch (Exception ex)
                    {
                        Log($"删除 subs-check.exe 时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                        MessageBox.Show($"无法删除现有的 subs-check.exe 文件: {ex.Message}\n\n请手动删除后重试，或者检查文件是否被其他程序占用。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buttonUpdateKernel.Enabled = true;
                        return;
                    }
                }
                else
                {
                    Log("未找到现有的 subs-check.exe 文件，将直接下载最新版本", GetRichTextBoxAllLog());
                }

                //// 检测可用的 GitHub 代理
                //githubProxyURL = await GetGithubProxyUrlAsync();
                //if (githubProxyURL == "")
                //{
                //    Log("未设置 GitHub 代理，将尝试直接下载", GetRichTextBoxAllLog(), true);
                //}

                // 下载最新版本的 subs-check.exe
                await DownloadSubsCheckEXE();

                // 完成
                Log("内核更新完成！", GetRichTextBoxAllLog());
            }
            catch (Exception ex)
            {
                Log($"操作过程中出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                MessageBox.Show($"处理过程中出现错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonUpdateKernel.Enabled = true;
                buttonStartCheck.Enabled = true;
            }
        }

        private decimal 订阅端口;
        private decimal SubStore端口;
        private void numericUpDownWebUIPort_ValueChanged(object sender, EventArgs e)
        {
            // 检查numericUpDown7是否存在并且与numericUpDown6的值相等
            if (numericUpDownWebUIPort.Value == numericUpDownSubStorePort.Value)
            {
                // 显示警告消息
                MessageBox.Show("订阅端口 和 Sub-Store端口 不能相同！",
                               "端口冲突",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);

                // 将numericUpDown6的值恢复为更改前的值
                numericUpDownWebUIPort.Value = 订阅端口;
                numericUpDownSubStorePort.Value = SubStore端口;
            }
            else
            {
                // 保存当前值作为下次比较的基准
                订阅端口 = numericUpDownWebUIPort.Value;
                SubStore端口 = numericUpDownSubStorePort.Value;
            }
        }


        private async Task KillNodeProcessAsync()
        {
            try
            {
                Log("检查 node.exe 进程状态...", GetRichTextBoxAllLog());

                // 获取当前应用程序的执行目录
                string executablePath = Path.GetDirectoryName(Application.ExecutablePath);
                string nodeExePath_old = Path.Combine(executablePath, "output", "node.exe");
                string nodeExePath_new = Path.Combine(executablePath, "output", "sub-store", "node.exe");

                // 初始化要检查的路径数组
                string[] nodeExePaths = new string[] { nodeExePath_old, nodeExePath_new };

                // 获取所有 node.exe 进程
                Process[] nodeProcesses = Process.GetProcessesByName("node");

                if (nodeProcesses.Length == 0)
                {
                    Log("未发现运行中的 node.exe 进程", GetRichTextBoxAllLog());
                    return;
                }

                Log($"发现 {nodeProcesses.Length} 个 node.exe 进程，开始检查并终止匹配路径的进程...", GetRichTextBoxAllLog());

                int terminatedCount = 0;

                foreach (Process process in nodeProcesses)
                {
                    try
                    {
                        // 使用 WMI 获取进程路径，避免 32/64 位访问冲突
                        string processPath = await Task.Run(() => GetProcessPathByWmi(process.Id));

                        // 检查是否匹配我们要查找的 node.exe 路径
                        foreach (var nodeExePath in nodeExePaths)
                        {
                            if (!string.IsNullOrEmpty(processPath) &&
                                processPath.Equals(nodeExePath, StringComparison.OrdinalIgnoreCase))
                            {
                                Log($"发现匹配路径的 node.exe 进程(ID: {process.Id})，正在强制结束...", GetRichTextBoxAllLog());

                                await Task.Run(() =>
                                {
                                    process.Kill();
                                    process.WaitForExit();
                                });

                                Log($"成功结束 node.exe 进程(ID: {process.Id})", GetRichTextBoxAllLog());
                                terminatedCount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"访问或终止进程(ID: {process.Id})时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                    }
                }

                if (terminatedCount > 0)
                {
                    Log($"总共终止了 {terminatedCount} 个匹配路径的 node.exe 进程", GetRichTextBoxAllLog());
                }
                else
                {
                    Log("未发现需要终止的 node.exe 进程", GetRichTextBoxAllLog());
                }
            }
            catch (Exception ex)
            {
                Log($"检查或终止 node.exe 进程时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
            }
        }

        /// <summary>
        /// 使用 WMI 获取指定进程的可执行文件路径
        /// 这样可以避免 32 位进程访问 64 位进程 MainModule 时的异常
        /// </summary>
        private string GetProcessPathByWmi(int processId)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = {processId}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["ExecutablePath"]?.ToString();
                    }
                }
            }
            catch
            {
                // 忽略异常，返回 null
            }
            return null;
        }

        private async void textBoxSubsUrls_DoubleClick(object sender, EventArgs e)
        {
            if (textBoxSubsUrls.Enabled)
            {
                // 创建EditURLs窗口的实例
                EditURLs editURLsForm = new EditURLs();

                // 传递当前textBox1的内容到EditURLs窗口
                editURLsForm.UrlContent = textBoxSubsUrls.Text + "\n";
                editURLsForm.githubProxys = comboBoxGithubProxyUrl.Items;
                editURLsForm.githubProxy = comboBoxGithubProxyUrl.Text;
                editURLsForm.LogAction = (msg) => Log(msg, richTextBoxAllLog); // 传递主窗体的 Log 方法
                // 显示对话框并等待结果
                DialogResult result = editURLsForm.ShowDialog();

                // 如果用户点击了"保存并关闭"按钮（返回DialogResult.OK）
                if (result == DialogResult.OK)
                {
                    // 获取编辑后的内容，按行拆分，过滤空行
                    string[] lines = editURLsForm.UrlContent.Split(
                        new[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.RemoveEmptyEntries);

                    // 去除每行首尾的空白字符
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = lines[i].Trim();
                    }

                    // 再次过滤掉空行
                    lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

                    // 将处理后的内容更新到Form1的textBox1
                    textBoxSubsUrls.Text = string.Join(Environment.NewLine, lines);
                    await SaveConfig(false);
                    Log("已保存订阅地址列表。", GetRichTextBoxAllLog());
                }
            }

        }

        private void checkBoxEnableRenameNode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableRenameNode.Checked == false) checkBoxEnableMediaCheck.Checked = false;
        }

        private void checkBoxEnableMediaCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableMediaCheck.Checked == true) checkBoxEnableRenameNode.Checked = true;
        }

        private async void timerRestartSchedule_Tick(object sender, EventArgs e)
        {
            if (buttonStartCheck.Text == "⏹️ 停止")
            {
                buttonStartCheck.ForeColor = Color.Red;
                Log("subs-check.exe 运行时满24小时，自动重启清理内存占用。", GetRichTextBoxAllLog());
                // 停止 subs-check.exe 程序
                StopSubsCheckProcess();
                // 结束 Sub-Store
                await KillNodeProcessAsync();
                // 重新启动 subs-check.exe 程序
                StartSubsCheckProcess();
                numericUpDownConcurrent.Enabled = false;
                numericUpDownInterval.Enabled = false;
                numericUpDownTimeout.Enabled = false;
                numericUpDownMinSpeed.Enabled = false;
                numericUpDownDLTimehot.Enabled = false;
                numericUpDownDownloadMb.Enabled = false;
                numericUpDownTotalBandwidthLimit.Enabled = false;
                numericUpDownWebUIPort.Enabled = false;
                numericUpDownSubStorePort.Enabled = false;
                comboBoxSaveMethod.Enabled = false;
                textBoxSubsUrls.Enabled = false;
                groupBoxAdvanceSettings.Enabled = false;
                groupBoxGist.Enabled = false;
                groupBoxR2.Enabled = false;
                groupBoxWebdav.Enabled = false;
                buttonStartCheck.Text = "⏹️ 停止";
                buttonStartCheck.ForeColor = Color.Red;
            }
        }

        private void buttonCheckUpdate_Click(object sender, EventArgs e)
        {
            // 创建 CheckUpdates 窗口实例
            CheckUpdates checkUpdatesForm = new CheckUpdates();

            // 传递必要的数据和状态
            checkUpdatesForm.githubProxys = comboBoxGithubProxyUrl.Items;
            checkUpdatesForm.githubProxy = comboBoxGithubProxyUrl.Text;

            checkUpdatesForm.当前subsCheck版本号 = 当前subsCheck版本号;
            checkUpdatesForm.当前GUI版本号 = 当前GUI版本号;
            checkUpdatesForm.最新GUI版本号 = 最新GUI版本号;
            checkUpdatesForm.EnableHighConcurrent = checkBoxHighConcurrent.Checked;
            checkUpdatesForm.EnableArch64 = checkBoxSwitchArch64.Checked;



            // 为 CheckUpdates 的 button2 添加点击事件处理程序
            checkUpdatesForm.FormClosed += (s, args) =>
            {
                // 移除事件处理，避免内存泄漏
                if (checkUpdatesForm.DialogResult == DialogResult.OK)
                {
                    // 如果返回OK结果，表示按钮被点击并需要更新内核
                    buttonUpdateKernel_Click(this, EventArgs.Empty);
                }
            };

            // 设置 button2 点击后关闭窗口并返回 DialogResult.OK
            // 这需要在 CheckUpdates.cs 中修改 buttonUpdateKernel_Click 方法

            // 显示 CheckUpdates 窗口
            checkUpdatesForm.ShowDialog();
        }

        private void checkBoxEnableSuccessLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableSuccessLimit.Checked) numericUpDownSuccessLimit.Enabled = true;
            else numericUpDownSuccessLimit.Enabled = false;
        }

        private void checkBoxTotalBandwidthLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTotalBandwidthLimit.Checked) numericUpDownTotalBandwidthLimit.Enabled = true;
            else
            {
                numericUpDownTotalBandwidthLimit.Enabled = false;
                numericUpDownTotalBandwidthLimit.Value = 0;
            }
        }

        private async void comboBoxOverwriteUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOverwriteUrls.Text.Contains("[内置]")) await comboBoxOverwriteUrlsSelection(true);
        }

        private async Task comboBoxOverwriteUrlsSelection(bool 汇报Log = false)
        {
            // 确定文件名和下载URL
            string fileName, downloadUrl, displayName;
            string executablePath = Path.GetDirectoryName(Application.ExecutablePath);

            if (comboBoxOverwriteUrls.Text.Contains("[内置]布丁狗"))
            {
                fileName = "bdg.yaml";
                displayName = "[内置]布丁狗的订阅转换";
                downloadUrl = "https://raw.githubusercontent.com/cmliu/ACL4SSR/main/yaml/bdg.yaml";
            }
            else
            {
                fileName = "ACL4SSR_Online_Full.yaml";
                displayName = "[内置]ACL4SSR_Online_Full";
                downloadUrl = "https://raw.githubusercontent.com/beck-8/override-hub/main/yaml/ACL4SSR_Online_Full.yaml";
            }

            string outputFolderPath = Path.Combine(executablePath, "output");
            Directory.CreateDirectory(outputFolderPath);

            string downloadFilePath = Path.Combine(outputFolderPath, fileName);

            if (File.Exists(downloadFilePath))
            {
                if (汇报Log) Log($"{displayName} 覆写配置文件 已就绪。", GetRichTextBoxAllLog());
                return;
            }

            Log($"{displayName} 覆写配置文件 未找到，正在下载...", GetRichTextBoxAllLog());
            progressBarAll.Value = 0;
            progressBarAll.Visible = true;

            // 优先尝试 GitHub 带来，再尝试系统代理,最后使用无代理直连
            var (success, lastError) = await TryDownloadWithStrategiesAsync(
                downloadUrl,
                downloadFilePath,
                new[] { DownloadStrategy.GithubProxy, DownloadStrategy.SystemProxy, DownloadStrategy.Direct },
                false);

            if (!success)
            {
                Log($"{displayName} 覆写配置文件 下载失败: {lastError}", GetRichTextBoxAllLog(), true);
            }
            else
            {
                //richTextBoxAllLog.Clear();
                Log($"{displayName} 覆写配置文件 下载成功", GetRichTextBoxAllLog());
            }
        }

        /// <summary>
        /// 下载策略
        /// </summary>
        enum DownloadStrategy
        {
            GithubProxy,
            SystemProxy,
            Direct
        }

        private async Task<(bool success, string lastError)> TryDownloadWithStrategiesAsync(
            string downloadUrl,
            string downloadFilePath,
            IEnumerable<DownloadStrategy> strategyOrder,
            bool noRepeat = true)
        {
            bool success = false;
            string lastError = "";

            // 策略映射表
            var strategyFuncs = new Dictionary<DownloadStrategy, Func<Task<(bool useSysProxy, string url)>>>()
            {
                [DownloadStrategy.GithubProxy] = async () =>
                {
                    string githubProxyURL = await GetGithubProxyUrlAsync();
                    if (githubProxyURL == "")
                    {
                        return (false, null); // null 表示跳过
                    }
                    return (false, githubProxyURL + downloadUrl);
                },
                [DownloadStrategy.SystemProxy] = async () =>
                {
                    if (SysProxySetting != null)
                        await AutoCheckSysProxy(noRepeat);
                    return SysProxySetting.IsAvailable ? (true, downloadUrl) : (true, null); // null 表示跳过
                },
                [DownloadStrategy.Direct] = () => Task.FromResult((false, downloadUrl))
            };

            var strategies = strategyOrder.ToList();
            int totalTries = strategies.Count;

            for (int i = 0; i < totalTries && !success; i++)
            {
                var strategy = strategies[i];
                try
                {
                    var (useSysProxy, url) = await strategyFuncs[strategy]();

                    if (url == null)
                    {
                        Log($"[尝试{i + 1}/{totalTries}] 策略 {strategy} 不可用,跳过检测。", GetRichTextBoxAllLog(), true);
                        continue; // 直接跳过
                    }

                    using (HttpClientHandler handler = new HttpClientHandler
                    {
                        UseProxy = useSysProxy,
                        Proxy = useSysProxy ? WebRequest.DefaultWebProxy : null
                    })
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86)");
                        client.Timeout = TimeSpan.FromSeconds(15);

                        success = await DownloadFileWithProgressAsync(client, url, downloadFilePath);
                    }
                }
                catch (Exception ex)
                {
                    lastError = ex.Message;
                    Log($"[尝试{i + 1}/{totalTries}] 使用 {strategy} 下载失败: {ex.Message}", GetRichTextBoxAllLog(), true);
                }
            }

            return (success, lastError);
        }


        /// <summary>
        /// 下载文件并更新进度条
        /// </summary>
        private async Task<bool> DownloadFileWithProgressAsync(HttpClient httpClient, string url, string filePath)
        {
            try
            {
                // 获取文件大小
                HttpResponseMessage headResponse = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                headResponse.EnsureSuccessStatusCode(); // 确保请求成功
                long totalBytes = headResponse.Content.Headers.ContentLength ?? 0;

                // 下载文件
                using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        byte[] buffer = new byte[8192];
                        long totalBytesRead = 0;
                        int bytesRead;

                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalBytesRead += bytesRead;

                            // 更新进度条
                            if (totalBytes > 0)
                            {
                                int progressPercentage = (int)((totalBytesRead * 100) / totalBytes);
                                progressPercentage = Math.Min(100, Math.Max(0, progressPercentage));
                                progressBarAll.Enabled = true;
                                progressBarAll.Visible = true;
                                progressBarAll.Value = progressPercentage;
                            }
                        }
                    }
                }
                progressBarAll.Value = 0;
                progressBarAll.Visible = false;
                return true; // 下载成功
            }
            catch
            {
                throw; // 重新抛出异常，让调用者处理
            }
        }


        private void numericUpDownConcurrent_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxHighConcurrent.Checked)
            {
                Log("已启用流水线高并发模式✨\n- 此值将作为计算测活-测速-流媒体检测各阶段并发数的基准.\n- 内核已启用衰减算法,可放心设置", GetRichTextBoxAllLog());
            }
            else
            {
                if (numericUpDownConcurrent.Value > 128)
                {
                    string warningMessage =
                        "⚠️ 高并发风险提醒 ⚠️\n\n" +
                        "您设置的并发数值过高，可能导致：\n\n" +
                        "• 运营商判定为异常流量并限制网络\n" +
                        "• 路由器性能压力过大\n" +
                        "• 测速结果不准确\n\n" +
                        "并发数设置建议：\n" +
                        "• 宽带峰值/50Mbps：一般对网络无影响\n" +
                        "• 宽带峰值/25Mbps：可能会影响同网络下载任务\n" +
                        "• 宽带峰值/10Mbps：可能会影响同网络下其他设备的上网体验\n";

                    Log(warningMessage, GetRichTextBoxAllLog());
                }
            }
        }

        private void checkBoxEnableWebUI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableWebUI.Checked) textBoxWebUiAPIKey.Enabled = true;
            else textBoxWebUiAPIKey.Enabled = false;
        }

        private void buttonWebUi_Click(object sender, EventArgs e)
        {
            string 本地IP = GetLocalLANIP();
            try
            {
                // 构造URL
                string url = $"http://{本地IP}:{numericUpDownWebUIPort.Value}/admin";

                // 使用系统默认浏览器打开URL
                System.Diagnostics.Process.Start(url);

                Log($"正在浏览器中打开 Subs-Check 配置管理: {url}", GetRichTextBoxAllLog());
            }
            catch (Exception ex)
            {
                Log($"打开浏览器失败: {ex.Message}", GetRichTextBoxAllLog(), true);
                MessageBox.Show($"打开浏览器时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取API状态信息并返回包含6个元素的字符串数组
        /// </summary>
        /// <returns>
        /// 包含6个元素的字符串数组：
        /// [0] - 状态类型 ("checking"/"idle"/"error")
        /// [1] - 状态图标类别 ("primary"/"success"/"danger")
        /// [2] - 状态文本 ("正在检测中..."/"空闲"/"获取状态失败")
        /// [3] - 节点总数 (proxyCount或"N/A")
        /// [4] - 进度百分比 (progress或"N/A")
        /// [5] - 可用节点数量 (available或"N/A")
        /// </returns>
        private async Task<string[]> GetApiStatusAsync()
        {
            string[] resultArray = new string[6];
            string baseUrl = $"http://127.0.0.1:{numericUpDownWebUIPort.Value}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 设置基础URL
                    client.BaseAddress = new Uri(baseUrl);

                    // 添加API密钥请求头
                    client.DefaultRequestHeaders.Add("X-API-Key", WebUIapiKey);

                    // 设置超时时间
                    client.Timeout = TimeSpan.FromSeconds(5);

                    // 发送请求
                    HttpResponseMessage response = await client.GetAsync("/api/status");

                    // 检查响应状态
                    if (response.IsSuccessStatusCode)
                    {
                        // 读取响应内容
                        string content = await response.Content.ReadAsStringAsync();

                        // 解析JSON
                        JObject data = JObject.Parse(content);

                        if (data["checking"] != null && data["checking"].Value<bool>())
                        {
                            // 正在检测状态
                            resultArray[0] = "checking";
                            resultArray[1] = "primary";
                            resultArray[2] = "正在检测中...";

                            // 提取节点数据
                            resultArray[3] = data["proxyCount"]?.ToString() ?? "0";
                            resultArray[4] = data["progress"]?.ToString() ?? "0";
                            resultArray[5] = data["available"]?.ToString() ?? "0";
                        }
                        else
                        {
                            // 空闲状态
                            resultArray[0] = "idle";
                            resultArray[1] = "success";
                            resultArray[2] = "空闲";

                            // 空闲时相关数据设为N/A
                            resultArray[3] = "N/A";
                            resultArray[4] = "N/A";
                            resultArray[5] = "N/A";
                        }
                    }
                    else
                    {
                        // 请求失败，例如未授权
                        resultArray[0] = "error";
                        resultArray[1] = "danger";
                        resultArray[2] = $"API请求失败: {(int)response.StatusCode}";
                        resultArray[3] = "N/A";
                        resultArray[4] = "N/A";
                        resultArray[5] = "N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                // 发生异常
                resultArray[0] = "error";
                resultArray[1] = "danger";
                resultArray[2] = $"获取状态失败: {ex.Message}";
                resultArray[3] = "N/A";
                resultArray[4] = "N/A";
                resultArray[5] = "N/A";

                // 可选：记录错误到日志
                Log($"获取API状态失败: {ex.Message}", GetRichTextBoxAllLog(), true);
            }

            return resultArray;
        }

        private async void timerRefresh_Tick(object sender, EventArgs e)
        {
            //if (!button7.Enabled) button7.Enabled = true;
            string[] subscheck状态 = await GetApiStatusAsync();
            string 状态类型 = subscheck状态[0];
            string 状态图标类别 = subscheck状态[1];
            string 状态文本 = subscheck状态[2];
            string 节点总数 = subscheck状态[3];
            string 进度百分比 = subscheck状态[4];
            string 可用节点数量 = subscheck状态[5];
            // 更新状态文本

            if (状态类型 == "checking")
            {
                buttonTriggerCheck.Text = buttonTriggerCheck.Text == "⌛获取订阅" ? buttonTriggerCheck.Text : "⌛获取订阅";
                labelLogNodeInfo.ForeColor = Color.Black;
                nodeInfo = $"({进度百分比}/{节点总数}) 可用: {可用节点数量}";

                int.TryParse(节点总数, out int nodeTotal);
                int.TryParse(进度百分比, out int curr);

                if (nodeTotal > 0)
                {
                    int 进度条百分比 = curr * 100 / nodeTotal;
                    if (进度条百分比 < 0) 进度条百分比 = 0;
                    if (进度条百分比 > 100) 进度条百分比 = 100;

                    progressBarAll.Enabled = true;
                    progressBarAll.Visible = true;
                    progressBarAll.Value = 进度条百分比;

                    if (!buttonTriggerCheck.Enabled) buttonTriggerCheck.Enabled = true;
                    buttonTriggerCheck.Text = "⏸️结束检测";
                    buttonTriggerCheck.ForeColor = HexToRgbColor("#6633f4");
                    labelLogNodeInfo.ForeColor = Color.Black;
                }

                // 仅在文本变化时更新 NotifyIcon，避免频繁重绘
                string notifyText = "SubsCheck: " + nodeInfo;
                if (notifyText.Length > 63) notifyText = notifyText.Substring(0, 60) + "...";
                if (_lastNotifyText != notifyText)
                {
                    _lastNotifyText = notifyText;
                    notifyIcon1.Text = notifyText;
                }

                if (textBoxSubsUrls.Enabled) textBoxSubsUrls.Enabled = false; // 仅在需要时改变
            }
            else if (状态类型 == "idle")
            {
                if (buttonTriggerCheck.Text != "⏯️开始检测") buttonTriggerCheck.Text = "⏯️开始检测";
                buttonTriggerCheck.ForeColor = HexToRgbColor("#35bc00");
                //labelLogNodeInfo.Text = $"{nextCheckTime}";
                //labelLogNodeInfo.ForeColor = Color.Green;

                progressBarAll.Visible = false;


                string idleNotify = "SubsCheck: 已就绪\n" + nextCheckTime;
                if (_lastNotifyText != idleNotify)
                {
                    nodeInfo = $"{nextCheckTime}";
                    _lastNotifyText = idleNotify;
                    notifyIcon1.Text = idleNotify;
                }

                if (!textBoxSubsUrls.Enabled) textBoxSubsUrls.Enabled = true;
            }
            else if (状态类型 == "error")
            {
                if (buttonTriggerCheck.Text != "🔀 未知") buttonTriggerCheck.Text = "🔀 未知";
                nodeInfo = 状态文本;
                labelLogNodeInfo.Text = "实时日志";
                labelLogNodeInfo.ForeColor = Color.Black;
            }

            // 仅在标题文字确实变化时更新，避免父容器反复重绘引起的闪烁
            string groupTitle = $"实时日志 {nodeInfo}";
            if (_lastLogLabelNodeInfoText != groupTitle)
            {
                _lastLogLabelNodeInfoText = groupTitle;
                labelLogNodeInfo.Text = groupTitle;
            }
        }

        private async void buttonTriggerCheck_Click(object sender, EventArgs e)
        {
            buttonTriggerCheck.Enabled = false;
            timerRefresh.Enabled = false;

            try
            {
                bool isSuccess;

                if (buttonTriggerCheck.Text == "⏯️开始检测")
                {
                    buttonTriggerCheck.ForeColor = HexToRgbColor("#00BFFF");
                    //labelLogNodeInfo.Text = $"启动检测";
                    labelLogNodeInfo.ForeColor = Color.Black;

                    await AutoCheckSysProxy();
                    isSuccess = await SendApiRequestAsync("/api/trigger-check", "发送手动检查信号");
                    if (isSuccess)
                    {
                        buttonTriggerCheck.Text = "⏸️结束检测";
                        buttonTriggerCheck.Enabled = false;
                        labelLogNodeInfo.ForeColor = Color.Black;
                        textBoxSubsUrls.Enabled = false; // 检查开始后禁用订阅编辑
                    }
                }
                else // "⏸️结束检测"
                {
                    labelLogNodeInfo.ForeColor = Color.Black;
                    isSuccess = await SendApiRequestAsync("/api/force-close", "发送提前结束检测信号");
                }

                // 如果请求失败，更新按钮状态为未知
                if (!isSuccess) buttonTriggerCheck.Text = "🔀 未知";
                buttonTriggerCheck.ForeColor = Color.Gray;
                labelLogNodeInfo.ForeColor = Color.Black;
            }
            finally
            {
                // 无论成功失败都重新启用定时器和按钮
                timerRefresh.Enabled = true;
                timerRefresh.Start();
                //button7.Enabled = true;
            }
        }

        /// <summary>
        /// 发送API请求到SubsCheck服务
        /// </summary>
        /// <param name="endpoint">API端点路径</param>
        /// <param name="operationName">操作名称(用于日志)</param>
        /// <returns>操作是否成功</returns>
        private async Task<bool> SendApiRequestAsync(string endpoint, string operationName)
        {
            try
            {
                // 获取API基础地址和API密钥
                string baseUrl = $"http://127.0.0.1:{numericUpDownWebUIPort.Value}";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Add("X-API-Key", WebUIapiKey);
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // 发送POST请求
                    HttpResponseMessage response = await client.PostAsync(endpoint, new StringContent(""));

                    // 检查响应状态
                    if (response.IsSuccessStatusCode)
                    {
                        Log($"成功{operationName}", GetRichTextBoxAllLog());
                        return true;
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Log($"{operationName}失败: HTTP {(int)response.StatusCode} {response.ReasonPhrase}\n{errorContent}", GetRichTextBoxAllLog(), true);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"{operationName}时发生错误: {ex.Message}", GetRichTextBoxAllLog(), true);
                return false;
            }
        }

        private void textBoxCron_Leave(object sender, EventArgs e)
        {
            if (IsValidCronExpression(textBoxCron.Text))
            {
                // 计算并显示cron表达式的说明
                string cronDescription = GetCronExpressionDescription(textBoxCron.Text);
                // 可以用工具提示或者消息框显示，这里使用消息框
                //MessageBox.Show(cronDescription, "Cron表达式说明", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Cron表达式说明 {cronDescription}", GetRichTextBoxAllLog());
            }
            else
            {
                MessageBox.Show("请输入有效的cron表达式，例如：*/30 * * * *", "无效的cron表达式",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCron.Focus();
                textBoxCron.Text = "0 */2 * * *"; // 恢复默认值
            }
        }

        /// <summary>
        /// 验证输入文本是否是合法的cron表达式
        /// </summary>
        /// <returns>如果是合法的cron表达式，则返回true；否则返回false</returns>
        private bool IsValidCronExpression(string cron表达式)
        {
            string cronExpression = cron表达式.Trim();

            // 如果是空字符串，则不是有效表达式
            if (string.IsNullOrWhiteSpace(cronExpression))
                return false;

            // 分割cron表达式为5个部分：分钟 小时 日期 月份 星期
            string[] parts = cronExpression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // cron表达式必须有5个部分
            if (parts.Length != 5)
                return false;

            try
            {
                // 验证每个部分
                // 分钟 (0-59)
                if (!IsValidCronField(parts[0], 0, 59))
                    return false;

                // 小时 (0-23)
                if (!IsValidCronField(parts[1], 0, 23))
                    return false;

                // 日期 (1-31)
                if (!IsValidCronField(parts[2], 1, 31))
                    return false;

                // 月份 (1-12)
                if (!IsValidCronField(parts[3], 1, 12))
                    return false;

                // 星期 (0-7，0和7都表示星期日)
                if (!IsValidCronField(parts[4], 0, 7))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证cron表达式中的单个字段是否合法
        /// </summary>
        /// <param name="field">字段值</param>
        /// <param name="min">最小允许值</param>
        /// <param name="max">最大允许值</param>
        /// <returns>如果字段合法，则返回true；否则返回false</returns>
        private bool IsValidCronField(string field, int min, int max)
        {
            // 处理通配符 "*"
            if (field == "*")
                return true;

            // 处理步长 "*/n"
            if (field.StartsWith("*/"))
            {
                string stepStr = field.Substring(2);
                if (int.TryParse(stepStr, out int step))
                    return step > 0 && step <= max;
                return false;
            }

            // 处理范围 "n-m"
            if (field.Contains("-"))
            {
                string[] range = field.Split('-');
                if (range.Length != 2)
                    return false;

                if (int.TryParse(range[0], out int start) && int.TryParse(range[1], out int end))
                    return start >= min && end <= max && start <= end;
                return false;
            }

            // 处理列表 "n,m,k"
            if (field.Contains(","))
            {
                string[] values = field.Split(',');
                foreach (string item in values)
                {
                    if (!int.TryParse(item, out int itemValue) || itemValue < min || itemValue > max)
                        return false;
                }
                return true;
            }

            // 处理单个数字
            if (int.TryParse(field, out int fieldValue))
                return fieldValue >= min && fieldValue <= max;

            return false;
        }

        /// <summary>
        /// 获取cron表达式的友好文本说明
        /// </summary>
        /// <param name="cron表达式">要解析的cron表达式</param>
        /// <returns>返回cron表达式的执行时间说明</returns>
        private string GetCronExpressionDescription(string cron表达式)
        {
            try
            {
                string cronExpression = cron表达式.Trim();
                string[] parts = cronExpression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 5)
                    return "无效的cron表达式";

                // 分别解析每个部分
                string minuteDesc = ParseCronPart(parts[0], "分钟", 0, 59);
                string hourDesc = ParseCronPart(parts[1], "小时", 0, 23);
                string dayDesc = ParseCronPart(parts[2], "日", 1, 31);
                string monthDesc = ParseCronPart(parts[3], "月", 1, 12);
                string weekDesc = ParseCronPart(parts[4], "星期", 0, 7, true);

                // 组合最终说明
                string description = "执行时间: ";

                // 月份
                if (monthDesc != "每月")
                    description += monthDesc + "的";

                // 星期与日期的关系
                if (parts[2] == "*" && parts[4] != "*")
                    description += weekDesc + "的";
                else if (parts[2] != "*" && parts[4] == "*")
                    description += dayDesc;
                else if (parts[2] != "*" && parts[4] != "*")
                    description += $"{dayDesc}或{weekDesc}";
                else
                    description += "每天";

                // 时间（小时:分钟）
                description += $"{hourDesc}{minuteDesc}";

                return description;
            }
            catch
            {
                return "无法解析cron表达式";
            }
        }

        /// <summary>
        /// 解析cron表达式的单个部分
        /// </summary>
        private string ParseCronPart(string part, string unit, int min, int max, bool isWeekday = false)
        {
            // 处理星号，表示每个时间单位
            if (part == "*")
            {
                return $"每{unit}";
            }

            // 处理步长 */n
            if (part.StartsWith("*/"))
            {
                int step = int.Parse(part.Substring(2));
                return $"每{step}{unit}";
            }

            // 处理范围 n-m
            if (part.Contains("-"))
            {
                string[] range = part.Split('-');
                int start = int.Parse(range[0]);
                int end = int.Parse(range[1]);

                if (isWeekday)
                {
                    string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
                    return $"从{weekdays[start]}到{weekdays[end]}";
                }

                return $"从{start}{unit}到{end}{unit}";
            }

            // 处理列表 n,m,k
            if (part.Contains(","))
            {
                string[] values = part.Split(',');
                if (isWeekday)
                {
                    string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
                    return string.Join("、", values.Select(v => weekdays[int.Parse(v)]));
                }
                return $"{string.Join("、", values)}{unit}";
            }

            // 处理单个数字
            int value = int.Parse(part);
            if (isWeekday)
            {
                string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
                return weekdays[value];
            }
            return $"{value}{unit}";
        }

        private void 切换cron表达式(object sender, EventArgs e)
        {
            if (textBoxCron.Visible)
            {
                labelCron.Visible = false;
                textBoxCron.Visible = false;
                labelInterval.Visible = true;
                numericUpDownInterval.Visible = true;
                Log("下次检查时间间隔 使用分钟倒计时", GetRichTextBoxAllLog());
            }
            else
            {
                labelCron.Location = new Point(labelCron.Location.X, labelInterval.Location.Y);
                textBoxCron.Location = new Point(textBoxCron.Location.X, numericUpDownInterval.Location.Y);
                labelCron.Visible = true;
                textBoxCron.Visible = true;
                labelInterval.Visible = false;
                numericUpDownInterval.Visible = false;
                Log("下次检查时间间隔 使用cron表达式", GetRichTextBoxAllLog());
            }
        }

        /// <summary>
        /// 获取计算机名的MD5哈希值
        /// </summary>
        /// <returns>返回计算机名的MD5哈希字符串(32位小写)</returns>
        private string GetComputerNameMD5()
        {
            try
            {
                // 获取计算机名
                string computerName = System.Environment.MachineName;

                // 引入必要的命名空间
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    // 将计算机名转换为字节数组
                    byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(computerName);

                    // 计算MD5哈希值
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // 将字节数组转换为十六进制字符串
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }

                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Log($"计算计算机名MD5时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                return "CMLiussss";
            }
        }

        private static about aboutWindow = null;
        private void linkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 检查窗口是否已经打开
            if (aboutWindow != null && !aboutWindow.IsDisposed)
            {
                // 窗口已经存在，激活它
                aboutWindow.Activate();
                return;
            }

            // 需要创建新窗口
            this.BeginInvoke(new Action(() =>
            {
                // 创建about窗口实例
                aboutWindow = new about();

                // 传递版本号信息
                aboutWindow.GuiVersion = 当前GUI版本号;
                aboutWindow.CoreVersion = 当前subsCheck版本号;

                // 添加窗口关闭时的处理，清除静态引用
                aboutWindow.FormClosed += (s, args) => aboutWindow = null;

                // 非模态显示窗口
                aboutWindow.Show(this);

                // 设置TopMost确保窗口显示在最前面
                aboutWindow.TopMost = true;
            }));
        }

        private void buttonMoreSettings_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建MoreYAML窗口实例
                MoreYAML moreYamlWindow = new MoreYAML();

                // 显示为模态对话框，这会阻塞主线程直到窗口关闭
                DialogResult result = moreYamlWindow.ShowDialog(this);

                // 如果需要，可以处理对话框的返回结果
                if (result == DialogResult.OK)
                {
                    // 用户点击了"确定"或某种完成操作的按钮
                    Log("补充参数配置已成功保存到 more.yaml 文件！设置已应用", GetRichTextBoxAllLog());
                }
            }
            catch (Exception ex)
            {
                Log($"打开MoreYAML窗口时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                MessageBox.Show($"打开MoreYAML窗口时出错: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxStartup.Enabled = false;
            try
            {
                // 获取当前应用程序的可执行文件路径
                string appPath = Application.ExecutablePath;
                // 获取应用程序名称（不包含扩展名）
                string appName = Path.GetFileNameWithoutExtension(appPath);
                // 获取启动文件夹的路径
                string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                // 快捷方式文件的完整路径
                string shortcutPath = Path.Combine(startupFolderPath, $"{appName}.lnk");

                if (checkBoxStartup.Checked)
                {
                    // 检查启动文件夹中是否已存在该快捷方式
                    if (File.Exists(shortcutPath))
                    {
                        Log("开机启动项已存在，无需重复创建", GetRichTextBoxAllLog());
                    }
                    else
                    {
                        // 创建快捷方式
                        CreateShortcut(appPath, shortcutPath, "-auto");
                        Log("已成功创建开机启动项，下次电脑启动时将自动运行程序", GetRichTextBoxAllLog());
                    }
                }
                else
                {
                    // 删除启动项
                    if (File.Exists(shortcutPath))
                    {
                        File.Delete(shortcutPath);
                        Log("已移除开机启动项，下次开机将不会自动启动", GetRichTextBoxAllLog());
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"设置开机启动项时出错: {ex.Message}", GetRichTextBoxAllLog(), true);
                MessageBox.Show($"设置开机启动项失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 恢复CheckBox状态，避免UI状态与实际状态不一致
                checkBoxStartup.CheckedChanged -= checkBoxStartup_CheckedChanged;
                checkBoxStartup.Checked = !checkBoxStartup.Checked;
                checkBoxStartup.CheckedChanged += checkBoxStartup_CheckedChanged;
            }
            checkBoxStartup.Enabled = true;
            await SaveConfig(false);
        }

        /// <summary>
        /// 创建指向指定路径应用程序的快捷方式
        /// </summary>
        /// <param name="targetPath">目标应用程序的完整路径</param>
        /// <param name="shortcutPath">要创建的快捷方式的完整路径</param>
        /// <param name="arguments">可选的启动参数</param>
        private void CreateShortcut(string targetPath, string shortcutPath, string arguments = "")
        {
            // 使用COM接口创建快捷方式
            Type t = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(t);
            var shortcut = shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = targetPath;
            if (!string.IsNullOrEmpty(arguments))
                shortcut.Arguments = arguments; // 设置启动参数

            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
            shortcut.WindowStyle = 7; // 最小化启动: 7, 正常启动: 1, 最大化启动: 3
            shortcut.Description = "SubsCheck Win GUI自启动快捷方式";
            shortcut.IconLocation = targetPath + ",0"; // 使用应用程序自身的图标

            // 保存快捷方式
            shortcut.Save();

            // 释放COM对象
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
        }

        /// <summary>
        /// 检查启动参数中是否包含指定的参数
        /// </summary>
        /// <param name="parameterName">要检查的参数名称，例如"-autoup"</param>
        /// <returns>如果存在指定参数，则返回true；否则返回false</returns>
        private bool CheckCommandLineParameter(string parameterName)
        {
            // 获取命令行参数数组
            string[] args = Environment.GetCommandLineArgs();

            // 遍历所有参数，检查是否有匹配的参数
            foreach (string arg in args)
            {
                // 不区分大小写比较
                if (string.Equals(arg, parameterName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void richTextBoxAllLog_DoubleClick(object sender, EventArgs e)
        {
            // 检查是否有日志内容
            if (richTextBoxAllLog.TextLength > 0)
            {
                // 显示确认对话框，询问用户是否要清空日志
                DialogResult result = MessageBox.Show(
                    "是否要清空当前日志？",
                    "清空日志确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2); // 默认选择"否"按钮

                if (result == DialogResult.Yes)
                {
                    // 清空richTextBox1内容
                    richTextBoxAllLog.Clear();
                    // 记录一条清空日志的操作信息
                    Log("日志已清空", GetRichTextBoxAllLog());
                }
            }
        }

        private void numericUpDownMinSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownMinSpeed.Value > 4096)
            {
                string warningMessage =
                    "⚠️ 测速下限设置提醒 ⚠️\n\n" +
                    "您设置的测速下限值过高，可能导致：\n\n" +
                    "• 可用节点数量显著减少\n" +
                    "• 部分低速但稳定的节点被过滤\n" +
                    "测速下限设置建议：\n" +
                    "• 日常浏览：512-1024 KB/s\n" +
                    "• 视频观看：1024-2048 KB/s\n" +
                    "• 大文件下载：根据实际需求设置\n";

                Log(warningMessage, GetRichTextBoxAllLog());
            }
        }

        private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownTimeout.Value < 5000)
            {
                string warningMessage =
                    "⚠️ 超时时间设置提醒 ⚠️\n\n" +
                    "该超时时间并非延迟时间，除非您的网络极其优秀，否则超时时间过低会导致无可用节点。\n\n" +
                    "• 超时时间是真连接测试的最大等待时间\n" +
                    "• 设置过低会导致大部分节点连接失败\n" +
                    "• 推荐设置不低于5000ms\n\n" +
                    "建议超时时间设置：\n" +
                    "• 普通网络环境：5000± ms\n" +
                    "• 极好网络环境：3000± ms\n";

                Log(warningMessage, GetRichTextBoxAllLog());
            }

        }

        // 获取 githubproxy 地址（带 30 分钟间隔限制）
        public async Task<string> GetGithubProxyUrlAsync()
        {
            const string AUTO = "自动选择";
            if (comboBoxGithubProxyUrl == null) return githubProxyURL;

            // 如果上次运行时间距今不足 1 分钟，直接返回上次结果（若有）
            if (_lastGithubProxyUrl != null && (DateTime.Now - _lastGetGithubProxyRunTime).TotalMinutes < 1)
            {
                Log($"GitHub Proxy：{_lastGithubProxyUrl}", GetRichTextBoxAllLog());
                return _lastGithubProxyUrl;
            }

            // 如已指定 githubproxy，直接返回结果
            var text = (comboBoxGithubProxyUrl.Text ?? "");
            if (text != AUTO && text.Length > 0)
            {
                _lastGithubProxyUrl = $"https://{text}/";
                _lastGetGithubProxyRunTime = DateTime.Now;
                return _lastGithubProxyUrl;
            }

            // 随机候选列表
            var candidates = comboBoxGithubProxyUrl.Items
                .OfType<string>()
                .Where(s => !string.IsNullOrWhiteSpace(s) && s != AUTO)
                .OrderBy(_ => Guid.NewGuid())
                .ToList();

            if (!candidates.Any())
            {
                _lastGithubProxyUrl = githubProxyURL;
                _lastGetGithubProxyRunTime = DateTime.Now;
                return _lastGithubProxyUrl;
            }

            try
            {
                var detected = await DetectGitHubProxyAsync(candidates);
                _lastGithubProxyUrl = string.IsNullOrWhiteSpace(detected) ? githubProxyURL : detected;
            }
            catch
            {
                _lastGithubProxyUrl = githubProxyURL;
            }

            // 更新最后运行时间
            _lastGetGithubProxyRunTime = DateTime.Now;
            return _lastGithubProxyUrl;
        }

        // 切换高并发内核和原版内核设置项
        private void SwitchHighConcurrentLayout(bool EnableHighConcurrent)
        {
            bool collapsed = buttonAdvanceSettings.Text == "高级设置∨";
            groupBoxPipeConcurrent.Visible = EnableHighConcurrent;
            groupBoxEnhance.Visible = EnableHighConcurrent;
            if (collapsed)
            {
                if (EnableHighConcurrent)
                {
                    if (!_originalLocationSaved)
                    {
                        _originalLocationSaved = true;
                        _pipeOriginalLocation = groupBoxPipeConcurrent.Location;
                        _enhanceOriginalLocation = groupBoxEnhance.Location;
                    }
                    groupBoxPipeConcurrent.Location = groupBoxAdvanceSettings.Location;
                    groupBoxEnhance.Location = new Point(groupBoxEnhance.Location.X, groupBoxAdvanceSettings.Location.Y);
                }
            }
            else
            {
                if (EnableHighConcurrent)
                {
                    groupBoxPipeConcurrent.Location = _pipeOriginalLocation;
                    groupBoxEnhance.Location = _enhanceOriginalLocation;
                    groupBoxGist.Location = new Point(groupBoxGist.Location.X, _pipeOriginalLocation.Y + groupBoxPipeConcurrent.Height);
                    groupBoxR2.Location = groupBoxGist.Location;
                    groupBoxWebdav.Location = groupBoxGist.Location;
                }
                else
                {
                    groupBoxGist.Location = _pipeOriginalLocation; groupBoxR2.Location = groupBoxGist.Location; groupBoxWebdav.Location = groupBoxGist.Location;
                }
            }
        }

        // 状态变更检查、统一获取 proxy、更新并保存
        private async void checkBoxHighConcurrent_CheckedChanged(object sender, EventArgs e)
        {
            bool EnableHighConcurrent = checkBoxHighConcurrent.Checked;


            // 先进行控件切换
            SwitchHighConcurrentLayout(EnableHighConcurrent);

            // 判断是否需要下载新内核
            string want = EnableHighConcurrent ? "高并发内核" : "原版内核";
            if (currentKernel != want)
            {
                if (EnableHighConcurrent && !checkBoxSwitchArch64.Checked)
                {
                    DialogResult result = MessageBox.Show(
                        $"建议使用现代的 x64 架构，以实现更高性能\n\n" +
                        "· 点击【确定】将使用 x64 内核\n\n" +
                        "· 点击【取消】将使用 i386 内核\n\n",
                        "内核架构选择",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        //临时禁用事件
                        checkBoxSwitchArch64.CheckedChanged -= checkBoxSwitchArch64_CheckedChanged;
                        checkBoxSwitchArch64.Checked = true;
                        // 恢复事件
                        checkBoxSwitchArch64.CheckedChanged += checkBoxSwitchArch64_CheckedChanged;
                    }
                    else
                    {
                        //临时禁用事件
                        checkBoxSwitchArch64.CheckedChanged -= checkBoxSwitchArch64_CheckedChanged;
                        checkBoxSwitchArch64.Checked = false;
                        // 恢复事件
                        checkBoxSwitchArch64.CheckedChanged += checkBoxSwitchArch64_CheckedChanged;
                    }
                    if (checkBoxHighConcurrent.Checked)
                    {
                        comboBoxSubscriptionType.Items.AddRange(new object[] { "Singbox1.11", "Singbox1.12" });
                    }
                }

                checkBoxSwitchArch64.Enabled = false;
                checkBoxHighConcurrent.Enabled = false;
                buttonCheckUpdate.Enabled = false;

                Log(EnableHighConcurrent ? "切换为 高并发 内核,可单独设置测活-测速-媒体检测各阶段并发数,大幅提高性能" : "切换为 原版 内核", GetRichTextBoxAllLog());
                await DownloadSubsCheckEXE();// 若要后台并行改为 _ = DownloadSubsCheckEXE();
                currentKernel = want;
                if (!EnableHighConcurrent)
                {
                    if (comboBoxSpeedtestUrl.Text == "random") comboBoxSpeedtestUrl.Text = "不测速";
                    comboBoxSpeedtestUrl.Items.Remove("random");
                    numericUpDownPipeAlive.Value = 0; numericUpDownPipeSpeed.Value = 0; numericUpDownPipeMedia.Value = 0;
                }
                else if (!comboBoxSpeedtestUrl.Items.Contains("random"))
                {
                    // 只有当列表里至少有1个元素时，才能插在第2个位置（索引1）  
                    // 否则只能插在第1个位置（索引0）  
                    int insertIndex = comboBoxSpeedtestUrl.Items.Count > 0 ? 1 : 0;

                    comboBoxSpeedtestUrl.Items.Insert(insertIndex, "random");
                }

                checkBoxSwitchArch64.Enabled = true;
                checkBoxHighConcurrent.Enabled = true;
                buttonCheckUpdate.Enabled = true;
            }
            Log(EnableHighConcurrent ? "已切换高并发内核，测活-测速-媒体检测 流水线式并发运行。" : "使用原版内核。", GetRichTextBoxAllLog());
        }

        // x64 按钮切换事件
        private async void checkBoxSwitchArch64_CheckedChanged(object sender, EventArgs e)
        {
            bool useX64 = checkBoxSwitchArch64.Checked;
            string want = useX64 ? "x86_64" : "i386";
            if (currentArch != want)
            {
                checkBoxSwitchArch64.Enabled = false;
                checkBoxHighConcurrent.Enabled = false;
                buttonCheckUpdate.Enabled = false;
                githubProxyURL = await GetGithubProxyUrlAsync();
                Log(useX64 ? "切换为 x64 内核,内存占用更高,但CPU占用可能较低" : "切换为 i386 内核,内存占用更低,但CPU占用可能更高", GetRichTextBoxAllLog());
                await DownloadSubsCheckEXE();
                currentArch = want;
                checkBoxSwitchArch64.Enabled = true;
                checkBoxHighConcurrent.Enabled = true;
                buttonCheckUpdate.Enabled = true;
            }
            Log(useX64 ? "使用64位内核,如内存占用较高,可在[高级设置]切换" : "使用32位内核,如CPU占用较高,可在[高级设置]切换", GetRichTextBoxAllLog());
        }

        // 计算一个推荐并发参数
        private (int alive, int speed, int media) CalcSimpleConcurrent()
        {
            int baseVal = (int)numericUpDownConcurrent.Value;
            int aliveConc = baseVal * 4;
            int speedConc = baseVal;
            int mediaConc = baseVal * 2;

            // alive 最小为 200
            aliveConc = Math.Max(aliveConc, 200);
            // media 最大为 100
            mediaConc = Math.Min(mediaConc, 100);

            // 处理总带宽限制与最小速度
            double totalBw = (double)numericUpDownTotalBandwidthLimit.Value; // 用户设定的总带宽（单位与逻辑由你定义）
            double minSpeed = (double)numericUpDownMinSpeed.Value; // 单个连接的最小速度

            if (totalBw <= 0)
            {
                // 无带宽限制时，给 speed 一个保守上限
                speedConc = Math.Min(speedConc, 32);
            }
            else if (minSpeed > 0)
            {
                // 估算在 minSpeed 情况下能支持的最大并发数（向下取整）
                int estimated = (int)Math.Floor(totalBw / minSpeed);
                estimated = Math.Max(1, estimated); // 至少为 1
                speedConc = Math.Max(speedConc, estimated); // 把 speedConc 限制到估算值
            }
            else
            {
                speedConc = Math.Min(speedConc, 32);
            }

            speedConc = Math.Max(1, speedConc);

            return (aliveConc, speedConc, mediaConc);
        }

        // 类成员：唯一的程序化修改旗标（防重入/防循环）
        private bool _inProgrammaticChange = false;

        // 安全写入 NumericUpDown（避免超出 Min/Max 导致异常）
        private void SetNumericUpDownValueSafe(NumericUpDown ctrl, int value)
        {
            if (ctrl == null) return;
            int min = (int)ctrl.Minimum;
            int max = (int)ctrl.Maximum;
            if (value < min) value = min;
            if (value > max) value = max;
            if ((decimal)value != ctrl.Value) // 只有值变化时才赋值，减少不必要触发
                ctrl.Value = (decimal)value;
        }

        // switchPipeAutoConcurrent：根据当前 numericUpDown 的值决定是否为自动模式
        private void switchPipeAutoConcurrent()
        {
            // 计算是否进入自动模式（任一为 0 则自动）
            bool anyZero = (int)numericUpDownPipeAlive.Value <= 0
                           || (int)numericUpDownPipeSpeed.Value <= 0
                           || (int)numericUpDownPipeMedia.Value <= 0;

            // 在程序化修改期间抑制事件响应
            _inProgrammaticChange = true;
            try
            {
                if (anyZero)
                {
                    // 进入自适应：禁用控件并把值统一为 0（只在必要时写入）
                    numericUpDownPipeAlive.Enabled = false;
                    numericUpDownPipeSpeed.Enabled = false;
                    numericUpDownPipeMedia.Enabled = false;

                    SetNumericUpDownValueSafe(numericUpDownPipeAlive, 0);
                    SetNumericUpDownValueSafe(numericUpDownPipeSpeed, 0);
                    SetNumericUpDownValueSafe(numericUpDownPipeMedia, 0);

                    if (!checkBoxPipeAuto.Checked) checkBoxPipeAuto.Checked = true; // 程序化设置
                }
                else
                {
                    // 退出自适应：启用控件，但不要覆盖用户已经设置的数值
                    numericUpDownPipeAlive.Enabled = true;
                    numericUpDownPipeSpeed.Enabled = true;
                    numericUpDownPipeMedia.Enabled = true;

                    if (checkBoxPipeAuto.Checked) checkBoxPipeAuto.Checked = false; // 程序化设置
                }
            }
            finally
            {
                _inProgrammaticChange = false;
            }
        }

        // checkBoxPipeAuto_CheckedChanged：用户点击或程序化修改都会走这里，使用 guard 来区分
        private void checkBoxPipeAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (_inProgrammaticChange) return; // 如果是程序化触发，直接忽略

            _inProgrammaticChange = true;
            try
            {
                if (checkBoxPipeAuto.Checked)
                {
                    // 切到自适应：禁用并清零
                    numericUpDownPipeAlive.Enabled = false;
                    numericUpDownPipeSpeed.Enabled = false;
                    numericUpDownPipeMedia.Enabled = false;

                    SetNumericUpDownValueSafe(numericUpDownPipeAlive, 0);
                    SetNumericUpDownValueSafe(numericUpDownPipeSpeed, 0);
                    SetNumericUpDownValueSafe(numericUpDownPipeMedia, 0);
                    Log("并发检测模式: 自适应分段流水线(内核自带衰减算法)", GetRichTextBoxAllLog());
                }
                else
                {
                    // 退出自适应：启用并使用推荐值（推荐值从函数获得）
                    numericUpDownPipeAlive.Enabled = true;
                    numericUpDownPipeSpeed.Enabled = true;
                    numericUpDownPipeMedia.Enabled = true;

                    var (alive, speed, media) = CalcSimpleConcurrent();

                    SetNumericUpDownValueSafe(numericUpDownPipeAlive, alive);
                    SetNumericUpDownValueSafe(numericUpDownPipeSpeed, speed);
                    SetNumericUpDownValueSafe(numericUpDownPipeMedia, media);
                    Log($"默认并发参数: 测活: {alive}, 测速: {speed}, 流媒体: {media} [根据并发数 {numericUpDownConcurrent.Value} 计算]", GetRichTextBoxAllLog());
                }
            }
            finally
            {
                _inProgrammaticChange = false;
            }
        }

        // ValueChanged 事件：只有用户交互时才触发 switchPipeAutoConcurrent（guard 防止程序化导致二次处理）
        private void numericUpDownPipeAlive_ValueChanged(object sender, EventArgs e)
        {
            if (_inProgrammaticChange) return;
            switchPipeAutoConcurrent();
            Log($"已设置流水线并发检测参数: Alive: {numericUpDownPipeAlive.Value}, Speed: {numericUpDownPipeSpeed.Value}, Media: {numericUpDownPipeMedia.Value}", GetRichTextBoxAllLog());
        }
        private void numericUpDownPipeSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (_inProgrammaticChange) return;
            switchPipeAutoConcurrent();
            Log($"已设置流水线并发检测参数: Alive: {numericUpDownPipeAlive.Value}, Speed: {numericUpDownPipeSpeed.Value}, Media: {numericUpDownPipeMedia.Value}", GetRichTextBoxAllLog());
        }
        private void numericUpDownPipeMedia_ValueChanged(object sender, EventArgs e)
        {
            if (_inProgrammaticChange) return;
            switchPipeAutoConcurrent();
            Log($"已设置流水线并发检测参数: Alive: {numericUpDownPipeAlive.Value}, Speed: {numericUpDownPipeSpeed.Value}, Media: {numericUpDownPipeMedia.Value}", GetRichTextBoxAllLog());
        }

        private void NumericUpDownTotalBandwidthLimit_ValueChanged(object sender, EventArgs e)
        {
            float calcBandWidth = (float)numericUpDownTotalBandwidthLimit.Value * 8;
            if (calcBandWidth > 0)
            {
                Log($"当前设置下载速度限制带宽 {calcBandWidth} 兆。", GetRichTextBoxAllLog());
                toolTip1.SetToolTip(numericUpDownTotalBandwidthLimit, $"总下载速度限制(MB/s)：\n建议设置为 <=带宽/8, \n比如你是 200 兆的宽带, 支持的最大下载速度 200/8 = 25 MB/s, 可以设置为 20。\n\n当前设置下载速度对应带宽 {calcBandWidth}");
            }
        }
        public static Color HexToRgbColor(String hexColour)
        {
            Color colour = new Color();
            try
            {
                colour = ColorTranslator.FromHtml(hexColour);
            }
            catch (System.Exception)
            {
                return Color.Empty;
            }
            return colour;
        }
    }
}