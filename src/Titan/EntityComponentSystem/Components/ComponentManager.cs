using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titan.Core.EventSystem;
using Titan.Systems.Components;
using Titan.Systems.Components.Events;

namespace Titan.EntityComponentSystem.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IEventManager _eventManager;
        private readonly IDictionary<Type, object> _pools = new Dictionary<Type, object>();

        public ComponentManager(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public void Register<T>(IComponentPool<T> componentPool) where T : IComponent
        {
            Debug.Assert(!_pools.ContainsKey(typeof(T)));
            _pools[typeof(T)] = componentPool;
        }

        public T Create<T>() where T : IComponent
        {
            Debug.Assert(_pools.ContainsKey(typeof(T)));
            var component = ((IComponentPool<T>) _pools[typeof(T)]).Get();
            
            _eventManager.Publish(new ComponentCreatedEvent(component));

            //TODO: Set unique id per instance?
            return component;
        }

        public void Destroy<T>(T component) where T : IComponent
        {
            Debug.Assert(_pools.ContainsKey(typeof(T)));
            ((IComponentPool<T>)_pools[typeof(T)]).Put(component);
            _eventManager.Publish(new ComponentDestroyedEvent(component));
        }
    }
}
