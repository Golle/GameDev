namespace Titan.ECS.Systems
{
    public interface IContext
    {
        void OnEntityDestroyed(uint entity);
        void OnEntityCreated(uint entity);
        void RegisterSystem(ISystem system);
    }
}
