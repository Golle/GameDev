using System;
using System.Runtime.CompilerServices;

namespace Titan.ECS2.Systems
{
    public abstract class EntitySystem : ISystem
    {
        private readonly EntitySet _entitySet;
        public bool IsEnabled => true;

        protected EntitySystem(EntitySet entitySet)
        {
            _entitySet = entitySet;
        }

        public abstract void Update(float deltaTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ReadOnlySpan<uint> GetEntities() => _entitySet.GetEntities();

        public void Dispose()
        {
            _entitySet.Dispose();
        }
    }
}