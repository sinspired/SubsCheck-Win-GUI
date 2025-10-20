using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace subs_check.win.gui
{
    public partial class about : Form
    {
        // 添加两个公共属性来接收版本号
        public string GuiVersion { set { label2.Text = value; } }
        public string CoreVersion { set { label3.Text = value; } }

        public about()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/beck-8/subs-check");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://t.me/CMLiussss");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://url.cmliussss.com/SCWinGUI");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/sinspired/SubsCheck-Win-GUI");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/sinspired/subs-check");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/bestruirui/BestSub");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/sub-store-org/Sub-Store");
        }
    }
}
