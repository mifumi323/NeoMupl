﻿using System;
using MifuminLib.DM7Lib;

namespace NeoMupl.Player
{
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

    /// <summary>DirectMusicクラス</summary>
    public class MusicPlayerDM : MusicPlayerBase
    {
        #region 変数

        private readonly DirectMusic music = new DirectMusic();
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
}
