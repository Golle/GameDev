using System.Runtime.InteropServices.ComTypes;

namespace Titan.ECS.Components
{
    public interface IComponentMapper
    {
        void DestroyComponent(uint entity, uint index);
        uint CreateComponent(uint entity);
    }

    public interface IComponentMapper<T> : IComponentMapper
    {
        ref T CreateComponent(uint entity, out uint index);
        ref T this[uint entity] { get; }
        ref T Get(uint entity);
    }
}
