using System;

namespace Titan.Sound
{
    public interface ISoundSystem : IDisposable
    {
        void AddPlayer(string identifier, in SoundPlayerConfiguration configuration);
        ISoundPlayer GetPlayer(string identifier);
    }
}
