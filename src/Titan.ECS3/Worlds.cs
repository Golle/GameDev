using System.Runtime.CompilerServices;
using Titan.ECS3.Entities;

namespace Titan.ECS3
{
    internal static class Worlds
    {
        //TODO: maybe make this dynamic ?
        private static readonly World[] _worlds = new World[20];
        private static readonly IdDispatcher IdDispatcher = new IdDispatcher();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AttachEntity(uint worldId, uint parent, uint child) => _worlds[worldId].EntityManager.Attach(parent, child);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DestroyEntity(uint worldId, uint entityId) => _worlds[worldId].EntityManager.Destroy(entityId);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyWorld(World world)
        {
            _worlds[world.Id] = null;
            IdDispatcher.Free(world.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint AddWorld(World world)
        {
            var id = IdDispatcher.Next();
            _worlds[id] = world;
            return id;
        }
    }
}
