using System;
using Titan.ECS3.Components;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    public interface IWorld : IDisposable
    {
        Entity CreateEntity();

        internal uint MaxEntities { get; }

        EntityFilter EntityFilter(uint maxEntitiesInFilter = 0);
        internal IComponentMap<T> GetComponentMap<T>() where T : struct;
    }
}
