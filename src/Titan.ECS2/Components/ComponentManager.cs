using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Titan.ECS2.Components
{
    public class ComponentManager : IComponentManager
    {
        private readonly uint _maxEntities;
        private IDictionary<ulong, IComponentMapper> _mappers = new Dictionary<ulong, IComponentMapper>();

        public ComponentManager(uint maxEntities)
        {
            _maxEntities = maxEntities;
        }
        public void RegisterComponent<T>(uint size) where T : struct
        {
            var id = ComponentId<T>.Id;
            var pool = new ComponentPool<T>(size);
            _mappers[id] = new ComponentMapper<T>(pool, _maxEntities);
        }

        public void RegisterComponent(Type type, uint size)
        {
            var id = (ulong) typeof(ComponentId<>).MakeGenericType(type).GetProperty("Id").GetValue(null);
            var pool = Activator.CreateInstance(typeof(ComponentPool<>).MakeGenericType(type), size);
            _mappers[id] = (IComponentMapper)Activator.CreateInstance(typeof(ComponentMapper<>).MakeGenericType(type), pool, _maxEntities);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IComponentMapper<T> GetMapper<T>() where T : struct
        {
            return (IComponentMapper<T>) _mappers[ComponentId<T>.Id];
        }

        public void Dispose()
        {
            foreach (var mapper in _mappers.Values)
            {
                mapper.Dispose();
            }
            _mappers.Clear();
            _mappers = null;
        }
    }
}
