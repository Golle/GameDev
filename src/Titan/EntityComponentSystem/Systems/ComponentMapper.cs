using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Titan.EntityComponentSystem.Components;

namespace Titan.EntityComponentSystem.Systems
{
    internal class ComponentMapper<T> : IComponentMapper<T> where T : unmanaged
    {
        private readonly IComponentPool<T> _pool;
        private readonly IDictionary<uint, uint> _componentIndexMap;

        public ComponentMapper(uint poolSize)
        {
            _pool = new ComponentPool<T>(poolSize);
            _componentIndexMap = new Dictionary<uint, uint>();
        }

        public void DestroyComponent(uint entity, uint index)
        {
            _componentIndexMap.Remove(entity);
            _pool.Free(index);
        }

        public uint CreateComponent(uint entity)
        {
            Debug.Assert(!_componentIndexMap.ContainsKey(entity), $"Only one component of type {typeof(T).Name} is allowed on entity {entity}" );
            var index = _pool.Create();
            return _componentIndexMap[entity] = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(uint entity) => ref _pool[_componentIndexMap[entity]];

        public ref T this[uint entity] => ref _pool[_componentIndexMap[entity]];
    }
}