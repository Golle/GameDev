using System;
using System.Diagnostics;
using System.IO;
using Titan.Components;
using Titan.Core.Configuration;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Sound;

namespace Titan.Resources
{
    internal class SoundClipManager : ResourceManager<string ,ISoundClip>
    {
        private readonly ISoundLoader _soundLoader;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public SoundClipManager(ISoundLoader soundLoader, IConfiguration configuration, ILogger logger)
        {
            _soundLoader = soundLoader;
            _configuration = configuration;
            _logger = logger;
        }
        protected override ISoundClip Load(in string identifier)
        {
            var filename = Path.Combine(_configuration.Paths.Sounds, identifier);
            if (Path.GetExtension(filename) != ".wav")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            var timer = Stopwatch.StartNew();
            var soundclip = _soundLoader.Load(filename);
            timer.Stop();
            _logger.Debug("SoundClip: {0} loaded in {1} ms", identifier, timer.Elapsed.TotalMilliseconds);
            return soundclip;
        }

        protected override void Unload(in string identifier, ISoundClip resource)
        {
            _logger.Debug("Soundclip: {0} unloaded", identifier);
            resource.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, ISoundClip soundClip)
        {
            entity.AddComponent(new SoundClipComponent{Clip = soundClip });
        }
    }
}
