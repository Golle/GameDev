using System;
using System.Runtime.CompilerServices;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Messaging;
using Titan.ECS.Messaging.Messages;
using Titan.ECS.Serialization;

namespace Titan.ECS
{
    internal class World : IWorld
    {
        private readonly EntityManager _entityManager;
        private readonly ComponentManager _componentManager;
        private readonly Publisher _publisher;
        
        private readonly ISerializer _serializer;
        public uint MaxEntities { get; }
        public uint Id { get; }
        internal World(WorldConfiguration configuration)
        {
            Id = Worlds.AddWorld(this);
            MaxEntities = configuration.MaxEntities;
            
            _publisher = new Publisher(Id);

            _entityManager = new EntityManager(Id, configuration.MaxEntities, _publisher);

            _componentManager = new ComponentManager(configuration.MaxEntities, _publisher);
            foreach (var (componentType, size) in configuration.Components())
            {
                _componentManager.RegisterComponent(componentType, size);
            }
            _serializer = new EntitySerializer(this, _publisher);
        }

        public Entity CreateEntity()
        {
            var entity = _entityManager.Create();
            return entity;
        }

        public void Dispose()
        {
            _publisher.Publish(new WorldDisposingMessage(Id));
            Worlds.DestroyWorld(this);

            _publisher.Publish(new WorldDisposedMessage(Id));
            _publisher.Dispose();
        }

        public void RemoveComponent<T>(in uint entityId) where T : struct
        {
            var components = _entityManager.GetInfo(entityId).Components ^= ComponentId<T>.Id;
            _publisher.Publish(new ComponentRemovedMessage<T>(entityId, components));
            _componentManager.Remove<T>(entityId);
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

        public EntityFilter EntityFilter(uint maxEntitiesInFilter = 0)
        {
            return new EntityFilter(MaxEntities, maxEntitiesInFilter == 0 ? MaxEntities : maxEntitiesInFilter, _publisher);
        }

        public void WriteToStream()
        {
            _serializer.Serialize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IComponentMap<T> IWorld.GetComponentMap<T>() where T : struct => _componentManager.Map<T>();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IRelationship IWorld.GetRelationship() => _entityManager;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IDisposable IWorld.Subscribe<T>(MessageHandler<T> messageHandler) => _publisher.Subscribe(messageHandler);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>(uint entityId) where T : struct => _componentManager.Map<T>().Has(entityId);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetComponent<T>(uint entityId) where T : struct => ref _componentManager.Map<T>()[entityId];
    }
}
