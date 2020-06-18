using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS3.Entities
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct EntityInfo
    {
        internal uint Parent;
        internal List<uint> Children;
        //internal ComponentSomething ComponentFlags; TODO: add component mask here
        //internal ushort NumberOfParents; TODO: add calculation of number of parents to enable sorting based on a hierarchy


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool HasParent() => Parent > 0u;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool HasChildren() => Children?.Count > 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Reset()
        {
            Parent = 0u;
            Children?.Clear();
        }
    }
}
