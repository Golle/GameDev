namespace Titan.ECS.Components
{
    public interface IComponentMapper
    {
        void DestroyComponent(uint entity);
    }

    public interface IComponentMapper<T> : IComponentMapper
    {
        ref T CreateComponent(uint entity);
        ref T this[uint entity] { get; }
        ref T Get(uint entity);
    }
}
