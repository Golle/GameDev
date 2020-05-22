using System.Runtime.InteropServices;

namespace Titan.EntityComponentSystem.Entities
{

    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint))]
    public readonly struct Entity
    {
        public uint Id { get; }
        public Entity(uint id)
        {
            Id = id;
        }

        public static implicit operator Entity(uint id)
        {
            return new Entity(id);
        }
    }
}
