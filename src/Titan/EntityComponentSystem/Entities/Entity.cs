using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Titan.EntityComponentSystem.Components;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Entities
{
    public class Entity
    {
        private static ulong _staticId = 0;
        public ulong Id { get; } = Interlocked.Increment(ref _staticId);

        private readonly IEntityManager _entityManager;
        private readonly IComponentManager _componentManager;

        private readonly IDictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
        public IEnumerable<IComponent> Components => _components.Values;
        public Entity(IEntityManager entityManager, IComponentManager componentManager)
        {
            _entityManager = entityManager;
            _componentManager = componentManager;
        }

        public T AddComponent<T>() where T : IComponent
        {
            Debug.Assert(!_components.ContainsKey(typeof(T)), $"The entity already contains the {typeof(T)} component.");

            return (T)(_components[typeof(T)] = _componentManager.Create<T>());
        }

        public T GetComponent<T>()
        {
            Debug.Assert(_components.ContainsKey(typeof(T)));

            return (T) _components[typeof(T)];
        }

        public void Destroy()
        {
            _entityManager.Destroy(this);
            foreach (var component in _components.Values)
            {
                _componentManager.Destroy(component);
            }
        }
    }
}
