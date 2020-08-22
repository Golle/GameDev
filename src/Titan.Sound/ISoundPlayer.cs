using System;
using Titan.Core.Assets.Wave;

namespace Titan.Sound
{
    public interface ISoundPlayer : IDisposable
    {
        float GetVolume();
        void SetVolume(float volume);
        void Play(WaveData wave); // TODO: add configuration like looping, channel etc
        void Stop();
    }
}
