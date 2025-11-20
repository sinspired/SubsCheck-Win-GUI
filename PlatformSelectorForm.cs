using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace subs_check.win.gui
{
    public partial class PlatformSelectorForm : Form
    {
        public List<string> SelectedPlatforms { get; private set; } = new List<string>();

        public PlatformSelectorForm(List<string> preSelected)
        {
            InitializeComponent();

            string[] platforms = { "iprisk", "openai", "gemini", "youtube", "tiktok", "netflix", "disney", "x" };

            int y = 20;
            foreach (var p in platforms)
            {
                var cb = new CheckBox
                {
                    Text = p,
                    Left = 20,
                    Top = y,
                    AutoSize = true,
                    Checked = preSelected != null && preSelected.Contains(p)
                };
                this.Controls.Add(cb);
                y += 30;
            }

            var btnOk = new Button
            {
                Text = "确定",
                Left = 20,
                Top = y,
                Width = 80
            };
            btnOk.Click += BtnOk_Click;
            this.Controls.Add(btnOk);

            this.Text = "选择平台";
            this.Width = 250;
            this.Height = y + 80;
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            SelectedPlatforms.Clear();
            foreach (var control in this.Controls)
            {
                if (control is CheckBox cb && cb.Checked)
                {
                    SelectedPlatforms.Add(cb.Text);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
