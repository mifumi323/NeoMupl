#nullable enable
using System;
using System.Windows.Forms;
using System.IO;

namespace NeoMupl
{
    public class Log
    {
        public enum LogType
        {
            Notify,
            Error,
            Play,
            Stop,
            FatalError,
        }

        public static Setting? setting = null;
        public static IErrorNotifier? errorNotifier = null;

        public static void Write(LogType logtype, string message)
        {
            if (setting == null) return;
            using StreamWriter sw = new StreamWriter(setting.LogFile, true);
            sw.WriteLine($"{DateTime.Now}\t{logtype}\t{message.Replace("\r\n", "\t").Replace('\r', '\t').Replace('\n', '\t')}");
        }

        public static bool ShowError(string message, IErrorNotifier.NoticeType type = IErrorNotifier.NoticeType.MessageOnly)
        {
            if (setting != null && setting.ErrorLog) Write(LogType.Error, message);
            return errorNotifier?.Notify(message, type) ?? true;
        }

        public static bool ShowException(string message, Exception e, IErrorNotifier.NoticeType type = IErrorNotifier.NoticeType.MessageOnly)
        {
            if (setting != null && setting.ReportException) message += $"\n\nエラー内容：\n{e.Message}";
            return ShowError(message, type);
        }
    }
}
