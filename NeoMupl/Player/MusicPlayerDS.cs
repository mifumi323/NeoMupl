using System;
using QuartzTypeLib;

namespace NeoMupl.Player
{
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
}
