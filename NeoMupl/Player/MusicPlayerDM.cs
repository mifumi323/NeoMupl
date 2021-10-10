#nullable enable
using System;
using MifuminLib.DM7Lib;

namespace NeoMupl.Player
{
    /// <summary>DirectMusicクラス</summary>
    public class MusicPlayerDM : MusicPlayerBase
    {
        #region 変数

        private readonly DirectMusic music = new DirectMusic();
        private DirectMusicSegment? segment = null;

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

        public override void Play(bool bLoop, double from)
        {
            if (segment != null)
            {
                try
                {
                    if (MusicData.Option is DMOption dmo)
                    {
                        music.SelectPort(dmo.port);
                    }
                    else
                    {
                        SelectDefaultPort();
                    }
                }
                catch (Exception)
                {
                    SelectDefaultPort();
                }
                music.Reset(Reset.GM);
                music.SetMasterVolume((int)(MusicData.Volume > 0 ? 2000 * Math.Log10(MusicData.Volume / 100) : -10000));
                segment.SetLoop(bLoop, (int)MusicData.LoopStart, (int)MusicData.LoopEnd);
                music.Play(segment, SegmentFlags.AfterPrepareTime, (long)from);
            }
        }

        private void SelectDefaultPort()
        {
            try { music.SelectPort(DMOption.portdefault); }
            catch (Exception)
            {
                music.SelectDefaultPort();
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
            return segment != null && music.IsPlaying(segment);
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
