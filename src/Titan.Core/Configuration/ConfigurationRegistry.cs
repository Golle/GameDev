using Titan.IOC;

namespace Titan.Core.Configuration
{
    internal class ConfigurationRegistry : IRegistry
    {
        public void Register(IContainer container)
        {
            container
                .Register<IEnginePaths, EnginePaths>()
                .Register<IEngineConfigurationHandler, EngineConfigurationHandler>()
                ;
        }
    }
}
