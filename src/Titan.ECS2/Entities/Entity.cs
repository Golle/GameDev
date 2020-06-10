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

        public ref T Get<T>() where T : struct => ref WorldCollection.GetComponent<T>(WorldId, Id);
        public void Set<T>(in T value) where T : struct => WorldCollection.SetComponent<T>(WorldId, Id, value);


        public void Destroy() => WorldCollection.DestroyEntity(WorldId, Id);
    }
}
