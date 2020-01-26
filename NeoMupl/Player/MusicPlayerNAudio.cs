#nullable enable
using System;
using System.Globalization;
using NAudio.Vorbis;
using NAudio.Wave;

namespace NeoMupl.Player
{
    /// <summary>NAudioクラス</summary>
    public class MusicPlayerNAudio : MusicPlayerBase
    {
        IWavePlayer? waveOutDevice;
        IWaveProvider? audioFileReader;

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
            return (audioFileReader as WaveStream)?.TotalTime.TotalSeconds ?? 0.0;
        }

        public override void Loop()
        {
            var length = Length();
            if (length > 0 && audioFileReader is WaveStream ws)
            {
                if (!IsPlaying())
                {
                    ws.CurrentTime = TimeSpan.FromSeconds(MusicData.LoopStart);
                    waveOutDevice?.Play();
                }
                else
                {
                    var loopStart = MusicData.LoopStart;
                    var loopEnd = (MusicData.LoopEnd <= loopStart || MusicData.LoopEnd >= length) ? length : MusicData.LoopEnd;
                    var position = Position();
                    var overrun = position - loopEnd;
                    if (overrun >= 0)
                    {
                        ws.CurrentTime = TimeSpan.FromSeconds(loopStart + (overrun % (loopEnd - loopStart)));
                    }
                }
            }
            else
            {
                if (!IsPlaying())
                {
                    waveOutDevice?.Stop();
                    if (audioFileReader is WaveStream ws2) ws2.Position = 0;
                    waveOutDevice?.Play();
                }
            }
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
            if (waveOutDevice != null)
            {
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Volume = (float)(MusicData.Volume * 0.01);
                waveOutDevice.Play();
            }
        }

        public override double Position()
        {
            return (audioFileReader as WaveStream)?.CurrentTime.TotalSeconds ?? 0.0;
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
