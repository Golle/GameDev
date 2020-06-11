using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS2.Components
{
    internal class ComponentMapper<T> : IComponentMapper<T> where T : struct
    {
        private readonly ComponentPool<T> _pool;
        private readonly uint[] _componentMap;

        public ComponentMapper(ComponentPool<T> pool, uint maxEntities)
        {
            _pool = pool;
            _componentMap = new uint[maxEntities];
        }

        public ref T this[uint entityId]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Debug.Assert(_componentMap[entityId] != 0, "Trying to get a component which has not be created.");
                return ref _pool[_componentMap[entityId]];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Create(uint entityId, in T value)
        {
            var index = _componentMap[entityId] = _pool.Create();
            _pool[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Create(uint entityId) => _componentMap[entityId] = _pool.Create();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy(uint entityId)
        {
            ref var index = ref _componentMap[entityId];
            Debug.Assert(index !=0, "Trying to destroy a component that doesn't exist.");
            _pool.Destroy(index);
            index = 0;
        }

        public void Dispose()
        {
            _pool.Dispose();
        }
    }
}
