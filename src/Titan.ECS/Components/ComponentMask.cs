using System.Runtime.CompilerServices;

namespace Titan.ECS.Components
{
    public readonly struct ComponentMask
    {
        public readonly ulong Components;
        public ComponentMask(ulong components)
        {
            Components = components;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>() where T : struct=> (ComponentId<T>.Id & Components) > 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent(ulong componentId) => (componentId & Components) > 0;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in ComponentMask mask) => (mask.Components & Components) == mask.Components;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSupsetOf(in ComponentMask mask) => (mask.Components & Components) == Components;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComponentMask operator |(in ComponentMask mask1, in ComponentMask mask2) => new ComponentMask(mask1.Components | mask2.Components);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComponentMask operator |(in ComponentMask mask1, ulong componentId) => new ComponentMask(mask1.Components | componentId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComponentMask operator ^(in ComponentMask mask1, ulong componentId) => new ComponentMask(mask1.Components ^ componentId);
    }
}
