using System.Collections.Generic;

namespace Titan.Systems.TransformSystem
{
    internal class Transform3DPool : ITransform3DPool
    {

        private const int InitialPoolSize = 100;
        private readonly Queue<Transform3D> _pooledItems = new Queue<Transform3D>(InitialPoolSize);

        public Transform3DPool()
        {
            for (var i = 0; i < InitialPoolSize; ++i)
            {
                _pooledItems.Enqueue(new Transform3D());
            }
        }
        public Transform3D GetOrCreate()
        {
            if (_pooledItems.TryDequeue(out var transform))
            {
                transform.Init();
                return transform;
            }
            return new Transform3D();
        }

        public void Return(Transform3D transform)
        {
            _pooledItems.Enqueue(transform);
        }
    }
}
