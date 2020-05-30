using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titan.Core.EventSystem;
using Titan.ECS.Systems;

namespace Titan.ECS.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IContext _context;
        private readonly IEventManager _eventManager;
        private readonly IDictionary<ulong, IComponentMapper> _componentMappers = new Dictionary<ulong, IComponentMapper>();
        public ComponentManager(IContext context, IEventManager eventManager)
        {
            _context = context;
            _eventManager = eventManager;
        }

        public void RegisterComponent<T>(uint capacity) where T : unmanaged
        {
            RegisterComponent(typeof(T), capacity);
        }

        public void RegisterComponent(Type type, uint capacity)
        {
            var componentId = type.ComponentMask();
            Debug.Assert(!_componentMappers.ContainsKey(componentId), $"The component {type} has already been registered.");

            var instance = Activator.CreateInstance(typeof(ComponentMapper<>).MakeGenericType(type), capacity, _eventManager) ?? throw new InvalidOperationException($"Failed to created a ComponentMapper for type {type}");
            _componentMappers[componentId] = (IComponentMapper)instance;
        }

        public IComponentMapper<T> GetComponentMapper<T>() where T : unmanaged
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");
            return (IComponentMapper<T>)_componentMappers[componentId];
        }

        public ulong Create<T>(uint entity, in T initialData) where T : unmanaged
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");

            var mapper = (IComponentMapper<T>)_componentMappers[componentId];
            ref var component = ref mapper.CreateComponent(entity);
            component = initialData;
            return componentId;
        }

        public ulong Create<T>(uint entity) where T : unmanaged
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");

            var mapper = (IComponentMapper<T>)_componentMappers[componentId];
            mapper.CreateComponent(entity);
            return componentId;
        }

        public void Free(uint entityId, ulong componentId)
        {
            _componentMappers[componentId].DestroyComponent(entityId);
        }
    }
}
