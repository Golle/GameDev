using System.Collections.Generic;
using System.Diagnostics;
using Titan.EntityComponentSystem.Systems;

namespace Titan.EntityComponentSystem.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IContext _context;
        private readonly IDictionary<ulong, IComponentMapper> _componentMappers = new Dictionary<ulong, IComponentMapper>();
        public ComponentManager(IContext context)
        {
            _context = context;
        }

        public void RegisterComponent<T>(uint capacity) where T : unmanaged
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(!_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has already been registered.");
            _componentMappers[componentId] = new ComponentMapper<T>(capacity);
        }

        public IComponentMapper<T> GetComponentMapper<T>() where T : unmanaged
        {
            var componentId = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(componentId), $"The component {typeof(T)} has not been registered.");
            return (IComponentMapper<T>)_componentMappers[componentId];
        }

        public Component Create<T>(uint entity) where T : unmanaged
        {
            var id = typeof(T).ComponentMask();
            Debug.Assert(_componentMappers.ContainsKey(id), $"The component {typeof(T)} has not been registered.");

            var mapper = _componentMappers[id];
            var index = mapper.CreateComponent(entity);
            var component = new Component(id, index, entity);
            
            _context.OnComponentCreated(component);
            return component;
        }

        public void Free(in Component component)
        {
            _componentMappers[component.Id].DestroyComponent(component.Entity, component.Index);
            _context.OnComponentDestroyed(component);
        }
    }
}
