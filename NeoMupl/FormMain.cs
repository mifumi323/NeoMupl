#nullable enable
using System;
using System.IO;
using System.Windows.Forms;
using MifuminLib;
using NeoMupl.Player;

namespace NeoMupl
{
    public enum FinishAction { Stop, Replay, Next, Previous, Random }
    public enum Sorting { FileName, Title, LastPlayed, Directory }

    public partial class FormMain : Form, IErrorNotifier
    {
        MusicList musicList;
        readonly MusicController musicController;
        Comparison<MusicData> comparison;
        enum DirtyLevel
        {
            None,
            SelectedIndex,
            ListItem,
            ListCount,
        }
        DirtyLevel dirty = DirtyLevel.None;
        int lastRemoved = int.MaxValue;

        delegate void PlayMethod();
        PlayMethod playMethod;
        DateTime lastPlayed = DateTime.Now;
        readonly Setting setting;

        readonly Random random = new Random();

        bool terminated = false;

        enum Status
        {
            Stopped,
            Reading,
            Preparing,
            Playing,
            LoopPlaying,
            ReadError,
            PlayError,
        }
        Status statusValue;
        MusicData? statusData = null;

        #region 初期化処理

        public FormMain()
        {
            InitializeComponent();

            // 起動後即座に初期化処理でnewされるけど初期化処理前に使用されるケースが残っているためここでインスタンスを作っておく
            musicList = new MusicList();

            // 初期化処理
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Log.setting = setting = new Setting();
            musicController = new MusicController();
            comparison = CompareFileName;
            playMethod = PlaySelected;
            MachineLocked += FormMain_MachineLocked;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (setting.MainWidth <= 0) setting.MainWidth = Width;
            if (setting.MainHeight <= 0) setting.MainHeight = Height;
            SetDesktopBounds(setting.MainLeft, setting.MainTop, setting.MainWidth, setting.MainHeight);
            WindowState = setting.MainWindowState;
            SetFinishAction(setting.FinishAction);
            SetSorting(setting.Sorting);
            reverseToolStripMenuItem.Checked = setting.Reversed;
            DMOption.portdefault = setting.Port;
            foreach (string port in musicController.GetDirectMusicPorts())
            {
                ToolStripMenuItem item = new ToolStripMenuItem(port, null, new EventHandler(PortToolStripMenuItem_Click))
                {
                    Checked = port == setting.Port
                };
                portToolStripMenuItem.DropDownItems.Add(item);
            }
            UpdateStatusBar();
            CreateList();
            timer1.Start();
            Log.errorNotifier = this;
        }

        private void CreateList()
        {
            musicList = new MusicList();
            musicList.Load(setting.ListFile);
            UpdateList(DirtyLevel.ListCount);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Terminate();
        }

        private void Terminate()
        {
            if (terminated)
            {
                return;
            }
            terminated = true;
            musicController.Dispose();
            musicList.Save(setting.ListFile);
            setting.MainWindowState = WindowState;
            WindowState = FormWindowState.Normal;
            setting.MainLeft = DesktopBounds.X;
            setting.MainTop = DesktopBounds.Y;
            setting.MainWidth = DesktopBounds.Width;
            setting.MainHeight = DesktopBounds.Height;
            setting.Save();
            if (setting.EraseLogOnExit && File.Exists(setting.LogFile))
                File.Delete(setting.LogFile);
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            FormFatalError.Result result = new FormFatalError.Result();
            try
            {
                if (setting.ErrorLog)
                    Log.Write(Log.LogType.FatalError, $"想定外のエラーです。\n\n{e.Exception}");
                FormFatalError ff = new FormFatalError(e.Exception, result);
                ff.ShowDialog(this);
                switch (result.PlayList)
                {
                    case 1:
                        // 上書き保存
                        musicList.Save(setting.ListFile);
                        break;
                    case 2:
                        // 別名保存
                        musicList.Save(PathMaker.GetAnotherName(setting.ListFile));
                        break;
                }
                switch (result.Setting)
                {
                    case 1:
                        // 上書き保存
                        setting.Save();
                        break;
                    case 2:
                        // 別名保存
                        setting.SaveAnotherName();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (setting != null && setting.ErrorLog)
                    Log.Write(Log.LogType.FatalError, "エラー処理中にエラーが発生しました。\n\n" + ex.ToString());
                throw;
            }
            switch (result.Program)
            {
                case 1:
                    // 正常終了
                    Application.Exit();
                    break;
                case 2:
                    // エラー終了
                    throw e.Exception;
            }
        }

        #endregion

        #region 各種再生メソッド

        private void PlayStop()
        {
            if (musicController.Data == null) return;
            if (setting.StopLog) Log.Write(Log.LogType.Stop, musicController.Data.FileName);
            UpdateCaption(Status.Stopped, musicController.Data);
            musicController.Data = null;
        }
        private void PlaySelected()
        {
            Play(false);
        }
        private void PlayNext()
        {
            if (lstMusic.Items.Count == 0) return;
            if (lstMusic.SelectedIndex < lstMusic.Items.Count - 1) lstMusic.SelectedIndex++;
            else lstMusic.SelectedIndex = 0;
            PlaySelected();
        }
        private void PlayPrevious()
        {
            if (lstMusic.Items.Count == 0) return;
            if (lstMusic.SelectedIndex > 0) lstMusic.SelectedIndex--;
            else lstMusic.SelectedIndex = lstMusic.Items.Count - 1;
            PlaySelected();
        }
        private void PlayRandom()
        {
            if (lstMusic.Items.Count == 0) return;
            long lt = 0, et = DateTime.Now.Ticks;
            foreach (object var in lstMusic.Items)
            {
                MusicData m = (MusicData)var;
                if (m.LastPlayedTicks > lt) lt = m.LastPlayedTicks;
                if (m.LastPlayedTicks < et) et = m.LastPlayedTicks;
            }
            double sum = 0.0;
            double timeRate = setting.TimeWeight;
            if (lt != et) timeRate /= lt - et;
            double fixedRate = 100.0 - setting.TimeWeight;
            int index = random.Next(lstMusic.Items.Count), i = 0;
            foreach (object var in lstMusic.Items)
            {
                MusicData m = (MusicData)var;
                double p = (100.0 - m.SkipRate) * (fixedRate + timeRate * (double)(lt - m.LastPlayedTicks));
                sum += p;
                if (sum * random.Next() / int.MaxValue < p) index = i;
                i++;
            }
            lstMusic.SelectedIndex = index;
            PlaySelected();
        }
        private void PlayLoop()
        {
            Play(true);
        }
        private void Play(bool loop)
        {
            Cursor preCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (lstMusic.SelectedItem == null) return;
                MusicData data = (MusicData)lstMusic.SelectedItem;
                if (!File.Exists(data.FileName))
                {
                    musicController.Data = null;
                    UpdateCaption(Status.ReadError, data);
                    if (Log.ShowError(
                        $"再生しようとしたファイルは存在しません。\n{data.FileName}\n\nリストから削除しますか？",
                        IErrorNotifier.NoticeType.AskMaybeNo))
                        removeItemToolStripMenuItem.PerformClick();
                    return;
                }
                UpdateCaption(Status.Reading, data);
                try { musicController.Data = data; }
                catch (Exception e)
                {
                    musicController.Data = null;
                    UpdateCaption(Status.ReadError, data);
                    if (Log.ShowException(
                        $"読み込みに失敗しました！\n読み込みに対応していない形式である可能性があります。\n{data.FileName}\n\nリストから削除しますか？",
                        e,
                        IErrorNotifier.NoticeType.AskMaybeNo))
                        removeItemToolStripMenuItem.PerformClick();
                    return;
                }
                UpdateCaption(Status.Preparing, data);
                try { musicController.Play(loop); }
                catch (Exception e)
                {
                    musicController.Data = null;
                    UpdateCaption(Status.PlayError, data);
                    Log.ShowException($"再生に失敗しました！\n{data.FileName}", e);
                    return;
                }
                lastPlayed = DateTime.Now;
                UpdateCaption((loop ? Status.LoopPlaying : Status.Playing), data);
                if (setting.PlayLog) Log.Write(Log.LogType.Play, data.FileName);
                UpdateStatusBar();
                if (setting.Sorting == Sorting.LastPlayed) UpdateList(DirtyLevel.ListItem);
            }
            finally
            {
                Cursor.Current = preCursor;
            }
        }

        #endregion

        #region 項目追加とか

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) AddFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void AddFiles(string[] p)
        {
            if (p.Length == 0) return;
            musicList.Add(p, setting.ExtensionRules);
            Dirty(DirtyLevel.ListCount);
            UpdateList();
        }

        #endregion

        #region リストの表示・並べ替え

        /// <summary>汚くする。でも表示の更新はしない。</summary>
        /// <param name="dirty">dirtyってレベルじゃねーぞ</param>
        private void Dirty(DirtyLevel dirty)
        {
            if (this.dirty < dirty) this.dirty = dirty;
        }

        /// <summary>表示の更新をする</summary>
        private void UpdateList()
        {
            if (dirty >= DirtyLevel.ListItem)
            {
                object selected = lstMusic.SelectedItem;
                lstMusic.BeginUpdate();
                if (dirty >= DirtyLevel.ListCount)
                {
                    if (lstMusic.Items.Count < musicList.Count)
                    {
                        while (lstMusic.Items.Count < musicList.Count) lstMusic.Items.Add("");
                    }
                    else if (lstMusic.Items.Count > musicList.Count)
                    {
                        while (lstMusic.Items.Count > musicList.Count)
                            lstMusic.Items.RemoveAt(Math.Min(lastRemoved, lstMusic.Items.Count - 1));
                    }
                    musicList.Save(setting.ListFile);
                }
                MusicData[] list = new MusicData[musicList.Count];
                int i = 0;
                foreach (MusicData data in musicList.Values) list[i++] = data;
                Array.Sort(list, comparison);
                if (setting.Reversed) Array.Reverse(list);
                i = 0;
                foreach (MusicData data in list)
                {
                    lstMusic.Items[i++] = data;
                }
                lstMusic.SelectedItem = selected;
                lstMusic.EndUpdate();
            }
            else if (dirty == DirtyLevel.SelectedIndex)
            {
            }
            dirty = DirtyLevel.None;
        }

        /// <summary>表示の更新をする</summary>
        /// <param name="dirty">dirtyってレベルじゃねーぞ</param>
        private void UpdateList(DirtyLevel dirty)
        {
            Dirty(dirty);
            UpdateList();
        }

        public int CompareFileName(MusicData x, MusicData y)
        {
            return string.Compare(x.FileName, y.FileName);
        }
        public int CompareTitle(MusicData x, MusicData y)
        {
            int ret = string.Compare(x.Title, y.Title);
            return ret != 0 ? ret : string.Compare(x.FileName, y.FileName);
        }
        public int CompareLastPlayed(MusicData x, MusicData y)
        {
            int ret = Math.Sign(y.LastPlayedTicks - x.LastPlayedTicks);
            return ret != 0 ? ret : string.Compare(x.FileName, y.FileName);
        }
        public int CompareDirectory(MusicData x, MusicData y)
        {
            int ret = string.Compare(x.Directory, y.Directory);
            if (ret != 0) return ret;
            ret = string.Compare(x.FileTitle, y.FileTitle);
            return ret != 0 ? ret : string.Compare(x.FileName, y.FileName);
        }

        private void SortFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.FileName);
        }
        private void SortTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.Title);
        }
        private void SortLastPlayedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.LastPlayed);
        }
        private void SortFolderNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.Directory);
        }

        private void SetSorting(Sorting sorting)
        {
            sortFileNameToolStripMenuItem.Checked =
            sortFolderNameToolStripMenuItem.Checked =
            sortTitleToolStripMenuItem.Checked =
            sortLastPlayedToolStripMenuItem.Checked = false;
            switch (sorting)
            {
                case Sorting.FileName:
                    comparison = CompareFileName;
                    sortFileNameToolStripMenuItem.Checked = true;
                    break;
                case Sorting.Title:
                    comparison = CompareTitle;
                    sortTitleToolStripMenuItem.Checked = true;
                    break;
                case Sorting.LastPlayed:
                    comparison = CompareLastPlayed;
                    sortLastPlayedToolStripMenuItem.Checked = true;
                    break;
                case Sorting.Directory:
                    comparison = CompareDirectory;
                    sortFolderNameToolStripMenuItem.Checked = true;
                    break;
            }
            setting.Sorting = sorting;
            UpdateList(DirtyLevel.ListItem);
        }

        private void ReverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseToolStripMenuItem.Checked = setting.Reversed = !setting.Reversed;
            UpdateList(DirtyLevel.ListItem);
        }

        #endregion

        #region 再生メニュー

        private void PlaySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySelected();
        }
        private void PlayNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayNext();
        }
        private void PlayPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayPrevious();
        }
        private void PlayRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayRandom();
        }
        private void LoopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayLoop();
        }
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStop();
        }

        #endregion

        #region 再生終了後の処理

        private void FinishActionStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Stop);
        }
        private void FinishActionReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Replay);
        }
        private void FinishActionNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Next);
        }
        private void FinishActionPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Previous);
        }
        private void FinishActionRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Random);
        }

        private void SetFinishAction(FinishAction finishAction)
        {
            finishActionStopToolStripMenuItem.Checked =
            finishActionReplayToolStripMenuItem.Checked =
            finishActionNextToolStripMenuItem.Checked =
            finishActionPreviousToolStripMenuItem.Checked =
            finishActionRandomToolStripMenuItem.Checked = false;
            switch (finishAction)
            {
                case FinishAction.Replay:
                    playMethod = PlaySelected;
                    finishActionReplayToolStripMenuItem.Checked = true;
                    break;
                case FinishAction.Next:
                    playMethod = PlayNext;
                    finishActionNextToolStripMenuItem.Checked = true;
                    break;
                case FinishAction.Previous:
                    playMethod = PlayPrevious;
                    finishActionPreviousToolStripMenuItem.Checked = true;
                    break;
                case FinishAction.Random:
                    playMethod = PlayRandom;
                    finishActionRandomToolStripMenuItem.Checked = true;
                    break;
                default:
                    playMethod = PlayStop;
                    finishActionStopToolStripMenuItem.Checked = true;
                    break;
            }
            setting.FinishAction = finishAction;
        }

        bool timerProcessing = false;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timerProcessing) return;
            timerProcessing = true;
            try
            {
                // 現在の状態をチェックする
                if (musicController.Data != null)
                {
                    if (musicController.Loop)
                    {
                        musicController.LoopMethod();
                    }
                    else
                    {
                        if (lastPlayed.AddSeconds(setting.MinPlayTime).CompareTo(DateTime.Now) < 0)
                        {
                            if (!musicController.IsPlaying)
                            {
                                playMethod();
                            }
                        }
                    }
                    UpdateStatusBar();
                }
            }
            catch (Exception ex)
            {
                if (!setting.IgnoreTimerError)
                {
                    if (Log.ShowException("再生中にエラーが発生しました。\n"
                        + "このエラーは連続して発生し続ける可能性があります。\n"
                        + "その場合はエラーを一時的に無視することをお勧めします。"
                        + "今後再生中のエラーを無視しますか？"
                        , ex, IErrorNotifier.NoticeType.AskMaybeYes))
                    {
                        setting.IgnoreTimerError = true;
                    }
                }
            }
            timerProcessing = false;
        }

        #endregion

        private void LstMusic_DoubleClick(object sender, EventArgs e)
        {
            PlayLoop();
        }

        private void TempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                musicController.SetTempo(double.Parse(sender.ToString().Remove(sender.ToString().Length - 1)) / 100.0);
            }
            catch (Exception ex)
            {
                Log.ShowException("再生速度変更に失敗しました！", ex);
                return;
            }
            foreach (ToolStripMenuItem item in tempoToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        private void PortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicController.SetDirectMusicPort(DMOption.portdefault = setting.Port = sender.ToString());
            foreach (ToolStripMenuItem item in portToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        #region ファイルメニュー

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ItemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicData data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            string oldFileName = data.FileName;
            FormItem f = new FormItem(musicController, data);
            if (f.ShowDialog() == DialogResult.OK)
            {
                musicList.Set(oldFileName, data);
                UpdateList(DirtyLevel.ListCount);
                // リストにある別のファイル名に変更したとき要素が減るのだ
            }
        }

        private void RemoveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicData data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            lastRemoved = lstMusic.SelectedIndex--;
            musicList.Remove(data.FileName);
            UpdateList(DirtyLevel.ListCount);
        }

        private void AddFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdMusicFiles.ShowDialog() == DialogResult.OK)
            {
                musicList.Add(ofdMusicFiles.FileNames, setting.ExtensionRules);
                UpdateList(DirtyLevel.ListCount);
            }
        }

        private void OpenListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdListFile.ShowDialog() == DialogResult.OK)
            {
                musicList.Save(setting.ListFile);
                setting.ListFile = ofdListFile.FileName;
                CreateList();
            }
        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetting f = new FormSetting(setting);
            f.ShowDialog();
            OnLayout(new LayoutEventArgs(this, ""));
            UpdateStatusBar();
            UpdateCaption();
        }

        private void ListPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.ShowError("リストのプロパティは未実装です。");
        }

        private void SaveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicList.Save(setting.ListFile);
        }

        #endregion

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.ProductName + "\n" + Application.ProductVersion);
        }

        private void UpdateStatusBar()
        {
            foreach (StatusItem item in setting.StatusItems)
            {
                item.Update(musicController);
            }
        }

        private void FormMain_Layout(object sender, LayoutEventArgs e)
        {
            if (setting == null) return;
            int listHeight = ClientSize.Height;
            if (menuStrip1.Visible)
            {
                listHeight -= menuStrip1.Height;
                lstMusic.Top = menuStrip1.Height;
            }
            if (statusStrip1.Visible = setting.ShowStatus)
            {
                listHeight -= statusStrip1.Height;
                statusStrip1.Items.Clear();
                foreach (StatusItem item in setting.StatusItems)
                {
                    item.Add(statusStrip1);
                    item.Update(musicController);
                }
            }
            lstMusic.Height = listHeight;
            lstMusic.Width = ClientSize.Width;
        }

        private void SaveOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting.Save();
        }

        private void UpdateCaption(Status status, MusicData data)
        {
            statusValue = status;
            statusData = data;
            UpdateCaption();
        }

        private void UpdateCaption()
        {
            string DataTitle = statusData != null ? statusData.Title : " ";
            string FileTitle = statusData != null ? statusData.FileTitle : " ";
            string FileName = statusData != null ? statusData.FileName : " ";
            Text = setting.WindowTitlePattern
                .Replace("<StatusJ>", GetStatusText(statusValue))
                .Replace("<Status>", statusValue.ToString())
                .Replace("<Title>", DataTitle)
                .Replace("<FileTitle>", FileTitle)
                .Replace("<FileName>", FileName)
                .Replace("<Version>", Application.ProductVersion)
                ;
        }

        private string GetStatusText(Status status)
        {
            return status switch
            {
                Status.Stopped => "再生停止",
                Status.Reading => "読み込み中",
                Status.Preparing => "再生準備中",
                Status.Playing => "再生中",
                Status.LoopPlaying => "ループ再生中",
                Status.ReadError => "読み込み失敗",
                Status.PlayError => "再生失敗",
                _ => "",
            };
        }

        private void RebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminate();
            Application.Restart();
        }

        private void FormMain_MachineLocked(object sender, EventArgs e)
        {
            if (setting.StopWhenWindowsLocked)
            {
                PlayStop();
            }
        }

        bool IErrorNotifier.Notify(string message, IErrorNotifier.NoticeType type)
        {
            var buttons = MessageBoxButtons.OK;
            var defaultButton = MessageBoxDefaultButton.Button1;
            var expects = DialogResult.OK;
            switch (type)
            {
                case IErrorNotifier.NoticeType.MessageOnly:
                    break;
                case IErrorNotifier.NoticeType.AskMaybeYes:
                    buttons = MessageBoxButtons.YesNo;
                    defaultButton = MessageBoxDefaultButton.Button2;
                    expects = DialogResult.Yes;
                    break;
                case IErrorNotifier.NoticeType.AskMaybeNo:
                    buttons = MessageBoxButtons.YesNo;
                    expects = DialogResult.Yes;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return MessageBox.Show(this, message, "NeoMupl エラー！", buttons, MessageBoxIcon.Error, defaultButton) == expects;
        }

        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            ProcessHelper.OpenFileLocation(data.FileName);
        }
    }
}
