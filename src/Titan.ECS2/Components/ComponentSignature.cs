using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS2.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ComponentSignature
    {
        // TODO: use a bitarray instead (or replicate the same behaviour in a struct)
        private ulong _mask;

        public ComponentSignature(ulong mask)
        {
            _mask = mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ulong component) => (component & _mask) > 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Matches(in ComponentSignature signature) => (_mask & signature._mask) == signature._mask;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(ulong component) => _mask |= component;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(ulong component) => _mask ^= component;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(ulong mask) => _mask = mask;
    }
}
