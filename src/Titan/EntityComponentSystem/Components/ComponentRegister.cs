using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Titan.EntityComponentSystem.Components
{
    internal class ComponentRegister : IComponentRegister
    {
        private ulong _currentMask = 1;
        private int _count;
        private readonly IDictionary<Type, ulong> _identifiers = new Dictionary<Type, ulong>();
        public ulong Register<T>()
        {
            var type = typeof(T);
            Debug.Assert(!_identifiers.ContainsKey(type), $"Type {type.Name} has already been added to the registry");
            Debug.Assert(_count <= 64, $"Too many components registered, only supports 64 components at the moment.");

            var mask = _currentMask;
            _identifiers[type] = mask;
            _currentMask = mask << 1;
            _count++;
            return mask;
        }

        public ulong Get<T>()
        {
            var type = typeof(T);
            Debug.Assert(_identifiers.ContainsKey(type));
            return _identifiers[type];
        }
    }
}
