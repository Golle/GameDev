using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;
using Titan.Core.Logging;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;

namespace Titan.ECS.World
{
    internal class World : IWorld
    {
        private readonly ISystemsRunner _systemsRunner;
        private readonly IComponentManager _componentManager;
        private readonly IEntityManager _entityManager;
        private readonly ILogger _logger;

        public World(IEventManager eventManager, ISystemsRunner systemsRunner, IComponentManager componentManager, IEntityManager entityManager, ILogger logger)
        {
            _systemsRunner = systemsRunner;
            _componentManager = componentManager;
            _entityManager = entityManager;
            _logger = logger;

            eventManager.Subscribe<UpdateEvent>(OnUpdate);
        }

        public IEntity CreateEntity()
        {
            // TODO: entities should be pooled
            var entity = new Entity(_entityManager, _componentManager);
            entity.Init();

            return entity;
        }

        public void Destroy()
        {
            _logger.Debug("Destroying World");
        }

        private void OnUpdate(in UpdateEvent @event)
        {
            _systemsRunner.Update(@event.ElapsedTime);
        }
    }
}
