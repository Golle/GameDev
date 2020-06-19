using System;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    public interface IWorld : IDisposable
    {
        Entity CreateEntity();

        internal uint MaxEntities { get; }

        internal EntityFilter EntityFilter(uint maxEntitiesInFilter = 0);
    }
}
