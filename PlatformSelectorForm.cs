using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace subs_check.win.gui
{
    public partial class PlatformSelectorForm : Form
    {
        // 公开属性获取结果
        public List<string> SelectedPlatforms { get; private set; } = new List<string>();

        // 保存 CheckBox 的引用，避免在 Click 事件中遍历 UI 树
        private List<CheckBox> _checkBoxList = new List<CheckBox>();

        private readonly string[] _platforms =
        {
            "iprisk", "openai", "gemini", "youtube",
            "tiktok", "netflix", "disney", "x"
        };

        public PlatformSelectorForm(List<string> preSelected)
        {
            InitializeComponent();
            InitializeCustomComponents(preSelected);
        }

        private void InitializeCustomComponents(List<string> preSelected)
        {
            // 1. 窗体基础设置
            this.Text = "媒体解锁检测";
            this.Font = new Font("宋体", 9F); 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BackColor = Color.WhiteSmoke; 

            // 2. 主布局：两列 (左侧列表 | 右侧按钮)
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0) // 移除外边距，由内部控件控制
            };

            // 左列自适应，右列固定宽度
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));

            // ==================== 左侧：复选框列表区域 ====================
            var listPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                Padding = new Padding(20, 20, 20, 20),
                Dock = DockStyle.Fill
            };

            _checkBoxList.Clear();
            foreach (var platform in _platforms)
            {
                var cb = new CheckBox
                {
                    Text = GetFriendlyName(platform),
                    // 虽然窗体设置了宋体，但为了可读性，建议内容控件稍微大一点
                    Font = new Font("宋体", 9F),
                    AutoSize = true,
                    Margin = new Padding(5, 8, 5, 8), // 增加垂直间距
                    Cursor = Cursors.Hand,
                    Checked = preSelected != null && preSelected.Contains(platform),
                    Tag = platform
                };

                _checkBoxList.Add(cb);
                listPanel.Controls.Add(cb);
            }

            // ==================== 右侧：按钮区域 ====================
            var actionPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(15, 25, 15, 20), // 上边距加大，与列表对齐
                BackColor = Color.FromArgb(245, 246, 247) // 浅灰背景，区分功能区
            };

            var btnOk = CreateStyledButton("确定", DialogResult.OK, Color.FromArgb(0, 120, 215), Color.White);
            btnOk.Click += BtnOk_Click;

            var btnCancel = CreateStyledButton("取消", DialogResult.Cancel, Color.White, Color.Black);
            // 取消按钮加个边框色
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btnCancel.FlatAppearance.BorderSize = 1;

            actionPanel.Controls.Add(btnOk);
            actionPanel.Controls.Add(btnCancel);

            // ==================== 组装 ====================
            mainLayout.Controls.Add(listPanel, 0, 0);
            mainLayout.Controls.Add(actionPanel, 1, 0);

            this.Controls.Add(mainLayout);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        /// <summary>
        /// 创建统一风格的按钮
        /// </summary>
        private Button CreateStyledButton(string text, DialogResult result, Color bg, Color fg)
        {
            var btn = new Button
            {
                Text = text,
                DialogResult = result,
                Size = new Size(100, 36),
                Font = new Font("宋体", 10F), // 按钮也使用宋体
                FlatStyle = FlatStyle.Flat,
                BackColor = bg,
                ForeColor = fg,
                Margin = new Padding(0, 0, 0, 15), // 按钮之间的间距
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            // 简单的悬停效果逻辑
            if (bg != Color.White)
            {
                // 深色按钮变亮
                btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(bg);
            }
            else
            {
                // 浅色按钮变灰
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
            }

            return btn;
        }

        private string GetFriendlyName(string key)
        {
            switch (key)
            {
                case "x": return "X(Twitter)";
                case "iprisk": return "IPRisk(风控)";
                case "openai": return "OpenAI/ChatGPT";
                case "gemini": return "Gemini";
                case "youtube": return "YouTube";
                case "tiktok": return "TikTok";
                case "netflix": return "Netflix";
                case "disney": return "Disney+";
                default: return char.ToUpper(key[0]) + key.Substring(1);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // 优化：直接遍历已保存的 List<CheckBox>，无需在控件树中查找
            SelectedPlatforms = _checkBoxList
                .Where(cb => cb.Checked)
                .Select(cb => cb.Tag.ToString())
                .ToList();
        }
    }
}