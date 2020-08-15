using System;
using Titan.Core.Assets.Wave;
using Titan.Xaudio2.Bindings;

namespace Titan.Sound
{
    internal class XAudio2SoundClip : ISoundClip
    {
        private readonly WaveData _data;

        public XAudio2SoundClip(WaveData data)
        {
            _data = data;
        }
        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _data?.Dispose();
        }
    }
}
