using System.Runtime.InteropServices;

namespace Titan.EntityComponentSystem.Components
{
    [StructLayout(LayoutKind.Sequential, Size = sizeof(ulong) + sizeof(uint) + sizeof(uint), Pack = 4)]
    public readonly struct Component
    {
        public readonly ulong Id;
        public readonly uint Index;
        public readonly uint Entity;
        public Component(ulong id, uint index, uint entity)
        {
            Id = id;
            Index = index;
            Entity = entity;
        }
    }
}
