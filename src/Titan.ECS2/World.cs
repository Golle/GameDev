using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Titan.ECS2.Components;
using Titan.ECS2.Components.Messages;
using Titan.ECS2.Entities;
using Titan.ECS2.Messages;
using Titan.ECS2.Systems;

namespace Titan.ECS2
{
    public class World : IDisposable
    {
        public uint MaxEntities { get; }
        public ushort Id { get; }

        private readonly IEntityManager _entityManager;
        private readonly IComponentManager _componentManager;
        private readonly IPublisher _publisher;

        internal World(ushort id, uint maxEntities, IEntityManager entityManager, IComponentManager componentManager, IPublisher publisher)
        {
            Id = id;
            MaxEntities = maxEntities;
            _entityManager = entityManager;
            _componentManager = componentManager;
            _publisher = publisher;
        }

        public Entity CreateEntity()
        {
            var entity = _entityManager.Create();

            _publisher.Publish(new EntityCreatedMessage(entity.Id));

            return entity;
        }

        public EntitySetBuilder CreateEntitySetBuilder()
        {
            return new EntitySetBuilder(MaxEntities, _publisher);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IComponentMapper<T> GetComponentMapper<T>() where T : struct => _componentManager.GetMapper<T>();

        
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref T GetComponent<T>(uint entityId) where T : struct => ref _componentManager.GetMapper<T>()[entityId];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SetComponent<T>(uint entityId, in T value) where T : struct => _componentManager.GetMapper<T>()[entityId] = value;
        

        internal void AddComponent<T>(uint entityId, in T value) where T : struct
        {
            ref var entityInfo = ref _entityManager.GetEntityInfo(entityId);
            Debug.Assert(!entityInfo.Signature.Contains(ComponentId<T>.Id), $"Entity already have the {typeof(T).Name} component.");
            
            entityInfo.Signature.Add(ComponentId<T>.Id);
            _componentManager.GetMapper<T>().Create(entityId, value);
            _publisher.Publish(new ComponentAddedMessage<T>(entityId, entityInfo.Signature));
        }

        internal void RemoveComponent<T>(uint entityId) where T : struct
        {
            ref var entityInfo = ref _entityManager.GetEntityInfo(entityId);
            Debug.Assert(entityInfo.Signature.Contains(ComponentId<T>.Id), $"Entity doesn't have the {typeof(T).Name} component.");

            entityInfo.Signature.Remove(ComponentId<T>.Id);
            _componentManager.GetMapper<T>().Destroy(entityId);
            _publisher.Publish(new ComponentRemovedMessage<T>(entityId, entityInfo.Signature));
        }
        
        internal void DestroyEntity(uint entityId)
        {
            _entityManager.Destroy(entityId);
            _publisher.Publish(new EntityDestroyedMessage(entityId));
            // TODO: destroy components
            // TODO: destroy children
        }

        public void Destroy() => _publisher.Publish(new WorldDestroyedMessage(Id));

        public void Dispose()
        {
            _publisher.Dispose();
            _componentManager.Dispose();
        }
    }
}
