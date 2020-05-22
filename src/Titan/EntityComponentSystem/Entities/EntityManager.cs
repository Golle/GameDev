using System.Threading;
using Titan.Core.EventSystem;
using Titan.Systems.EntitySystem.Events;

namespace Titan.EntityComponentSystem.Entities
{
    internal class EntityManager : IEntityManager
    {
        private uint _index;
        private readonly IEventManager _eventManager;

        public EntityManager(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public Entity Create()
        {
            var id = Interlocked.Increment(ref _index);
            _eventManager.Publish(new EntityCreatedEvent(id));
            return id;
        }

        public void Free(Entity entity)
        {
            _eventManager.Publish(new EntityDestroyedEvent(entity));
        }
    }
}
