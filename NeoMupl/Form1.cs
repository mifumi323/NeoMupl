using System;
using System.Windows.Forms;
using System.IO;
using MifuminLib;

namespace NeoMupl
{
    public enum FinishAction { Stop, Replay, Next, Previous, Random }
    public enum Sorting { FileName, Title, LastPlayed, Directory }

    public partial class Form1 : Form
    {
        MusicList musicList;
        MusicPlayer musicPlayer;
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

        #region ����������
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Log.setting = setting = new Setting();
            musicPlayer = setting.NewPlayer ? (MusicPlayer)new MusicPlayerNew(Handle) : (MusicPlayer)new MusicPlayerOld();
            if (setting.MainWidth <= 0) setting.MainWidth = Width;
            if (setting.MainHeight <= 0) setting.MainHeight = Height;
            SetDesktopBounds(setting.MainLeft, setting.MainTop, setting.MainWidth, setting.MainHeight);
            WindowState = setting.MainWindowState;
            SetFinishAction(setting.FinishAction);
            SetSorting(setting.Sorting);
            reverseToolStripMenuItem.Checked = setting.Reversed;
            DMOption.portdefault = setting.Port;
            foreach (string port in musicPlayer.GetDirectMusicPorts())
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
            musicPlayer.Dispose();
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
                    Log.Write(Log.LogType.FatalError, "�z��O�̃G���[�ł��B\n\n" + e.Exception.ToString());
                FormFatalError ff = new FormFatalError();
                ff.SetException(e.Exception, result);
                ff.ShowDialog(this);
                switch (result.PlayList)
                {
                    case 1:
                        // �㏑���ۑ�
                        musicList.Save(setting.ListFile);
                        break;
                    case 2:
                        // �ʖ��ۑ�
                        musicList.Save(PathMaker.GetAnotherName(setting.ListFile));
                        break;
                }
                switch (result.Setting)
                {
                    case 1:
                        // �㏑���ۑ�
                        setting.Save();
                        break;
                    case 2:
                        // �ʖ��ۑ�
                        setting.SaveAnotherName();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (setting != null && setting.ErrorLog)
                    Log.Write(Log.LogType.FatalError, "�G���[�������ɃG���[���������܂����B\n\n" + ex.ToString());
                throw;
            }
            switch (result.Program)
            {
                case 1:
                    // ����I��
                    Application.Exit();
                    break;
                case 2:
                    // �G���[�I��
                    throw e.Exception;
            }
        }

        #endregion

        #region �e��Đ����\�b�h

        private void PlayStop()
        {
            if (musicPlayer.Data == null) return;
            if (setting.StopLog) Log.Write(Log.LogType.Stop, musicPlayer.Data.FileName);
            UpdateCaption(Status.Stopped, musicPlayer.Data);
            musicPlayer.Data = null;
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
                    musicPlayer.Data = null;
                    UpdateCaption(Status.ReadError, data);
                    if (Log.Error(
                        "�Đ����悤�Ƃ����t�@�C���͑��݂��܂���B\n"
                        + data.FileName + "\n\n���X�g����폜���܂����H", MessageBoxButtons.YesNo,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        removeItemToolStripMenuItem_Click(null, null);
                    return;
                }
                UpdateCaption(Status.Reading, data);
                try { musicPlayer.Data = data; }
                catch (Exception e)
                {
                    musicPlayer.Data = null;
                    UpdateCaption(Status.ReadError, data);
                    if (Log.Error(
                        "�ǂݍ��݂Ɏ��s���܂����I\n�ǂݍ��݂ɑΉ����Ă��Ȃ��`���ł���\��������܂��B\n"
                        + data.FileName + "\n\n���X�g����폜���܂����H", e, MessageBoxButtons.YesNo,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        removeItemToolStripMenuItem_Click(null, null);
                    return;
                }
                UpdateCaption(Status.Preparing, data);
                try { musicPlayer.Play(loop); }
                catch (Exception e)
                {
                    musicPlayer.Data = null;
                    UpdateCaption(Status.PlayError, data);
                    Log.Error("�Đ��Ɏ��s���܂����I\n" + data.FileName, e);
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

        #region ���ڒǉ��Ƃ�

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

        #region ���X�g�̕\���E���בւ�

        /// <summary>��������B�ł��\���̍X�V�͂��Ȃ��B</summary>
        /// <param name="dirty">dirty���ă��x������ˁ[��</param>
        private void Dirty(DirtyLevel dirty)
        {
            if (this.dirty < dirty) this.dirty = dirty;
        }

        /// <summary>�\���̍X�V������</summary>
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

        /// <summary>�\���̍X�V������</summary>
        /// <param name="dirty">dirty���ă��x������ˁ[��</param>
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

        #region �Đ����j���[

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

        #region �Đ��I����̏���

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                // ���݂̏�Ԃ��`�F�b�N����
                if (musicPlayer.Data != null)
                {
                    if (musicPlayer.Loop)
                    {
                        musicPlayer.LoopMethod();
                    }
                    else
                    {
                        if (lastPlayed.AddSeconds(setting.MinPlayTime).CompareTo(DateTime.Now) < 0)
                        {
                            if (!musicPlayer.IsPlaying)
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
                    if (Log.Error("�Đ����ɃG���[���������܂����B\n"
                        + "���̃G���[�͘A�����Ĕ�����������\��������܂��B\n"
                        + "���̏ꍇ�̓G���[���ꎞ�I�ɖ������邱�Ƃ������߂��܂��B"
                        + "����Đ����̃G���[�𖳎����܂����H"
                        , ex, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        setting.IgnoreTimerError = true;
                    }
                }
            }
            timer1.Start();
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
                musicPlayer.SetTempo(double.Parse(sender.ToString().Remove(sender.ToString().Length - 1)) / 100.0);
            }
            catch (Exception ex)
            {
                Log.Error("�Đ����x�ύX�Ɏ��s���܂����I", ex);
                return;
            }
            foreach (ToolStripMenuItem item in tempoToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musicPlayer.SetDirectMusicPort(DMOption.portdefault = setting.Port = sender.ToString());
            foreach (ToolStripMenuItem item in portToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        #region �t�@�C�����j���[

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void itemPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicData data = (MusicData)lstMusic.SelectedItem;
            if (data == null) return;
            string oldFileName = data.FileName;
            FormItem f = new FormItem();
            f.Init(data, musicPlayer.GetDirectMusicPorts());
            if (f.ShowDialog() == DialogResult.OK)
            {
                musicList.Set(oldFileName, data);
                UpdateList(DirtyLevel.ListCount);
                // ���X�g�ɂ���ʂ̃t�@�C�����ɕύX�����Ƃ��v�f������̂�
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
            Log.Error("���X�g�̃v���p�e�B�͖������ł��B");
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
                item.Update(musicPlayer);
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
                    item.Update(musicPlayer);
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
                    return "�Đ���~";
                case Status.Reading:
                    return "�ǂݍ��ݒ�";
                case Status.Preparing:
                    return "�Đ�������";
                case Status.Playing:
                    return "�Đ���";
                case Status.LoopPlaying:
                    return "���[�v�Đ���";
                case Status.ReadError:
                    return "�ǂݍ��ݎ��s";
                case Status.PlayError:
                    return "�Đ����s";
                default:
                    return "";
            }
        }
    }
}