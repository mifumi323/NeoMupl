#nullable enable
using System;
using QuartzTypeLib;

namespace NeoMupl.Player
{
    /// <summary>DirectShowクラス</summary>
    public class MusicPlayerDS : MusicPlayerBase
    {
        #region 変数

        private IMediaControl? mediaControl;
        private double realLoopStart, realLoopEnd;
        private double rate = 1.0;

        #endregion

        public override void Open()
        {
            mediaControl = new FilgraphManagerClass();
            mediaControl.RenderFile(MusicData.FileName);
        }
        public override void Close() { }

        public override void Play(bool bLoop, double from)
        {
            if (mediaControl == null) return;
            ((IBasicAudio)mediaControl).Volume = (int)(MusicData.Volume > 0 ? 2000 * Math.Log10(MusicData.Volume / 100) : -10000);
            realLoopStart = MusicData.LoopStart;
            realLoopEnd = MusicData.LoopEnd > 0 ? MusicData.LoopEnd : ((IMediaPosition)mediaControl).Duration;
            ((IMediaPosition)mediaControl).Rate = rate;
            ((IMediaPosition)mediaControl).CurrentPosition = from;
            mediaControl.Run();
        }
        public override void Stop() { mediaControl?.Stop(); }
        public override void Loop()
        {
            try
            {
                if (mediaControl == null) return;
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
        { return mediaControl is IMediaPosition mp ? mp.Duration * mp.Rate : 0.0; }
        public override double Position() { return (mediaControl as IMediaPosition)?.CurrentPosition ?? 0.0; }

        public override void SetTempo(double tempo)
        {
            rate = tempo;
            if (mediaControl != null) ((IMediaPosition)mediaControl).Rate = tempo;
        }

        public override double GetVolume()
        { return mediaControl is IBasicAudio ba ? Math.Pow(10, ba.Volume / 2000.0) * 100 : 0.0; }

        public override void Dispose() { Close(); }
    }
}
