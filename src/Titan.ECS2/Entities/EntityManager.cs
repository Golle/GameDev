using System.Collections.Concurrent;
using System.Threading;

namespace Titan.ECS2.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly ushort _worldId;
        private readonly ConcurrentStack<uint> _freeEntities = new ConcurrentStack<uint>();
        private uint _nextId = 1u;
        public EntityManager(ushort worldId)
        {
            _worldId = worldId;
        }

        public Entity Create()
        {
            if (!_freeEntities.TryPop(out var id))
            {
                id = Interlocked.Increment(ref _nextId);
            }
            return new Entity(id, _worldId);
        }

        public void Destroy(uint entityId)
        {
            _freeEntities.Push(entityId);
        }
    }
}
