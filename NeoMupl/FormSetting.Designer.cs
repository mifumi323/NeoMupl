namespace NeoMupl
{
    partial class FormSetting
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbWindowTitlePattern = new System.Windows.Forms.ComboBox();
            this.btnWindowTitlePattern = new System.Windows.Forms.Button();
            this.txtWindowTitlePattern = new System.Windows.Forms.TextBox();
            this.lblWindowTitlePattern = new System.Windows.Forms.Label();
            this.lblTimeWeight = new System.Windows.Forms.Label();
            this.txtTimeWeight = new System.Windows.Forms.TextBox();
            this.trbMinPlayTime = new System.Windows.Forms.TrackBar();
            this.trbTimeWeight = new System.Windows.Forms.TrackBar();
            this.txtMinPlayTime = new System.Windows.Forms.TextBox();
            this.lblMinPlayTime = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvExtension = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.txtTitlePattern = new System.Windows.Forms.TextBox();
            this.lblTitlePattern = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.clbStatusItem = new System.Windows.Forms.CheckedListBox();
            this.chkShowStatus = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnRefLogFile = new System.Windows.Forms.Button();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbLog = new System.Windows.Forms.CheckedListBox();
            this.chkEraseLogOnExit = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.chkReportException = new System.Windows.Forms.CheckBox();
            this.chkIgnoreTimerError = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbMinPlayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeWeight)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtension)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(304, 298);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(385, 298);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(448, 280);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.cmbWindowTitlePattern);
            this.tabPage1.Controls.Add(this.btnWindowTitlePattern);
            this.tabPage1.Controls.Add(this.txtWindowTitlePattern);
            this.tabPage1.Controls.Add(this.lblWindowTitlePattern);
            this.tabPage1.Controls.Add(this.lblTimeWeight);
            this.tabPage1.Controls.Add(this.txtTimeWeight);
            this.tabPage1.Controls.Add(this.trbMinPlayTime);
            this.tabPage1.Controls.Add(this.trbTimeWeight);
            this.tabPage1.Controls.Add(this.txtMinPlayTime);
            this.tabPage1.Controls.Add(this.lblMinPlayTime);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(440, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全般設定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmbWindowTitlePattern
            // 
            this.cmbWindowTitlePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWindowTitlePattern.FormattingEnabled = true;
            this.cmbWindowTitlePattern.Items.AddRange(new object[] {
            "<Title>：項目のタイトル",
            "<FileName>：項目のフルパス名",
            "<FileTitle>：項目のファイル名",
            "<Status>：現在の状態（英語）",
            "<StatusJ>：現在の状態（日本語）",
            "<Version>：このソフトのバージョン",
            "NeoMupl：このソフトの名前"});
            this.cmbWindowTitlePattern.Location = new System.Drawing.Point(249, 128);
            this.cmbWindowTitlePattern.Name = "cmbWindowTitlePattern";
            this.cmbWindowTitlePattern.Size = new System.Drawing.Size(185, 20);
            this.cmbWindowTitlePattern.TabIndex = 26;
            this.cmbWindowTitlePattern.Enter += new System.EventHandler(this.CmbWindowTitlePattern_Enter);
            this.cmbWindowTitlePattern.Leave += new System.EventHandler(this.CmbWindowTitlePattern_Leave);
            // 
            // btnWindowTitlePattern
            // 
            this.btnWindowTitlePattern.Location = new System.Drawing.Point(219, 129);
            this.btnWindowTitlePattern.Name = "btnWindowTitlePattern";
            this.btnWindowTitlePattern.Size = new System.Drawing.Size(24, 19);
            this.btnWindowTitlePattern.TabIndex = 25;
            this.btnWindowTitlePattern.Text = "←";
            this.btnWindowTitlePattern.UseVisualStyleBackColor = true;
            this.btnWindowTitlePattern.Click += new System.EventHandler(this.BtnWindowTitlePattern_Click);
            // 
            // txtWindowTitlePattern
            // 
            this.txtWindowTitlePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowTitlePattern.Location = new System.Drawing.Point(10, 129);
            this.txtWindowTitlePattern.Name = "txtWindowTitlePattern";
            this.txtWindowTitlePattern.Size = new System.Drawing.Size(203, 19);
            this.txtWindowTitlePattern.TabIndex = 24;
            // 
            // lblWindowTitlePattern
            // 
            this.lblWindowTitlePattern.AutoSize = true;
            this.lblWindowTitlePattern.Location = new System.Drawing.Point(8, 114);
            this.lblWindowTitlePattern.Name = "lblWindowTitlePattern";
            this.lblWindowTitlePattern.Size = new System.Drawing.Size(145, 12);
            this.lblWindowTitlePattern.TabIndex = 23;
            this.lblWindowTitlePattern.Text = "ウィンドウのタイトル付け規則：";
            // 
            // lblTimeWeight
            // 
            this.lblTimeWeight.AutoSize = true;
            this.lblTimeWeight.Location = new System.Drawing.Point(8, 60);
            this.lblTimeWeight.Name = "lblTimeWeight";
            this.lblTimeWeight.Size = new System.Drawing.Size(56, 12);
            this.lblTimeWeight.TabIndex = 22;
            this.lblTimeWeight.Text = "時の重み：";
            // 
            // txtTimeWeight
            // 
            this.txtTimeWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimeWeight.Location = new System.Drawing.Point(384, 85);
            this.txtTimeWeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtTimeWeight.Name = "txtTimeWeight";
            this.txtTimeWeight.Size = new System.Drawing.Size(50, 19);
            this.txtTimeWeight.TabIndex = 21;
            this.txtTimeWeight.TextChanged += new System.EventHandler(this.TxtWithTrackBar_TextChanged);
            // 
            // trbMinPlayTime
            // 
            this.trbMinPlayTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbMinPlayTime.LargeChange = 10;
            this.trbMinPlayTime.Location = new System.Drawing.Point(5, 15);
            this.trbMinPlayTime.Margin = new System.Windows.Forms.Padding(0);
            this.trbMinPlayTime.Maximum = 100;
            this.trbMinPlayTime.Name = "trbMinPlayTime";
            this.trbMinPlayTime.Size = new System.Drawing.Size(379, 45);
            this.trbMinPlayTime.TabIndex = 17;
            this.trbMinPlayTime.TickFrequency = 10;
            this.trbMinPlayTime.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbMinPlayTime.Scroll += new System.EventHandler(this.TrbMinPlayTime_Scroll);
            // 
            // trbTimeWeight
            // 
            this.trbTimeWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trbTimeWeight.LargeChange = 10;
            this.trbTimeWeight.Location = new System.Drawing.Point(5, 72);
            this.trbTimeWeight.Margin = new System.Windows.Forms.Padding(0);
            this.trbTimeWeight.Maximum = 100;
            this.trbTimeWeight.Name = "trbTimeWeight";
            this.trbTimeWeight.Size = new System.Drawing.Size(379, 45);
            this.trbTimeWeight.TabIndex = 20;
            this.trbTimeWeight.TickFrequency = 10;
            this.trbTimeWeight.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbTimeWeight.Scroll += new System.EventHandler(this.TrbTimeWeight_Scroll);
            // 
            // txtMinPlayTime
            // 
            this.txtMinPlayTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinPlayTime.Location = new System.Drawing.Point(384, 28);
            this.txtMinPlayTime.Margin = new System.Windows.Forms.Padding(0);
            this.txtMinPlayTime.Name = "txtMinPlayTime";
            this.txtMinPlayTime.Size = new System.Drawing.Size(50, 19);
            this.txtMinPlayTime.TabIndex = 18;
            this.txtMinPlayTime.TextChanged += new System.EventHandler(this.TxtWithTrackBar_TextChanged);
            // 
            // lblMinPlayTime
            // 
            this.lblMinPlayTime.AutoSize = true;
            this.lblMinPlayTime.Location = new System.Drawing.Point(8, 3);
            this.lblMinPlayTime.Name = "lblMinPlayTime";
            this.lblMinPlayTime.Size = new System.Drawing.Size(83, 12);
            this.lblMinPlayTime.TabIndex = 19;
            this.lblMinPlayTime.Text = "最低再生時間：";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.dgvExtension);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Controls.Add(this.txtTitlePattern);
            this.tabPage2.Controls.Add(this.lblTitlePattern);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(440, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "項目追加時の動作";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvExtension
            // 
            this.dgvExtension.AllowUserToAddRows = false;
            this.dgvExtension.AllowUserToDeleteRows = false;
            this.dgvExtension.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtension.Location = new System.Drawing.Point(8, 192);
            this.dgvExtension.Name = "dgvExtension";
            this.dgvExtension.RowTemplate.Height = 21;
            this.dgvExtension.Size = new System.Drawing.Size(426, 56);
            this.dgvExtension.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "拡張子ごとの設定：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 43);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(497, 131);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "<fullpath> : フルパス名",
            "<filename> : ファイル名と拡張子",
            "<filetitle> : 拡張子なしのファイル名",
            "<directory> : ファイルの場所"});
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(159, 125);
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.LstTitleTemplate_DoubleClick);
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Items.AddRange(new object[] {
            "<0> : ドライブ名",
            "<1> : 左から一番目のフォルダ名",
            "<2> : 左から二番目のフォルダ名",
            "<3> : 左から三番目のフォルダ名",
            "<4> : 左から四番目のフォルダ名",
            "<5> : 左から五番目のフォルダ名",
            "<6> : 左から六番目のフォルダ名",
            "<7> : 左から七番目のフォルダ名",
            "<8> : 左から八番目のフォルダ名",
            "<9> : 左から九番目のフォルダ名"});
            this.listBox2.Location = new System.Drawing.Point(168, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(159, 125);
            this.listBox2.TabIndex = 0;
            this.listBox2.DoubleClick += new System.EventHandler(this.LstTitleTemplate_DoubleClick);
            // 
            // listBox3
            // 
            this.listBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Items.AddRange(new object[] {
            "<-0> : ファイル名",
            "<-1> : 右から一番目のフォルダ名",
            "<-2> : 右から二番目のフォルダ名",
            "<-3> : 右から三番目のフォルダ名",
            "<-4> : 右から四番目のフォルダ名",
            "<-5> : 右から五番目のフォルダ名",
            "<-6> : 右から六番目のフォルダ名",
            "<-7> : 右から七番目のフォルダ名",
            "<-8> : 右から八番目のフォルダ名",
            "<-9> : 右から九番目のフォルダ名"});
            this.listBox3.Location = new System.Drawing.Point(333, 3);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(161, 125);
            this.listBox3.TabIndex = 0;
            this.listBox3.DoubleClick += new System.EventHandler(this.LstTitleTemplate_DoubleClick);
            // 
            // txtTitlePattern
            // 
            this.txtTitlePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitlePattern.Location = new System.Drawing.Point(8, 18);
            this.txtTitlePattern.Name = "txtTitlePattern";
            this.txtTitlePattern.Size = new System.Drawing.Size(497, 19);
            this.txtTitlePattern.TabIndex = 7;
            // 
            // lblTitlePattern
            // 
            this.lblTitlePattern.AutoSize = true;
            this.lblTitlePattern.Location = new System.Drawing.Point(6, 3);
            this.lblTitlePattern.Name = "lblTitlePattern";
            this.lblTitlePattern.Size = new System.Drawing.Size(92, 12);
            this.lblTitlePattern.TabIndex = 6;
            this.lblTitlePattern.Text = "タイトル付け規則：";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.btnDown);
            this.tabPage3.Controls.Add(this.btnUp);
            this.tabPage3.Controls.Add(this.clbStatusItem);
            this.tabPage3.Controls.Add(this.chkShowStatus);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(440, 254);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ステータスバー";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Location = new System.Drawing.Point(478, 61);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(27, 27);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Location = new System.Drawing.Point(478, 28);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(27, 27);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // clbStatusItem
            // 
            this.clbStatusItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbStatusItem.FormattingEnabled = true;
            this.clbStatusItem.Location = new System.Drawing.Point(6, 28);
            this.clbStatusItem.Name = "clbStatusItem";
            this.clbStatusItem.Size = new System.Drawing.Size(466, 200);
            this.clbStatusItem.TabIndex = 1;
            // 
            // chkShowStatus
            // 
            this.chkShowStatus.AutoSize = true;
            this.chkShowStatus.Location = new System.Drawing.Point(6, 6);
            this.chkShowStatus.Name = "chkShowStatus";
            this.chkShowStatus.Size = new System.Drawing.Size(67, 16);
            this.chkShowStatus.TabIndex = 0;
            this.chkShowStatus.Text = "表示する";
            this.chkShowStatus.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.btnRefLogFile);
            this.tabPage4.Controls.Add(this.txtLogFile);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.clbLog);
            this.tabPage4.Controls.Add(this.chkEraseLogOnExit);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(440, 254);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "ログ";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRefLogFile
            // 
            this.btnRefLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefLogFile.Location = new System.Drawing.Point(383, 6);
            this.btnRefLogFile.Name = "btnRefLogFile";
            this.btnRefLogFile.Size = new System.Drawing.Size(51, 19);
            this.btnRefLogFile.TabIndex = 2;
            this.btnRefLogFile.Text = "参照";
            this.btnRefLogFile.UseVisualStyleBackColor = true;
            this.btnRefLogFile.Click += new System.EventHandler(this.BtnRefLogFile_Click);
            // 
            // txtLogFile
            // 
            this.txtLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFile.Location = new System.Drawing.Point(69, 6);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(308, 19);
            this.txtLogFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ログファイル";
            // 
            // clbLog
            // 
            this.clbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbLog.FormattingEnabled = true;
            this.clbLog.Location = new System.Drawing.Point(6, 53);
            this.clbLog.Name = "clbLog";
            this.clbLog.Size = new System.Drawing.Size(428, 186);
            this.clbLog.TabIndex = 4;
            // 
            // chkEraseLogOnExit
            // 
            this.chkEraseLogOnExit.AutoSize = true;
            this.chkEraseLogOnExit.Location = new System.Drawing.Point(6, 31);
            this.chkEraseLogOnExit.Name = "chkEraseLogOnExit";
            this.chkEraseLogOnExit.Size = new System.Drawing.Size(144, 16);
            this.chkEraseLogOnExit.TabIndex = 3;
            this.chkEraseLogOnExit.Text = "正常終了時にログを削除";
            this.chkEraseLogOnExit.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.chkReportException);
            this.tabPage5.Controls.Add(this.chkIgnoreTimerError);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(440, 254);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "エラー処理";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // chkReportException
            // 
            this.chkReportException.AutoSize = true;
            this.chkReportException.Location = new System.Drawing.Point(6, 28);
            this.chkReportException.Name = "chkReportException";
            this.chkReportException.Size = new System.Drawing.Size(203, 16);
            this.chkReportException.TabIndex = 25;
            this.chkReportException.Text = "エラーに付随する例外情報を表示する";
            this.chkReportException.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreTimerError
            // 
            this.chkIgnoreTimerError.AutoSize = true;
            this.chkIgnoreTimerError.Location = new System.Drawing.Point(6, 6);
            this.chkIgnoreTimerError.Name = "chkIgnoreTimerError";
            this.chkIgnoreTimerError.Size = new System.Drawing.Size(186, 16);
            this.chkIgnoreTimerError.TabIndex = 24;
            this.chkIgnoreTimerError.Text = "タイマーで起こったエラーを無視する";
            this.chkIgnoreTimerError.UseVisualStyleBackColor = true;
            // 
            // FormSetting
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(472, 333);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbMinPlayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTimeWeight)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtension)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTimeWeight;
        private System.Windows.Forms.TextBox txtTimeWeight;
        private System.Windows.Forms.TrackBar trbMinPlayTime;
        private System.Windows.Forms.TrackBar trbTimeWeight;
        private System.Windows.Forms.TextBox txtMinPlayTime;
        private System.Windows.Forms.Label lblMinPlayTime;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtTitlePattern;
        private System.Windows.Forms.Label lblTitlePattern;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkShowStatus;
        private System.Windows.Forms.CheckedListBox clbStatusItem;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkEraseLogOnExit;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbLog;
        private System.Windows.Forms.Button btnRefLogFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox chkIgnoreTimerError;
        private System.Windows.Forms.CheckBox chkReportException;
        private System.Windows.Forms.TextBox txtWindowTitlePattern;
        private System.Windows.Forms.Label lblWindowTitlePattern;
        private System.Windows.Forms.ComboBox cmbWindowTitlePattern;
        private System.Windows.Forms.Button btnWindowTitlePattern;
        private System.Windows.Forms.DataGridView dgvExtension;
        private System.Windows.Forms.Label label2;
    }
}