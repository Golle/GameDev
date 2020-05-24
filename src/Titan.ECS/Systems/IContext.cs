using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    public interface IContext
    {
        void OnEntityDestroyed(uint entity);
        void OnEntityCreated(uint entity);
        void OnComponentCreated(in Component component);
        void OnComponentDestroyed(in Component component);
        void RegisterSystem(ISystem system);
    }
}
