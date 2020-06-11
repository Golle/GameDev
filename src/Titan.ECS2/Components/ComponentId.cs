using System.Runtime.CompilerServices;

namespace Titan.ECS2.Components
{
    internal static class ComponentId<T>
    {
        private static readonly ulong _id;

        static ComponentId()
        {
            _id = ComponentIdDispatcher.Next();
        }
        public static ulong Id
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _id;
        }
    }
}
