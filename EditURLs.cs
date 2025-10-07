using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subs_check.win.gui
{
    public partial class EditURLs : Form
    {
        // 属性
        public string UrlContent { get; set; }
        public System.Windows.Forms.ComboBox.ObjectCollection githubProxys { get; set; }
        public string githubProxy { get; set; }

        private string githubProxyURL;
        private string SubsCheckURLs;
        private string UrlContentOriginal;
        public Action<string> LogAction { get; set; }

        // 默认 URL 列表
        private static readonly string[] DefaultUrls =
        {
            "https://raw.githubusercontent.com/snakem982/proxypool/main/source/clash-meta.yaml",
            "https://raw.githubusercontent.com/snakem982/proxypool/main/source/clash-meta-2.yaml",
            "https://raw.githubusercontent.com/go4sharing/sub/main/sub.yaml",
            "https://raw.githubusercontent.com/SoliSpirit/v2ray-configs/main/all_configs.txt"
        };

        private string GetDefaultUrlContent()
        {
            return string.Join(Environment.NewLine, DefaultUrls);
        }

        public EditURLs()
        {
            InitializeComponent();

            // 设置控件锚点
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button4.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            button5.Anchor = AnchorStyles.Right | AnchorStyles.Bottom; // 新增按钮：恢复默认值

            if (!string.IsNullOrEmpty(UrlContent))
            {
                UrlContentOriginal = UrlContent;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 优先显示传入内容，否则显示默认值
            if (!string.IsNullOrEmpty(UrlContent))
            {
                textBox1.Text = UrlContent;
            }
            else
            {
                textBox1.Text = GetDefaultUrlContent();
            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = !string.IsNullOrEmpty(UrlContentOriginal)
                    ? UrlContentOriginal
                    : GetDefaultUrlContent();

                textBox1.SelectionStart = textBox1.Text.Length;

                // 调用传入的日志方法
                LogAction?.Invoke("订阅池为空，恢复默认订阅池。");
            }

            UrlContent = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            string[] lines = textBox1.Text.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            string[] uniqueLines = lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            textBox1.Text = string.Join(Environment.NewLine, uniqueLines) + Environment.NewLine;

            int removed = lines.Length - uniqueLines.Length;
            if (removed > 0)
            {
                MessageBox.Show($"已移除 {removed} 个重复行。", "去重完成",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("没有发现重复行。", "去重完成",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (comboBox1.Text == "自动选择")
            {
                List<string> proxyItems = new List<string>();
                for (int j = 0; j < comboBox1.Items.Count; j++)
                {
                    string proxyItem = comboBox1.Items[j].ToString();
                    if (proxyItem != "自动选择")
                        proxyItems.Add(proxyItem);
                }

                Random random = new Random();
                proxyItems = proxyItems.OrderBy(x => random.Next()).ToList();

                githubProxyURL = await DetectGitHubProxyAsync(proxyItems);
            }
            else
            {
                githubProxyURL = $"https://{comboBox1.Text}/";
            }

            string SubsCheckURLsURL = $"{githubProxyURL}https://raw.githubusercontent.com/cmliu/cmliu/main/SubsCheck-URLs";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                    HttpResponseMessage response = await client.GetAsync(SubsCheckURLsURL);
                    if (response.IsSuccessStatusCode)
                    {
                        button2.Text = "在线获取";
                        button2.Enabled = true;
                        SubsCheckURLs = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        button2.Text = "获取失败";
                        button2.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                button2.Text = "获取失败";
                button2.Enabled = false;
            }
        }

        private async Task<string> DetectGitHubProxyAsync(List<string> proxyItems)
        {
            string detectedProxyURL = "";

            foreach (string proxyItem in proxyItems)
            {
                string checkUrl = $"https://{proxyItem}/https://raw.githubusercontent.com/cmliu/SubsCheck-Win-GUI/master/packages.config";

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.Timeout = TimeSpan.FromSeconds(5);
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win32; x86) AppleWebKit/537.36 (KHTML, like Gecko) cmliu/SubsCheck-Win-GUI");

                        HttpResponseMessage response = await client.GetAsync(checkUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            detectedProxyURL = $"https://{proxyItem}/";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return detectedProxyURL;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SubsCheckURLs))
            {
                MessageBox.Show("未能获取在线内容，请重试。", "获取失败",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                "请选择如何处理获取到的内容：\n\n" +
                "- 点击【是】将覆盖当前内容\n" +
                "- 点击【否】将追加到当前内容后面\n" +
                "- 点击【取消】不做任何操作",
                "操作选择",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string normalizedText = NormalizeLineEndings(SubsCheckURLs);
                textBox1.Text = normalizedText;
                MessageBox.Show("已用在线内容覆盖原有内容。", "感谢大自然的馈赠",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (result == DialogResult.No)
            {
                if (!textBox1.Text.EndsWith("\r\n") && !textBox1.Text.EndsWith("\n"))
                {
                    textBox1.Text += Environment.NewLine;
                }

                textBox1.Text += NormalizeLineEndings(SubsCheckURLs);
                MessageBox.Show("已将在线内容追加到原有内容后。", "感谢大自然的馈赠",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string NormalizeLineEndings(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            text = text.Replace("\n", Environment.NewLine);

            return text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(textBox1.Text))
            //{
            //    textBox1.Text = !string.IsNullOrEmpty(UrlContentOriginal)
            //        ? UrlContentOriginal
            //        : GetDefaultUrlContent();

            //    textBox1.SelectionStart = textBox1.Text.Length;
            //}
        }

        // 恢复默认值按钮事件

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = GetDefaultUrlContent();

            MessageBox.Show("已恢复为默认订阅地址。", "操作完成",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
