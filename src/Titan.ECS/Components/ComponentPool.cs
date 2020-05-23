using System.Collections.Generic;
using System.Diagnostics;

namespace Titan.ECS.Components
{
    internal class ComponentPool<T> : IComponentPool<T> where T : unmanaged
    {
        private readonly T[] _componentPool;
        private readonly Queue<uint> _free = new Queue<uint>(100);
        private uint _count;
        public ComponentPool(uint size)
        {
            _componentPool = new T[size];
        }

        public uint Create()
        {
            Debug.Assert(_count < _componentPool.Length, "No more components available");
            return _free.TryDequeue(out var index) ? index : _count++;
        }

        public ref T Create(out uint index)
        {
            Debug.Assert(_count < _componentPool.Length || _free.Count > 0, "No more components available");
            if (!_free.TryDequeue(out index))
            {
                index = _count++;
            }
            return ref _componentPool[index];
        }

        public ref T Get(uint index) => ref _componentPool[index];
        public ref T this[uint index] => ref _componentPool[index];
        public void Free(uint index) => _free.Enqueue(index);
    }
}
