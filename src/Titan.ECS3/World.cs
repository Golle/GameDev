using System.Runtime.CompilerServices;
using Titan.ECS3.Components;
using Titan.ECS3.Entities;
using Titan.ECS3.Messaging;
using Titan.ECS3.Messaging.Messages;

namespace Titan.ECS3
{
    internal class World : IWorld
    {
        internal uint Id { get; }

        private readonly EntityManager _entityManager;
        private readonly ComponentManager _componentManager;
        private readonly Publisher _publisher;

        private readonly uint _maxEntities;
        uint IWorld.MaxEntities => _maxEntities;

        internal World(WorldConfiguration configuration)
        {
            Id = Worlds.AddWorld(this);
            _maxEntities = configuration.MaxEntities;
            
            _publisher = new Publisher(Id);

            _entityManager = new EntityManager(Id, configuration.MaxEntities, _publisher);

            _componentManager = new ComponentManager(configuration.MaxEntities);
            foreach (var (componentType, size) in configuration.Components())
            {
                _componentManager.RegisterComponent(componentType, size);
            }
        }

        public Entity CreateEntity()
        {
            var entity = _entityManager.Create();
            return entity;
        }

        public void Dispose()
        {
            Worlds.DestroyWorld(this);
        }

        public void RemoveComponent<T>(in uint entityId) where T : struct
        {
            var components = _entityManager.GetInfo(entityId).Components ^= ComponentId<T>.Id;
            _componentManager.Remove<T>(entityId);
            _publisher.Publish(new ComponentRemovedMessage<T>(entityId, components));
        }

        public void AddComponent<T>(in uint entityId, in T value = default) where T : struct
        {
            var components = _entityManager.GetInfo(entityId).Components |= ComponentId<T>.Id;
            _componentManager.Add(entityId, value);
            _publisher.Publish(new ComponentAddedMessage<T>(entityId, components));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AttachEntity(in uint parent, in uint child) => _entityManager.Attach(parent, child);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DestroyEntity(in uint entityId) => _entityManager.Destroy(entityId);

        EntityFilter IWorld.EntityFilter(uint maxEntitiesInFilter)
        {
            if (maxEntitiesInFilter == 0)
            {
                maxEntitiesInFilter = _maxEntities;
            }
            return new EntityFilter(_maxEntities, maxEntitiesInFilter, _publisher);
        }
    }
}
