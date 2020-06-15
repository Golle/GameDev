using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS3.Entities
{
    internal class EntityIdDispatcher
    {
        private uint _nextId;
        private readonly ConcurrentQueue<uint> _free = new ConcurrentQueue<uint>();
        internal uint Next()
        {
            if (!_free.TryDequeue(out var id))
            {
                id = Interlocked.Increment(ref _nextId);
            }
            return id;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Free(uint entityId) => _free.Enqueue(entityId);
    }
}
