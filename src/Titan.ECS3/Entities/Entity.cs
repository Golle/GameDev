using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Titan.ECS3.Entities
{
    [DebuggerDisplay("Id = {" + nameof(Id) + "}, WorldId = {" + nameof(WorldId) + "}")]
    [StructLayout(LayoutKind.Explicit)]
    public readonly ref struct Entity
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

        public void Destroy()
        {
            Debug.Assert(WorldId != 0, "Entity was not created in a World");
            Worlds.DestroyEntity(WorldId, Id);
        }
    }
}
