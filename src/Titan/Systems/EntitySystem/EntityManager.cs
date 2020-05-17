using Titan.Core.EventSystem;
using Titan.Systems.Components;

namespace Titan.Systems.EntitySystem
{
    internal class EntityManager : IEntityManager
    {
        private readonly IComponentSystem _componentSystem;
        private readonly IEventManager _eventManager;

        public EntityManager(IComponentSystem componentSystem, IEventManager eventManager)
        {
            _componentSystem = componentSystem;
            _eventManager = eventManager;
        }
        public IEntity Create(string? name = null)
        {
            return new Entity(_componentSystem, _eventManager, this);
        }

        internal void Destroy(Entity entity)
        {
            
        }
    }
}
