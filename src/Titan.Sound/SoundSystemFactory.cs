using Titan.Core.Logging;
using Titan.Xaudio2;

namespace Titan.Sound
{
    internal class SoundSystemFactory : ISoundSystemFactory
    {
        private readonly IXAudioDeviceFactory _xAudioDeviceFactory;
        private readonly ILogger _logger;

        public SoundSystemFactory(IXAudioDeviceFactory xAudioDeviceFactory, ILogger logger)
        {
            _xAudioDeviceFactory = xAudioDeviceFactory;
            _logger = logger;
        }
        public ISoundSystem Create()
        {
            var xAudio2 = _xAudioDeviceFactory.CreateDevice();
            var masteringVoice = xAudio2.CreateMasteringVoice();

            return new SoundSystem(xAudio2, masteringVoice, _logger);
        }
    }
}
