namespace NeoMupl
{
    partial class FormFatalError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFatalError));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPlayList = new System.Windows.Forms.ComboBox();
            this.cmbSetting = new System.Windows.Forms.ComboBox();
            this.cmbProgram = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NeoMupl.Properties.Resources.Error;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(51, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "想定外のエラーが発生しました。";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(12, 50);
            this.txtMessage.MaxLength = 2147483647;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(466, 163);
            this.txtMessage.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.cmbPlayList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbSetting, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbProgram, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 219);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(466, 52);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // cmbPlayList
            // 
            this.cmbPlayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPlayList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlayList.FormattingEnabled = true;
            this.cmbPlayList.Items.AddRange(new object[] {
            "再生リストを保存しない",
            "再生リストを上書き保存する",
            "再生リストを別名で保存する（推奨）"});
            this.cmbPlayList.Location = new System.Drawing.Point(3, 3);
            this.cmbPlayList.Name = "cmbPlayList";
            this.cmbPlayList.Size = new System.Drawing.Size(197, 20);
            this.cmbPlayList.TabIndex = 2;
            // 
            // cmbSetting
            // 
            this.cmbSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetting.FormattingEnabled = true;
            this.cmbSetting.Items.AddRange(new object[] {
            "設定を保存しない",
            "設定を上書き保存する",
            "設定を別名で保存する（推奨）"});
            this.cmbSetting.Location = new System.Drawing.Point(206, 3);
            this.cmbSetting.Name = "cmbSetting";
            this.cmbSetting.Size = new System.Drawing.Size(197, 20);
            this.cmbSetting.TabIndex = 3;
            // 
            // cmbProgram
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbProgram, 2);
            this.cmbProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProgram.FormattingEnabled = true;
            this.cmbProgram.Items.AddRange(new object[] {
            "エラーを無視し、プログラムを続行する（非推奨）",
            "エラーを無視し、正常終了を試みる",
            "エラーとして終了する（推奨）"});
            this.cmbProgram.Location = new System.Drawing.Point(3, 29);
            this.cmbProgram.Name = "cmbProgram";
            this.cmbProgram.Size = new System.Drawing.Size(400, 20);
            this.cmbProgram.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(409, 3);
            this.btnOK.Name = "btnOK";
            this.tableLayoutPanel1.SetRowSpan(this.btnOK, 2);
            this.btnOK.Size = new System.Drawing.Size(54, 46);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FormFatalError
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 283);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormFatalError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NeoMupl 想定外のエラー";
            this.Load += new System.EventHandler(this.FormFatalError_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFatalError_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbPlayList;
        private System.Windows.Forms.ComboBox cmbSetting;
        private System.Windows.Forms.ComboBox cmbProgram;
        private System.Windows.Forms.Button btnOK;
    }
}