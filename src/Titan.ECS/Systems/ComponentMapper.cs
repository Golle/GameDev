using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    internal class ComponentMapper<T> : IComponentMapper<T> where T : struct
    {
        private readonly IContext _context;

        private readonly IComponentPool<T> _pool;
        private readonly IDictionary<uint, uint> _componentIndexMap;
        private static readonly ulong ComponentId = typeof(T).ComponentMask();

        public ComponentMapper(uint poolSize, IContext context)
        {
            _context = context;

            _pool = new ComponentPool<T>(poolSize);
            _componentIndexMap = new Dictionary<uint, uint>();
        }

        public void DestroyComponent(uint entity)
        {
            _componentIndexMap.Remove(entity, out var index);
            _pool.Free(index);
            _context.OnComponentDestroyed(entity, ComponentId);
        }

        public ref T CreateComponent(uint entity)
        {
            var index = _pool.Create();
            _componentIndexMap[entity] = index;

            _context.OnComponentCreated(entity, ComponentId);
            return ref _pool[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(uint entity) => ref _pool[_componentIndexMap[entity]];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(uint entity) => _componentIndexMap.ContainsKey(entity);

        public ref T this[uint entity] => ref _pool[_componentIndexMap[entity]];
    }
}
