using Titan.Core.Ioc;

namespace Titan.Xaudio2
{
    public class XAudio2Registry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IXAudioDeviceFactory, XAudioDeviceFactory>()
                ;
        }
    }
}
