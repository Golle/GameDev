using Titan.EntityComponentSystem.Configuration;

namespace Titan.Configuration
{
    internal class EngineConfigurationHandler : IEngineConfigurationHandler
    {
        private readonly IEntityComponentSystemConfiguration _entityComponentSystemConfiguration;

        public EngineConfigurationHandler(IEntityComponentSystemConfiguration entityComponentSystemConfiguration)
        {
            _entityComponentSystemConfiguration = entityComponentSystemConfiguration;
        }
        public void Initialize()
        {
            _entityComponentSystemConfiguration.Initialize();
        }
    }
}
