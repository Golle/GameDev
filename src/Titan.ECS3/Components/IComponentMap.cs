using System;

namespace Titan.ECS3.Components
{
    internal interface IComponentMap<T>
    {
        Span<T> AsSpan();
        ref T this[uint entityId] {get; }
    }
}
