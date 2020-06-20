using System;

namespace Titan.ECS.Components
{
    public interface IComponentMap<T>
    {
        Span<T> AsSpan();
        ref T this[uint entityId] {get; }
    }
}
