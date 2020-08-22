using System;
using System.Runtime.CompilerServices;
using Titan.ECS.Components;
using Titan.ECS.Messaging;
using Titan.ECS.Messaging.Messages;

namespace Titan.ECS
{
    public class EntityFilter
    {

        private readonly Publisher _publisher;
        private ComponentMask _withMask;

        private readonly uint[] _entities;
        private readonly int[] _mapping;

        private int _count;
        internal EntityFilter(uint maxEntities, uint maxEntitiesInFilter, Publisher publisher)
        {
            _publisher = publisher;
            _publisher.Subscribe<EntityDestroyedMessage>(EntityDestroyed);
            _entities = new uint[maxEntitiesInFilter];
            _mapping = new int[maxEntities];
            Array.Fill(_mapping, -1);
        }

        public EntityFilter With<T>() where T : struct
        {
            _withMask |= ComponentId<T>.Id;
            _publisher.Subscribe<ComponentAddedMessage<T>>(ComponentAdded);
            _publisher.Subscribe<ComponentRemovedMessage<T>>(ComponentRemoved);
            return this;
        }

        public EntityFilter WithAllEntities()
        {
            _publisher.Subscribe<EntityCreatedMessage>(EntityCreated);
            return this;
        }

        private void EntityCreated(in EntityCreatedMessage message)
        {
            AddEntity(message.Id);
        }

        private void EntityDestroyed(in EntityDestroyedMessage message)
        {
            RemoveEntity(message.Id);
        }

        private void ComponentRemoved<T>(in ComponentRemovedMessage<T> message) where T : struct
        {
            // this will be triggered every time a component is removed (in the with mask), but the entity might not exist in the filter.
            if (!_withMask.IsSubsetOf(message.Components))
            {
                RemoveEntity(message.EntityId);
            }
        }

        private void ComponentAdded<T>(in ComponentAddedMessage<T> message) where T : struct
        {
            if (_withMask.IsSubsetOf(message.Components))
            {
                AddEntity(message.EntityId);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddEntity(uint entityId)
        {
            ref var index = ref _mapping[entityId];
            if (index == -1)
            {
                _entities[index = _count++] = entityId;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RemoveEntity(uint entityId)
        {
            ref var index = ref _mapping[entityId];
            if (index != -1)
            {
                _entities[index] = _entities[--_count];
                index = -1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<uint> GetEntities() => new ReadOnlySpan<uint>(_entities, 0, _count);
    }
}
