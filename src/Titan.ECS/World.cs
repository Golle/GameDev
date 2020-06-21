using System.Runtime.CompilerServices;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Messaging;
using Titan.ECS.Messaging.Messages;

namespace Titan.ECS
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

        public EntityFilter EntityFilter(uint maxEntitiesInFilter)
        {
            if (maxEntitiesInFilter == 0)
            {
                maxEntitiesInFilter = _maxEntities;
            }
            return new EntityFilter(_maxEntities, maxEntitiesInFilter, _publisher);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IComponentMap<T> IWorld.GetComponentMap<T>() where T : struct => _componentManager.Map<T>();

        IRelationship IWorld.GetRelationship() => _entityManager;
    }
}
