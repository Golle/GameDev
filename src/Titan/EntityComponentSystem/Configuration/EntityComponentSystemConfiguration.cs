using Titan.EntityComponentSystem.Components;
using Titan.Systems.TransformSystem;

namespace Titan.EntityComponentSystem.Configuration
{
    internal class EntityComponentSystemConfiguration : IEntityComponentSystemConfiguration
    {
        private readonly IComponentManager _componentManager;

        public EntityComponentSystemConfiguration(IComponentManager componentManager)
        {
            _componentManager = componentManager;
        }

        public void Initialize()
        {
            _componentManager.Register(new ComponentPool<Transform3D>(100, 5000));
        }
    }
}
