using System.Diagnostics;

namespace Titan.ECS3.Components
{
    internal static class ComponentId<T> where T : struct
    {
        public static ulong Id { get; } = ComponentId.Next();
    }
    internal static class ComponentId
    {
        private static readonly object Lock = new object();
        private static ulong _id = 1;
        private static int _count;
        public static ulong Next()
        {
            lock (Lock)
            {
                AssertCount();
                var id = _id;
                _id <<= 1;
                return id;
            }
        }

        [Conditional("DEBUG")]
        private static void AssertCount()
        {
            _count++;
            Debug.Assert(_count < 64, "Maximum number of components created");
        }
    }

}
