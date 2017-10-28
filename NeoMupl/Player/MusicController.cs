using System;
using System.Collections.Generic;

namespace NeoMupl.Player
{
    public class MusicController : IMusicController
    {
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

        public MusicController()
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
