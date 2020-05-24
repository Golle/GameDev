using System;

namespace Titan.ECS.World
{
    public readonly struct ComponentConfiguration
    {
        public Type Type { get; }
        public uint Capacity { get; }
        public ComponentConfiguration(Type type, uint capacity)
        {
            Type = type;
            Capacity = capacity;
        }
    }
}
