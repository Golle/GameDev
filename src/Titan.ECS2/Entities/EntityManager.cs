using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS2.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly ushort _worldId;
        private readonly ConcurrentStack<uint> _freeEntities = new ConcurrentStack<uint>();
        private uint _nextId = 1u;
        private readonly EntityInfo[] _info;

        public EntityManager(ushort worldId, uint maxEntities)
        {
            _worldId = worldId;
            _info = new EntityInfo[maxEntities];
        }

        public Entity Create()
        {
            if (!_freeEntities.TryPop(out var id))
            {
                id = Interlocked.Increment(ref _nextId);
                _info[id] = default; // reset the info if it's a re-used ID.
            }
            
            return new Entity(id, _worldId);
        }

        public void Destroy(uint entityId)
        {
            _freeEntities.Push(entityId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref EntityInfo GetEntityInfo(uint entityId) => ref _info[entityId];
    }
}
