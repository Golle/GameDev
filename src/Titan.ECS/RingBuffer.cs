using System.Diagnostics;
using System.Numerics;

namespace Titan.ECS
{
    internal class RingBuffer<T> where T : struct
    {
        private readonly uint _maxSize;
        private uint _head;
        private uint _tail;

        private readonly T[] _buffer;
        private readonly uint _sizeMask;

        public uint Count { get; private set; }
        public RingBuffer(uint maxSize)
        {
            Debug.Assert(new BigInteger(maxSize).IsPowerOfTwo, "Must be power of 2");
            _maxSize = maxSize;
            _sizeMask = maxSize - 1;
            _head = 0;
            _tail = 0;
            _buffer = new T[maxSize];
        }

        public void Enqueue(in T item)
        {
            Debug.Assert(Count != _maxSize, "Max size reached");
            _buffer[_tail] = item;
            Count++;
            _tail = (_tail + 1) & _sizeMask;
        }

        public ref T this[uint index] => ref _buffer[(_head + index) & _sizeMask];

        public void Clear()
        {
            // Clear the "buffer"
            Count = 0;
            _head = _tail;
        }
    }
}
