namespace subs_check.win.gui
{
    partial class MainGui
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGui));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerinitial = new System.Windows.Forms.Timer(this.components);
            this.groupBoxComonSettings = new System.Windows.Forms.GroupBox();
            this.numericUpDownMinSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownConcurrent = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSaveMethod = new System.Windows.Forms.ComboBox();
            this.checkBoxSwitchArch64 = new System.Windows.Forms.CheckBox();
            this.textBoxCron = new System.Windows.Forms.TextBox();
            this.labelConcurrent = new System.Windows.Forms.Label();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.labelMinSpped = new System.Windows.Forms.Label();
            this.labelSaveMethod = new System.Windows.Forms.Label();
            this.checkBoxHighConcurrent = new System.Windows.Forms.CheckBox();
            this.comboBoxSubscriptionType = new System.Windows.Forms.ComboBox();
            this.buttonCopySubscriptionUrl = new System.Windows.Forms.Button();
            this.buttonTriggerCheck = new System.Windows.Forms.Button();
            this.buttonWebUi = new System.Windows.Forms.Button();
            this.checkBoxStartup = new System.Windows.Forms.CheckBox();
            this.labelCron = new System.Windows.Forms.Label();
            this.labelSubUrls = new System.Windows.Forms.Label();
            this.textBoxSubsUrls = new System.Windows.Forms.TextBox();
            this.buttonStartCheck = new System.Windows.Forms.Button();
            this.buttonAdvanceSettings = new System.Windows.Forms.Button();
            this.numericUpDownWebUIPort = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDLTimehot = new System.Windows.Forms.NumericUpDown();
            this.labelWebUIPort = new System.Windows.Forms.Label();
            this.labelDownloadTimeout = new System.Windows.Forms.Label();
            this.numericUpDownSubStorePort = new System.Windows.Forms.NumericUpDown();
            this.labelSubstorePort = new System.Windows.Forms.Label();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.buttonUpdateKernel = new System.Windows.Forms.Button();
            this.labelLogNodeInfo = new System.Windows.Forms.Label();
            this.richTextBoxAllLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxAdvanceSettings = new System.Windows.Forms.GroupBox();
            this.comboBoxOverwriteUrls = new System.Windows.Forms.ComboBox();
            this.textBoxSubStorePath = new System.Windows.Forms.TextBox();
            this.labelSubstoreParh = new System.Windows.Forms.Label();
            this.numericUpDownTotalBandwidthLimit = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSuccessLimit = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDownloadMb = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSysProxy = new System.Windows.Forms.ComboBox();
            this.comboBoxSpeedtestUrl = new System.Windows.Forms.ComboBox();
            this.comboBoxGithubProxyUrl = new System.Windows.Forms.ComboBox();
            this.labelOverwriteUrls = new System.Windows.Forms.Label();
            this.labelDownloadMb = new System.Windows.Forms.Label();
            this.textBoxWebUiAPIKey = new System.Windows.Forms.TextBox();
            this.checkBoxEnableWebUI = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableRenameNode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxEnableMediaCheck = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepSucced = new System.Windows.Forms.CheckBox();
            this.checkBoxTotalBandwidthLimit = new System.Windows.Forms.CheckBox();
            this.buttonCheckUpdate = new System.Windows.Forms.Button();
            this.buttonMoreSettings = new System.Windows.Forms.Button();
            this.labelGithubProxyUrl = new System.Windows.Forms.Label();
            this.labelSpeedtestUrl = new System.Windows.Forms.Label();
            this.checkBoxEnableSuccessLimit = new System.Windows.Forms.CheckBox();
            this.progressBarAll = new System.Windows.Forms.ProgressBar();
            this.timerCopySubscriptionUrl = new System.Windows.Forms.Timer(this.components);
            this.groupBoxGist = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxR2 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBoxWebdav = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.timerRestartSchedule = new System.Windows.Forms.Timer(this.components);
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBoxPipeConcurrent = new System.Windows.Forms.GroupBox();
            this.numericUpDownPipeMedia = new System.Windows.Forms.NumericUpDown();
            this.labelPipeMedia = new System.Windows.Forms.Label();
            this.numericUpDownPipeSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelPipeSpeed = new System.Windows.Forms.Label();
            this.checkBoxPipeAuto = new System.Windows.Forms.CheckBox();
            this.numericUpDownPipeAlive = new System.Windows.Forms.NumericUpDown();
            this.labelPipeAlive = new System.Windows.Forms.Label();
            this.groupBoxEnhance = new System.Windows.Forms.GroupBox();
            this.checkBoxDropBadCFNodes = new System.Windows.Forms.CheckBox();
            this.checkBoxEhanceTag = new System.Windows.Forms.CheckBox();
            this.checkBoxSubsStats = new System.Windows.Forms.CheckBox();
            this.groupBoxComonSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConcurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWebUIPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDLTimehot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubStorePort)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            this.groupBoxAdvanceSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalBandwidthLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuccessLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownloadMb)).BeginInit();
            this.groupBoxGist.SuspendLayout();
            this.groupBoxR2.SuspendLayout();
            this.groupBoxWebdav.SuspendLayout();
            this.groupBoxPipeConcurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeAlive)).BeginInit();
            this.groupBoxEnhance.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Subs-Check";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Subs-Check：未运行";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // timerinitial
            // 
            this.timerinitial.Enabled = true;
            this.timerinitial.Interval = 1;
            this.timerinitial.Tick += new System.EventHandler(this.timerinitial_Tick);
            // 
            // groupBoxComonSettings
            // 
            this.groupBoxComonSettings.Controls.Add(this.numericUpDownMinSpeed);
            this.groupBoxComonSettings.Controls.Add(this.numericUpDownTimeout);
            this.groupBoxComonSettings.Controls.Add(this.numericUpDownInterval);
            this.groupBoxComonSettings.Controls.Add(this.numericUpDownConcurrent);
            this.groupBoxComonSettings.Controls.Add(this.comboBoxSaveMethod);
            this.groupBoxComonSettings.Controls.Add(this.checkBoxSwitchArch64);
            this.groupBoxComonSettings.Controls.Add(this.textBoxCron);
            this.groupBoxComonSettings.Controls.Add(this.labelConcurrent);
            this.groupBoxComonSettings.Controls.Add(this.labelInterval);
            this.groupBoxComonSettings.Controls.Add(this.labelTimeout);
            this.groupBoxComonSettings.Controls.Add(this.labelMinSpped);
            this.groupBoxComonSettings.Controls.Add(this.labelSaveMethod);
            this.groupBoxComonSettings.Controls.Add(this.checkBoxHighConcurrent);
            this.groupBoxComonSettings.Controls.Add(this.comboBoxSubscriptionType);
            this.groupBoxComonSettings.Controls.Add(this.buttonCopySubscriptionUrl);
            this.groupBoxComonSettings.Controls.Add(this.buttonTriggerCheck);
            this.groupBoxComonSettings.Controls.Add(this.buttonWebUi);
            this.groupBoxComonSettings.Controls.Add(this.checkBoxStartup);
            this.groupBoxComonSettings.Controls.Add(this.labelCron);
            this.groupBoxComonSettings.Controls.Add(this.labelSubUrls);
            this.groupBoxComonSettings.Controls.Add(this.textBoxSubsUrls);
            this.groupBoxComonSettings.Controls.Add(this.buttonStartCheck);
            this.groupBoxComonSettings.Controls.Add(this.buttonAdvanceSettings);
            this.groupBoxComonSettings.Location = new System.Drawing.Point(26, 14);
            this.groupBoxComonSettings.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxComonSettings.Name = "groupBoxComonSettings";
            this.groupBoxComonSettings.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxComonSettings.Size = new System.Drawing.Size(319, 768);
            this.groupBoxComonSettings.TabIndex = 0;
            this.groupBoxComonSettings.TabStop = false;
            this.groupBoxComonSettings.Text = "参数设置";
            // 
            // numericUpDownMinSpeed
            // 
            this.numericUpDownMinSpeed.Location = new System.Drawing.Point(197, 180);
            this.numericUpDownMinSpeed.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownMinSpeed.Maximum = new decimal(new int[] {
            20480,
            0,
            0,
            0});
            this.numericUpDownMinSpeed.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownMinSpeed.Name = "numericUpDownMinSpeed";
            this.numericUpDownMinSpeed.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownMinSpeed.TabIndex = 13;
            this.numericUpDownMinSpeed.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownMinSpeed.ValueChanged += new System.EventHandler(this.numericUpDownMinSpeed_ValueChanged);
            // 
            // numericUpDownTimeout
            // 
            this.numericUpDownTimeout.Location = new System.Drawing.Point(197, 131);
            this.numericUpDownTimeout.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTimeout.Name = "numericUpDownTimeout";
            this.numericUpDownTimeout.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownTimeout.TabIndex = 12;
            this.numericUpDownTimeout.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.numericUpDownTimeout.ValueChanged += new System.EventHandler(this.numericUpDownTimeout_ValueChanged);
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(197, 82);
            this.numericUpDownInterval.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownInterval.TabIndex = 11;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numericUpDownInterval.DoubleClick += new System.EventHandler(this.切换cron表达式);
            // 
            // numericUpDownConcurrent
            // 
            this.numericUpDownConcurrent.Location = new System.Drawing.Point(197, 33);
            this.numericUpDownConcurrent.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownConcurrent.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownConcurrent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownConcurrent.Name = "numericUpDownConcurrent";
            this.numericUpDownConcurrent.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownConcurrent.TabIndex = 10;
            this.numericUpDownConcurrent.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownConcurrent.ValueChanged += new System.EventHandler(this.numericUpDownConcurrent_ValueChanged);
            // 
            // comboBoxSaveMethod
            // 
            this.comboBoxSaveMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSaveMethod.FormattingEnabled = true;
            this.comboBoxSaveMethod.Items.AddRange(new object[] {
            "本地",
            "gist",
            "r2",
            "webdav"});
            this.comboBoxSaveMethod.Location = new System.Drawing.Point(197, 230);
            this.comboBoxSaveMethod.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxSaveMethod.Name = "comboBoxSaveMethod";
            this.comboBoxSaveMethod.Size = new System.Drawing.Size(106, 29);
            this.comboBoxSaveMethod.TabIndex = 16;
            this.comboBoxSaveMethod.TextChanged += new System.EventHandler(this.comboBoxSaveMethod_TextChanged);
            // 
            // checkBoxSwitchArch64
            // 
            this.checkBoxSwitchArch64.AutoSize = true;
            this.checkBoxSwitchArch64.Location = new System.Drawing.Point(170, 539);
            this.checkBoxSwitchArch64.Name = "checkBoxSwitchArch64";
            this.checkBoxSwitchArch64.Size = new System.Drawing.Size(111, 25);
            this.checkBoxSwitchArch64.TabIndex = 38;
            this.checkBoxSwitchArch64.Text = "x64内核";
            this.checkBoxSwitchArch64.UseVisualStyleBackColor = true;
            this.checkBoxSwitchArch64.CheckedChanged += new System.EventHandler(this.checkBoxSwitchArch64_CheckedChanged);
            // 
            // textBoxCron
            // 
            this.textBoxCron.AcceptsReturn = true;
            this.textBoxCron.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxCron.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.textBoxCron.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCron.Location = new System.Drawing.Point(89, 496);
            this.textBoxCron.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxCron.Name = "textBoxCron";
            this.textBoxCron.Size = new System.Drawing.Size(214, 31);
            this.textBoxCron.TabIndex = 21;
            this.textBoxCron.Text = "0 4,16 * * *";
            this.textBoxCron.Visible = false;
            this.textBoxCron.DoubleClick += new System.EventHandler(this.切换cron表达式);
            this.textBoxCron.Leave += new System.EventHandler(this.textBoxCron_Leave);
            // 
            // labelConcurrent
            // 
            this.labelConcurrent.AutoSize = true;
            this.labelConcurrent.Location = new System.Drawing.Point(13, 38);
            this.labelConcurrent.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelConcurrent.Name = "labelConcurrent";
            this.labelConcurrent.Size = new System.Drawing.Size(136, 21);
            this.labelConcurrent.TabIndex = 2;
            this.labelConcurrent.Text = "并发线程数：";
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(13, 87);
            this.labelInterval.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(179, 21);
            this.labelInterval.TabIndex = 3;
            this.labelInterval.Text = "检查间隔(分钟)：";
            this.labelInterval.DoubleClick += new System.EventHandler(this.切换cron表达式);
            // 
            // labelTimeout
            // 
            this.labelTimeout.AutoSize = true;
            this.labelTimeout.Location = new System.Drawing.Point(13, 136);
            this.labelTimeout.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(179, 21);
            this.labelTimeout.TabIndex = 4;
            this.labelTimeout.Text = "超时时间(毫秒)：";
            // 
            // labelMinSpped
            // 
            this.labelMinSpped.AutoSize = true;
            this.labelMinSpped.Location = new System.Drawing.Point(13, 185);
            this.labelMinSpped.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelMinSpped.Name = "labelMinSpped";
            this.labelMinSpped.Size = new System.Drawing.Size(181, 21);
            this.labelMinSpped.TabIndex = 5;
            this.labelMinSpped.Text = "测速下限(KB/s)：";
            // 
            // labelSaveMethod
            // 
            this.labelSaveMethod.AutoSize = true;
            this.labelSaveMethod.Location = new System.Drawing.Point(13, 234);
            this.labelSaveMethod.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSaveMethod.Name = "labelSaveMethod";
            this.labelSaveMethod.Size = new System.Drawing.Size(115, 21);
            this.labelSaveMethod.TabIndex = 8;
            this.labelSaveMethod.Text = "保存方法：";
            // 
            // checkBoxHighConcurrent
            // 
            this.checkBoxHighConcurrent.AutoSize = true;
            this.checkBoxHighConcurrent.Location = new System.Drawing.Point(18, 539);
            this.checkBoxHighConcurrent.Name = "checkBoxHighConcurrent";
            this.checkBoxHighConcurrent.Size = new System.Drawing.Size(141, 25);
            this.checkBoxHighConcurrent.TabIndex = 39;
            this.checkBoxHighConcurrent.Text = "高并发模式";
            this.checkBoxHighConcurrent.UseVisualStyleBackColor = true;
            this.checkBoxHighConcurrent.CheckedChanged += new System.EventHandler(this.checkBoxHighConcurrent_CheckedChanged);
            // 
            // comboBoxSubscriptionType
            // 
            this.comboBoxSubscriptionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSubscriptionType.FormattingEnabled = true;
            this.comboBoxSubscriptionType.Items.AddRange(new object[] {
            "通用订阅",
            "Clash"});
            this.comboBoxSubscriptionType.Location = new System.Drawing.Point(15, 578);
            this.comboBoxSubscriptionType.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxSubscriptionType.Name = "comboBoxSubscriptionType";
            this.comboBoxSubscriptionType.Size = new System.Drawing.Size(144, 29);
            this.comboBoxSubscriptionType.TabIndex = 19;
            // 
            // buttonCopySubscriptionUrl
            // 
            this.buttonCopySubscriptionUrl.Enabled = false;
            this.buttonCopySubscriptionUrl.Location = new System.Drawing.Point(165, 575);
            this.buttonCopySubscriptionUrl.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCopySubscriptionUrl.Name = "buttonCopySubscriptionUrl";
            this.buttonCopySubscriptionUrl.Size = new System.Drawing.Size(138, 40);
            this.buttonCopySubscriptionUrl.TabIndex = 18;
            this.buttonCopySubscriptionUrl.Text = "复制订阅";
            this.buttonCopySubscriptionUrl.UseVisualStyleBackColor = true;
            this.buttonCopySubscriptionUrl.Click += new System.EventHandler(this.buttonCopySubscriptionUrl_Click);
            // 
            // buttonTriggerCheck
            // 
            this.buttonTriggerCheck.Enabled = false;
            this.buttonTriggerCheck.Location = new System.Drawing.Point(13, 625);
            this.buttonTriggerCheck.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonTriggerCheck.Name = "buttonTriggerCheck";
            this.buttonTriggerCheck.Size = new System.Drawing.Size(146, 40);
            this.buttonTriggerCheck.TabIndex = 30;
            this.buttonTriggerCheck.Text = "🔀未启动";
            this.buttonTriggerCheck.UseVisualStyleBackColor = true;
            this.buttonTriggerCheck.Click += new System.EventHandler(this.buttonTriggerCheck_Click);
            // 
            // buttonWebUi
            // 
            this.buttonWebUi.Enabled = false;
            this.buttonWebUi.Location = new System.Drawing.Point(165, 625);
            this.buttonWebUi.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonWebUi.Name = "buttonWebUi";
            this.buttonWebUi.Size = new System.Drawing.Size(138, 40);
            this.buttonWebUi.TabIndex = 29;
            this.buttonWebUi.Text = "访问WebUI";
            this.buttonWebUi.UseVisualStyleBackColor = true;
            this.buttonWebUi.Click += new System.EventHandler(this.buttonWebUi_Click);
            // 
            // checkBoxStartup
            // 
            this.checkBoxStartup.AutoSize = true;
            this.checkBoxStartup.Location = new System.Drawing.Point(170, 676);
            this.checkBoxStartup.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxStartup.Name = "checkBoxStartup";
            this.checkBoxStartup.Size = new System.Drawing.Size(120, 25);
            this.checkBoxStartup.TabIndex = 30;
            this.checkBoxStartup.Text = "开机自启";
            this.checkBoxStartup.UseVisualStyleBackColor = true;
            this.checkBoxStartup.CheckedChanged += new System.EventHandler(this.checkBoxStartup_CheckedChanged);
            // 
            // labelCron
            // 
            this.labelCron.AutoSize = true;
            this.labelCron.Location = new System.Drawing.Point(13, 501);
            this.labelCron.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelCron.Name = "labelCron";
            this.labelCron.Size = new System.Drawing.Size(73, 21);
            this.labelCron.TabIndex = 40;
            this.labelCron.Text = "计划：";
            this.labelCron.Visible = false;
            this.labelCron.DoubleClick += new System.EventHandler(this.切换cron表达式);
            // 
            // labelSubUrls
            // 
            this.labelSubUrls.AutoSize = true;
            this.labelSubUrls.Location = new System.Drawing.Point(13, 278);
            this.labelSubUrls.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSubUrls.Name = "labelSubUrls";
            this.labelSubUrls.Size = new System.Drawing.Size(284, 21);
            this.labelSubUrls.TabIndex = 9;
            this.labelSubUrls.Text = "节点池订阅链接(点击编辑)：";
            this.labelSubUrls.Click += new System.EventHandler(this.textBoxSubsUrls_DoubleClick);
            // 
            // textBoxSubsUrls
            // 
            this.textBoxSubsUrls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSubsUrls.Location = new System.Drawing.Point(17, 304);
            this.textBoxSubsUrls.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxSubsUrls.Multiline = true;
            this.textBoxSubsUrls.Name = "textBoxSubsUrls";
            this.textBoxSubsUrls.ReadOnly = true;
            this.textBoxSubsUrls.Size = new System.Drawing.Size(286, 223);
            this.textBoxSubsUrls.TabIndex = 17;
            this.textBoxSubsUrls.Text = resources.GetString("textBoxSubsUrls.Text");
            this.textBoxSubsUrls.WordWrap = false;
            this.textBoxSubsUrls.Click += new System.EventHandler(this.textBoxSubsUrls_DoubleClick);
            this.textBoxSubsUrls.DoubleClick += new System.EventHandler(this.textBoxSubsUrls_DoubleClick);
            // 
            // buttonStartCheck
            // 
            this.buttonStartCheck.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStartCheck.Location = new System.Drawing.Point(13, 676);
            this.buttonStartCheck.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonStartCheck.Name = "buttonStartCheck";
            this.buttonStartCheck.Size = new System.Drawing.Size(146, 79);
            this.buttonStartCheck.TabIndex = 0;
            this.buttonStartCheck.Text = "▶️ 启动";
            this.buttonStartCheck.UseVisualStyleBackColor = true;
            this.buttonStartCheck.Click += new System.EventHandler(this.buttonStartCheck_Click);
            // 
            // buttonAdvanceSettings
            // 
            this.buttonAdvanceSettings.Location = new System.Drawing.Point(165, 715);
            this.buttonAdvanceSettings.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonAdvanceSettings.Name = "buttonAdvanceSettings";
            this.buttonAdvanceSettings.Size = new System.Drawing.Size(138, 40);
            this.buttonAdvanceSettings.TabIndex = 1;
            this.buttonAdvanceSettings.Text = "高级设置∧";
            this.buttonAdvanceSettings.UseVisualStyleBackColor = true;
            this.buttonAdvanceSettings.Click += new System.EventHandler(this.buttonAdvanceSettings_Click);
            // 
            // numericUpDownWebUIPort
            // 
            this.numericUpDownWebUIPort.Location = new System.Drawing.Point(632, 127);
            this.numericUpDownWebUIPort.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownWebUIPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownWebUIPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWebUIPort.Name = "numericUpDownWebUIPort";
            this.numericUpDownWebUIPort.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownWebUIPort.TabIndex = 15;
            this.numericUpDownWebUIPort.Value = new decimal(new int[] {
            8199,
            0,
            0,
            0});
            this.numericUpDownWebUIPort.ValueChanged += new System.EventHandler(this.numericUpDownWebUIPort_ValueChanged);
            // 
            // numericUpDownDLTimehot
            // 
            this.numericUpDownDLTimehot.Location = new System.Drawing.Point(632, 79);
            this.numericUpDownDLTimehot.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownDLTimehot.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDLTimehot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDLTimehot.Name = "numericUpDownDLTimehot";
            this.numericUpDownDLTimehot.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownDLTimehot.TabIndex = 14;
            this.numericUpDownDLTimehot.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelWebUIPort
            // 
            this.labelWebUIPort.AutoSize = true;
            this.labelWebUIPort.Location = new System.Drawing.Point(465, 132);
            this.labelWebUIPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelWebUIPort.Name = "labelWebUIPort";
            this.labelWebUIPort.Size = new System.Drawing.Size(170, 21);
            this.labelWebUIPort.TabIndex = 7;
            this.labelWebUIPort.Text = "HTTP 服务端口：";
            // 
            // labelDownloadTimeout
            // 
            this.labelDownloadTimeout.AutoSize = true;
            this.labelDownloadTimeout.Location = new System.Drawing.Point(465, 84);
            this.labelDownloadTimeout.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDownloadTimeout.Name = "labelDownloadTimeout";
            this.labelDownloadTimeout.Size = new System.Drawing.Size(158, 21);
            this.labelDownloadTimeout.TabIndex = 6;
            this.labelDownloadTimeout.Text = "测速时间(秒)：";
            // 
            // numericUpDownSubStorePort
            // 
            this.numericUpDownSubStorePort.Location = new System.Drawing.Point(632, 175);
            this.numericUpDownSubStorePort.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownSubStorePort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownSubStorePort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubStorePort.Name = "numericUpDownSubStorePort";
            this.numericUpDownSubStorePort.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownSubStorePort.TabIndex = 21;
            this.numericUpDownSubStorePort.Value = new decimal(new int[] {
            8299,
            0,
            0,
            0});
            this.numericUpDownSubStorePort.ValueChanged += new System.EventHandler(this.numericUpDownWebUIPort_ValueChanged);
            // 
            // labelSubstorePort
            // 
            this.labelSubstorePort.AutoSize = true;
            this.labelSubstorePort.Location = new System.Drawing.Point(465, 180);
            this.labelSubstorePort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSubstorePort.Name = "labelSubstorePort";
            this.labelSubstorePort.Size = new System.Drawing.Size(172, 21);
            this.labelSubstorePort.TabIndex = 20;
            this.labelSubstorePort.Text = "Sub-Store端口：";
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.CausesValidation = false;
            this.groupBoxLog.Controls.Add(this.linkLabelAbout);
            this.groupBoxLog.Controls.Add(this.buttonUpdateKernel);
            this.groupBoxLog.Controls.Add(this.labelLogNodeInfo);
            this.groupBoxLog.Controls.Add(this.richTextBoxAllLog);
            this.groupBoxLog.Location = new System.Drawing.Point(354, 14);
            this.groupBoxLog.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxLog.Size = new System.Drawing.Size(1100, 768);
            this.groupBoxLog.TabIndex = 1;
            this.groupBoxLog.TabStop = false;
            // 
            // linkLabelAbout
            // 
            this.linkLabelAbout.AutoSize = true;
            this.linkLabelAbout.Location = new System.Drawing.Point(850, 0);
            this.linkLabelAbout.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.linkLabelAbout.Name = "linkLabelAbout";
            this.linkLabelAbout.Size = new System.Drawing.Size(250, 21);
            this.linkLabelAbout.TabIndex = 21;
            this.linkLabelAbout.TabStop = true;
            this.linkLabelAbout.Text = "关于 SubsCheck Win GUI";
            this.linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // buttonUpdateKernel
            // 
            this.buttonUpdateKernel.Location = new System.Drawing.Point(956, 802);
            this.buttonUpdateKernel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonUpdateKernel.Name = "buttonUpdateKernel";
            this.buttonUpdateKernel.Size = new System.Drawing.Size(138, 40);
            this.buttonUpdateKernel.TabIndex = 20;
            this.buttonUpdateKernel.Text = "更新内核";
            this.buttonUpdateKernel.UseVisualStyleBackColor = true;
            this.buttonUpdateKernel.Visible = false;
            this.buttonUpdateKernel.Click += new System.EventHandler(this.buttonUpdateKernel_Click);
            // 
            // labelLogNodeInfo
            // 
            this.labelLogNodeInfo.AutoSize = true;
            this.labelLogNodeInfo.Location = new System.Drawing.Point(0, 0);
            this.labelLogNodeInfo.Name = "labelLogNodeInfo";
            this.labelLogNodeInfo.Size = new System.Drawing.Size(94, 21);
            this.labelLogNodeInfo.TabIndex = 22;
            this.labelLogNodeInfo.Text = "实时日志";
            // 
            // richTextBoxAllLog
            // 
            this.richTextBoxAllLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBoxAllLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAllLog.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxAllLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBoxAllLog.Location = new System.Drawing.Point(6, 29);
            this.richTextBoxAllLog.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.richTextBoxAllLog.Name = "richTextBoxAllLog";
            this.richTextBoxAllLog.ReadOnly = true;
            this.richTextBoxAllLog.Size = new System.Drawing.Size(1088, 734);
            this.richTextBoxAllLog.TabIndex = 0;
            this.richTextBoxAllLog.Text = "";
            this.richTextBoxAllLog.DoubleClick += new System.EventHandler(this.richTextBoxAllLog_DoubleClick);
            // 
            // groupBoxAdvanceSettings
            // 
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxSubsStats);
            this.groupBoxAdvanceSettings.Controls.Add(this.comboBoxOverwriteUrls);
            this.groupBoxAdvanceSettings.Controls.Add(this.textBoxSubStorePath);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelSubstoreParh);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownTotalBandwidthLimit);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownSuccessLimit);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownSubStorePort);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownWebUIPort);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownDownloadMb);
            this.groupBoxAdvanceSettings.Controls.Add(this.numericUpDownDLTimehot);
            this.groupBoxAdvanceSettings.Controls.Add(this.comboBoxSysProxy);
            this.groupBoxAdvanceSettings.Controls.Add(this.comboBoxSpeedtestUrl);
            this.groupBoxAdvanceSettings.Controls.Add(this.comboBoxGithubProxyUrl);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelSubstorePort);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelOverwriteUrls);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelWebUIPort);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelDownloadMb);
            this.groupBoxAdvanceSettings.Controls.Add(this.textBoxWebUiAPIKey);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelDownloadTimeout);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxEnableWebUI);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxEnableRenameNode);
            this.groupBoxAdvanceSettings.Controls.Add(this.label1);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxEnableMediaCheck);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxKeepSucced);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxTotalBandwidthLimit);
            this.groupBoxAdvanceSettings.Controls.Add(this.buttonCheckUpdate);
            this.groupBoxAdvanceSettings.Controls.Add(this.buttonMoreSettings);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelGithubProxyUrl);
            this.groupBoxAdvanceSettings.Controls.Add(this.labelSpeedtestUrl);
            this.groupBoxAdvanceSettings.Controls.Add(this.checkBoxEnableSuccessLimit);
            this.groupBoxAdvanceSettings.Location = new System.Drawing.Point(26, 792);
            this.groupBoxAdvanceSettings.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxAdvanceSettings.Name = "groupBoxAdvanceSettings";
            this.groupBoxAdvanceSettings.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxAdvanceSettings.Size = new System.Drawing.Size(1430, 216);
            this.groupBoxAdvanceSettings.TabIndex = 2;
            this.groupBoxAdvanceSettings.TabStop = false;
            this.groupBoxAdvanceSettings.Text = "高级设置";
            this.groupBoxAdvanceSettings.Visible = false;
            // 
            // comboBoxOverwriteUrls
            // 
            this.comboBoxOverwriteUrls.FormattingEnabled = true;
            this.comboBoxOverwriteUrls.Items.AddRange(new object[] {
            "[内置]布丁狗的订阅转换",
            "[内置]ACL4SSR_Online_Full",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/布丁狗的订阅转" +
                "换.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/ACL4SSR" +
                "_Online_Full.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/ACL4SSR" +
                "_Online_Full_WithIcon.yaml",
            "https://raw.githubusercontent.com/mihomo-party-org/override-hub/main/yaml/添加直连规则." +
                "yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/布丁狗的订阅转换.y" +
                "aml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/ACL4SSR_On" +
                "line_Full.yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/ACL4SSR_On" +
                "line_Full_WithIcon.yaml",
            "https://fastly.jsdelivr.net/gh/mihomo-party-org/override-hub@main/yaml/添加直连规则.yam" +
                "l"});
            this.comboBoxOverwriteUrls.Location = new System.Drawing.Point(914, 128);
            this.comboBoxOverwriteUrls.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxOverwriteUrls.Name = "comboBoxOverwriteUrls";
            this.comboBoxOverwriteUrls.Size = new System.Drawing.Size(504, 29);
            this.comboBoxOverwriteUrls.TabIndex = 24;
            this.comboBoxOverwriteUrls.SelectedIndexChanged += new System.EventHandler(this.comboBoxOverwriteUrls_SelectedIndexChanged);
            // 
            // textBoxSubStorePath
            // 
            this.textBoxSubStorePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSubStorePath.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxSubStorePath.Location = new System.Drawing.Point(914, 175);
            this.textBoxSubStorePath.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxSubStorePath.Name = "textBoxSubStorePath";
            this.textBoxSubStorePath.Size = new System.Drawing.Size(144, 31);
            this.textBoxSubStorePath.TabIndex = 41;
            this.textBoxSubStorePath.Text = "请输入路径";
            this.textBoxSubStorePath.Enter += new System.EventHandler(this.textBoxSubStorePath_Enter);
            this.textBoxSubStorePath.Leave += new System.EventHandler(this.textBoxSubStorePath_Leave);
            // 
            // labelSubstoreParh
            // 
            this.labelSubstoreParh.AutoSize = true;
            this.labelSubstoreParh.Location = new System.Drawing.Point(735, 180);
            this.labelSubstoreParh.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSubstoreParh.Name = "labelSubstoreParh";
            this.labelSubstoreParh.Size = new System.Drawing.Size(183, 21);
            this.labelSubstoreParh.TabIndex = 42;
            this.labelSubstoreParh.Text = "Sub-Store 路径：";
            // 
            // numericUpDownTotalBandwidthLimit
            // 
            this.numericUpDownTotalBandwidthLimit.Enabled = false;
            this.numericUpDownTotalBandwidthLimit.Location = new System.Drawing.Point(365, 79);
            this.numericUpDownTotalBandwidthLimit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownTotalBandwidthLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTotalBandwidthLimit.Name = "numericUpDownTotalBandwidthLimit";
            this.numericUpDownTotalBandwidthLimit.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownTotalBandwidthLimit.TabIndex = 37;
            this.numericUpDownTotalBandwidthLimit.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownTotalBandwidthLimit.ValueChanged += new System.EventHandler(this.NumericUpDownTotalBandwidthLimit_ValueChanged);
            // 
            // numericUpDownSuccessLimit
            // 
            this.numericUpDownSuccessLimit.Enabled = false;
            this.numericUpDownSuccessLimit.Location = new System.Drawing.Point(365, 31);
            this.numericUpDownSuccessLimit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownSuccessLimit.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownSuccessLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSuccessLimit.Name = "numericUpDownSuccessLimit";
            this.numericUpDownSuccessLimit.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownSuccessLimit.TabIndex = 22;
            this.numericUpDownSuccessLimit.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownDownloadMb
            // 
            this.numericUpDownDownloadMb.Location = new System.Drawing.Point(632, 31);
            this.numericUpDownDownloadMb.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownDownloadMb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDownloadMb.Name = "numericUpDownDownloadMb";
            this.numericUpDownDownloadMb.Size = new System.Drawing.Size(87, 31);
            this.numericUpDownDownloadMb.TabIndex = 30;
            this.numericUpDownDownloadMb.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // comboBoxSysProxy
            // 
            this.comboBoxSysProxy.AutoCompleteCustomSource.AddRange(new string[] {
            "127.0.0.1:7890",
            "127.0.0.1:10808",
            "127.0.0.1:10809",
            "127.0.0.1:7891",
            "127.0.0.1:1080",
            "127.0.0.1:8080",
            "127.0.0.1:443"});
            this.comboBoxSysProxy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSysProxy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.comboBoxSysProxy.FormattingEnabled = true;
            this.comboBoxSysProxy.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.comboBoxSysProxy.Items.AddRange(new object[] {
            "自动检测",
            "127.0.0.1:443",
            "127.0.0.1:7890",
            "127.0.0.1:7891",
            "127.0.0.1:1080",
            "127.0.0.1:8080",
            "127.0.0.1:10808",
            "127.0.0.1:10809",
            "127.0.0.1:3067",
            "127.0.0.1:2080",
            "127.0.0.1:1194",
            "127.0.0.1:1082",
            "127.0.0.1:12334",
            "127.0.0.1:12335"});
            this.comboBoxSysProxy.Location = new System.Drawing.Point(848, 32);
            this.comboBoxSysProxy.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxSysProxy.Name = "comboBoxSysProxy";
            this.comboBoxSysProxy.Size = new System.Drawing.Size(210, 29);
            this.comboBoxSysProxy.TabIndex = 39;
            // 
            // comboBoxSpeedtestUrl
            // 
            this.comboBoxSpeedtestUrl.FormattingEnabled = true;
            this.comboBoxSpeedtestUrl.Items.AddRange(new object[] {
            "不测速",
            "https://github.com/2dust/v2rayN/releases/download/7.15.7/v2rayN-windows-64-SelfCo" +
                "ntained.zip",
            "https://github.com/AaronFeng753/Waifu2x-Extension-GUI/releases/download/v2.21.12/" +
                "Waifu2x-Extension-GUI-v2.21.12-Portable.7z",
            "https://github.com/2dust/v2rayN/releases/download/7.10.4/v2rayN-windows-64-deskto" +
                "p.zip",
            "https://github.com/VSCodium/vscodium/releases/download/1.98.0.25067/codium-1.98.0" +
                ".25067-el9.aarch64.rpm"});
            this.comboBoxSpeedtestUrl.Location = new System.Drawing.Point(848, 80);
            this.comboBoxSpeedtestUrl.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxSpeedtestUrl.Name = "comboBoxSpeedtestUrl";
            this.comboBoxSpeedtestUrl.Size = new System.Drawing.Size(570, 29);
            this.comboBoxSpeedtestUrl.TabIndex = 19;
            this.comboBoxSpeedtestUrl.Text = "不测速";
            // 
            // comboBoxGithubProxyUrl
            // 
            this.comboBoxGithubProxyUrl.FormattingEnabled = true;
            this.comboBoxGithubProxyUrl.Items.AddRange(new object[] {
            "自动选择",
            "1.github.010716.xyz",
            "113355.kabaka.xyz",
            "80888888.xyz",
            "aa.w0x7ce.eu",
            "acc.meiqer.com",
            "api-ghp.fjy.zone",
            "armg1.jyhk.tk",
            "armg2.jyhk.tk",
            "b.yesican.top",
            "bakht1.jsdelivr.fyi",
            "bakht2.jsdelivr.fyi",
            "bakht3.jsdelivr.fyi",
            "booster.ookkk.ggff.net",
            "c.gatepro.cn",
            "cc.ikakatoo.us",
            "ccgit1.5gyh.cf",
            "ccgit2.5gyh.cf",
            "cdn-gh.141888.xyz",
            "cfghproxy.165683.xyz",
            "chirophy.online",
            "choner.eu.org",
            "d.scyun.top",
            "daili.6dot.cn",
            "dh.guluy.top",
            "dh.jeblove.com",
            "dl.github.mirror.shalo.link",
            "dnsvip.uk",
            "docker.bkxhkoo.com",
            "docker.ppp.ac.cn",
            "down.avi.gs",
            "download.ojbk.one",
            "download.serein.cc",
            "f.shenbing.nyc.mn",
            "fastgithub.starryfun.icu",
            "file.justgame.top",
            "ft.v1k1.xin",
            "fuck-flow.nobige.cn",
            "g.108964.xyz",
            "g.blfrp.cn",
            "g.bravexist.cn",
            "g.down.0ms.net",
            "g.jscdn.cn",
            "g.yeyuqiufeng.cn",
            "gh.136361.xyz",
            "gh.13x.plus",
            "gh.19121912.xyz",
            "gh.193.gs",
            "gh.220106.xyz",
            "gh.222322.xyz",
            "gh.244224659.xyz",
            "gh.2i.gs",
            "gh.316688.xyz",
            "gh.321122.xyz",
            "gh.334433.xyz",
            "gh.39.al",
            "gh.518298.xyz",
            "gh.52099520.xyz",
            "gh.654535.xyz",
            "gh.777000.best",
            "gh.799154.xyz",
            "gh.860686.xyz",
            "gh.8p.gs",
            "gh.960980.xyz",
            "gh.accn.eu.org",
            "gh.amirrors.com",
            "gh.andiest.com",
            "gh.aurzex.top",
            "gh.avmine.com",
            "gh.b52m.cn",
            "gh.bhexo.cn",
            "gh.cdn.fullcone.cn",
            "gh.chewable.eu.org",
            "gh.chillwaytech.com",
            "gh.cnbattle.com",
            "gh.crond.dev",
            "gh.dev.438250.xyz",
            "gh.duang.io",
            "gh.duckcc.com",
            "gh.dwsy.link",
            "gh.ecdn.ip-ddns.com",
            "gh.flewsea.news",
            "gh.flewsea.pw",
            "gh.flyrr.cc",
            "gh.gongyi.tk",
            "gh.gorun.eu.org",
            "gh.gxb.pub",
            "gh.haloless.com",
            "gh.heshiheng.top",
            "gh.hitcs.cc",
            "gh.i3.pw",
            "gh.ibridge.eu.org",
            "gh.iinx.top",
            "gh.j8.work",
            "gh.jadelive.top",
            "gh.jscdn.cn",
            "gh.jxq.io",
            "gh.kejilion.pro",
            "gh.kemon.ai",
            "gh.kmxm.online",
            "gh.lib.cx",
            "gh.lkwplus.com",
            "gh.lux1983.com",
            "gh.luzy.top",
            "gh.lyh.moe",
            "gh.miaomiao.video",
            "gh.micedns.cloudns.org",
            "gh.mirror.190211.xyz",
            "gh.mirror.coolfeature.top",
            "gh.moetools.net",
            "gh.momonomi.xyz",
            "gh.mrskye.cn",
            "gh.mtx72.cc",
            "gh.nekhill.top",
            "gh.nekorect.eu.org",
            "gh.nextfuture.top",
            "gh.oevery.me",
            "gh.oneproxy.top",
            "gh.opsproxy.com",
            "gh.osspub.cn",
            "gh.padao.fun",
            "gh.prlrr.com",
            "gh.pylas.xyz",
            "gh.qptf.eu.org",
            "gh.qsd.onl",
            "gh.qsq.one",
            "gh.rem.asia",
            "gh.riye.de",
            "gh.scy.ink",
            "gh.someo.top",
            "gh.stanl.ee",
            "gh.stewitch.com",
            "gh.suite.eu.org",
            "gh.tbw.wiki",
            "gh.tou.lu",
            "gh.tryxd.cn",
            "gh.uclort.com",
            "gh.wglee.org",
            "gh.wowforever.xyz",
            "gh.wuuu.cc",
            "gh.wwang.de",
            "gh.wygg.us.kg",
            "gh.xbzza.cn",
            "gh.xda.plus",
            "gh.yahool.com.cn",
            "gh.yushum.com",
            "ghac.760710.xyz",
            "ghacc.cpuhk.eu.org",
            "ghb.wglee.org",
            "gh-boost.oneboy.app",
            "gh-deno.mocn.top",
            "ghfast.top",
            "ghjs.131412.eu.org",
            "ghp.618032.xyz",
            "ghp.9e6.site",
            "ghp.dnsplus.uk",
            "ghp.fit2.fun",
            "ghp.imc.re",
            "ghp.jokeme.top",
            "ghp.lanchonghai.com",
            "ghp.miaostay.com",
            "ghp.opendatahub.xyz",
            "ghp.pbren.com",
            "ghp.src.moe",
            "ghp.tryanks.com",
            "ghp.vatery.com",
            "ghp.xiaopan.ai",
            "ghp.ybot.xin",
            "ghp.yeye.f5.si",
            "ghproxy.0081024.xyz",
            "ghproxy.053000.xyz",
            "ghproxy.200502.xyz",
            "ghproxy.943689.xyz",
            "ghproxy.alltobid.cc",
            "ghproxy.amayakite.xyz",
            "ghproxy.bugungu.top",
            "gh-proxy.dorz.tech",
            "ghproxy.dsdog.tk",
            "ghproxy.ducknet.work",
            "ghproxy.gopher.ink",
            "ghproxy.gpnu.org",
            "ghproxy.hoshizukimio.top",
            "gh-proxy.iflyelf.com",
            "ghproxy.imoyuapp.win",
            "gh-proxy.jacksixth.top",
            "gh-proxy.jmper.me",
            "ghproxy.joylian.com",
            "gh-proxy.just520.top",
            "ghproxy.licardo.vip",
            "gh-proxy.mereith.com",
            "ghproxy.minge.dev",
            "ghproxy.missfuture.eu.org",
            "ghproxy.moweilong.com",
            "ghproxy.nanakorobi.com",
            "ghproxy.net",
            "gh-proxy.not.icu",
            "ghproxy.ownyuan.top",
            "gh-proxy.rxliuli.com",
            "ghproxy.sakuramoe.dev",
            "ghproxy.smallfawn.work",
            "ghproxy.sveir.xyz",
            "ghproxy.temoa.fun",
            "ghproxy.thefoxnet.com",
            "ghproxy.tracemouse.top",
            "ghproxy.txq.life",
            "ghproxy.viper.pub",
            "ghproxy.vyronlee-lab.com",
            "ghproxy.weizhiwen.net",
            "ghproxy.wjsphy.top",
            "ghproxy.workers.haoutil.com",
            "ghproxy.xiaohei-studio-chatgpt-proxy.com.cn",
            "gh-proxy.yuntao.me",
            "git.1999111.xyz",
            "git.22345678.xyz",
            "git.40609891.xyz",
            "git.5gyh.cf",
            "git.988896.xyz",
            "git.aaltozz.info",
            "git.acap.cc",
            "git.amoluo.win",
            "git.anye.in",
            "git.binbow.link",
            "git.blaow.cloudns.org",
            "git.closersyu.top",
            "git.ifso.site",
            "git.imvery.moe",
            "git.ixdd.de",
            "git.ldvx.de",
            "git.lincloud.pro",
            "git.liunasc.xyz",
            "git.llvho.com",
            "git.loushi.site",
            "git.lzzz.ink",
            "git.maomao.ovh",
            "git.mokoc.live",
            "git.niege.app",
            "git.nyar.work",
            "git.o8.cx",
            "git.outtw.com",
            "git.ppp.ac.cn",
            "git.repcz.link",
            "git.txaff.com",
            "git.verynb.org",
            "git.wsl.icu",
            "git.wyy.sh",
            "git.xiny.eu.org",
            "git.xuantan.icu",
            "git.zlong.eu.org",
            "git3.openapi.site",
            "git-clone.ccrui.dev",
            "github.08050611.xyz",
            "github.143760.xyz",
            "github.170011.xyz",
            "github.17263241.xyz",
            "github.180280.xyz",
            "github.197909.xyz",
            "github.19890821.xyz",
            "github.201068.xyz",
            "github.333033.xyz",
            "github.4240333.xyz",
            "github.564456.xyz",
            "github.732086.xyz",
            "github.776884.xyz",
            "github.789056.xyz",
            "github.818668.xyz",
            "github.8void.sbs",
            "github.9394961.xyz",
            "github.960118.xyz",
            "github.abyss.moe",
            "github.atzzz.com",
            "github.axcio.dns-dynamic.net",
            "github.boki.moe",
            "github.boringhex.top",
            "github.bullb.net",
            "github.c1g.top",
            "github.cf.xihale.top",
            "github.chasun.top",
            "github.chuancey.eu.org",
            "github.cnxiaobai.com",
            "github.computerqwq.top",
            "github.cswklt.top",
            "github.ctios.cn",
            "github.ddlink.asia",
            "github.dockerspeed.site",
            "github.eejsq.net",
            "github.ffffffff0x.com",
            "github.gdzja.site",
            "github.haodiy.xyz",
            "github.hhh.sd",
            "github.hi.edu.rs",
            "github.hostscc.top",
            "github.hx208.top",
            "github.ilxyz.xyz",
            "github.intellisensing.tech",
            "github.jerryliang.win",
            "github.jimmyshjj.top",
            "github.jinenyy.vip",
            "github.jscdn.cn",
            "github.kidos.top",
            "github.kuugo.top",
            "github.lao.plus",
            "github.mayx.eu.org",
            "github.mirror.qlnu-sec.cn",
            "github.mirror.vurl.eu.org",
            "github.mirrors.hikafeng.com",
            "github.mistudio.top",
            "github.orangbus.cn",
            "github.pipers.cn",
            "github.proxy.zerozone.tech",
            "github.pxy.lnsee.com",
            "github.quickso.net",
            "github.ruxi.org",
            "github.sagolu.top",
            "github.serein.cc",
            "github.snakexgc.com",
            "github.space520.eu.org",
            "github.sssss.work",
            "github.static.cv",
            "github.suyijun.top",
            "github.try255.com",
            "github.unipus.site",
            "github.verynb.org",
            "github.vipchanel.com",
            "github.widiazine.top",
            "github.workers.lv10.ren",
            "github.workersnail.com",
            "github.xin-yu.cloud",
            "github.xiongmx.com",
            "github.xwb009.xyz",
            "github.xxlab.tech",
            "github.xxqq.de",
            "github.xykcloud.com",
            "github.yeep6.eu.org",
            "github.ylyhtools.top",
            "github.yoloarea.com",
            "github.yunfile.fun",
            "github.zhaolele.top",
            "github.zhou2008.cn",
            "github.zhulin240520.buzz",
            "github.zyhmifan.top",
            "githubacc.caiaiwan.com",
            "githubapi.jjchizha.com",
            "githubgo.856798.xyz",
            "github-proxy.ai-lulu.top",
            "github-proxy.caoayu.top",
            "github-proxy.explorexd.uk",
            "github-proxy.fjiabinc.cn",
            "github-proxy.sharefree.site",
            "githubproxy.unix.do",
            "github-quick.1ms.dev",
            "github-raw-download.nekhill.top",
            "githubsg.lilyya.top",
            "gitproxy.mrhjx.cn",
            "gitproxy.ozoo.top",
            "godlike.ezpull.top",
            "gp.19841106.xyz",
            "gp.dahe.now.cc",
            "gp.daxei.now.cc",
            "g-p.loli.us",
            "gp.ownorigin.top",
            "gt.changqqq.xyz",
            "gxb.pub",
            "hay.uxio.cn",
            "hg.19840228.top",
            "hh.newhappy.cf",
            "hk.114914.xyz",
            "hub.12138.3653655.xyz",
            "hub.326998.xyz",
            "hub.885666.xyz",
            "hub.fnas64.xin",
            "hub.jeblove.com",
            "hub.naloong.de",
            "hub.vps.861020.xyz",
            "hub.vvn.me",
            "hub.why-ing.top",
            "hub.xjl.ch",
            "jh.ussh.net",
            "jias.ayanjiu.top",
            "jiasu.iwtriptqt1016.eu.org",
            "jiasughapi.lingjun.cc",
            "jiasuqi.167889.xyz",
            "jisuan.xyz",
            "js.wd666.cloudns.biz",
            "l0l0l.cc",
            "m.seafood.loan",
            "mc.cn.eu.org",
            "mdv.162899.xyz",
            "micromatrix.gq",
            "mip.cnzzla.com",
            "my.iiso.site",
            "my.losesw.live",
            "mygh.api.xiaomao.eu.org",
            "nav.253874.net",
            "nav.cxycsx.vip",
            "nav.gjcloak.xyz",
            "nav.hgd1999.com",
            "nav.hoiho.cn",
            "nav.syss.fun",
            "nav.tossp.com",
            "nav.wxapp.xyz",
            "nav.yyxw.tk",
            "navs.itmax.cn",
            "neoz.chat",
            "noad.keliyan.top",
            "node2.txq.life",
            "or.tianba.eu.org",
            "p.jackyu.cn",
            "privateghproxy.iil.im",
            "proxy.191027.xyz",
            "proxy.atoposs.com",
            "proxy.ccc8.vip",
            "proxy.dragontea.cc",
            "proxy.fakups.cn",
            "proxydl.lcayun.cn",
            "proxy-gh.1l1.icu",
            "q-github.cnxiaobai.com",
            "ql.l50.top",
            "raw.nmd.im",
            "rst.567812.xyz",
            "static.kaixinwu.vip",
            "static.yiwangmeng.com",
            "t.992699.xyz",
            "tpe.corpa.me",
            "tube.20140301.xyz",
            "vps.pansen626.com",
            "wfgithub.xiaonuomi.ie.eu.org"});
            this.comboBoxGithubProxyUrl.Location = new System.Drawing.Point(1231, 32);
            this.comboBoxGithubProxyUrl.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxGithubProxyUrl.Name = "comboBoxGithubProxyUrl";
            this.comboBoxGithubProxyUrl.Size = new System.Drawing.Size(187, 29);
            this.comboBoxGithubProxyUrl.TabIndex = 21;
            this.comboBoxGithubProxyUrl.Leave += new System.EventHandler(this.comboBoxGithubProxyUrl_Leave);
            // 
            // labelOverwriteUrls
            // 
            this.labelOverwriteUrls.AutoSize = true;
            this.labelOverwriteUrls.Location = new System.Drawing.Point(735, 132);
            this.labelOverwriteUrls.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelOverwriteUrls.Name = "labelOverwriteUrls";
            this.labelOverwriteUrls.Size = new System.Drawing.Size(181, 21);
            this.labelOverwriteUrls.TabIndex = 23;
            this.labelOverwriteUrls.Text = "Clash 覆写配置：";
            // 
            // labelDownloadMb
            // 
            this.labelDownloadMb.AutoSize = true;
            this.labelDownloadMb.Location = new System.Drawing.Point(465, 36);
            this.labelDownloadMb.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDownloadMb.Name = "labelDownloadMb";
            this.labelDownloadMb.Size = new System.Drawing.Size(159, 21);
            this.labelDownloadMb.TabIndex = 31;
            this.labelDownloadMb.Text = "测速大小(MB)：";
            // 
            // textBoxWebUiAPIKey
            // 
            this.textBoxWebUiAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxWebUiAPIKey.Enabled = false;
            this.textBoxWebUiAPIKey.Location = new System.Drawing.Point(255, 175);
            this.textBoxWebUiAPIKey.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxWebUiAPIKey.Name = "textBoxWebUiAPIKey";
            this.textBoxWebUiAPIKey.PasswordChar = '*';
            this.textBoxWebUiAPIKey.Size = new System.Drawing.Size(197, 31);
            this.textBoxWebUiAPIKey.TabIndex = 6;
            this.textBoxWebUiAPIKey.Text = "admin";
            this.textBoxWebUiAPIKey.Enter += new System.EventHandler(this.textBoxWebUiAPIKey_Enter);
            this.textBoxWebUiAPIKey.Leave += new System.EventHandler(this.textBoxWebUiAPIKey_Leave);
            // 
            // checkBoxEnableWebUI
            // 
            this.checkBoxEnableWebUI.AutoSize = true;
            this.checkBoxEnableWebUI.Location = new System.Drawing.Point(18, 178);
            this.checkBoxEnableWebUI.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxEnableWebUI.Name = "checkBoxEnableWebUI";
            this.checkBoxEnableWebUI.Size = new System.Drawing.Size(239, 25);
            this.checkBoxEnableWebUI.TabIndex = 28;
            this.checkBoxEnableWebUI.Text = "自定义 WebUI 密钥：";
            this.checkBoxEnableWebUI.UseVisualStyleBackColor = true;
            this.checkBoxEnableWebUI.CheckedChanged += new System.EventHandler(this.checkBoxEnableWebUI_CheckedChanged);
            // 
            // checkBoxEnableRenameNode
            // 
            this.checkBoxEnableRenameNode.AutoSize = true;
            this.checkBoxEnableRenameNode.Checked = true;
            this.checkBoxEnableRenameNode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableRenameNode.Location = new System.Drawing.Point(18, 34);
            this.checkBoxEnableRenameNode.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxEnableRenameNode.Name = "checkBoxEnableRenameNode";
            this.checkBoxEnableRenameNode.Size = new System.Drawing.Size(162, 25);
            this.checkBoxEnableRenameNode.TabIndex = 22;
            this.checkBoxEnableRenameNode.Text = "节点地址查询";
            this.checkBoxEnableRenameNode.UseVisualStyleBackColor = true;
            this.checkBoxEnableRenameNode.CheckedChanged += new System.EventHandler(this.checkBoxEnableRenameNode_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(735, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 40;
            this.label1.Text = "系统代理：";
            // 
            // checkBoxEnableMediaCheck
            // 
            this.checkBoxEnableMediaCheck.AutoSize = true;
            this.checkBoxEnableMediaCheck.Location = new System.Drawing.Point(18, 82);
            this.checkBoxEnableMediaCheck.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxEnableMediaCheck.Name = "checkBoxEnableMediaCheck";
            this.checkBoxEnableMediaCheck.Size = new System.Drawing.Size(141, 25);
            this.checkBoxEnableMediaCheck.TabIndex = 25;
            this.checkBoxEnableMediaCheck.Text = "流媒体检测";
            this.checkBoxEnableMediaCheck.UseVisualStyleBackColor = true;
            this.checkBoxEnableMediaCheck.CheckedChanged += new System.EventHandler(this.checkBoxEnableMediaCheck_CheckedChanged);
            // 
            // checkBoxKeepSucced
            // 
            this.checkBoxKeepSucced.AutoSize = true;
            this.checkBoxKeepSucced.Location = new System.Drawing.Point(18, 130);
            this.checkBoxKeepSucced.Name = "checkBoxKeepSucced";
            this.checkBoxKeepSucced.Size = new System.Drawing.Size(162, 25);
            this.checkBoxKeepSucced.TabIndex = 38;
            this.checkBoxKeepSucced.Text = "保留成功节点";
            this.checkBoxKeepSucced.UseVisualStyleBackColor = true;
            // 
            // checkBoxTotalBandwidthLimit
            // 
            this.checkBoxTotalBandwidthLimit.AutoSize = true;
            this.checkBoxTotalBandwidthLimit.Location = new System.Drawing.Point(192, 82);
            this.checkBoxTotalBandwidthLimit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxTotalBandwidthLimit.Name = "checkBoxTotalBandwidthLimit";
            this.checkBoxTotalBandwidthLimit.Size = new System.Drawing.Size(185, 25);
            this.checkBoxTotalBandwidthLimit.TabIndex = 36;
            this.checkBoxTotalBandwidthLimit.Text = "带宽限制MB/s：";
            this.checkBoxTotalBandwidthLimit.UseVisualStyleBackColor = true;
            this.checkBoxTotalBandwidthLimit.CheckedChanged += new System.EventHandler(this.checkBoxTotalBandwidthLimit_CheckedChanged);
            // 
            // buttonCheckUpdate
            // 
            this.buttonCheckUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCheckUpdate.FlatAppearance.BorderSize = 0;
            this.buttonCheckUpdate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCheckUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCheckUpdate.Location = new System.Drawing.Point(1280, 170);
            this.buttonCheckUpdate.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCheckUpdate.Name = "buttonCheckUpdate";
            this.buttonCheckUpdate.Size = new System.Drawing.Size(138, 40);
            this.buttonCheckUpdate.TabIndex = 26;
            this.buttonCheckUpdate.Text = "检查更新";
            this.buttonCheckUpdate.UseVisualStyleBackColor = true;
            this.buttonCheckUpdate.Click += new System.EventHandler(this.buttonCheckUpdate_Click);
            // 
            // buttonMoreSettings
            // 
            this.buttonMoreSettings.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMoreSettings.Location = new System.Drawing.Point(1130, 170);
            this.buttonMoreSettings.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonMoreSettings.Name = "buttonMoreSettings";
            this.buttonMoreSettings.Size = new System.Drawing.Size(138, 40);
            this.buttonMoreSettings.TabIndex = 29;
            this.buttonMoreSettings.Text = "补充参数";
            this.buttonMoreSettings.UseVisualStyleBackColor = true;
            this.buttonMoreSettings.Click += new System.EventHandler(this.buttonMoreSettings_Click);
            // 
            // labelGithubProxyUrl
            // 
            this.labelGithubProxyUrl.AutoSize = true;
            this.labelGithubProxyUrl.Location = new System.Drawing.Point(1070, 36);
            this.labelGithubProxyUrl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelGithubProxyUrl.Name = "labelGithubProxyUrl";
            this.labelGithubProxyUrl.Size = new System.Drawing.Size(163, 21);
            this.labelGithubProxyUrl.TabIndex = 20;
            this.labelGithubProxyUrl.Text = "Github Proxy：";
            // 
            // labelSpeedtestUrl
            // 
            this.labelSpeedtestUrl.AutoSize = true;
            this.labelSpeedtestUrl.Location = new System.Drawing.Point(735, 84);
            this.labelSpeedtestUrl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSpeedtestUrl.Name = "labelSpeedtestUrl";
            this.labelSpeedtestUrl.Size = new System.Drawing.Size(115, 21);
            this.labelSpeedtestUrl.TabIndex = 18;
            this.labelSpeedtestUrl.Text = "测速地址：";
            // 
            // checkBoxEnableSuccessLimit
            // 
            this.checkBoxEnableSuccessLimit.AutoSize = true;
            this.checkBoxEnableSuccessLimit.Location = new System.Drawing.Point(192, 34);
            this.checkBoxEnableSuccessLimit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxEnableSuccessLimit.Name = "checkBoxEnableSuccessLimit";
            this.checkBoxEnableSuccessLimit.Size = new System.Drawing.Size(183, 25);
            this.checkBoxEnableSuccessLimit.TabIndex = 27;
            this.checkBoxEnableSuccessLimit.Text = "节点限制数量：";
            this.checkBoxEnableSuccessLimit.UseVisualStyleBackColor = true;
            this.checkBoxEnableSuccessLimit.CheckedChanged += new System.EventHandler(this.checkBoxEnableSuccessLimit_CheckedChanged);
            // 
            // progressBarAll
            // 
            this.progressBarAll.Location = new System.Drawing.Point(0, 0);
            this.progressBarAll.Margin = new System.Windows.Forms.Padding(0);
            this.progressBarAll.Name = "progressBarAll";
            this.progressBarAll.Size = new System.Drawing.Size(1466, 4);
            this.progressBarAll.Step = 1;
            this.progressBarAll.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarAll.TabIndex = 3;
            this.toolTip1.SetToolTip(this.progressBarAll, "检测进度");
            this.progressBarAll.Visible = false;
            // 
            // timerCopySubscriptionUrl
            // 
            this.timerCopySubscriptionUrl.Interval = 2000;
            this.timerCopySubscriptionUrl.Tick += new System.EventHandler(this.timerCopySubscriptionUrl_Tick);
            // 
            // groupBoxGist
            // 
            this.groupBoxGist.Controls.Add(this.textBox4);
            this.groupBoxGist.Controls.Add(this.label13);
            this.groupBoxGist.Controls.Add(this.textBox3);
            this.groupBoxGist.Controls.Add(this.label12);
            this.groupBoxGist.Controls.Add(this.textBox2);
            this.groupBoxGist.Controls.Add(this.label11);
            this.groupBoxGist.Location = new System.Drawing.Point(26, 1103);
            this.groupBoxGist.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxGist.Name = "groupBoxGist";
            this.groupBoxGist.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxGist.Size = new System.Drawing.Size(1430, 79);
            this.groupBoxGist.TabIndex = 4;
            this.groupBoxGist.TabStop = false;
            this.groupBoxGist.Text = "Gist 上传参数";
            this.groupBoxGist.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(830, 32);
            this.textBox4.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(584, 31);
            this.textBox4.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(633, 37);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(205, 21);
            this.label13.TabIndex = 4;
            this.label13.Text = "API Mirror(可选)：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(464, 32);
            this.textBox3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(157, 31);
            this.textBox3.TabIndex = 3;
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(308, 37);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 21);
            this.label12.TabIndex = 2;
            this.label12.Text = "Github Token：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(114, 32);
            this.textBox2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(185, 31);
            this.textBox2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Gist ID：";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 500;
            this.toolTip1.ShowAlways = true;
            // 
            // groupBoxR2
            // 
            this.groupBoxR2.Controls.Add(this.textBox6);
            this.groupBoxR2.Controls.Add(this.label15);
            this.groupBoxR2.Controls.Add(this.textBox7);
            this.groupBoxR2.Controls.Add(this.label16);
            this.groupBoxR2.Location = new System.Drawing.Point(26, 1192);
            this.groupBoxR2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxR2.Name = "groupBoxR2";
            this.groupBoxR2.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxR2.Size = new System.Drawing.Size(1430, 79);
            this.groupBoxR2.TabIndex = 6;
            this.groupBoxR2.TabStop = false;
            this.groupBoxR2.Text = "R2 上传参数";
            this.groupBoxR2.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(794, 32);
            this.textBox6.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.PasswordChar = '*';
            this.textBox6.Size = new System.Drawing.Size(620, 31);
            this.textBox6.TabIndex = 3;
            this.textBox6.Text = "1234567890";
            this.textBox6.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox6.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(633, 37);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(163, 21);
            this.label15.TabIndex = 2;
            this.label15.Text = "Worker Token：";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(161, 32);
            this.textBox7.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(460, 31);
            this.textBox7.TabIndex = 1;
            this.textBox7.Text = "https://example.worker.dev";
            this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 37);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(141, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "Worker URL：";
            // 
            // groupBoxWebdav
            // 
            this.groupBoxWebdav.Controls.Add(this.textBox5);
            this.groupBoxWebdav.Controls.Add(this.label14);
            this.groupBoxWebdav.Controls.Add(this.textBox8);
            this.groupBoxWebdav.Controls.Add(this.label17);
            this.groupBoxWebdav.Controls.Add(this.textBox9);
            this.groupBoxWebdav.Controls.Add(this.label18);
            this.groupBoxWebdav.Location = new System.Drawing.Point(26, 1281);
            this.groupBoxWebdav.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxWebdav.Name = "groupBoxWebdav";
            this.groupBoxWebdav.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBoxWebdav.Size = new System.Drawing.Size(1430, 79);
            this.groupBoxWebdav.TabIndex = 6;
            this.groupBoxWebdav.TabStop = false;
            this.groupBoxWebdav.Text = "Webdav 上传参数";
            this.groupBoxWebdav.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(768, 32);
            this.textBox5.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(646, 31);
            this.textBox5.TabIndex = 5;
            this.textBox5.Text = "https://example.com/dav/";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(633, 37);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(141, 21);
            this.label14.TabIndex = 4;
            this.label14.Text = "Webdav URL：";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(424, 32);
            this.textBox8.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox8.Name = "textBox8";
            this.textBox8.PasswordChar = '*';
            this.textBox8.Size = new System.Drawing.Size(197, 31);
            this.textBox8.TabIndex = 3;
            this.textBox8.Text = "admin";
            this.textBox8.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox8.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(308, 37);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 21);
            this.label17.TabIndex = 2;
            this.label17.Text = "Password：";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(128, 32);
            this.textBox9.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(171, 31);
            this.textBox9.TabIndex = 1;
            this.textBox9.Text = "admin";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 37);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(119, 21);
            this.label18.TabIndex = 0;
            this.label18.Text = "Username：";
            // 
            // timerRestartSchedule
            // 
            this.timerRestartSchedule.Interval = 86400000;
            this.timerRestartSchedule.Tick += new System.EventHandler(this.timerRestartSchedule_Tick);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // groupBoxPipeConcurrent
            // 
            this.groupBoxPipeConcurrent.Controls.Add(this.numericUpDownPipeMedia);
            this.groupBoxPipeConcurrent.Controls.Add(this.labelPipeMedia);
            this.groupBoxPipeConcurrent.Controls.Add(this.numericUpDownPipeSpeed);
            this.groupBoxPipeConcurrent.Controls.Add(this.labelPipeSpeed);
            this.groupBoxPipeConcurrent.Controls.Add(this.checkBoxPipeAuto);
            this.groupBoxPipeConcurrent.Controls.Add(this.numericUpDownPipeAlive);
            this.groupBoxPipeConcurrent.Controls.Add(this.labelPipeAlive);
            this.groupBoxPipeConcurrent.Location = new System.Drawing.Point(26, 1016);
            this.groupBoxPipeConcurrent.Name = "groupBoxPipeConcurrent";
            this.groupBoxPipeConcurrent.Size = new System.Drawing.Size(967, 79);
            this.groupBoxPipeConcurrent.TabIndex = 7;
            this.groupBoxPipeConcurrent.TabStop = false;
            this.groupBoxPipeConcurrent.Text = "流水线并发 参数";
            // 
            // numericUpDownPipeMedia
            // 
            this.numericUpDownPipeMedia.Location = new System.Drawing.Point(830, 36);
            this.numericUpDownPipeMedia.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownPipeMedia.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPipeMedia.Name = "numericUpDownPipeMedia";
            this.numericUpDownPipeMedia.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownPipeMedia.TabIndex = 43;
            this.numericUpDownPipeMedia.ValueChanged += new System.EventHandler(this.numericUpDownPipeMedia_ValueChanged);
            // 
            // labelPipeMedia
            // 
            this.labelPipeMedia.AutoSize = true;
            this.labelPipeMedia.Location = new System.Drawing.Point(633, 41);
            this.labelPipeMedia.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPipeMedia.Name = "labelPipeMedia";
            this.labelPipeMedia.Size = new System.Drawing.Size(199, 21);
            this.labelPipeMedia.TabIndex = 42;
            this.labelPipeMedia.Text = "流媒体检测并发数：";
            // 
            // numericUpDownPipeSpeed
            // 
            this.numericUpDownPipeSpeed.Location = new System.Drawing.Point(513, 36);
            this.numericUpDownPipeSpeed.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownPipeSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownPipeSpeed.Name = "numericUpDownPipeSpeed";
            this.numericUpDownPipeSpeed.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownPipeSpeed.TabIndex = 41;
            this.numericUpDownPipeSpeed.ValueChanged += new System.EventHandler(this.numericUpDownPipeSpeed_ValueChanged);
            // 
            // labelPipeSpeed
            // 
            this.labelPipeSpeed.AutoSize = true;
            this.labelPipeSpeed.Location = new System.Drawing.Point(387, 41);
            this.labelPipeSpeed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPipeSpeed.Name = "labelPipeSpeed";
            this.labelPipeSpeed.Size = new System.Drawing.Size(136, 21);
            this.labelPipeSpeed.TabIndex = 40;
            this.labelPipeSpeed.Text = "下载并发数：";
            // 
            // checkBoxPipeAuto
            // 
            this.checkBoxPipeAuto.AutoSize = true;
            this.checkBoxPipeAuto.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxPipeAuto.Location = new System.Drawing.Point(18, 39);
            this.checkBoxPipeAuto.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxPipeAuto.Name = "checkBoxPipeAuto";
            this.checkBoxPipeAuto.Size = new System.Drawing.Size(99, 25);
            this.checkBoxPipeAuto.TabIndex = 38;
            this.checkBoxPipeAuto.Text = "自适应";
            this.checkBoxPipeAuto.UseVisualStyleBackColor = true;
            this.checkBoxPipeAuto.CheckedChanged += new System.EventHandler(this.checkBoxPipeAuto_CheckedChanged);
            // 
            // numericUpDownPipeAlive
            // 
            this.numericUpDownPipeAlive.Location = new System.Drawing.Point(264, 36);
            this.numericUpDownPipeAlive.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numericUpDownPipeAlive.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPipeAlive.Name = "numericUpDownPipeAlive";
            this.numericUpDownPipeAlive.Size = new System.Drawing.Size(106, 31);
            this.numericUpDownPipeAlive.TabIndex = 39;
            this.numericUpDownPipeAlive.Tag = "";
            this.numericUpDownPipeAlive.ValueChanged += new System.EventHandler(this.numericUpDownPipeAlive_ValueChanged);
            // 
            // labelPipeAlive
            // 
            this.labelPipeAlive.AutoSize = true;
            this.labelPipeAlive.Location = new System.Drawing.Point(135, 41);
            this.labelPipeAlive.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPipeAlive.Name = "labelPipeAlive";
            this.labelPipeAlive.Size = new System.Drawing.Size(136, 21);
            this.labelPipeAlive.TabIndex = 38;
            this.labelPipeAlive.Text = "测活并发数：";
            // 
            // groupBoxEnhance
            // 
            this.groupBoxEnhance.Controls.Add(this.checkBoxDropBadCFNodes);
            this.groupBoxEnhance.Controls.Add(this.checkBoxEhanceTag);
            this.groupBoxEnhance.Location = new System.Drawing.Point(999, 1016);
            this.groupBoxEnhance.Name = "groupBoxEnhance";
            this.groupBoxEnhance.Size = new System.Drawing.Size(457, 79);
            this.groupBoxEnhance.TabIndex = 44;
            this.groupBoxEnhance.TabStop = false;
            this.groupBoxEnhance.Text = "Enhance 参数";
            // 
            // checkBoxDropBadCFNodes
            // 
            this.checkBoxDropBadCFNodes.AutoSize = true;
            this.checkBoxDropBadCFNodes.Location = new System.Drawing.Point(201, 38);
            this.checkBoxDropBadCFNodes.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxDropBadCFNodes.Name = "checkBoxDropBadCFNodes";
            this.checkBoxDropBadCFNodes.Size = new System.Drawing.Size(205, 25);
            this.checkBoxDropBadCFNodes.TabIndex = 39;
            this.checkBoxDropBadCFNodes.Text = "丢弃低质量CF节点";
            this.checkBoxDropBadCFNodes.UseVisualStyleBackColor = true;
            // 
            // checkBoxEhanceTag
            // 
            this.checkBoxEhanceTag.AutoSize = true;
            this.checkBoxEhanceTag.Location = new System.Drawing.Point(18, 39);
            this.checkBoxEhanceTag.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.checkBoxEhanceTag.Name = "checkBoxEhanceTag";
            this.checkBoxEhanceTag.Size = new System.Drawing.Size(162, 25);
            this.checkBoxEhanceTag.TabIndex = 38;
            this.checkBoxEhanceTag.Text = "增强位置标签";
            this.checkBoxEhanceTag.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubsStats
            // 
            this.checkBoxSubsStats.AutoSize = true;
            this.checkBoxSubsStats.Location = new System.Drawing.Point(192, 130);
            this.checkBoxSubsStats.Name = "checkBoxSubsStats";
            this.checkBoxSubsStats.Size = new System.Drawing.Size(162, 25);
            this.checkBoxSubsStats.TabIndex = 43;
            this.checkBoxSubsStats.Text = "统计订阅信息";
            this.checkBoxSubsStats.UseVisualStyleBackColor = true;
            // 
            // MainGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1466, 1374);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxComonSettings);
            this.Controls.Add(this.groupBoxAdvanceSettings);
            this.Controls.Add(this.groupBoxPipeConcurrent);
            this.Controls.Add(this.groupBoxEnhance);
            this.Controls.Add(this.groupBoxGist);
            this.Controls.Add(this.groupBoxR2);
            this.Controls.Add(this.groupBoxWebdav);
            this.Controls.Add(this.progressBarAll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "MainGui";
            this.Text = "SubsCheck Win GUI";
            this.groupBoxComonSettings.ResumeLayout(false);
            this.groupBoxComonSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConcurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWebUIPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDLTimehot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubStorePort)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.groupBoxAdvanceSettings.ResumeLayout(false);
            this.groupBoxAdvanceSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotalBandwidthLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuccessLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownloadMb)).EndInit();
            this.groupBoxGist.ResumeLayout(false);
            this.groupBoxGist.PerformLayout();
            this.groupBoxR2.ResumeLayout(false);
            this.groupBoxR2.PerformLayout();
            this.groupBoxWebdav.ResumeLayout(false);
            this.groupBoxWebdav.PerformLayout();
            this.groupBoxPipeConcurrent.ResumeLayout(false);
            this.groupBoxPipeConcurrent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipeAlive)).EndInit();
            this.groupBoxEnhance.ResumeLayout(false);
            this.groupBoxEnhance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timerinitial;
        private System.Windows.Forms.GroupBox groupBoxComonSettings;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Label labelLogNodeInfo;
        private System.Windows.Forms.Button buttonAdvanceSettings;
        private System.Windows.Forms.Button buttonStartCheck;
        private System.Windows.Forms.GroupBox groupBoxAdvanceSettings;
        private System.Windows.Forms.Label labelSubUrls;
        private System.Windows.Forms.Label labelSaveMethod;
        private System.Windows.Forms.Label labelWebUIPort;
        private System.Windows.Forms.Label labelDownloadTimeout;
        private System.Windows.Forms.Label labelMinSpped;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelCron;
        private System.Windows.Forms.Label labelConcurrent;
        private System.Windows.Forms.NumericUpDown numericUpDownWebUIPort;
        private System.Windows.Forms.NumericUpDown numericUpDownDLTimehot;
        private System.Windows.Forms.NumericUpDown numericUpDownMinSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownConcurrent;
        private System.Windows.Forms.TextBox textBoxSubsUrls;
        private System.Windows.Forms.ComboBox comboBoxSaveMethod;
        private System.Windows.Forms.ComboBox comboBoxSpeedtestUrl;
        private System.Windows.Forms.Label labelSpeedtestUrl;
        private System.Windows.Forms.ComboBox comboBoxGithubProxyUrl;
        private System.Windows.Forms.Label labelGithubProxyUrl;
        private System.Windows.Forms.CheckBox checkBoxEnableRenameNode;
        private System.Windows.Forms.RichTextBox richTextBoxAllLog;
        private System.Windows.Forms.ProgressBar progressBarAll;
        private System.Windows.Forms.Button buttonCopySubscriptionUrl;
        private System.Windows.Forms.Timer timerCopySubscriptionUrl;
        private System.Windows.Forms.GroupBox groupBoxGist;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBoxR2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBoxWebdav;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button buttonUpdateKernel;
        private System.Windows.Forms.ComboBox comboBoxSubscriptionType;
        private System.Windows.Forms.NumericUpDown numericUpDownSubStorePort;
        private System.Windows.Forms.Label labelSubstorePort;
        private System.Windows.Forms.Label labelOverwriteUrls;
        private System.Windows.Forms.ComboBox comboBoxOverwriteUrls;
        private System.Windows.Forms.CheckBox checkBoxEnableMediaCheck;
        private System.Windows.Forms.Timer timerRestartSchedule;
        private System.Windows.Forms.Button buttonCheckUpdate;
        private System.Windows.Forms.NumericUpDown numericUpDownSuccessLimit;
        private System.Windows.Forms.TextBox textBoxWebUiAPIKey;
        private System.Windows.Forms.CheckBox checkBoxEnableWebUI;
        private System.Windows.Forms.Button buttonWebUi;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Button buttonTriggerCheck;
        private System.Windows.Forms.TextBox textBoxCron;
        private System.Windows.Forms.LinkLabel linkLabelAbout;
        private System.Windows.Forms.Button buttonMoreSettings;
        private System.Windows.Forms.CheckBox checkBoxStartup;
        private System.Windows.Forms.NumericUpDown numericUpDownDownloadMb;
        private System.Windows.Forms.Label labelDownloadMb;
        private System.Windows.Forms.CheckBox checkBoxTotalBandwidthLimit;
        private System.Windows.Forms.NumericUpDown numericUpDownTotalBandwidthLimit;
        private System.Windows.Forms.CheckBox checkBoxSwitchArch64;
        private System.Windows.Forms.CheckBox checkBoxHighConcurrent;
        private System.Windows.Forms.GroupBox groupBoxPipeConcurrent;
        private System.Windows.Forms.NumericUpDown numericUpDownPipeAlive;
        private System.Windows.Forms.Label labelPipeAlive;
        private System.Windows.Forms.CheckBox checkBoxPipeAuto;
        private System.Windows.Forms.NumericUpDown numericUpDownPipeMedia;
        private System.Windows.Forms.Label labelPipeMedia;
        private System.Windows.Forms.NumericUpDown numericUpDownPipeSpeed;
        private System.Windows.Forms.Label labelPipeSpeed;
        private System.Windows.Forms.GroupBox groupBoxEnhance;
        private System.Windows.Forms.CheckBox checkBoxEhanceTag;
        private System.Windows.Forms.CheckBox checkBoxDropBadCFNodes;
        private System.Windows.Forms.CheckBox checkBoxKeepSucced;
        private System.Windows.Forms.ComboBox comboBoxSysProxy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxEnableSuccessLimit;
        private System.Windows.Forms.Label labelSubstoreParh;
        private System.Windows.Forms.TextBox textBoxSubStorePath;
        private System.Windows.Forms.CheckBox checkBoxSubsStats;
    }
}

