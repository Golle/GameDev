using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS2.Components
{
    internal class ComponentPool<T> : IComponentPool where T : struct
    {
        private readonly PinnedMemoryBlock<T> _memoryBlock;
        private readonly Queue<uint> _free = new Queue<uint>();
        
        private uint _nextId = 0;
        public ComponentPool(uint size) 
        {
            _memoryBlock = new PinnedMemoryBlock<T>(size);
        }

        public uint Create()
        {
            if (!_free.TryDequeue(out var index))
            {
                index = Interlocked.Increment(ref _nextId);
            }
            return index;
        }

        public void Destroy(uint index)
        {
            _free.Enqueue(index);
        }

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _memoryBlock[index];
        }

        public void Dispose()
        {
            _memoryBlock.Dispose();
        }
    }
}
