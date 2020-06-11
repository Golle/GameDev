using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS2.Entities
{
    [StructLayout(LayoutKind.Explicit, Size = sizeof(uint) + sizeof(ushort))]
    public readonly struct Entity
    {
        [field: FieldOffset(0)]
        public uint Id { get; }

        [field: FieldOffset(4)]
        public ushort WorldId { get; }

        public Entity(uint id, ushort worldId)
        {
            Id = id;
            WorldId = worldId;
        }


        /// <summary>
        /// This should not be used by systems, better to get a reference to the mapper and use that.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>() where T : struct => ref WorldCollection.GetComponent<T>(WorldId, Id);

        /// <summary>
        /// This should not be used by systems, better to get a reference to the mapper and use that.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(in T value = default) where T : struct => WorldCollection.AddComponent<T>(WorldId, Id, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set<T>(in T value) where T : struct => WorldCollection.SetComponent<T>(WorldId, Id, value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Destroy() => WorldCollection.DestroyEntity(WorldId, Id);
    }
}
