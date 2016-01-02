using System;
using System.Windows.Forms;

namespace NeoMupl
{
    public partial class FormFatalError : Form
    {
        public class Result
        {
            public int PlayList;
            public int Setting;
            public int Program;
        }
        Result result;

        public FormFatalError()
        {
            InitializeComponent();
        }

        public void SetException(Exception e, Result r)
        {
            txtMessage.Text =
                "ご迷惑をかけて申し訳ございません。\r\n" +
                "エラーに対する適切な対応のなされていない処理の実行中にエラーが発生しました。\r\n" +
                "このままプログラムを続行すると、プログラムが更に異常動作を起こす可能性があります。\r\n" +
                "以下はエラーの詳細です。開発者に報告すると調査、修正を行える可能性があります。\r\n" +
                "\r\n" +
                "エラーメッセージ：\r\n" +
                e.ToString() + "\r\n" +
                "\r\n" +
                "スタックトレース：\r\n" +
                e.StackTrace;
            result = r;
        }

        private void FormFatalError_Load(object sender, EventArgs e)
        {
            cmbPlayList.SelectedIndex = 2;
            cmbSetting.SelectedIndex = 2;
            cmbProgram.SelectedIndex = 2;
        }

        private void FormFatalError_FormClosing(object sender, FormClosingEventArgs e)
        {
            result.PlayList = cmbPlayList.SelectedIndex;
            result.Setting = cmbSetting.SelectedIndex;
            result.Program = cmbProgram.SelectedIndex;
        }
    }
}