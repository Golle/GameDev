using Titan.Core.Ioc;
using Titan.Xaudio2;

namespace Titan.Sound
{
    public class SoundRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .AddRegistry<XAudio2Registry>()

                .Register<ISoundLoader, SoundLoader>()

                ;
        }
    }
}
