using System;

namespace Titan.Sound
{
    public interface ISoundClip : IDisposable
    {
        void Play(ISoundPlayer player); // TODO: this sound clip needs to be pushed to a queue to support multiple sources playing the same sound.
        void Stop();
    }
}
