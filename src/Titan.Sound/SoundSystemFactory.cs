using Titan.Core.Configuration;
using Titan.Core.Logging;
using Titan.Xaudio2;

namespace Titan.Sound
{
    internal class SoundSystemFactory : ISoundSystemFactory
    {
        private readonly IXAudioDeviceFactory _xAudioDeviceFactory;
        private readonly ILogger _logger;

        public SoundSystemFactory(IXAudioDeviceFactory xAudioDeviceFactory, IConfiguration configuration, ILogger logger)
        {
            _xAudioDeviceFactory = xAudioDeviceFactory;
            _logger = logger;
        }
        public ISoundSystem Create()
        {
            var xAudio2 = _xAudioDeviceFactory.CreateDevice();
            var masteringVoice = xAudio2.CreateMasteringVoice();

            var soundSystem = new SoundSystem(xAudio2, masteringVoice, _logger);
            return soundSystem;
        }
    }
}
