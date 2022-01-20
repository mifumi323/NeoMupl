namespace NeoMupl
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lstMusic = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playRandomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.finishActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishActionStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishActionReplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishActionNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishActionPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishActionRandomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tempoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortFolderNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortLastPlayedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ofdMusicFiles = new System.Windows.Forms.OpenFileDialog();
            this.ofdListFile = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstMusic
            // 
            this.lstMusic.AllowDrop = true;
            this.lstMusic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMusic.ContextMenuStrip = this.contextMenuStrip1;
            this.lstMusic.FormattingEnabled = true;
            this.lstMusic.ItemHeight = 12;
            this.lstMusic.Location = new System.Drawing.Point(0, 24);
            this.lstMusic.Name = "lstMusic";
            this.lstMusic.Size = new System.Drawing.Size(472, 280);
            this.lstMusic.TabIndex = 0;
            this.lstMusic.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.lstMusic.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.lstMusic.DoubleClick += new System.EventHandler(this.LstMusic_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem1,
            this.loopToolStripMenuItem1,
            this.stopToolStripMenuItem1,
            this.toolStripMenuItem9,
            this.propertyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 98);
            // 
            // playToolStripMenuItem1
            // 
            this.playToolStripMenuItem1.Name = "playToolStripMenuItem1";
            this.playToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.playToolStripMenuItem1.Text = "再生";
            this.playToolStripMenuItem1.Click += new System.EventHandler(this.PlaySelectedToolStripMenuItem_Click);
            // 
            // loopToolStripMenuItem1
            // 
            this.loopToolStripMenuItem1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.loopToolStripMenuItem1.Name = "loopToolStripMenuItem1";
            this.loopToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.loopToolStripMenuItem1.Text = "ループ再生";
            this.loopToolStripMenuItem1.Click += new System.EventHandler(this.LoopToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem1
            // 
            this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
            this.stopToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.stopToolStripMenuItem1.Text = "停止";
            this.stopToolStripMenuItem1.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(127, 6);
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.propertyToolStripMenuItem.Text = "プロパティ";
            this.propertyToolStripMenuItem.Click += new System.EventHandler(this.ItemPropertiesToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.playToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(472, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.menuStrip1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openListToolStripMenuItem,
            this.saveListToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addFilesToolStripMenuItem,
            this.removeItemToolStripMenuItem,
            this.toolStripMenuItem2,
            this.itemPropertiesToolStripMenuItem,
            this.listPropertiesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.optionToolStripMenuItem,
            this.saveOptionToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem,
            this.rebootToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.fileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // openListToolStripMenuItem
            // 
            this.openListToolStripMenuItem.Name = "openListToolStripMenuItem";
            this.openListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openListToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.openListToolStripMenuItem.Text = "リストを開く";
            this.openListToolStripMenuItem.Click += new System.EventHandler(this.OpenListToolStripMenuItem_Click);
            // 
            // saveListToolStripMenuItem
            // 
            this.saveListToolStripMenuItem.Name = "saveListToolStripMenuItem";
            this.saveListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveListToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveListToolStripMenuItem.Text = "リストを保存";
            this.saveListToolStripMenuItem.Click += new System.EventHandler(this.SaveListToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(174, 6);
            // 
            // addFilesToolStripMenuItem
            // 
            this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
            this.addFilesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.addFilesToolStripMenuItem.Text = "項目を追加";
            this.addFilesToolStripMenuItem.Click += new System.EventHandler(this.AddFilesToolStripMenuItem_Click);
            // 
            // removeItemToolStripMenuItem
            // 
            this.removeItemToolStripMenuItem.Name = "removeItemToolStripMenuItem";
            this.removeItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeItemToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.removeItemToolStripMenuItem.Text = "項目の削除";
            this.removeItemToolStripMenuItem.Click += new System.EventHandler(this.RemoveItemToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(174, 6);
            // 
            // itemPropertiesToolStripMenuItem
            // 
            this.itemPropertiesToolStripMenuItem.Name = "itemPropertiesToolStripMenuItem";
            this.itemPropertiesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.itemPropertiesToolStripMenuItem.Text = "項目のプロパティ";
            this.itemPropertiesToolStripMenuItem.Click += new System.EventHandler(this.ItemPropertiesToolStripMenuItem_Click);
            // 
            // listPropertiesToolStripMenuItem
            // 
            this.listPropertiesToolStripMenuItem.Name = "listPropertiesToolStripMenuItem";
            this.listPropertiesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.listPropertiesToolStripMenuItem.Text = "リスト全体のプロパティ";
            this.listPropertiesToolStripMenuItem.Visible = false;
            this.listPropertiesToolStripMenuItem.Click += new System.EventHandler(this.ListPropertiesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(174, 6);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.optionToolStripMenuItem.Text = "NeoMuplの設定";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.OptionToolStripMenuItem_Click);
            // 
            // saveOptionToolStripMenuItem
            // 
            this.saveOptionToolStripMenuItem.Name = "saveOptionToolStripMenuItem";
            this.saveOptionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveOptionToolStripMenuItem.Text = "今すぐ設定を保存";
            this.saveOptionToolStripMenuItem.Click += new System.EventHandler(this.SaveOptionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(174, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "終了";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // rebootToolStripMenuItem
            // 
            this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
            this.rebootToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.rebootToolStripMenuItem.Text = "NeoMuplを再起動";
            this.rebootToolStripMenuItem.Click += new System.EventHandler(this.RebootToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playSelectedToolStripMenuItem,
            this.playNextToolStripMenuItem,
            this.playPreviousToolStripMenuItem,
            this.playRandomToolStripMenuItem,
            this.loopToolStripMenuItem,
            this.toolStripMenuItem5,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem6,
            this.finishActionToolStripMenuItem,
            this.toolStripMenuItem8,
            this.tempoToolStripMenuItem,
            this.portToolStripMenuItem});
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.playToolStripMenuItem.Text = "再生(&P)";
            // 
            // playSelectedToolStripMenuItem
            // 
            this.playSelectedToolStripMenuItem.Name = "playSelectedToolStripMenuItem";
            this.playSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.playSelectedToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.playSelectedToolStripMenuItem.Text = "現在の選択項目";
            this.playSelectedToolStripMenuItem.Click += new System.EventHandler(this.PlaySelectedToolStripMenuItem_Click);
            // 
            // playNextToolStripMenuItem
            // 
            this.playNextToolStripMenuItem.Name = "playNextToolStripMenuItem";
            this.playNextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.playNextToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.playNextToolStripMenuItem.Text = "現在の選択項目の次";
            this.playNextToolStripMenuItem.Click += new System.EventHandler(this.PlayNextToolStripMenuItem_Click);
            // 
            // playPreviousToolStripMenuItem
            // 
            this.playPreviousToolStripMenuItem.Name = "playPreviousToolStripMenuItem";
            this.playPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.playPreviousToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.playPreviousToolStripMenuItem.Text = "現在の選択項目の前";
            this.playPreviousToolStripMenuItem.Click += new System.EventHandler(this.PlayPreviousToolStripMenuItem_Click);
            // 
            // playRandomToolStripMenuItem
            // 
            this.playRandomToolStripMenuItem.Name = "playRandomToolStripMenuItem";
            this.playRandomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.playRandomToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.playRandomToolStripMenuItem.Text = "ランダム";
            this.playRandomToolStripMenuItem.Click += new System.EventHandler(this.PlayRandomToolStripMenuItem_Click);
            // 
            // loopToolStripMenuItem
            // 
            this.loopToolStripMenuItem.Name = "loopToolStripMenuItem";
            this.loopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loopToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.loopToolStripMenuItem.Text = "ループ再生";
            this.loopToolStripMenuItem.Click += new System.EventHandler(this.LoopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(226, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.stopToolStripMenuItem.Text = "停止";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(226, 6);
            // 
            // finishActionToolStripMenuItem
            // 
            this.finishActionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishActionStopToolStripMenuItem,
            this.finishActionReplayToolStripMenuItem,
            this.finishActionNextToolStripMenuItem,
            this.finishActionPreviousToolStripMenuItem,
            this.finishActionRandomToolStripMenuItem});
            this.finishActionToolStripMenuItem.Name = "finishActionToolStripMenuItem";
            this.finishActionToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.finishActionToolStripMenuItem.Text = "再生終了後の処理";
            // 
            // finishActionStopToolStripMenuItem
            // 
            this.finishActionStopToolStripMenuItem.Name = "finishActionStopToolStripMenuItem";
            this.finishActionStopToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.finishActionStopToolStripMenuItem.Text = "ストップ";
            this.finishActionStopToolStripMenuItem.Click += new System.EventHandler(this.FinishActionStopToolStripMenuItem_Click);
            // 
            // finishActionReplayToolStripMenuItem
            // 
            this.finishActionReplayToolStripMenuItem.Name = "finishActionReplayToolStripMenuItem";
            this.finishActionReplayToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.finishActionReplayToolStripMenuItem.Text = "選択項目を再生";
            this.finishActionReplayToolStripMenuItem.Click += new System.EventHandler(this.FinishActionReplayToolStripMenuItem_Click);
            // 
            // finishActionNextToolStripMenuItem
            // 
            this.finishActionNextToolStripMenuItem.Name = "finishActionNextToolStripMenuItem";
            this.finishActionNextToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.finishActionNextToolStripMenuItem.Text = "選択項目の次を再生";
            this.finishActionNextToolStripMenuItem.Click += new System.EventHandler(this.FinishActionNextToolStripMenuItem_Click);
            // 
            // finishActionPreviousToolStripMenuItem
            // 
            this.finishActionPreviousToolStripMenuItem.Name = "finishActionPreviousToolStripMenuItem";
            this.finishActionPreviousToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.finishActionPreviousToolStripMenuItem.Text = "選択項目の前を再生";
            this.finishActionPreviousToolStripMenuItem.Click += new System.EventHandler(this.FinishActionPreviousToolStripMenuItem_Click);
            // 
            // finishActionRandomToolStripMenuItem
            // 
            this.finishActionRandomToolStripMenuItem.Name = "finishActionRandomToolStripMenuItem";
            this.finishActionRandomToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.finishActionRandomToolStripMenuItem.Text = "ランダム再生";
            this.finishActionRandomToolStripMenuItem.Click += new System.EventHandler(this.FinishActionRandomToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(226, 6);
            // 
            // tempoToolStripMenuItem
            // 
            this.tempoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem15});
            this.tempoToolStripMenuItem.Name = "tempoToolStripMenuItem";
            this.tempoToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.tempoToolStripMenuItem.Text = "再生速度";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem10.Text = "25%";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem11.Text = "50%";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem12.Text = "75%";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem13.Text = "100%";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem14.Text = "150%";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem15.Text = "200%";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.TempoToolStripMenuItem_Click);
            // 
            // portToolStripMenuItem
            // 
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            this.portToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.portToolStripMenuItem.Text = "MIDIポート";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.viewToolStripMenuItem.Text = "表示(&V)";
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortFileNameToolStripMenuItem,
            this.sortFolderNameToolStripMenuItem,
            this.sortTitleToolStripMenuItem,
            this.sortLastPlayedToolStripMenuItem,
            this.toolStripMenuItem7,
            this.reverseToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.sortToolStripMenuItem.Text = "並び替え(&S)";
            // 
            // sortFileNameToolStripMenuItem
            // 
            this.sortFileNameToolStripMenuItem.Name = "sortFileNameToolStripMenuItem";
            this.sortFileNameToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sortFileNameToolStripMenuItem.Text = "ファイル名順(&F)";
            this.sortFileNameToolStripMenuItem.Click += new System.EventHandler(this.SortFileNameToolStripMenuItem_Click);
            // 
            // sortFolderNameToolStripMenuItem
            // 
            this.sortFolderNameToolStripMenuItem.Name = "sortFolderNameToolStripMenuItem";
            this.sortFolderNameToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sortFolderNameToolStripMenuItem.Text = "フォルダ順(&D)";
            this.sortFolderNameToolStripMenuItem.Click += new System.EventHandler(this.SortFolderNameToolStripMenuItem_Click);
            // 
            // sortTitleToolStripMenuItem
            // 
            this.sortTitleToolStripMenuItem.Name = "sortTitleToolStripMenuItem";
            this.sortTitleToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sortTitleToolStripMenuItem.Text = "タイトル順(&T)";
            this.sortTitleToolStripMenuItem.Click += new System.EventHandler(this.SortTitleToolStripMenuItem_Click);
            // 
            // sortLastPlayedToolStripMenuItem
            // 
            this.sortLastPlayedToolStripMenuItem.Name = "sortLastPlayedToolStripMenuItem";
            this.sortLastPlayedToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sortLastPlayedToolStripMenuItem.Text = "再生された日時順(L)";
            this.sortLastPlayedToolStripMenuItem.Click += new System.EventHandler(this.SortLastPlayedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(173, 6);
            // 
            // reverseToolStripMenuItem
            // 
            this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
            this.reverseToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.reverseToolStripMenuItem.Text = "逆順(&R)";
            this.reverseToolStripMenuItem.Click += new System.EventHandler(this.ReverseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutToolStripMenuItem.Text = "バージョン情報(&A)...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowDrop = true;
            this.statusStrip1.Location = new System.Drawing.Point(0, 311);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(472, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.statusStrip1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // ofdMusicFiles
            // 
            this.ofdMusicFiles.Filter = "全ての音声ファイル|*.wav;*.mp3;*.wma;*.mid|全てのファイル|*.*";
            this.ofdMusicFiles.Multiselect = true;
            this.ofdMusicFiles.Title = "音声ファイルの追加";
            // 
            // ofdListFile
            // 
            this.ofdListFile.CheckFileExists = false;
            this.ofdListFile.Filter = "NeoMuplリスト|*.nmp|全てのファイル|*.*";
            this.ofdListFile.Title = "リストを開く";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 333);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lstMusic);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NeoMupl";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.FormMain_Layout);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstMusic;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem itemPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playRandomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem finishActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishActionStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishActionReplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishActionNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishActionPreviousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishActionRandomToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortLastPlayedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem tempoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdMusicFiles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdListFile;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortFolderNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveOptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
    }
}

