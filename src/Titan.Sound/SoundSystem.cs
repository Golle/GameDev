using System.Collections.Generic;
using Titan.Core.Logging;
using Titan.Xaudio2;

namespace Titan.Sound
{
    /*
        TODO
            * Dont call COM Release on anything except the main XAUdio object.why ?
            *On release, stop all sounds, release buffers and wait for all to stop(callbacks called) or it might crash.
         * */
    internal class SoundSystem : ISoundSystem
    {
        private readonly IXAudio2 _xAudio2;
        private readonly IXAudio2MasteringVoice _masteringVoice;
        private readonly ILogger _logger;

        private readonly IDictionary<string, ISoundPlayer> _players = new Dictionary<string, ISoundPlayer>(10);

        public SoundSystem(IXAudio2 xAudio2, IXAudio2MasteringVoice masteringVoice, ILogger logger)
        {
            _xAudio2 = xAudio2;
            _masteringVoice = masteringVoice;
            _logger = logger;
        }

        public void AddPlayer(string identifier, in SoundPlayerConfiguration configuration)
        {
            _logger.Debug("Creating Sound player with identifier '{0}' with a maximum of {1} sounds playing simultaneously.", identifier, configuration.NumberOfPlayers);
            _players.Add(identifier, new SoundPlayer(_xAudio2, configuration, _logger));
        }

        public ISoundPlayer GetPlayer(string identifier) => _players.TryGetValue(identifier, out var player) ? player : null;
        public void Dispose()
        {
            foreach (var player in _players.Values)
            {
                player.Stop();
                player.Dispose();
            }
            _players.Clear();
            //_masteringVoice.Dispose();
            _xAudio2.Dispose();
        }
    }
}
