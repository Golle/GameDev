using System.Runtime.CompilerServices;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    internal class World : IWorld
    {
        internal uint Id { get; }

        internal EntityManager EntityManager { get; }

        private readonly uint _maxEntities;
        uint IWorld.MaxEntities => _maxEntities;

        internal World(WorldConfiguration configuration)
        {
            Id = Worlds.AddWorld(this);
            _maxEntities = configuration.MaxEntities;
            EntityManager = new EntityManager(Id, configuration.MaxEntities);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity CreateEntity()
        {
            var entity = EntityManager.Create();
            return entity;
        }

        public void Dispose()
        {
            Worlds.DestroyWorld(this);
        }
    }
}
