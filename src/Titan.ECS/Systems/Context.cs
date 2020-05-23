using System.Collections.Generic;
using Titan.Core.Logging;
using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    internal class Context : IContext
    {
        private readonly ILogger _logger;
        private readonly IDictionary<uint, ulong> _entityComponentSignature = new Dictionary<uint, ulong>(100_000);
        private readonly IList<ISystem> _systems = new List<ISystem>();

        public Context(ILogger logger)
        {
            _logger = logger;
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

        public void OnComponentCreated(in Component component)
        {
            // TODO: do something with the component.Index
            var signature = _entityComponentSignature[component.Entity] |= component.Id;
            //_logger.Debug("Added Component ID {0} with index {1} to entity {2}", component.Id, component.Index, component.Entity.Id);
            foreach (var system in _systems)
            {
                if (system.IsMatch(signature))
                {
                    system.Add(component.Entity);
                }
            }

        }

        public void OnComponentDestroyed(in Component component)
        {
            var oldSignature = _entityComponentSignature[component.Entity];
            foreach (var system in _systems)
            {
                if ((system.Signature & component.Id) != 0 && (system.Signature & oldSignature) == system.Signature)
                {
                    system.Remove(component.Entity);
                }
            }
            
            _entityComponentSignature[component.Entity] ^= component.Id;
            //_logger.Debug("Removed Component ID {0} with index {1} to entity {2}", component.Id, component.Index, component.Entity.Id);
        }
    }
}
