namespace Titan.EntityComponentSystem.Components
{
    internal interface IComponentMapper
    {
        void DestroyComponent(uint entity, uint index);
        uint CreateComponent(uint entity);
    }
    internal interface IComponentMapper<T> : IComponentMapper
    {
        ref T this[uint entity] { get; }
        ref T Get(uint entity);
    }
}
