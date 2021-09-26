#nullable enable
using System;
using System.Threading;
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
            if (ProcessHelper.SetPrevProcessToForeground())
            {
                // 再起動中かもしれないのでちょっと待ってみる
                // TODO: もっとちゃんとした実装するか、再起動不要にする対応をしたい
                Thread.Sleep(1000);
                if (ProcessHelper.SetPrevProcessToForeground())
                {
                    // 本当に起動中だったので心置きなく終了する
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}