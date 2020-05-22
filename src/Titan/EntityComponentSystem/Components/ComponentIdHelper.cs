using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Titan.EntityComponentSystem.Components
{
    public static class ComponentIdHelper
    {
        private static ulong _currentMask = 1;
        private static int _count;
        private static readonly IDictionary<Type, ulong> Identifiers = new Dictionary<Type, ulong>();

        public static ulong ComponentMask(this Type type)
        {
            Debug.Assert(_count <= 64);
            if (Identifiers.TryGetValue(type, out var mask))
            {
                return mask;
            }

            Identifiers[type] = mask = _currentMask;
            _count++;
            _currentMask <<= 1;
            return mask;
        }
    }
}
