using System;
using System.Runtime.CompilerServices;
using Titan.ECS2.Components;
using Titan.ECS2.Entities;
using Titan.ECS2.Messages;

namespace Titan.ECS2
{
    internal class World : IWorld, IDisposable
    {
        private readonly ushort _id;

        private readonly IEntityManager _entityManager;
        private readonly IComponentManager _componentManager;
        private readonly IPublisher _publisher;

        internal World(ushort id, IEntityManager entityManager, IComponentManager componentManager, IPublisher publisher)
        {
            _id = id;
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
        
        public void Destroy() => _publisher.Publish(new WorldDestroyedMessage(_id));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref T GetComponent<T>(uint entityId) where T : struct => ref _componentManager.GetMapper<T>()[entityId];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SetComponent<T>(uint entityId, in T value) where T : struct => _componentManager.GetMapper<T>()[entityId] = value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(uint entityId, in T value) where T : struct => _componentManager.GetMapper<T>().Create(entityId, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent<T>(uint entityId) where T : struct => _componentManager.GetMapper<T>().Destroy(entityId);

        internal void DestroyEntity(uint entityId)
        {
            _entityManager.Destroy(entityId);
            _publisher.Publish(new EntityDestroyedMessage(entityId));
            // TODO: destroy components
            // TODO: destroy children
        }

        public void Dispose()
        {
            _publisher.Dispose();
            _componentManager.Dispose();
        }
    }
}
