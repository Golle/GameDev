using System;
using System.Collections.Generic;
using System.Diagnostics;
using Titan.Systems.Components;

namespace Titan.EntityComponentSystem.Configuration
{
    public static class ComponentExtensions
    {
        private static ulong _currentMask = 1;
        private static int _count;
        private static readonly IDictionary<Type, ulong> Identifiers = new Dictionary<Type, ulong>();
        
        public static ulong Mask(this IComponent component)
        {
            Debug.Assert(_count <= 64);
            var type = component.GetType();
            if(!Identifiers.TryGetValue(type, out var mask))
            {
                Identifiers[type] = mask = _currentMask;
                _count++;
                _currentMask <<= 1;
            }
            return mask;
        }

        public static ulong ComponentMask(this Type type)
        {
            Debug.Assert(Identifiers.ContainsKey(type), $"Type {type.Name} has not been registered as component type.");
            return Identifiers[type];
        }
    }
}
