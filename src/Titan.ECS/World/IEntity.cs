namespace Titan.ECS.World
{
    public interface IEntity
    {
        void AddComponent<T>() where T : struct;
        void AddComponent<T>(in T initialData) where T : struct;
        void Destroy();
    }
}
