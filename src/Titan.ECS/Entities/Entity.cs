using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS.Entities
{
    [DebuggerDisplay("Id = {" + nameof(Id) + "}, WorldId = {" + nameof(WorldId) + "}")]
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct Entity
    {
        [field: FieldOffset(0)]
        public uint Id { get; }

        [field: FieldOffset(4)]
        public uint WorldId { get; } // TODO: saving 4 bytes (pointer to a world instance would be 8 bytes, is it worth it?)
        public Entity(uint id, uint worldId)
        {
            WorldId = worldId;
            Id = id;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>() where T : struct => Worlds.AddComponent<T>(WorldId, Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddComponent<T>(in T value) where T : struct => Worlds.AddComponent<T>(WorldId, Id, value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveComponent<T>() where T : struct => Worlds.RemoveComponent<T>(WorldId, Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Attach(in Entity entity) => Worlds.AttachEntity(WorldId, Id, entity.Id);
        public void Destroy()
        {
            Debug.Assert(WorldId != 0, "Entity was not created in a World");
            Worlds.DestroyEntity(WorldId, Id);
        }
    }
}
