namespace Titan.ECS.Components
{
    public interface IComponentMapper
    {
        void DestroyComponent(uint entity, uint index);
        uint CreateComponent(uint entity);
    }

    public interface IComponentMapper<T> : IComponentMapper
    {
        ref T this[uint entity] { get; }
        ref T Get(uint entity);
    }
}
