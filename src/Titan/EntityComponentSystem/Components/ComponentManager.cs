using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IDictionary<Type, object> _pools = new Dictionary<Type, object>();
        public void Register<T>(IComponentPool<T> componentPool) where T : IComponent
        {
            Debug.Assert(!_pools.ContainsKey(typeof(T)));
            _pools[typeof(T)] = componentPool;
        }

        public T Create<T>() where T : IComponent
        {
            Debug.Assert(_pools.ContainsKey(typeof(T)));

            return ((IComponentPool<T>) _pools[typeof(T)]).Get();
        }
    }
}
