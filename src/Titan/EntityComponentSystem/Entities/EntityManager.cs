using Titan.Core.EventSystem;
using Titan.EntityComponentSystem.Components;
using Titan.Systems.EntitySystem.Events;

namespace Titan.EntityComponentSystem.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly IComponentManager _componentManager;
        private readonly IEventManager _eventManager;

        public EntityManager(IComponentManager componentManager, IEventManager eventManager)
        {
            _componentManager = componentManager;
            _eventManager = eventManager;
        }

        public Entity Create()
        {
            // TODO: add an entity pool for optimization
            var entity = new Entity(this, _componentManager);
            _eventManager.Publish(new EntityCreatedEvent());
            return entity;
        }

        public void Destroy(Entity entity)
        {
            _eventManager.Publish(new EntityDestroyedEvent(entity));
        }
    }
}
