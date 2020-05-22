using System.Threading;
using Titan.EntityComponentSystem.Systems;

namespace Titan.EntityComponentSystem.Entities
{
    internal class EntityManager : IEntityManager
    {
        private readonly IContext _context;
        private uint _index;

        public EntityManager(IContext context)
        {
            _context = context;
        }

        public uint Create()
        {
            var entity = Interlocked.Increment(ref _index);
            _context.OnEntityCreated(entity);
            return entity;
        }

        public void Free(uint entity)
        {
            //TODO: Free the Components used by this entity
            _context.OnEntityDestroyed(entity);
        }
    }
}
