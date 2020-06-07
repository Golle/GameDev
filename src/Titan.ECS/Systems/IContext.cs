namespace Titan.ECS.Systems
{
    public interface IContext
    {
        void OnEntityDestroyed(uint entity);
        void OnEntityCreated(uint entity);
        void RegisterSystem(ISystem system);
        void OnComponentCreated(uint entity, ulong componentId);
        void OnComponentDestroyed(uint entity, ulong componentId);
    }
}
