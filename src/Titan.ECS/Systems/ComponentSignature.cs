using System;
using Titan.ECS.Components;

namespace Titan.ECS.Systems
{
    public readonly struct ComponentSignature
    {
        public ulong Value { get; }
        public ComponentSignature(params Type[] types)
        {
            Value = 0;
            foreach (var type in types)
            {
                Value |= type.ComponentMask();
            }
        }
    }
}
