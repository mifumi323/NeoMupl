using System;
using System.Windows.Forms;
using System.IO;

namespace NeoMupl
{
    internal class Log
    {
        public enum LogType
        {
            Notify,
            Error,
            Play,
            Stop,
            FatalError,
        }

        public static Setting setting = null;
        public static IWin32Window owner = null;

        public static void Write(LogType logtype, string message)
        {
            if (setting == null) return;
            using (StreamWriter sw = new StreamWriter(setting.LogFile, true))
            {
                sw.WriteLine(
                    DateTime.Now.ToString() + "\t" +
                    logtype.ToString() + "\t" +
                    message.Replace("\r\n", "\t").Replace('\r', '\t').Replace('\n', '\t'));
            }
        }

        public static DialogResult Error(string message)
        {
            return Error(message, MessageBoxButtons.OK);
        }

        public static DialogResult Error(string message, MessageBoxButtons buttons)
        {
            return Error(message, buttons, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Error(string message, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            if (setting != null && setting.ErrorLog) Write(LogType.Error, message);
            return MessageBox.Show(owner, message, "NeoMupl エラー！", buttons, MessageBoxIcon.Error, defaultButton);
        }

        public static DialogResult Error(string message, Exception e)
        {
            return Error(message, e, MessageBoxButtons.OK);
        }

        public static DialogResult Error(string message, Exception e, MessageBoxButtons buttons)
        {
            return Error(message, e, buttons, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Error(string message, Exception e, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            if (setting != null && setting.ReportException) message += "\n\nエラー内容：\n" + e.Message;
            return Error(message, buttons, defaultButton);
        }
    }
}
