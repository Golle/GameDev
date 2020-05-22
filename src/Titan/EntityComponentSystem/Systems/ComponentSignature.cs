using System;
using Titan.EntityComponentSystem.Components;

namespace Titan.EntityComponentSystem.Systems
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
