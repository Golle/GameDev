using System.Runtime.InteropServices;
using Titan.ECS.Entities;

namespace Titan.ECS.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)] // Pack by 4 bytes since we use UINT for the entity ID or this struct will take up 8 * 4 bytes in memory.
    public struct Relationship
    {
        public uint First;
        public uint Next;
        public uint Previous;
        public uint Parent;

        public static readonly Relationship Default = new Relationship
        {
            First = EntityId.Invalid, 
            Next = EntityId.Invalid, 
            Previous = EntityId.Invalid, 
            Parent = EntityId.Invalid
        };
    }
}
