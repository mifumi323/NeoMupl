using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace NeoMupl
{
    public partial class FormSetting : Form
    {
        private class PropStringPair
        {
            public PropertyInfo prop;
            public string desc;

            public PropStringPair(string property, string description)
            {
                prop = typeof(Setting).GetProperty(property);
                desc = description;
            }

            public override string ToString()
            {
                return desc;
            }
        }

        public Setting setting;

        private void AddCheckItem(CheckedListBox list, string property, string description)
        {
            PropStringPair prop = new PropStringPair(property, description);
            bool value = (bool)prop.prop.GetValue(setting, null);
            list.Items.Add(prop, value);
        }

        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            // 全般設定
            txtMinPlayTime.Text = setting.MinPlayTime.ToString();
            txtMinPlayTime.Tag = trbMinPlayTime;
            trbMinPlayTime.Value = (int)setting.MinPlayTime;
            txtTimeWeight.Text = setting.TimeWeight.ToString();
            txtTimeWeight.Tag = trbTimeWeight;
            trbTimeWeight.Value = (int)setting.TimeWeight;
            txtWindowTitlePattern.Text = setting.WindowTitlePattern;

            // 追加時の設定
            txtTitlePattern.Text = setting.TitlePattern;

            // ステータスバー
            chkShowStatus.Checked = setting.ShowStatus;
            foreach (StatusItem item in setting.StatusItems)
            {
                clbStatusItem.Items.Add(item.ToString(), true);
            }
            foreach (string name in StatusItem.GetNames())
            {
                if (clbStatusItem.Items.Contains(name)) continue;
                clbStatusItem.Items.Add(name, false);
            }

            // ログ
            chkEraseLogOnExit.Checked = setting.EraseLogOnExit;
            txtLogFile.Text = setting.LogFile;
            AddCheckItem(clbLog, "PlayLog", "再生時にログ記録");
            AddCheckItem(clbLog, "StopLog", "停止時にログ記録");
            AddCheckItem(clbLog, "ErrorLog", "エラー時にログ記録");

            // エラー処理
            chkIgnoreTimerError.Checked = setting.IgnoreTimerError;
            chkReportException.Checked = setting.ReportException;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 全般設定
            setting.MinPlayTime = double.Parse(txtMinPlayTime.Text);
            setting.TimeWeight = double.Parse(txtTimeWeight.Text);
            setting.WindowTitlePattern = txtWindowTitlePattern.Text;

            // 追加時の設定
            setting.TitlePattern = txtTitlePattern.Text;

            // ステータスバー
            setting.ShowStatus = chkShowStatus.Checked;
            setting.StatusItems.Clear();
            foreach (object item in clbStatusItem.CheckedItems)
            {
                setting.StatusItems.Add(StatusItem.Parse(item.ToString()));
            }

            // ログ
            setting.EraseLogOnExit = chkEraseLogOnExit.Checked;
            setting.LogFile = txtLogFile.Text;
            for (int i = 0; i < clbLog.Items.Count; i++)
            {
                ((PropStringPair)clbLog.Items[i]).prop.SetValue(setting, clbLog.GetItemChecked(i), null);
            }

            // エラー処理
            setting.IgnoreTimerError = chkIgnoreTimerError.Checked;
            setting.ReportException = chkReportException.Checked;
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

        private void trbMinPlayTime_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtMinPlayTime.Text) - (double)trbMinPlayTime.Value) >= 1)
                    txtMinPlayTime.Text = trbMinPlayTime.Value.ToString();
            }
            catch (Exception) { }
        }

        private void trbTimeWeight_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (Math.Abs(double.Parse(txtTimeWeight.Text) - (double)trbTimeWeight.Value) >= 1)
                    txtTimeWeight.Text = trbTimeWeight.Value.ToString();
            }
            catch (Exception) { }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = clbStatusItem.SelectedIndex;
            if (index > 0) SwapStatusItem(index, index - 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = clbStatusItem.SelectedIndex;
            if (index < clbStatusItem.Items.Count - 1) SwapStatusItem(index, index + 1);
        }

        private void SwapStatusItem(int from, int to)
        {
            object tmp = clbStatusItem.Items[from];
            clbStatusItem.Items[from] = clbStatusItem.Items[to];
            clbStatusItem.Items[to] = tmp;
            bool b = clbStatusItem.GetItemChecked(from);
            clbStatusItem.SetItemChecked(from, clbStatusItem.GetItemChecked(to));
            clbStatusItem.SetItemChecked(to, b);
            clbStatusItem.SelectedIndex = to;
        }

        private void btnRefLogFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(txtLogFile.Text);
            saveFileDialog1.FileName = txtLogFile.Text;
            saveFileDialog1.DefaultExt = "log";
            saveFileDialog1.Filter = "ログファイル(*.log)|*.log|テキストファイル(*.txt)|*.txt|全てのファイル(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogFile.Text = saveFileDialog1.FileName;
            }
        }

        private void lstTitleTemplate_DoubleClick(object sender, EventArgs e)
        {
            txtTitlePattern.SelectedText = ((ListBox)sender).SelectedItem.ToString().Split(' ')[0];
        }

        private void btnWindowTitlePattern_Click(object sender, EventArgs e)
        {
            if (cmbWindowTitlePattern.Text.Contains("："))
                txtWindowTitlePattern.SelectedText = cmbWindowTitlePattern.Text.Split('：')[0];
            txtWindowTitlePattern.Focus();
        }

        private void cmbWindowTitlePattern_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnWindowTitlePattern;
        }

        private void cmbWindowTitlePattern_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnOK;
        }
    }
}