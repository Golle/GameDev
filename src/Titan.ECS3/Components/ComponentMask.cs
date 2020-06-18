using System.Runtime.CompilerServices;

namespace Titan.ECS3.Components
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
    }
}
