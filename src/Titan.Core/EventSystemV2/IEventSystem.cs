using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Titan.Core.EventSystem;

namespace Titan.Core.EventSystemV2
{
    public class EventQueue<T>
    {
        public uint Count { get; }
        public ref readonly T this[uint index] => ref _events[(_tail + index) % MaxEventCount];

        private const int MaxEventCount = 8 * 1024;
        private const int MaxEventCountMask = MaxEventCount - 1;
        private readonly T[] _events = new T[MaxEventCount];

        private int _head = 0;
        private int _tail = 0;

        private uint _numberOfEvents;

        public EventQueue()
        {
            Debug.Assert(MaxEventCount != 0u && (MaxEventCount & (MaxEventCount - 1u)) == 0u,
                "MaxEventCount must be power of 2");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(in T @event)
        {
            //Interlocked.Increment(ref _head);
            //if(_head >= MaxEventCount)
            //Interlocked.And(ref _head, MaxEventCountMask);
            //lock (this)
            //{
            //_head++;
            //if (_head >= MaxEventCount)
            //    _head &= MaxEventCountMask;
            ;
            //}


            _numberOfEvents++;
            _events[_head = (_head + 1) & MaxEventCountMask] = @event;
        }
    }
}
