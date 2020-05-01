using Titan.Core.Common;
using Titan.Core.Configuration;
using Titan.Core.EventSystem;
using Titan.Core.Ioc;
using Titan.Core.Logging;

namespace Titan.Core
{
    public class Bootstrapper
    {
        public static IContainer CreateContainer()
        {
            return new Container()

                    // Core functions
                    .Register<IDateTime, DateTimeWrapper>()


                    // Logging
                    .Register<ILogger, ConsoleLogger>() // should depend on configuration
                    .Register<ILogFormatter, LogFormatter>()


                    // configuration    
                    .RegisterSingleton<IConfiguration>(new EngineConfiguration()) // currently using hardcoded configuration
                    
                    // Event Manager
                    .Register<IEventManager, EventManager>()

                ;

        }
    }
}
