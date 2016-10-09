namespace NeoMupl.Player
{
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

}