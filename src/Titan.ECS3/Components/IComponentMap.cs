using System;

namespace Titan.ECS3.Components
{
    public interface IComponentMap<T>
    {
        Span<T> AsSpan();
        ref T this[uint entityId] {get; }
    }
}
