#nullable enable
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MifuminLib
{
    public class ProcessHelper
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern bool SetForegroundWindow(
            IntPtr hWnd
        );

        /// <summary>
        /// 同名のプロセスが起動中の場合、メイン ウィンドウをアクティブにします。
        /// 参考：http://jeanne.wankuma.com/tips/csharp/process/activewindow.html
        /// </summary>
        /// <returns>既に起動中であれば true。それ以外は false。</returns>
        public static bool SetPrevProcessToForeground()
        {
            Process hThisProcess = Process.GetCurrentProcess();
            Process[] hProcesses = Process.GetProcessesByName(hThisProcess.ProcessName);
            int iThisProcessId = hThisProcess.Id;

            foreach (Process hProcess in hProcesses)
            {
                if (hProcess.Id != iThisProcessId)
                {
                    SetForegroundWindow(hProcess.MainWindowHandle);
                    return true;
                }
            }

            return false;
        }
    }
}
