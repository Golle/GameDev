using System;
using System.Collections.Generic;
using Titan.Core.Configuration;
using Titan.IOC;
using Titan.Core.Logging;
using Titan.Graphics;
using Titan.Sound;

namespace Titan
{
    internal class Startup : IDisposable
    {
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();
        
        public void InitializeEngine(IContainer container)
        {
            var configuration = container.GetInstance<IEngineConfigurationHandler>()
                .LoadConfiguration();
            container.RegisterSingleton(configuration);
            var logger = container.GetInstance<ILogger>();

            {
                logger.Debug("Configuration loaded");
            }

            {
                logger.Debug("Initialize D3D device and Window");
                var display = container
                    .GetInstance<IDisplayFactory>()
                    .Create(configuration.Title, configuration.Width, configuration.Height);

                container
                    .RegisterSingleton(display.Window)
                    .RegisterSingleton(display.Device);

                _disposables.Add(display);
            }
            {
                logger.Debug("Initializing sound system with {0} player(s)", configuration.Sound.Players.Length);
                var soundSystem = container
                    .GetInstance<ISoundSystemFactory>()
                    .Create();
                foreach (var player in configuration.Sound.Players)
                {
                    soundSystem.AddPlayer(player.Identifier,
                        new SoundPlayerConfiguration
                        {
                            NumberOfPlayers = player.NumberOfPlayers,
                            AverageBytesPerSecond = (uint) player.AverageBytesPerSecond,
                            SamplesPerSecond = (uint) player.SamplesPerSecond,
                            BlockAlign = (ushort) player.BlockAlign,
                            BitsPerSample = (ushort) player.BitsPerSample,
                            NumberOfChannels = (ushort) player.NumberOfChannels
                        });
                }
                container.RegisterSingleton(soundSystem);
                _disposables.Add(soundSystem);
            }
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
