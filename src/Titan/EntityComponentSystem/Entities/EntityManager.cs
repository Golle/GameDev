using Titan.EntityComponentSystem.Components;

namespace Titan.EntityComponentSystem.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly IComponentManager _componentManager;

        public EntityManager(IComponentManager componentManager)
        {
            _componentManager = componentManager;
        }

        public Entity Create()
        {
            return new Entity(_componentManager);
        }
    }
}
