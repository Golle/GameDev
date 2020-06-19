using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Titan.ECS3.Components;
using Titan.ECS3.Messaging;
using Titan.ECS3.Messaging.Messages;

namespace Titan.ECS3
{
    public class EntityFilter : IDisposable
    {

        private readonly Publisher _publisher;
        private ComponentMask _withMask;
        private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();

        private readonly uint[] _entities;
        private readonly int[] _mapping;

        private int _count = 0;
        internal EntityFilter(uint maxEntities, uint maxEntitiesInFilter, Publisher publisher)
        {
            _publisher = publisher;
            _subscriptions.Add(_publisher.Subscribe<EntityDestroyedMessage>(EntityDestroyed));
            _entities = new uint[maxEntitiesInFilter];
            _mapping = new int[maxEntities];
            Array.Fill(_mapping, -1);
        }

        public EntityFilter With<T>() where T : struct
        {
            _withMask |= ComponentId<T>.Id;
            _subscriptions.Add(_publisher.Subscribe<ComponentAddedMessage<T>>(ComponentAdded));
            _subscriptions.Add(_publisher.Subscribe<ComponentRemovedMessage<T>>(ComponentRemoved));
            
            return this;
        }

        private void EntityDestroyed(in EntityDestroyedMessage message)
        {
            RemoveEntity(message.Id);
        }

        private void RemoveEntity(uint entityId)
        {
            Console.WriteLine("Trying to remove entity");
            ref var index = ref _mapping[entityId];
            if (index != -1)
            {
                _entities[index] = _entities[--_count];
                Console.WriteLine("Entity removed");
            }
        }

        private void ComponentRemoved<T>(in ComponentRemovedMessage<T> message) where T : struct
        {
            // this will be triggered every time a component is removed (in the with mask), but the entity might not exist in the filter.
            if (!_withMask.IsSupsetOf(message.Components))
            {
                RemoveEntity(message.EntityId);
            }
        }

        private void ComponentAdded<T>(in ComponentAddedMessage<T> message) where T : struct
        {
            if (_withMask.IsSupsetOf(message.Components))
            {
                Console.WriteLine("Trying to add entity");
                ref var index = ref _mapping[message.EntityId];
                if (index == -1)
                {
                    _entities[index = _count++] = message.EntityId;
                    Console.WriteLine("Entity added");
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<uint> GetEntities() => new ReadOnlySpan<uint>(_entities, 0, _count);

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }
    }
}
