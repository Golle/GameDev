using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Titan.ECS2.Components;
using Titan.ECS2.Entities;

namespace Titan.ECS2
{
    internal static class WorldCollection
    {
        private static readonly Queue<ushort> FreeIds = new Queue<ushort>();
        private static uint _nextId = 0;
        private static World[] _worlds = new World[10];
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ref T GetComponent<T>(ushort worldId, uint entityId) where T : struct => ref _worlds[worldId].GetComponent<T>(entityId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetComponent<T>(ushort worldId, uint entityId, in T value) where T : struct => _worlds[worldId].SetComponent<T>(entityId, value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AddComponent<T>(ushort worldId, uint entityId, in T value) where T : struct => _worlds[worldId].AddComponent<T>(entityId, value);
        public static IWorld CreateWorld(in uint maxEntities, IEnumerable<(Type componentType, uint size)> components)
        {
            var worldId = (ushort)Interlocked.Increment(ref _nextId);
            lock (_worlds)
            {
                if (_worlds.Length <= worldId)
                {
                    Array.Resize(ref _worlds, worldId);
                }
            }

            var simplePublisher = new SimplePublisher(worldId);
            simplePublisher.Subscribe<WorldDestroyedMessage>(WorldDestroyed);

            var entityManager = new EntityManager(worldId);
            var componentManager = new ComponentManager(maxEntities);
            foreach (var (componentType, size) in components)
            {
                componentManager.RegisterComponent(componentType, size);
            }

            return _worlds[worldId] = new World(worldId, entityManager, componentManager, simplePublisher);
        }

        private static void WorldDestroyed(in WorldDestroyedMessage @event)
        {
            var world = _worlds[@event.Id];
            world.Dispose();
            _worlds[@event.Id] = null;
            FreeIds.Enqueue(@event.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DestroyEntity(ushort worldId, uint entityId) => _worlds[worldId].DestroyEntity(entityId);
    }
}
