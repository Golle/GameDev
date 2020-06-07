using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Titan.ECS.Systems;

namespace Titan.ECS.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IContext _context;
        private readonly IDictionary<ulong, IComponentMapper> _componentMappers = new Dictionary<ulong, IComponentMapper>();
        public ComponentManager(IContext context)
        {
            _context = context;
        }

        public IComponentManager RegisterComponent<T>(uint capacity) where T : struct
        {
            return RegisterComponent(typeof(T), capacity);
        }
        
        public IComponentManager RegisterComponent(Type type, uint capacity)
        {
            var componentId = type.ComponentMask();
            Debug.Assert(!_componentMappers.ContainsKey(componentId), $"The component {type} has already been registered.");

            var instance = Activator.CreateInstance(typeof(ComponentMapper<>).MakeGenericType(type), capacity, _context) ?? throw new InvalidOperationException($"Failed to created a ComponentMapper for type {type}");
            _componentMappers[componentId] = (IComponentMapper)instance;
            return this;
        }

        public IComponentMapper<T> GetComponentMapper<T>() where T : struct
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");
            return (IComponentMapper<T>)_componentMappers[componentId];
        }

        public ulong Create<T>(uint entity, in T initialData) where T : struct
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");

            var mapper = (IComponentMapper<T>)_componentMappers[componentId];
            ref var component = ref mapper.CreateComponent(entity);
            component = initialData;
            return componentId;
        }

        public ulong Create<T>(uint entity) where T : struct
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");

            var mapper = (IComponentMapper<T>)_componentMappers[componentId];
            mapper.CreateComponent(entity);
            return componentId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free(uint entityId, ulong componentId)
        {
            _componentMappers[componentId].DestroyComponent(entityId);
        }
    }
}
