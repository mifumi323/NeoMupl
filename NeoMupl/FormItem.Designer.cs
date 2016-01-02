namespace NeoMupl
{
    partial class FormItem
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
            this.txtNavigation = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMIDIPort = new System.Windows.Forms.Label();
            this.cmbMIDIPort = new System.Windows.Forms.ComboBox();
            this.btnPlayMethod = new System.Windows.Forms.Button();
            this.lblPlayMethod = new System.Windows.Forms.Label();
            this.txtSkipRate = new System.Windows.Forms.TextBox();
            this.trbSkipRate = new System.Windows.Forms.TrackBar();
            this.lblSkipRate = new System.Windows.Forms.Label();
            this.lblLoopUnit = new System.Windows.Forms.Label();
            this.txtLoop2 = new System.Windows.Forms.TextBox();
            this.lblLoopTo = new System.Windows.Forms.Label();
            this.txtLoop1 = new System.Windows.Forms.TextBox();
            this.lblLoop = new System.Windows.Forms.Label();
            this.btnTitle = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFileName = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.trbVolume = new System.Windows.Forms.TrackBar();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.cmbPlayMethod = new System.Windows.Forms.ComboBox();
            this.lblLastPlayed = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbSkipRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(394, 270);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(469, 270);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtNavigation
            // 
            this.txtNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNavigation.Location = new System.Drawing.Point(0, 270);
            this.txtNavigation.Margin = new System.Windows.Forms.Padding(0);
            this.txtNavigation.Multiline = true;
            this.txtNavigation.Name = "txtNavigation";
            this.txtNavigation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNavigation.Size = new System.Drawing.Size(394, 30);
            this.txtNavigation.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 270);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblMIDIPort, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cmbMIDIPort, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnPlayMethod, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblPlayMethod, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtSkipRate, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.trbSkipRate, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblSkipRate, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblLoopUnit, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLoop2, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblLoopTo, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtLoop1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblLoop, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnTitle, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtTitle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblFileName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFileName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFileName, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblVolume, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trbVolume, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtVolume, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbPlayMethod, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblLastPlayed, 0, 7);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(543, 199);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblMIDIPort
            // 
            this.lblMIDIPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMIDIPort.AutoSize = true;
            this.lblMIDIPort.Location = new System.Drawing.Point(3, 165);
            this.lblMIDIPort.Name = "lblMIDIPort";
            this.lblMIDIPort.Size = new System.Drawing.Size(71, 12);
            this.lblMIDIPort.TabIndex = 1;
            this.lblMIDIPort.Text = "MIDIポート(&P)";
            // 
            // cmbMIDIPort
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbMIDIPort, 3);
            this.cmbMIDIPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMIDIPort.FormattingEnabled = true;
            this.cmbMIDIPort.Location = new System.Drawing.Point(77, 161);
            this.cmbMIDIPort.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMIDIPort.Name = "cmbMIDIPort";
            this.cmbMIDIPort.Size = new System.Drawing.Size(415, 20);
            this.cmbMIDIPort.TabIndex = 2;
            this.cmbMIDIPort.Enter += new System.EventHandler(this.cmbMIDIPort_Enter);
            // 
            // btnPlayMethod
            // 
            this.btnPlayMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlayMethod.Location = new System.Drawing.Point(492, 141);
            this.btnPlayMethod.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlayMethod.Name = "btnPlayMethod";
            this.btnPlayMethod.Size = new System.Drawing.Size(51, 20);
            this.btnPlayMethod.TabIndex = 19;
            this.btnPlayMethod.Text = "自動";
            this.btnPlayMethod.UseVisualStyleBackColor = true;
            this.btnPlayMethod.Click += new System.EventHandler(this.btnPlayMethod_Click);
            this.btnPlayMethod.Enter += new System.EventHandler(this.btnPlayMethod_Enter);
            // 
            // lblPlayMethod
            // 
            this.lblPlayMethod.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlayMethod.AutoSize = true;
            this.lblPlayMethod.Location = new System.Drawing.Point(3, 145);
            this.lblPlayMethod.Margin = new System.Windows.Forms.Padding(0);
            this.lblPlayMethod.Name = "lblPlayMethod";
            this.lblPlayMethod.Size = new System.Drawing.Size(70, 12);
            this.lblPlayMethod.TabIndex = 17;
            this.lblPlayMethod.Text = "再生方法(&M)";
            // 
            // txtSkipRate
            // 
            this.txtSkipRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSkipRate.Location = new System.Drawing.Point(492, 110);
            this.txtSkipRate.Margin = new System.Windows.Forms.Padding(0);
            this.txtSkipRate.Name = "txtSkipRate";
            this.txtSkipRate.Size = new System.Drawing.Size(51, 19);
            this.txtSkipRate.TabIndex = 16;
            this.txtSkipRate.TextChanged += new System.EventHandler(this.txtWithTrackBar_TextChanged);
            this.txtSkipRate.Enter += new System.EventHandler(this.trbSkipRate_Enter);
            // 
            // trbSkipRate
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.trbSkipRate, 3);
            this.trbSkipRate.LargeChange = 10;
            this.trbSkipRate.Location = new System.Drawing.Point(77, 99);
            this.trbSkipRate.Margin = new System.Windows.Forms.Padding(0);
            this.trbSkipRate.Maximum = 100;
            this.trbSkipRate.Name = "trbSkipRate";
            this.trbSkipRate.Size = new System.Drawing.Size(415, 42);
            this.trbSkipRate.TabIndex = 15;
            this.trbSkipRate.TickFrequency = 10;
            this.trbSkipRate.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbSkipRate.Scroll += new System.EventHandler(this.trbSkipRate_Scroll);
            this.trbSkipRate.Enter += new System.EventHandler(this.trbSkipRate_Enter);
            // 
            // lblSkipRate
            // 
            this.lblSkipRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSkipRate.AutoSize = true;
            this.lblSkipRate.Location = new System.Drawing.Point(2, 114);
            this.lblSkipRate.Margin = new System.Windows.Forms.Padding(0);
            this.lblSkipRate.Name = "lblSkipRate";
            this.lblSkipRate.Size = new System.Drawing.Size(73, 12);
            this.lblSkipRate.TabIndex = 14;
            this.lblSkipRate.Text = "スキップ率%(&S)";
            // 
            // lblLoopUnit
            // 
            this.lblLoopUnit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoopUnit.AutoSize = true;
            this.lblLoopUnit.Location = new System.Drawing.Point(503, 83);
            this.lblLoopUnit.Margin = new System.Windows.Forms.Padding(0);
            this.lblLoopUnit.Name = "lblLoopUnit";
            this.lblLoopUnit.Size = new System.Drawing.Size(29, 12);
            this.lblLoopUnit.TabIndex = 13;
            this.lblLoopUnit.Text = "単位";
            // 
            // txtLoop2
            // 
            this.txtLoop2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoop2.Location = new System.Drawing.Point(293, 80);
            this.txtLoop2.Margin = new System.Windows.Forms.Padding(0);
            this.txtLoop2.Name = "txtLoop2";
            this.txtLoop2.Size = new System.Drawing.Size(199, 19);
            this.txtLoop2.TabIndex = 12;
            this.txtLoop2.Enter += new System.EventHandler(this.txtLoop1_Enter);
            // 
            // lblLoopTo
            // 
            this.lblLoopTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoopTo.AutoSize = true;
            this.lblLoopTo.Location = new System.Drawing.Point(276, 83);
            this.lblLoopTo.Margin = new System.Windows.Forms.Padding(0);
            this.lblLoopTo.Name = "lblLoopTo";
            this.lblLoopTo.Size = new System.Drawing.Size(17, 12);
            this.lblLoopTo.TabIndex = 11;
            this.lblLoopTo.Text = "～";
            // 
            // txtLoop1
            // 
            this.txtLoop1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLoop1.Location = new System.Drawing.Point(77, 80);
            this.txtLoop1.Margin = new System.Windows.Forms.Padding(0);
            this.txtLoop1.Name = "txtLoop1";
            this.txtLoop1.Size = new System.Drawing.Size(199, 19);
            this.txtLoop1.TabIndex = 10;
            this.txtLoop1.Enter += new System.EventHandler(this.txtLoop1_Enter);
            // 
            // lblLoop
            // 
            this.lblLoop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoop.AutoSize = true;
            this.lblLoop.Location = new System.Drawing.Point(2, 83);
            this.lblLoop.Margin = new System.Windows.Forms.Padding(0);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(72, 12);
            this.lblLoop.TabIndex = 9;
            this.lblLoop.Text = "ループ位置(&L)";
            // 
            // btnTitle
            // 
            this.btnTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTitle.Location = new System.Drawing.Point(492, 19);
            this.btnTitle.Margin = new System.Windows.Forms.Padding(0);
            this.btnTitle.Name = "btnTitle";
            this.btnTitle.Size = new System.Drawing.Size(51, 19);
            this.btnTitle.TabIndex = 5;
            this.btnTitle.Text = "読込";
            this.btnTitle.UseVisualStyleBackColor = true;
            this.btnTitle.Click += new System.EventHandler(this.btnTitle_Click);
            this.btnTitle.Enter += new System.EventHandler(this.btnTitle_Enter);
            // 
            // txtTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtTitle, 3);
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(77, 19);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(415, 19);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.Enter += new System.EventHandler(this.txtTitle_Enter);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(11, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(55, 12);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "タイトル(&T)";
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(5, 3);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(66, 12);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "ファイル名(&F)";
            // 
            // txtFileName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtFileName, 3);
            this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.Location = new System.Drawing.Point(77, 0);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(0);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(415, 19);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Enter += new System.EventHandler(this.txtFileName_Enter);
            // 
            // btnFileName
            // 
            this.btnFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFileName.Location = new System.Drawing.Point(492, 0);
            this.btnFileName.Margin = new System.Windows.Forms.Padding(0);
            this.btnFileName.Name = "btnFileName";
            this.btnFileName.Size = new System.Drawing.Size(51, 19);
            this.btnFileName.TabIndex = 2;
            this.btnFileName.Text = "参照";
            this.btnFileName.UseVisualStyleBackColor = true;
            this.btnFileName.Click += new System.EventHandler(this.btnFileName_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(13, 53);
            this.lblVolume.Margin = new System.Windows.Forms.Padding(0);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(51, 12);
            this.lblVolume.TabIndex = 6;
            this.lblVolume.Text = "音量%(&V)";
            // 
            // trbVolume
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.trbVolume, 3);
            this.trbVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbVolume.LargeChange = 10;
            this.trbVolume.Location = new System.Drawing.Point(77, 38);
            this.trbVolume.Margin = new System.Windows.Forms.Padding(0);
            this.trbVolume.Maximum = 100;
            this.trbVolume.Name = "trbVolume";
            this.trbVolume.Size = new System.Drawing.Size(415, 42);
            this.trbVolume.TabIndex = 7;
            this.trbVolume.TickFrequency = 10;
            this.trbVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbVolume.Scroll += new System.EventHandler(this.trbVolume_Scroll);
            this.trbVolume.Enter += new System.EventHandler(this.trbVolume_Enter);
            // 
            // txtVolume
            // 
            this.txtVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVolume.Location = new System.Drawing.Point(492, 49);
            this.txtVolume.Margin = new System.Windows.Forms.Padding(0);
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(51, 19);
            this.txtVolume.TabIndex = 8;
            this.txtVolume.TextChanged += new System.EventHandler(this.txtWithTrackBar_TextChanged);
            this.txtVolume.Enter += new System.EventHandler(this.trbVolume_Enter);
            // 
            // cmbPlayMethod
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbPlayMethod, 3);
            this.cmbPlayMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPlayMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlayMethod.FormattingEnabled = true;
            this.cmbPlayMethod.Items.AddRange(new object[] {
            "DirectShow(通常のファイル)",
            "DirectMusic(MIDIに最適)",
            "MCI(うまく再生されないときに)"});
            this.cmbPlayMethod.Location = new System.Drawing.Point(77, 141);
            this.cmbPlayMethod.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPlayMethod.Name = "cmbPlayMethod";
            this.cmbPlayMethod.Size = new System.Drawing.Size(415, 20);
            this.cmbPlayMethod.TabIndex = 18;
            this.cmbPlayMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPlayMethod_SelectedIndexChanged);
            this.cmbPlayMethod.Enter += new System.EventHandler(this.cmbPlayMethod_Enter);
            // 
            // lblLastPlayed
            // 
            this.lblLastPlayed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLastPlayed.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblLastPlayed, 5);
            this.lblLastPlayed.Location = new System.Drawing.Point(211, 184);
            this.lblLastPlayed.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lblLastPlayed.Name = "lblLastPlayed";
            this.lblLastPlayed.Size = new System.Drawing.Size(120, 12);
            this.lblLastPlayed.TabIndex = 20;
            this.lblLastPlayed.Text = "最後に再生された日時：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormItem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(544, 300);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtNavigation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimizeBox = false;
            this.Name = "FormItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormItem";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbSkipRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtNavigation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFileName;
        private System.Windows.Forms.Button btnTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trbVolume;
        private System.Windows.Forms.Label lblLoop;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label lblLoopUnit;
        private System.Windows.Forms.TextBox txtLoop2;
        private System.Windows.Forms.Label lblLoopTo;
        private System.Windows.Forms.TextBox txtLoop1;
        private System.Windows.Forms.TextBox txtSkipRate;
        private System.Windows.Forms.TrackBar trbSkipRate;
        private System.Windows.Forms.Label lblSkipRate;
        private System.Windows.Forms.Label lblPlayMethod;
        private System.Windows.Forms.ComboBox cmbPlayMethod;
        private System.Windows.Forms.Button btnPlayMethod;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblLastPlayed;
        private System.Windows.Forms.ComboBox cmbMIDIPort;
        private System.Windows.Forms.Label lblMIDIPort;
    }
}