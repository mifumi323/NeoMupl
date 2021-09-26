using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NeoMupl
{
    // Windowsのロック関係の処理
    // 参考：https://www.pinvoke.net/default.aspx/wtsapi32.wtsregistersessionnotification
    //       https://social.msdn.microsoft.com/Forums/vstudio/ja-JP/f7f31073-f007-494d-8589-d02e6db313fe/pc1239812525124831246312434354693567212377124272604127861
    partial class FormMain
    {
        [DllImport("WtsApi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int dwFlags);
        [DllImport("WtsApi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        private const int NOTIFY_FOR_THIS_SESSION = 0;
        private const int WM_WTSSESSION_CHANGE = 0x2b1;
        private const int WTS_SESSION_LOCK = 0x7;
        private const int WTS_SESSION_UNLOCK = 0x8;

        public event EventHandler MachineLocked;
        public event EventHandler MachineUnlocked;

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!WTSRegisterSessionNotification(Handle, NOTIFY_FOR_THIS_SESSION))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // unregister the handle before it gets destroyed
            WTSUnRegisterSessionNotification(Handle);
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WTSSESSION_CHANGE)
            {
                int value = m.WParam.ToInt32();
                if (value == WTS_SESSION_LOCK)
                {
                    OnMachineLocked(EventArgs.Empty);
                }
                else if (value == WTS_SESSION_UNLOCK)
                {
                    OnMachineUnlocked(EventArgs.Empty);
                }
            }
            base.WndProc(ref m);
        }

        protected virtual void OnMachineLocked(EventArgs e)
        {
            MachineLocked?.Invoke(this, e);
        }

        protected virtual void OnMachineUnlocked(EventArgs e)
        {
            MachineUnlocked?.Invoke(this, e);
        }
    }
}
