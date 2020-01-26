#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NeoMupl.Player
{
    public class MCIException : ApplicationException
    {
        [DllImport("winmm.dll")]
#pragma warning disable IDE1006 // 命名スタイル
        static extern Int32 mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);
#pragma warning restore IDE1006 // 命名スタイル

        public MCIException()
        { }

        public MCIException(string message)
            : base(message)
        { }

        protected MCIException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }

        public MCIException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public MCIException(int errorCode)
            : base(GetErrorString(errorCode))
        { }

        static string GetErrorString(int errorCode)
        {
            StringBuilder buf = new StringBuilder(1024);
            mciGetErrorString(errorCode, buf, 1024);
            return buf.ToString();
        }
    }

    public class MusicPlayerMCI : MusicPlayerBase
    {
        [DllImport("winmm.dll")]
#pragma warning disable IDE1006 // 命名スタイル
        static extern Int32 mciSendString(String command, StringBuilder? buffer, Int32 bufferSize, IntPtr hwndCallback);
#pragma warning restore IDE1006 // 命名スタイル

        public override void Open()
        {
            SendString("open \"" + MusicData.FileName + "\" alias NeoMupl");
            SendString("set NeoMupl time format milliseconds");
        }
        public override void Close()
        {
            SendString("close NeoMupl");
        }

        public override void Play(bool bLoop)
        {
            SendString("play NeoMupl from 0");
        }
        public override void Stop()
        {
            SendString("stop NeoMupl");
        }
        public override void Loop()
        {
            if (!IsPlaying() && Position() >= Length()) SendString("play NeoMupl from 0");
        }

        public override bool IsPlaying()
        {
            return GetString("status NeoMupl mode").StartsWith("playing");
        }
        public override double Length()
        {
            return double.Parse(GetString("status NeoMupl length")) * 0.001;
        }
        public override double Position()
        {
            return double.Parse(GetString("status NeoMupl position")) * 0.001;
        }

        public override void SetTempo(double tempo) { }

        public override void Dispose() { }

        static public string GetString(string command)
        {
            StringBuilder buf = new StringBuilder(256);
            int ret = mciSendString(command, buf, 256, IntPtr.Zero);
            if (ret != 0)
            {
                throw new MCIException(ret);
            }
            return buf.ToString();
        }

        static public void SendString(string command)
        {
            int ret = mciSendString(command, null, 0, IntPtr.Zero);
            if (ret != 0)
            {
                throw new MCIException(ret);
            }
        }
    }
}
