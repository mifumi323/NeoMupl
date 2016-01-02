using System;
using System.Windows.Forms;
using MifuminLib;

namespace NeoMupl
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 既に起動中なら終了する
            if (ProcessHelper.ShowPrevProcess()) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}