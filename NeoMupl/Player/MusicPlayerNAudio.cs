﻿using System;
using System.Globalization;
using NAudio.Vorbis;
using NAudio.Wave;

namespace NeoMupl.Player
{
    /// <summary>NAudioクラス</summary>
    public class MusicPlayerNAudio : MusicPlayerBase
    {
        IWavePlayer waveOutDevice;
        IWaveProvider audioFileReader;

        public MusicPlayerNAudio()
        {
        }

        public override void Close()
        {
            if (audioFileReader != null)
            {
                Stop();
                ((WaveStream)audioFileReader).Dispose();
                audioFileReader = null;
            }
            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }

        public override void Dispose()
        {
        }

        public override bool IsPlaying()
        {
            return (waveOutDevice?.PlaybackState ?? PlaybackState.Stopped) == PlaybackState.Playing;
        }

        public override double Length()
        {
            return ((WaveStream)audioFileReader)?.TotalTime.TotalSeconds ?? 0.0;
        }

        public override void Loop()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            waveOutDevice = new WaveOut();
            if (MusicData.FileName.EndsWith(".ogg", true, CultureInfo.CurrentCulture))
            {
                audioFileReader = new VorbisWaveReader(MusicData.FileName);
            }
            else
            {
                audioFileReader = new AudioFileReader(MusicData.FileName);
            }
        }

        public override void Play(bool bLoop)
        {
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Volume = (float)(MusicData.Volume * 0.01);
            waveOutDevice.Play();
        }

        public override double Position()
        {
            return ((WaveStream)audioFileReader).CurrentTime.TotalSeconds;
        }

        public override void SetTempo(double tempo)
        {
            // TODO: テンポ変えられたらいいなー
        }

        public override void Stop()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
            }
        }
    }
}
