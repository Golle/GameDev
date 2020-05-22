using System.Collections.Generic;
using System.Diagnostics;
using Titan.EntityComponentSystem.Entities;

namespace Titan.EntityComponentSystem.Components
{
    internal class ComponentManager : IComponentManager
    {
        private readonly IDictionary<ulong, object> _pools = new Dictionary<ulong, object>();
        public void RegisterPool<T>(IComponentPool<T> pool) where T : unmanaged
        {
            var id = typeof(T).ComponentMask();
            Debug.Assert(!_pools.ContainsKey(id), $"A component pool for type {typeof(T)} has already been regitered.");
            _pools[id] = pool;
        }

        public Component Create<T>(Entity entity) where T : unmanaged
        {
            var id = typeof(T).ComponentMask();
            Debug.Assert(_pools.ContainsKey(id), $"No component pool has been registed for type {typeof(T)}.");

            var index = ((IComponentPool<T>) _pools[id]).Create();
            return new Component(id, index, entity);
        }

        public void Free(in Component component)
        {
            ((IComponentPool)_pools[component.Id]).Free(component.Index);
        }
    }
}
