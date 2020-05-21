using Titan.EntityComponentSystem;
using Titan.EntityComponentSystem.Components;
using Titan.Systems.TransformSystem;

namespace Titan.Configuration
{
    internal class EngineConfigurationHandler : IEngineConfigurationHandler
    {
        private readonly IComponentManager _componentManager;

        public EngineConfigurationHandler(IComponentManager componentManager)
        {
            _componentManager = componentManager;
        }
        public void Initialize()
        {
            _componentManager.Register(new ComponentPool<Transform3D>(100, 5000));

        }
    }
}
