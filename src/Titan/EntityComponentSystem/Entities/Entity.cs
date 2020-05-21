using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titan.EntityComponentSystem.Components;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Entities
{
    public class Entity
    {
        private readonly IComponentManager _componentManager;
        private readonly IDictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public IEnumerable<IComponent> Components => _components.Values;

        public Entity(IComponentManager componentManager)
        {
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
    }
}
