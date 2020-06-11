using System.Diagnostics;

namespace Titan.ECS2.Components
{
    internal static class ComponentIdDispatcher
    {
        private static ulong _id = 1;
        private static object _lockObject = new object();

        private static uint _count;
        public static ulong Next()
        {
            ulong id;
            lock (_lockObject)
            {
                _count++;
                Debug.Assert(_count < 64, "To many components registered");
                id = _id;
                _id <<= 1;
            }
            return id;
        }
    }
}
