using System;
using Titan.Core.Assets.Wave;
using Titan.Core.Logging;
using Titan.Windows;
using Titan.Xaudio2;
using Titan.Xaudio2.Bindings;

namespace Titan.Sound
{
    internal class SoundPlayer : ISoundPlayer, IXAudio2VoiceCallback
    {
        private readonly ILogger _logger;
        private readonly XAudio2Player[] _players;
        public SoundPlayer(IXAudio2 xAudio2, in SoundPlayerConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _players = new XAudio2Player[configuration.NumberOfPlayers];
            var format = new WaveformatEx
            {
                nAvgBytesPerSec = configuration.AverageBytesPerSecond,
                nBlockAlign = configuration.BlockAlign,
                nChannels = configuration.NumberOfChannels,
                nSamplesPerSec = configuration.SamplesPerSecond,
                wBitsPerSample = configuration.BitsPerSample,
                wFormatTag = 1,
                cbSize = 0
            };

            for (var i = 0; i < configuration.NumberOfPlayers; ++i)
            {
                _players[i] = new XAudio2Player(xAudio2.CreateSourceVoice(format, this)); ;
            }
        }

        public float GetVolume()
        {
            return _players[0].GetVolume();
        }

        public void SetVolume(float volume)
        {
            for (var i = 0; i < _players.Length; ++i)
            {
                _players[i].SetVolume(volume);
            }
        }

        public void Play(WaveData wave)
        {
            for (var i = 0; i < _players.Length; ++i)
            {
                var player = _players[i];
                if (player.IsPlaying)
                {
                    continue;
                }

                player.IsPlaying = true;
                var buffer = new Xaudio2Buffer();
                unsafe
                {
                    buffer.Context = (IntPtr)i;
                    buffer.AudioData = (byte*)wave.Data.ToPointer();
                    buffer.AudioBytes = wave.Size;
                    buffer.Flags = Xaudio2Constants.Xaudio2EndOfStream;
                }
                // maybe override format if required? there are APIs to update frequency etc
                _logger.Debug("Playing sound with player {0}", i);
                player.Play(buffer);
                break;
            }
        }

        public void Stop()
        {
            // TODO: add implementation to stop sound
        }

        public void OnVoiceProcessingPassStart(uint bytesRequired) { }
        public void OnVoiceProcessingPassEnd() { }
        public void OnStreamEnd() { }
        public void OnBufferStart(IntPtr pBufferContext) { }
        public void OnBufferEnd(IntPtr pBufferContext)
        {
            var index = pBufferContext.ToInt32();
            if (index < _players.Length)
            {
                _players[index].IsPlaying = false;
                _logger.Debug("Finished playing sound with player {0}", index);
            }
        }
        public void OnLoopEnd(IntPtr pBufferContext) { }
        public void OnVoiceError(IntPtr pBufferContext, HRESULT error) { }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                player.Dispose();
            }
        }
    }
}
