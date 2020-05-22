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
        private readonly IComponentRegister _componentRegister;
        private readonly IDictionary<Type, object> _pools = new Dictionary<Type, object>();

        public ComponentManager(IEventManager eventManager, IComponentRegister componentRegister)
        {
            _eventManager = eventManager;
            _componentRegister = componentRegister;
        }

        public void Register<T>(IComponentPool<T> componentPool) where T : IComponent
        {
            var type = typeof(T);
            Debug.Assert(!_pools.ContainsKey(type));
            _componentRegister.Register<T>();
            _pools[type] = componentPool;
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
