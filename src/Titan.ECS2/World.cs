using System;
using Titan.ECS2.Entities;
using Titan.ECS2.Messages;

namespace Titan.ECS2
{
    internal class World : IWorld, IDisposable
    {
        private readonly ushort _id;

        private readonly IEntityManager _entityManager;
        private readonly IPublisher _publisher;

        internal World(ushort id, IEntityManager entityManager, IPublisher publisher)
        {
            _id = id;
            _entityManager = entityManager;
            _publisher = publisher;
        }

        public Entity CreateEntity()
        {
            var entity = _entityManager.Create();

            _publisher.Publish(new EntityCreatedMessage(entity.Id));

            return entity;
        }

        public void Destroy() => _publisher.Publish(new WorldDestroyedMessage(_id));

        internal ref T GetComponent<T>(uint entityId) where T : struct => throw new NotImplementedException();
        internal void SetComponent<T>(uint entityId, in T value) where T : struct => throw new NotImplementedException();

        internal void DestroyEntity(uint entityId)
        {
            _entityManager.Destroy(entityId);
            _publisher.Publish(new EntityDestroyedMessage(entityId));
        }

        public void Dispose()
        {
            _publisher.Dispose();
        }
    }
}
