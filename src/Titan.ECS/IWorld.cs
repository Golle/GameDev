using System;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Messaging;

namespace Titan.ECS
{
    public interface IWorld : IDisposable
    {
        Entity CreateEntity();

        uint MaxEntities { get; }
        uint Id { get; }
        EntityFilter EntityFilter(uint maxEntitiesInFilter = 0);

        void WriteToStream();
        internal IComponentMap<T> GetComponentMap<T>() where T : struct;
        internal IRelationship GetRelationship();
        internal IDisposable Subscribe<T>(MessageHandler<T> messageHandler) where T : struct;

        
    }
}
