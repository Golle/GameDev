using System.Collections.Generic;
using System.Threading;
using Titan.ECS.Systems;

namespace Titan.ECS.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly IContext _context;
        private uint _index; // TODO: ID is unique per world, which means we need to handle events both global (full engine) and local (each World)

        //private readonly ConcurrentQueue<uint> _freeEntities = new ConcurrentQueue<uint>(); // TODO: Use this when/if we decide to use multithreading when creating entities
        
        private readonly Queue<uint> _freeEntities = new Queue<uint>(100);
        public EntityManager(IContext context)
        {
            _context = context;
        }

        public uint Create()
        {
            if (!_freeEntities.TryDequeue(out var entity))
            {
                entity = Interlocked.Increment(ref _index);
            }
            _context.OnEntityCreated(entity);
            return entity;
        }

        public void Free(uint entity)
        {
            //TODO: Free the Components used by this entity
            //TODO: handle entity destruction with events 
            // 1. Disable entity
            // 2. Flag for destruction (+ all children)
            // 3. Destroy entity (return it to pool)

            // do something
            
            _context.OnEntityDestroyed(entity);
            _freeEntities.Enqueue(entity);
        }
    }
}
