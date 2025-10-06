using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subs_check.win.gui
{
    public partial class MoreYAML : Form
    {
        string 初始化;
        public MoreYAML()
        {
            InitializeComponent();

            
            // 移除文本框的等待光标
            textBox1.UseWaitCursor = false;
            初始化 = textBox1.Text;

            // 设置 groupBox1 的 Anchor 属性，使其跟随窗体四边缩放
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 设置文本框跟随 groupBox1 缩放
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 设置 linkLabel1 固定在右上角
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 设置按钮固定在底部
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            
            // 检查并加载配置文件
            LoadMoreYamlConfig();
        }

        /// <summary>
        /// 检查并加载 more.yaml 配置文件
        /// </summary>
        private void LoadMoreYamlConfig()
        {
            try
            {
                // 获取当前程序的执行路径
                string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                // 指定config目录路径
                string configFolderPath = System.IO.Path.Combine(executablePath, "config");
                // 指定more.yaml文件的完整路径
                string moreYamlFilePath = System.IO.Path.Combine(configFolderPath, "more.yaml");

                // 检查文件是否存在
                if (System.IO.File.Exists(moreYamlFilePath))
                {
                    // 读取文件内容并赋值给textBox1
                    string yamlContent = System.IO.File.ReadAllText(moreYamlFilePath, Encoding.UTF8);
                    textBox1.Text = yamlContent;
                }
            }
            catch (Exception ex)
            {
                // 读取文件时出错，可以选择是否显示错误信息
                MessageBox.Show($"读取配置文件时发生错误：{ex.Message}", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/sinspired/subs-check/blob/master/config/config.example.yaml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证textbox1的内容是否符合YAML格式
                if (!IsValidYaml(textBox1.Text))
                {
                    MessageBox.Show("输入的内容不符合YAML格式，请检查后重试！", "格式错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 获取当前程序的执行路径
                string executablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                // 指定config目录路径
                string configFolderPath = System.IO.Path.Combine(executablePath, "config");
                // 指定more.yaml文件的完整路径
                string moreYamlFilePath = System.IO.Path.Combine(configFolderPath, "more.yaml");

                // 确保config目录存在
                if (!System.IO.Directory.Exists(configFolderPath))
                {
                    System.IO.Directory.CreateDirectory(configFolderPath);
                }

                // 将textBox1的内容写入more.yaml文件
                System.IO.File.WriteAllText(moreYamlFilePath, textBox1.Text, Encoding.UTF8);

                // 设置对话框结果为OK并关闭窗口
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置文件时发生错误：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 验证字符串是否为有效的YAML格式
        /// </summary>
        /// <param name="yamlContent">要验证的YAML字符串</param>
        /// <returns>如果是有效的YAML格式，返回true；否则返回false</returns>
        private bool IsValidYaml(string yamlContent)
        {
            if (string.IsNullOrWhiteSpace(yamlContent))
                return true; // 空内容被视为有效（可以根据需要更改）

            try
            {
                // 使用YamlDotNet尝试解析YAML内容
                var deserializer = new YamlDotNet.Serialization.Deserializer();
                deserializer.Deserialize<object>(yamlContent);
                return true;
            }
            catch (YamlDotNet.Core.YamlException)
            {
                // 捕获YAML解析异常，表示格式无效
                return false;
            }
            catch (Exception)
            {
                // 其他异常也视为格式无效
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = 初始化;
        }
    }
}
