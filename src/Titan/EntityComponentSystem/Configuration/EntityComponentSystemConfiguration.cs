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

            // This must be called before any system is initialized
            _componentManager.Register(new ComponentPool<Transform3D>(1000, 5000));
            _componentManager.Register(new ComponentPool<TestComponent1>(1000, 5000));
            _componentManager.Register(new ComponentPool<TestComponent2>(1000, 5000));
        }
    }
}
