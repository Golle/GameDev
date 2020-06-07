using System.Collections.Generic;
using Titan.Core.Logging;

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

        public void OnComponentCreated(uint entity, ulong componentId)
        {
            _entityComponentSignature.TryGetValue(entity, out var signature);
            _entityComponentSignature[entity] = signature |= componentId;
            foreach (var system in _systems)
            {
                if (system.IsMatch(signature))
                {
                    system.Add(entity);
                }
            }
        }

        public void OnComponentDestroyed(uint entity, ulong componentId)
        {
            var oldSignature = _entityComponentSignature[entity];
            foreach (var system in _systems)
            {
                if (system.HasComponent(componentId) && system.IsMatch(oldSignature))
                {
                    system.Remove(entity);
                }
            }
            _entityComponentSignature[entity] ^= componentId;
        }

        public void OnEntityDestroyed(uint entity)
        {
            foreach (var system in _systems)
            {
                system.Remove(entity);
            }
            _entityComponentSignature[entity] = 0UL;
        }

        public void OnEntityCreated(uint entity)
        {
            _entityComponentSignature[entity] = 0UL;
            //_logger.Debug("Created entity {0}", entity.Id);
        }
    }
}
