namespace Titan.ECS.World
{
    public interface IEntity
    {
        void AddComponent<T>() where T : unmanaged;
        void AddComponent<T>(in T initialData) where T : unmanaged;
        void Destroy();
    }
}
