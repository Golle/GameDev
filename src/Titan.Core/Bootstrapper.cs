using Titan.Core.Assets;
using Titan.Core.Common;
using Titan.Core.Configuration;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop;
using Titan.IOC;
using Titan.Core.Json;
using Titan.Core.Logging;

namespace Titan.Core
{
    public class Bootstrapper
    {
        public static IContainer CreateContainer()
        {
            return new Container()

                    .AddRegistry<ConfigurationRegistry>()
                    .AddRegistry<AssetsRegistry>()
                    
                    // Core functions
                    .Register<IDateTime, DateTimeWrapper>()
                    .Register<IFileReader, FileReader>()


                    // Logging
                    .Register<ILogger, ConsoleLogger>() // should depend on configuration
                    .Register<ILogFormatter, LogFormatter>()
                    
                    // Event Manager
                    .Register<IEventManager, EventManager>()

                    // GameLoop
                    .Register<IGameLoop, BasicGameLoop>()

                    .Register<IJsonSerializer, JsonSerializerWrapper>()

                ;

        }
    }
}
