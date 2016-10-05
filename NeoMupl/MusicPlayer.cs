using System;
using System.Text;
using QuartzTypeLib;
using MifuminLib.DM7Lib;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NAudio.Wave;
using NAudio.Vorbis;
using System.Globalization;

namespace NeoMupl.Player
{
    /// <summary>演奏方法</summary>
    public enum PlayMethod { DirectShow, DirectMusic, MCI, NAudio }

    public class DMOption
    {
        public string port;
        static public string portdefault = "Default";
        public DMOption()
        {
            port = "";
        }
        public DMOption(string port)
        {
            this.port = port;
        }
    }

    public class MCIException : ApplicationException
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);

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


    /// <summary>音楽再生統括クラス</summary>
    public interface IMusicPlayer
    {
        MusicData Data
        {
            get;
            set;
        }
        bool Loop
        {
            get;
        }
        bool IsPlaying
        {
            get;
        }
        double Length
        {
            get;
        }
        double Position
        {
            get;
        }
        void Play(bool bLoop);
        void Stop();
        void LoopMethod();
        void Dispose();
        void SetTempo(double tempo);
        double Volume
        {
            get;
        }
        IEnumerable<string> GetDirectMusicPorts();
        void SetDirectMusicPort(string p);
    }

    public class MusicPlayer : IMusicPlayer
    {
        #region 内部クラス

        /// <summary>音楽再生のための抽象クラス</summary>
        public abstract class MusicPlayerBase
        {
            private MusicData myMusicData;
            public MusicData MusicData
            {
                get { return myMusicData; }
                set { myMusicData = value; }
            }


            public abstract void Open();
            public abstract void Close();

            public abstract void Play(bool bLoop);
            public abstract void Stop();
            public abstract void Loop();

            public abstract bool IsPlaying();
            public abstract double Length();
            public abstract double Position();

            public abstract void SetTempo(double tempo);

            public abstract void Dispose();

            public virtual double GetVolume() { return MusicData.Volume; }
        }
        /// <summary>DirectShowクラス</summary>
        public class MusicPlayerDS : MusicPlayerBase
        {
            #region 変数

            private IMediaControl mediaControl;
            private double realLoopStart, realLoopEnd;
            private double rate = 1.0;

            #endregion

            public override void Open()
            {
                mediaControl = new FilgraphManagerClass();
                mediaControl.RenderFile(MusicData.FileName);
            }
            public override void Close() { }

            public override void Play(bool bLoop)
            {
                ((IBasicAudio)mediaControl).Volume = (int)(MusicData.Volume > 0 ? 2000 * Math.Log10(MusicData.Volume / 100) : -10000);
                realLoopStart = MusicData.LoopStart;
                realLoopEnd = MusicData.LoopEnd > 0 ? MusicData.LoopEnd : ((IMediaPosition)mediaControl).Duration;
                ((IMediaPosition)mediaControl).Rate = rate;
                if (typeof(IBasicVideo).IsInstanceOfType(mediaControl))
                {
                    IBasicVideo v = (IBasicVideo)mediaControl;
                }
                mediaControl.Run();
            }
            public override void Stop() { mediaControl.Stop(); }
            public override void Loop()
            {
                try
                {
                    double overrun = ((IMediaPosition)mediaControl).CurrentPosition - realLoopEnd;
                    if (overrun >= 0) ((IMediaPosition)mediaControl).CurrentPosition = realLoopStart + overrun;
                }
                finally { }
            }

            public override bool IsPlaying()
            {
                try { return Position() < Length(); }
                catch (Exception) { return false; }
            }
            public override double Length()
            { return ((IMediaPosition)mediaControl).Duration * ((IMediaPosition)mediaControl).Rate; }
            public override double Position() { return ((IMediaPosition)mediaControl).CurrentPosition; }

            public override void SetTempo(double tempo)
            {
                rate = tempo;
                if (mediaControl != null) ((IMediaPosition)mediaControl).Rate = tempo;
            }

            public override double GetVolume()
            { return Math.Pow(10, ((IBasicAudio)mediaControl).Volume / 2000.0) * 100; }

            public override void Dispose() { Close(); }
        }
        /// <summary>DirectMusicクラス</summary>
        public class MusicPlayerDM : MusicPlayerBase
        {
            #region 変数

            private DirectMusic music = new DirectMusic();
            private DirectMusicSegment segment = null;

            #endregion

            public MusicPlayerDM()
            { }

            public override void Open()
            {
                segment = music.CreateSegment(MusicData.FileName);
                if (!segment.IsLoaded())
                {
                    segment = null;
                    throw new Exception("Cannot create Segment");
                }
            }
            public override void Close()
            { }

            public override void Play(bool bLoop)
            {
                if (segment != null)
                {
                    try
                    {
                        music.SelectPort(((DMOption)MusicData.Option).port);
                    }
                    catch (Exception)
                    {
                        try { music.SelectPort(DMOption.portdefault); }
                        catch (Exception)
                        {
                            music.SelectDefaultPort();
                        }
                    }
                    music.Reset(Reset.GM);
                    music.SetMasterVolume((int)(MusicData.Volume > 0 ? 2000 * Math.Log10(MusicData.Volume / 100) : -10000));
                    segment.SetLoop(bLoop, (int)MusicData.LoopStart, (int)MusicData.LoopEnd);
                    music.Play(segment, SegmentFlags.AfterPrepareTime, 0);
                }
            }
            public override void Stop()
            {
                // セグメントを指定するとメッセージ配信は止まっても音自体は消えない
                if (segment != null) music.Stop(/*segment*/);
            }
            public override void Loop() { }

            public override bool IsPlaying()
            {
                return segment != null ? music.IsPlaying(segment) : false;
            }
            public override double Length() { return segment != null ? (double)segment.GetLength() : 0.0; }
            public override double Position() { return segment != null ? (double)segment.GetSeek() : 0.0; }

            public override void SetTempo(double tempo)
            {
                music.SetMasterTempo((float)tempo);
            }

            public override void Dispose() { }

            public string[] GetPorts()
            {
                return music.EnumPort();
            }
            public void SetPort(string portName)
            {
                music.SelectPort(portName);
            }
        }
        public class MusicPlayerMCI : MusicPlayerBase
        {
            [DllImport("winmm.dll")]
            static extern Int32 mciSendString(String command,
               StringBuilder buffer, Int32 bufferSize, IntPtr hwndCallback);

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
        /// <summary>NAudioクラス</summary>
        public class MusicPlayerNAudio : MusicPlayerBase
        {
            //Declarations required for audio out and the MP3 stream
            IWavePlayer waveOutDevice;
            IWaveProvider audioFileReader;

            public MusicPlayerNAudio()
            {
            }

            public override void Close()
            {
                if (audioFileReader != null)
                {
                    Stop();
                    ((WaveStream)audioFileReader).Dispose();
                    audioFileReader = null;
                }
                if (waveOutDevice != null)
                {
                    waveOutDevice.Dispose();
                    waveOutDevice = null;
                }
            }

            public override void Dispose()
            {
            }

            public override bool IsPlaying()
            {
                return (waveOutDevice?.PlaybackState ?? PlaybackState.Stopped) == PlaybackState.Playing;
            }

            public override double Length()
            {
                return ((WaveStream)audioFileReader)?.TotalTime.TotalSeconds ?? 0.0;
            }

            public override void Loop()
            {
                throw new NotImplementedException();
            }

            public override void Open()
            {
                waveOutDevice = new WaveOut();
                if (MusicData.FileName.EndsWith(".ogg", true, CultureInfo.CurrentCulture))
                {
                    audioFileReader = new VorbisWaveReader(MusicData.FileName);
                }
                else
                {
                    audioFileReader = new AudioFileReader(MusicData.FileName);
                }
            }

            public override void Play(bool bLoop)
            {
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Volume = (float)(MusicData.Volume * 0.01);
                waveOutDevice.Play();
            }

            public override double Position()
            {
                return ((WaveStream)audioFileReader).CurrentTime.TotalSeconds;
            }

            public override void SetTempo(double tempo)
            {
                // TODO: テンポ変えられたらいいなー
            }

            public override void Stop()
            {
                if (waveOutDevice != null)
                {
                    waveOutDevice.Stop();
                }
            }
        }
        /// <summary>何もしないクラス</summary>
        public class MusicPlayerNull : MusicPlayerBase
        {
            public override void Open() { }
            public override void Close() { }

            public override void Play(bool bLoop) { }
            public override void Stop() { }
            public override void Loop() { }

            public override bool IsPlaying() { return false; }
            public override double Length() { return 0; }
            public override double Position() { return 0; }

            public override void SetTempo(double tempo) { }

            public override void Dispose() { }
        }

        #endregion

        private MusicPlayerBase[] musicPlayers;
        private MusicPlayerBase musicPlayer = new MusicPlayerNull();
        private MusicPlayerDS musicPlayerDS;
        private MusicPlayerDM musicPlayerDM;
        private MusicPlayerMCI musicPlayerMCI;
        private MusicPlayerNAudio musicPlayerNAudio;

        private MusicData myData = null;
        public MusicData Data
        {
            get { return myData; }
            set
            {
                if (myData != null)
                {
                    musicPlayer.Stop();
                    musicPlayer.Close();
                }
                (musicPlayer = ((myData = value) != null) ? musicPlayers[(int)value.PlayMethod] : new MusicPlayerNull())
                    .MusicData = myData;
                musicPlayer.Open();
            }
        }

        public MusicPlayer()
        {
            musicPlayers = new MusicPlayerBase[] {
                new MusicPlayerNull(), new MusicPlayerNull(), new MusicPlayerNull(), new MusicPlayerNull()
            };
            try
            {
                musicPlayers[0] = musicPlayerDS = new MusicPlayerDS();
            }
            catch (Exception e)
            {
                Log.Error("DirectShowの初期化に失敗", e);
            }
            try
            {
                musicPlayers[1] = musicPlayerDM = new MusicPlayerDM();
            }
            catch (Exception e)
            {
                Log.Error("DirectMusicの初期化に失敗", e);
            }
            try
            {
                musicPlayers[2] = musicPlayerMCI = new MusicPlayerMCI();
            }
            catch (Exception e)
            {
                Log.Error("MCIの初期化に失敗\n", e);
            }
            try
            {
                musicPlayers[3] = musicPlayerNAudio = new MusicPlayerNAudio();
            }
            catch (Exception e)
            {
                Log.Error("NAudioの初期化に失敗\n", e);
            }
        }

        private bool myLoop;
        public bool Loop { get { return myLoop; } }

        public bool IsPlaying { get { return musicPlayer.IsPlaying(); } }
        public double Length { get { return musicPlayer.Length(); } }
        public double Position { get { return musicPlayer.Position(); } }
        public void Play(bool bLoop) { musicPlayer.Play(myLoop = bLoop); myData.TimeStamp(); }
        public void Stop() { foreach (MusicPlayerBase mp in musicPlayers) mp.Stop(); }
        public void LoopMethod() { musicPlayer.Loop(); }
        public void Dispose()
        {
            foreach (MusicPlayerBase mp in musicPlayers)
            {
                mp.Dispose();
            }
        }

        private MusicPlayerDS DirectShow { get { return musicPlayerDS; } }
        private MusicPlayerDM DirectMusic { get { return musicPlayerDM; } }


        public void SetTempo(double tempo)
        {
            foreach (MusicPlayerBase mp in musicPlayers)
            {
                mp.SetTempo(tempo);
            }
        }

        public double Volume
        {
            get { return musicPlayer.GetVolume(); }
        }

        public IEnumerable<string> GetDirectMusicPorts()
        {
            if (musicPlayerDM == null) return new string[0];
            return musicPlayerDM.GetPorts();
        }

        public void SetDirectMusicPort(string p)
        {
            musicPlayerDM.SetPort(p);
        }
    }
}
