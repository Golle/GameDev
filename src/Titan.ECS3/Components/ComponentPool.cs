using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS3.Components
{
    internal sealed class ComponentPool<T> where T : struct
    {
        private readonly T[] _components;
        private uint _nextIndex;
        private readonly ConcurrentQueue<uint> _free = new ConcurrentQueue<uint>();

        public ComponentPool(uint size)
        {
            _components = new T[size];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Create()
        {
            if (!_free.TryDequeue(out var index))
            {
                index = Interlocked.Increment(ref _nextIndex);
            }
            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy(uint index) => _free.Enqueue(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(uint index) => ref _components[index];
        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _components[index];
        }
    }
}
