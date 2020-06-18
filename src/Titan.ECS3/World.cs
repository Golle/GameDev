using System.Runtime.CompilerServices;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    internal class World : IWorld
    {
        private static readonly IdDispatcher IdDispatcher = new IdDispatcher();

        internal uint Id { get; } = IdDispatcher.Next();
        internal EntityManager EntityManager { get; }
        
        internal World(uint maxEntities)
        {
            Worlds.SetWorld(this);
            EntityManager = new EntityManager(Id, maxEntities);
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
            IdDispatcher.Free(Id);
        }
    }


    internal static class Worlds
    {
        //TODO: maybe make this dynamic ?
        private static readonly World[] _worlds = new World[20];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AttachEntity(uint worldId, uint parent, uint child) => _worlds[worldId].EntityManager.Attach(parent, child);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DestroyEntity(uint worldId, uint entityId) => _worlds[worldId].EntityManager.Destroy(entityId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetWorld(World world) => _worlds[world.Id] = world;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyWorld(World world) => _worlds[world.Id] = null;
    }
}
