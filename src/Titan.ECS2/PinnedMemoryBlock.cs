using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS2
{
    internal class PinnedMemoryBlock<T>  : IDisposable where T : struct
    {
        private T[] _items;
        private GCHandle _handle;
        public PinnedMemoryBlock(uint size)
        {
            _items = new T[size];
            _handle = GCHandle.Alloc(_items, GCHandleType.Pinned);
        }
        
        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _items[index];
        }

        public void Dispose()
        {
            if (_handle.IsAllocated)
            {
                _handle.Free();
            }
            _items = null;
        }
    }
}
