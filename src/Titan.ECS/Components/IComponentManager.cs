namespace Titan.ECS.Components
{
    public interface IComponentManager
    {
        void RegisterComponent<T>(uint capacity) where T : unmanaged;
        IComponentMapper<T> GetComponentMapper<T>() where T : unmanaged;
        Component Create<T>(uint entity) where T : unmanaged;
        void Free(in Component component);
    }
}
