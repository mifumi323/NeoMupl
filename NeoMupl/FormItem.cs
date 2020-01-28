#nullable enable
using System;
using System.Windows.Forms;
using NeoMupl.Player;

namespace NeoMupl
{
    public partial class FormItem : Form
    {
        MusicController musicController;
        public MusicController MusicController
        {
            get => musicController;
            set
            {
                musicController = value;
                cmbMIDIPort.Items.AddRange(musicController.GetDirectMusicPorts());
            }
        }

        MusicData musicData;
        public MusicData MusicData
        {
            get => musicData;
            set
            {
                musicData = value;
                txtFileName.Text = musicData.FileName;
                txtTitle.Text = musicData.Title;
                txtVolume.Text = musicData.Volume.ToString();
                txtLoop1.Text = musicData.LoopStart.ToString();
                txtLoop2.Text = musicData.LoopEnd.ToString();
                txtSkipRate.Text = musicData.SkipRate.ToString();
                cmbPlayMethod.SelectedIndex = (int)musicData.PlayMethod;
                try { cmbMIDIPort.Text = (musicData.Option as DMOption)?.port ?? "default"; }
                catch (Exception) { cmbMIDIPort.Text = "default"; }
                lblLastPlayed.Text = musicData.LastPlayedDateTime.ToString();
            }
        }

        enum State
        {
            Unable,
            Stopped,
            Looping,
            NearLoop,
        }
        State state;

        MusicPlayerBase? musicPlayer = null;

        public FormItem(MusicController musicController, MusicData musicData)
        {
            InitializeComponent();
            txtVolume.Tag = trbVolume;
            txtSkipRate.Tag = trbSkipRate;

            // なんか冗長だけどメソッドの中身までnullableを見に行かないからこうしないと
            MusicController = this.musicController = musicController;
            MusicData = this.musicData = musicData;

            // 再生中はテスト再生しちゃいけない
            state = musicController.IsPlaying ? State.Unable : State.Stopped;
            UpdatePlayButtons();
            btnPlayLoop.Enabled = btnPlayNearLoop.Enabled = !musicController.IsPlaying;
        }

        private void UpdatePlayButtons()
        {
            switch (state)
            {
                case State.Unable:
                    btnPlayLoop.Enabled = false;
                    btnPlayNearLoop.Enabled = false;
                    btnPlayLoop.Text = "ループ再生テスト";
                    btnPlayNearLoop.Text = "ループ前後5秒を再生";
                    break;
                case State.Stopped:
                    btnPlayLoop.Enabled = true;
                    btnPlayNearLoop.Enabled = true;
                    btnPlayLoop.Text = "ループ再生テスト";
                    btnPlayNearLoop.Text = "ループ前後5秒を再生";
                    break;
                case State.Looping:
                    btnPlayLoop.Enabled = true;
                    btnPlayNearLoop.Enabled = false;
                    btnPlayLoop.Text = "ループ再生テストを停止";
                    btnPlayNearLoop.Text = "ループ前後5秒を再生";
                    break;
                case State.NearLoop:
                    btnPlayLoop.Enabled = false;
                    btnPlayNearLoop.Enabled = true;
                    btnPlayLoop.Text = "ループ再生テスト";
                    btnPlayNearLoop.Text = "ループ前後5秒の再生を停止";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void BtnFileName_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = txtFileName.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtFileName.Text = openFileDialog1.FileName;
        }

        private void TrbVolume_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtVolume.Text) - (double)trbVolume.Value) >= 1)
                    txtVolume.Text = trbVolume.Value.ToString();
            }
            catch (Exception) { }
        }

        private void TxtWithTrackBar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double val = double.Parse(((TextBox)sender).Text);
                if (val < 0)
                {
                    ((TextBox)sender).Text = "0";
                    return;
                }
                if (val > 100)
                {
                    ((TextBox)sender).Text = "100";
                    return;
                }
                if (Math.Abs(val - (double)((TrackBar)((TextBox)sender).Tag).Value) >= 1)
                    ((TrackBar)((TextBox)sender).Tag).Value = (int)val;
            }
            catch (Exception) { }
        }

        private void TrbSkipRate_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtSkipRate.Text) - (double)trbSkipRate.Value) >= 1)
                    txtSkipRate.Text = trbSkipRate.Value.ToString();
            }
            catch (Exception) { }
        }

        private void CmbPlayMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((PlayMethod)cmbPlayMethod.SelectedIndex)
            {
                case PlayMethod.DirectShow:
                    lblLoopUnit.Text = "秒";
                    cmbMIDIPort.Enabled = false;
                    break;
                case PlayMethod.DirectMusic:
                    lblLoopUnit.Text = "MT";
                    cmbMIDIPort.Enabled = true;
                    break;
                case PlayMethod.MCI:
                    lblLoopUnit.Text = "";
                    cmbMIDIPort.Enabled = false;
                    break;
                case PlayMethod.NAudio:
                    lblLoopUnit.Text = "秒";
                    cmbMIDIPort.Enabled = false;
                    break;
            }
        }

        private void BtnPlayMethod_Click(object sender, EventArgs e)
        {
            cmbPlayMethod.SelectedIndex = txtFileName.Text.EndsWith(".mid", true, null) ? 1 : 0;
        }

        private void TxtFileName_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ファイル名をフルパスで入力します。";
        }

        private void TxtTitle_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "リストに表示されるタイトルを入力します。";
        }

        private void BtnTitle_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ファイルからタイトルを読み込んで自動設定します。";
        }

        private void TrbVolume_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "音量を調節します。";
        }

        private void TxtLoop1_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ループ位置を入力します。\r\n0であれば自動設定です。";
        }

        private void TrbSkipRate_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ランダム再生のとき選ばれにくくなる確率です。";
        }

        private void CmbPlayMethod_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "再生方法を選びます。\r\nファイル形式によって向き・不向きがあります。";
        }

        private void BtnPlayMethod_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "再生方法を自動的に判断します。";
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            CopyToMusicData(musicData);
        }

        private void CopyToMusicData(MusicData destination)
        {
            destination.FileName = txtFileName.Text;
            destination.Title = txtTitle.Text;
            destination.Volume = double.Parse(txtVolume.Text);
            destination.LoopStart = double.Parse(txtLoop1.Text);
            destination.LoopEnd = double.Parse(txtLoop2.Text);
            if (destination.LoopStart > destination.LoopEnd)
            {
                double buf = destination.LoopStart;
                destination.LoopStart = destination.LoopEnd;
                destination.LoopEnd = buf;
            }
            destination.SkipRate = double.Parse(txtSkipRate.Text);
            destination.PlayMethod = (PlayMethod)cmbPlayMethod.SelectedIndex;
            if (destination.PlayMethod == PlayMethod.DirectMusic)
                destination.Option = new DMOption(cmbMIDIPort.Text);
        }

        private void BtnTitle_Click(object sender, EventArgs e)
        {
            txtTitle.Text = MusicData.CreateTitle(txtFileName.Text);
        }

        private void CmbMIDIPort_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "MIDIの音源を選びます。\r\n特に指定しないと共通設定が使われます。";
        }

        private void BtnPlayLoop_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Stopped:
                    try
                    {
                        var testData = new MusicData("");
                        CopyToMusicData(testData);
                        musicPlayer = musicController.GetPlayer(testData.PlayMethod);
                        musicPlayer.MusicData = testData;
                        musicPlayer.Open();
                        musicPlayer.Play(true);
                        state = State.Looping;
                        UpdatePlayButtons();
                        timer1.Start();
                    }
                    catch
                    {
                        Stop();
                        MessageBox.Show("テスト再生に失敗しました。");
                    }
                    break;
                case State.Looping:
                    Stop();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Stop()
        {
            timer1.Stop();
            if (musicPlayer != null)
            {
                musicPlayer.Stop();
                musicPlayer.Close();
                musicPlayer = null;
            }
            state = State.Stopped;
            UpdatePlayButtons();
        }

        private void BtnPlayNearLoop_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Stopped:
                    state = State.NearLoop;
                    UpdatePlayButtons();
                    timer1.Start();
                    break;
                case State.NearLoop:
                    Stop();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Unable:
                    break;
                case State.Stopped:
                    break;
                case State.Looping:
                    musicPlayer?.Loop();
                    break;
                case State.NearLoop:
                    break;
                default:
                    break;
            }
        }

        private void FormItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }
    }
}