using System;
using Titan.Systems.TransformSystem;

namespace Titan.Systems.Components
{
    internal class ComponentSystem : IComponentSystem 
    {
        private readonly ITransform3DPool  _transform3DPool;

        public ComponentSystem(ITransform3DPool transform3DPool)
        {
            _transform3DPool = transform3DPool;
        }

        public IComponent Create(ComponentId id)
        {
            switch (id)
            {
                case ComponentId.Transform3D:
                    return _transform3DPool.GetOrCreate();
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}
