using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS3.Components
{
    internal sealed class ComponentMap<T> where T : struct
    {
        private readonly ComponentPool<T> _pool;
        private readonly uint[] _entityMap;

        public ComponentMap(uint maxEntities, uint size)
        {
            Debug.Assert(size <= maxEntities, "Component pool is greater than the max number of entities.");
            _pool = new ComponentPool<T>(size);
            _entityMap = new uint[maxEntities];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(uint entityId, in T initialValue)
        {
            Debug.Assert(_entityMap != null && _pool != null, "ComponentMap was created with default constructor.");
            Debug.Assert(_entityMap[entityId] == 0, $"Component {typeof(T)} has already been added to entity {entityId}");
            _pool[_entityMap[entityId] = _pool.Create()] = initialValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(uint entityId)
        {
            Debug.Assert(_entityMap != null && _pool != null, "ComponentMap was created with default constructor.");
            Debug.Assert(_entityMap[entityId] == 0, $"Component {typeof(T)} has already been added to entity {entityId}");
            _entityMap[entityId] = _pool.Create();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(uint entityId)
        {
            Debug.Assert(_entityMap != null && _pool != null, "ComponentMap was created with default constructor.");
            Debug.Assert(_entityMap[entityId] != 0, $"Component {typeof(T)} does not exist on entity {entityId}");
            ref var index = ref _entityMap[entityId];
            _pool.Destroy(index);
            index = 0;
        }

        public ref T this[uint entityId]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _pool[_entityMap[entityId]];
        }
    }
}
