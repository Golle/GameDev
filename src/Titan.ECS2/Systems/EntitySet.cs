using System;
using System.Collections.Generic;
using System.Linq;
using Titan.ECS2.Components;
using Titan.ECS2.Components.Messages;

namespace Titan.ECS2.Systems
{
    public class EntitySet : IDisposable
    {
        private readonly ComponentSignature _withFilter;

        private readonly uint[] _entities; 
        private readonly int[] _mapping;
        private IDisposable[] _subscriptions;
        public int Count { get; private set; }

        internal EntitySet(IPublisher publisher, uint maxEntities, in ComponentSignature withFilter, IEnumerable<Func<IPublisher, EntitySet, IDisposable>> subscriptions)
        {
            _withFilter = withFilter;
            _entities = new uint[maxEntities];
            _mapping = new int[maxEntities];
            Array.Fill(_mapping, -1);
            
            _subscriptions = subscriptions.Select(s => s(publisher, this)).ToArray();
        }

        internal ReadOnlySpan<uint> GetEntities() => new ReadOnlySpan<uint>(_entities, 0, Count);

        internal void Add(uint entityId)
        {
            ref var index = ref _mapping[entityId];
            if (index == -1)
            {
                index = Count;
                _entities[Count++] = entityId;
            }
        }

        internal void Remove(uint entityId)
        {
            ref var index = ref _mapping[entityId];
            if (index != -1)
            {
                _entities[index] = _entities[--Count];
                index = -1;
            }
        }

        internal void AddChecked<T>(in ComponentAddedMessage<T> message) where T : struct
        {
            if (message.Components.Matches(_withFilter))
            {
                Add(message.EntityId);
            }
        }

        internal void RemoveChecked<T>(in ComponentRemovedMessage<T> message) where T : struct
        {
            //TODO: add checks for other types of filters later, but right now we only support With filter, so if any component is removed just remove the entity.
            Remove(message.EntityId);
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
            _subscriptions = null;
        }
    }
}
