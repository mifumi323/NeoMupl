using System;
using System.Windows.Forms;
using NeoMupl.Player;

namespace NeoMupl
{
    public partial class FormItem : Form
    {
        IMusicController musicController;
        public IMusicController MusicController
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
                try { cmbMIDIPort.Text = ((DMOption)musicData.Option).port; }
                catch (Exception) { cmbMIDIPort.Text = "default"; }
                lblLastPlayed.Text = musicData.LastPlayedDateTime.ToString();
            }
        }

        public FormItem()
        {
            InitializeComponent();
            txtVolume.Tag = trbVolume;
            txtSkipRate.Tag = trbSkipRate;
        }

        private void btnFileName_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = txtFileName.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtFileName.Text = openFileDialog1.FileName;
        }

        private void trbVolume_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtVolume.Text) - (double)trbVolume.Value) >= 1)
                    txtVolume.Text = trbVolume.Value.ToString();
            }
            catch (Exception) { }
        }

        private void txtWithTrackBar_TextChanged(object sender, EventArgs e)
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

        private void trbSkipRate_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtSkipRate.Text) - (double)trbSkipRate.Value) >= 1)
                    txtSkipRate.Text = trbSkipRate.Value.ToString();
            }
            catch (Exception) { }
        }

        private void cmbPlayMethod_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnPlayMethod_Click(object sender, EventArgs e)
        {
            cmbPlayMethod.SelectedIndex = txtFileName.Text.EndsWith(".mid", true, null) ? 1 : 0;
        }

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ファイル名をフルパスで入力します。";
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "リストに表示されるタイトルを入力します。";
        }

        private void btnTitle_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ファイルからタイトルを読み込んで自動設定します。";
        }

        private void trbVolume_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "音量を調節します。";
        }

        private void txtLoop1_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ループ位置を入力します。\r\n0であれば自動設定です。";
        }

        private void trbSkipRate_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "ランダム再生のとき選ばれにくくなる確率です。";
        }

        private void cmbPlayMethod_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "再生方法を選びます。\r\nファイル形式によって向き・不向きがあります。";
        }

        private void btnPlayMethod_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "再生方法を自動的に判断します。";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            musicData.FileName = txtFileName.Text;
            musicData.Title = txtTitle.Text;
            musicData.Volume = double.Parse(txtVolume.Text);
            musicData.LoopStart = double.Parse(txtLoop1.Text);
            musicData.LoopEnd = double.Parse(txtLoop2.Text);
            if (musicData.LoopStart > musicData.LoopEnd)
            {
                double buf = musicData.LoopStart;
                musicData.LoopStart = musicData.LoopEnd;
                musicData.LoopEnd = buf;
            }
            musicData.SkipRate = double.Parse(txtSkipRate.Text);
            musicData.PlayMethod = (PlayMethod)cmbPlayMethod.SelectedIndex;
            if (musicData.PlayMethod == PlayMethod.DirectMusic)
                musicData.Option = new DMOption(cmbMIDIPort.Text);
        }

        private void btnTitle_Click(object sender, EventArgs e)
        {
            txtTitle.Text = MusicData.CreateTitle(txtFileName.Text);
        }

        private void cmbMIDIPort_Enter(object sender, EventArgs e)
        {
            txtNavigation.Text = "MIDIの音源を選びます。\r\n特に指定しないと共通設定が使われます。";
        }
    }
}