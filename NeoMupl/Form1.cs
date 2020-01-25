using System;
using System.IO;
using System.Windows.Forms;
using MifuminLib;
using NeoMupl.Player;

namespace NeoMupl
{
    public enum FinishAction { Stop, Replay, Next, Previous, Random }
    public enum Sorting { FileName, Title, LastPlayed, Directory }

    public partial class Form1 : Form
    {
        MusicList musicList;
        IMusicController musicController;
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

        Setting setting;
        Random random = new Random();

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
        MusicData statusData;

        #region 初期化処理
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Log.setting = setting = new Setting();
            musicController = new MusicController();
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
                ToolStripMenuItem item = new ToolStripMenuItem(port, null, new EventHandler(portToolStripMenuItem_Click));
                item.Checked = port == setting.Port;
                portToolStripMenuItem.DropDownItems.Add(item);
            }
            UpdateStatusBar();
            CreateList();
            timer1.Start();
            Log.owner = this;
        }

        private void CreateList()
        {
            musicList = new MusicList();
            musicList.Load(setting.ListFile);
            UpdateList(DirtyLevel.ListCount);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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
                if (setting != null && setting.ErrorLog)
                    Log.Write(Log.LogType.FatalError, "想定外のエラーです。\n\n" + e.Exception.ToString());
                FormFatalError ff = new FormFatalError();
                ff.SetException(e.Exception, result);
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
                    if (Log.Error(
                        "再生しようとしたファイルは存在しません。\n"
                        + data.FileName + "\n\nリストから削除しますか？", MessageBoxButtons.YesNo,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        removeItemToolStripMenuItem_Click(null, null);
                    return;
                }
                UpdateCaption(Status.Reading, data);
                try { musicController.Data = data; }
                catch (Exception e)
                {
                    musicController.Data = null;
                    UpdateCaption(Status.ReadError, data);
                    if (Log.Error(
                        "読み込みに失敗しました！\n読み込みに対応していない形式である可能性があります。\n"
                        + data.FileName + "\n\nリストから削除しますか？", e, MessageBoxButtons.YesNo,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        removeItemToolStripMenuItem_Click(null, null);
                    return;
                }
                UpdateCaption(Status.Preparing, data);
                try { musicController.Play(loop); }
                catch (Exception e)
                {
                    musicController.Data = null;
                    UpdateCaption(Status.PlayError, data);
                    Log.Error("再生に失敗しました！\n" + data.FileName, e);
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

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) AddFiles((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void AddFiles(string[] p)
        {
            if (p.Length == 0) return;
            musicList.Add(p);
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

        private void sortFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.FileName);
        }
        private void sortTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.Title);
        }
        private void sortLastPlayedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSorting(Sorting.LastPlayed);
        }
        private void sortFolderNameToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (musicList != null) UpdateList(DirtyLevel.ListItem);
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseToolStripMenuItem.Checked = setting.Reversed = !setting.Reversed;
            UpdateList(DirtyLevel.ListItem);
        }
        
        #endregion

        #region 再生メニュー

        private void playSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySelected();
        }
        private void playNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayNext();
        }
        private void playPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayPrevious();
        }
        private void playRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayRandom();
        }
        private void loopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayLoop();
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStop();
        }

        #endregion

        #region 再生終了後の処理

        private void finishActionStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Stop);
        }
        private void finishActionReplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Replay);
        }
        private void finishActionNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Next);
        }
        private void finishActionPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFinishAction(FinishAction.Previous);
        }
        private void finishActionRandomToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void timer1_Tick(object sender, EventArgs e)
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
                    if (Log.Error("再生中にエラーが発生しました。\n"
                        + "このエラーは連続して発生し続ける可能性があります。\n"
                        + "その場合はエラーを一時的に無視することをお勧めします。"
                        + "今後再生中のエラーを無視しますか？"
                        , ex, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        setting.IgnoreTimerError = true;
                    }
                }
            }
            timerProcessing = false;
        }

        #endregion

        private void lstMusic_DoubleClick(object sender, EventArgs e)
        {
            PlayLoop();
        }

        private void tempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                musicController.SetTempo(double.Parse(sender.ToString().Remove(sender.ToString().Length - 1)) / 100.0);
            }
            catch (Exception ex)
            {
                Log.Error("再生速度変更に失敗しました！", ex);
                return;
            }
            foreach (ToolStripMenuItem item in tempoToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicController.SetDirectMusicPort(DMOption.portdefault = setting.Port = sender.ToString());
            foreach (ToolStripMenuItem item in portToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        #region ファイルメニュー

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void itemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicData data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            string oldFileName = data.FileName;
            FormItem f = new FormItem
            {
                MusicController = musicController,
                MusicData = data,
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                musicList.Set(oldFileName, data);
                UpdateList(DirtyLevel.ListCount);
                // リストにある別のファイル名に変更したとき要素が減るのだ
            }
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicData data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            lastRemoved = lstMusic.SelectedIndex--;
            musicList.Remove(data.FileName);
            UpdateList(DirtyLevel.ListCount);
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdMusicFiles.ShowDialog() == DialogResult.OK)
            {
                musicList.Add(ofdMusicFiles.FileNames);
                UpdateList(DirtyLevel.ListCount);
            }
        }

        private void openListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdListFile.ShowDialog() == DialogResult.OK)
            {
                musicList.Save(setting.ListFile);
                setting.ListFile = ofdListFile.FileName;
                CreateList();
            }
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetting f = new FormSetting();
            f.setting = setting;
            f.ShowDialog();
            OnLayout(new LayoutEventArgs(this, ""));
            UpdateStatusBar();
            UpdateCaption();
        }

        private void listPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Error("リストのプロパティは未実装です。");
        }

        private void saveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicList.Save(setting.ListFile);
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void Form1_Layout(object sender, LayoutEventArgs e)
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

        private void saveOptionToolStripMenuItem_Click(object sender, EventArgs e)
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
                ;
        }

        private string GetStatusText(Status status)
        {
            switch (status)
            {
                case Status.Stopped:
                    return "再生停止";
                case Status.Reading:
                    return "読み込み中";
                case Status.Preparing:
                    return "再生準備中";
                case Status.Playing:
                    return "再生中";
                case Status.LoopPlaying:
                    return "ループ再生中";
                case Status.ReadError:
                    return "読み込み失敗";
                case Status.PlayError:
                    return "再生失敗";
                default:
                    return "";
            }
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminate();
            Application.Restart();
        }
    }
}