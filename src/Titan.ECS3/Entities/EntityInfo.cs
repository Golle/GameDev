using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.ECS3.Components;

namespace Titan.ECS3.Entities
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct EntityInfo
    {
        internal uint Parent;
        internal List<uint> Children;
        internal ComponentMask Components;
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
