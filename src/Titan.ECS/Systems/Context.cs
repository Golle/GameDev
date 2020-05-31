using System.Collections.Generic;
using Titan.Core.EventSystem;
using Titan.Core.Logging;

namespace Titan.ECS.Systems
{
    internal class Context : IContext
    {
        private readonly ILogger _logger;
        private readonly IDictionary<uint, ulong> _entityComponentSignature = new Dictionary<uint, ulong>(100_000);
        private readonly IList<ISystem> _systems = new List<ISystem>();

        public Context(ILogger logger, IEventManager eventManager)
        {
            _logger = logger;

            eventManager.Subscribe<ComponentCreatedEvent>(OnComponentCreated);
            eventManager.Subscribe<ComponentDestroyedEvent>(OnComponentDestroyed);
        }

        private void OnComponentDestroyed(in ComponentDestroyedEvent @event)
        {
            var oldSignature = _entityComponentSignature[@event.EntityId];
            foreach (var system in _systems)
            {
                if (system.Contains(@event.Id) && system.IsMatch(oldSignature))
                {
                    system.Remove(@event.EntityId);
                }
            }

            _entityComponentSignature[@event.EntityId] ^= @event.Id;
            //_logger.Debug("Removed Component ID {0} with index {1} to entity {2}", component.Id, component.Index, component.Entity.Id);
        }

        private void OnComponentCreated(in ComponentCreatedEvent @event)
        {
            // TODO: do something with the component.Index
            var signature = _entityComponentSignature[@event.EntityId] |= @event.Id;
            //_logger.Debug("Added Component ID {0} with index {1} to entity {2}", component.Id, component.Index, component.Entity.Id);
            foreach (var system in _systems)
            {
                if (system.IsMatch(signature))
                {
                    system.Add(@event.EntityId);
                }
            }
        }

        public void RegisterSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void OnEntityDestroyed(uint entity)
        {
            var signature = _entityComponentSignature[entity];
            foreach (var system in _systems)
            {
                if ((system.Signature & signature) == system.Signature)
                {
                    system.Remove(entity);
                }
            }
            
            _entityComponentSignature.Remove(entity);
            //_logger.Debug("Removed entity {0}", entity.Id);

            
        }

        public void OnEntityCreated(uint entity)
        {
            _entityComponentSignature[entity] = 0UL;
            //_logger.Debug("Created entity {0}", entity.Id);
        }
    }
}
