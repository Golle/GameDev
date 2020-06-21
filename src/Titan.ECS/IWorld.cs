using System;
using Titan.ECS.Components;
using Titan.ECS.Entities;

namespace Titan.ECS
{
    public interface IWorld : IDisposable
    {
        Entity CreateEntity();

        internal uint MaxEntities { get; }

        EntityFilter EntityFilter(uint maxEntitiesInFilter = 0);
        internal IComponentMap<T> GetComponentMap<T>() where T : struct;
        internal IRelationship GetRelationship();
    }
}
