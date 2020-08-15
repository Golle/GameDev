using System.Diagnostics;
using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Sound;

namespace Titan.Resources
{
    internal class SoundClipManager : ResourceManager<string ,ISoundClip>
    {
        private readonly ISoundLoader _soundLoader;
        private readonly ILogger _logger;

        public SoundClipManager(ISoundLoader soundLoader, ILogger logger)
        {
            _soundLoader = soundLoader;
            _logger = logger;
        }
        protected override ISoundClip Load(in string identifier)
        {
            var timer = Stopwatch.StartNew();
            var soundclip = _soundLoader.Load(identifier);
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
