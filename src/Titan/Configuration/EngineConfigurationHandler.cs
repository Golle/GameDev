using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Components;
using Titan.EntityComponentSystem.Configuration;
using Titan.Systems.TransformSystem;

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
