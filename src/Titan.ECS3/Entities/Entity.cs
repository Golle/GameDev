using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Titan.ECS3.Entities
{
    [DebuggerDisplay("Id = {" + nameof(Id) + "}")]
    [StructLayout(LayoutKind.Explicit)]
    public readonly ref struct Entity
    {
        [field: FieldOffset(0)]
        public uint Id { get; }

        public Entity(uint id)
        {
            Id = id;
        }
    }
}
