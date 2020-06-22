using System.Runtime.CompilerServices;
using Titan.ECS.Entities;

namespace Titan.ECS
{
    internal static class Worlds
    {
        //TODO: maybe make this dynamic ?
        private static readonly World[] _worlds = new World[20];
        private static readonly IdDispatcher IdDispatcher = new IdDispatcher();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AttachEntity(uint worldId, uint parent, uint child) => _worlds[worldId].AttachEntity(parent, child);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DestroyEntity(uint worldId, uint entityId) => _worlds[worldId].DestroyEntity(entityId);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AddComponent<T>(uint worldId, uint entityId) where T : struct => _worlds[worldId].AddComponent<T>(entityId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AddComponent<T>(uint worldId, uint entityId, in T value) where T : struct => _worlds[worldId].AddComponent<T>(entityId, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RemoveComponent<T>(uint worldId, uint entityId) where T : struct => _worlds[worldId].RemoveComponent<T>(entityId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(uint worldId, uint id) where T : struct => _worlds[worldId].HasComponent<T>(id);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetComponent<T>(uint worldId, uint id) where T : struct => ref _worlds[worldId].GetComponent<T>(id);

        internal static void DestroyWorld(World world)
        {
            _worlds[world.Id] = null;
            IdDispatcher.Free(world.Id);
        }

        internal static uint AddWorld(World world)
        {
            var id = IdDispatcher.Next();
            _worlds[id] = world;
            return id;
        }
    }
}
