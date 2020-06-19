using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Titan.ECS3.Entities
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct EntityInfo
    {
        internal uint Parent;
        internal List<uint> Children;
        //internal ComponentSomething ComponentFlags; TODO: add component mask here
        internal ushort NumberOfParents;

        internal bool HasParent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Parent > 0u;
        }
        
        internal bool HasChildren
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Children?.Count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Reset()
        {
            Parent = 0u;
            Children?.Clear();
        }
    }
}
