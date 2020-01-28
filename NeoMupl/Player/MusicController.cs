#nullable enable
using System;

namespace NeoMupl.Player
{
    /// <summary>音楽再生統括クラス</summary>
    public class MusicController
    {
        private readonly MusicPlayerBase[] musicPlayers;
        private MusicPlayerBase musicPlayer = new MusicPlayerNull();
        public MusicPlayerDS? MusicPlayerDS { get; }
        public MusicPlayerDM? MusicPlayerDM { get; }
        public MusicPlayerMCI? MusicPlayerMCI { get; }
        public MusicPlayerNAudio? MusicPlayerNAudio { get; }

        private MusicData? myData = null;
        public MusicData? Data
        {
            get => myData;
            set
            {
                if (myData != null)
                {
                    musicPlayer.Stop();
                    musicPlayer.Close();
                }
                myData = value;
                musicPlayer = (value != null) ? GetPlayer(value.PlayMethod) : new MusicPlayerNull();
                if (value != null)
                {
                    musicPlayer.MusicData = value;
                    musicPlayer.Open();
                }
            }
        }

        public MusicController()
        {
            musicPlayers = new MusicPlayerBase[] {
                new MusicPlayerNull(), new MusicPlayerNull(), new MusicPlayerNull(), new MusicPlayerNull()
            };
            try
            {
                musicPlayers[0] = MusicPlayerDS = new MusicPlayerDS();
            }
            catch (Exception e)
            {
                Log.Error("DirectShowの初期化に失敗", e);
            }
            try
            {
                musicPlayers[1] = MusicPlayerDM = new MusicPlayerDM();
            }
            catch (Exception e)
            {
                Log.Error("DirectMusicの初期化に失敗", e);
            }
            try
            {
                musicPlayers[2] = MusicPlayerMCI = new MusicPlayerMCI();
            }
            catch (Exception e)
            {
                Log.Error("MCIの初期化に失敗\n", e);
            }
            try
            {
                musicPlayers[3] = MusicPlayerNAudio = new MusicPlayerNAudio();
            }
            catch (Exception e)
            {
                Log.Error("NAudioの初期化に失敗\n", e);
            }
        }

        public bool Loop { get; private set; }

        public bool IsPlaying => musicPlayer.IsPlaying();
        public double Length => musicPlayer.Length();
        public double Position => musicPlayer.Position();
        public void Play(bool bLoop) { if (myData != null) { musicPlayer.Play(Loop = bLoop); myData.TimeStamp(); } }
        public void Stop() { foreach (MusicPlayerBase mp in musicPlayers) mp.Stop(); }
        public void LoopMethod() { musicPlayer.Loop(); }
        public void Dispose()
        {
            foreach (MusicPlayerBase mp in musicPlayers)
            {
                mp.Dispose();
            }
        }

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

        public string[] GetDirectMusicPorts()
        {
            if (MusicPlayerDM == null) return new string[0];
            return MusicPlayerDM.GetPorts();
        }

        public void SetDirectMusicPort(string p)
        {
            MusicPlayerDM?.SetPort(p);
        }

        public MusicPlayerBase GetPlayer(PlayMethod playMethod)
        {
            return musicPlayers[(int)playMethod];
        }
    }
}
