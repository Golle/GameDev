using System;

namespace Titan.ECS2.Components
{
    public interface IComponentMapper : IDisposable
    {
        void Destroy(uint entityId);

    }
    public interface IComponentMapper<T> : IComponentMapper where T : struct
    {
        ref T this[uint entityId] { get; }
        void Create(uint entityId, in T value);
    }
}
