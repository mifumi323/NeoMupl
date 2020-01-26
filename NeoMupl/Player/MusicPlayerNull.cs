#nullable enable

namespace NeoMupl.Player
{
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
}
