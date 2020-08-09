#nullable enable

namespace NeoMupl.Player
{
    /// <summary>音楽再生のための抽象クラス</summary>
    public abstract class MusicPlayerBase
    {
        public MusicData MusicData { get; set; } = new MusicData("");

        public abstract void Open();
        public abstract void Close();

        public abstract void Play(bool bLoop, double from);
        public abstract void Stop();
        public abstract void Loop();

        public abstract bool IsPlaying();
        public abstract double Length();
        public abstract double Position();

        public abstract void SetTempo(double tempo);

        public abstract void Dispose();

        public virtual double GetVolume() { return MusicData.Volume; }
    }

}