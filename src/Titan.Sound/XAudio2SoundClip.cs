using System;
using Titan.Core.Assets.Wave;

namespace Titan.Sound
{
    internal class XAudio2SoundClip : ISoundClip
    {
        private readonly WaveData _data;
        public XAudio2SoundClip(WaveData data)
        {
            _data = data;
        }

        public void Play(ISoundPlayer player)
        {
            // TODO: add some kind of reference to the playing sound
            player.Play(_data);
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
