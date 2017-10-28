using System.Collections.Generic;

namespace NeoMupl.Player
{
    /// <summary>音楽再生統括クラス</summary>
    public interface IMusicController
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
}
