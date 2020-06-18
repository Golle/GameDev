using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Titan.ECS3.Systems
{
    /// <summary>
    /// Base system that does logic on a set of Components (entities)
    /// </summary>
    public abstract class EntitySystem : ISystem
    {
        private readonly uint[] _entities;
        private readonly int[] _mapping;


        private int _count;

        protected EntitySystem(IWorld world) :this(world, world.MaxEntities)
        {
        }

        protected EntitySystem(IWorld world, uint maxEntitiesInSystem)
        {
            _entities = new uint[world.MaxEntities]; // must me MaxEntities
            _mapping = new int[maxEntitiesInSystem]; // can be lower depending on how many expected entities will have this component setup
        }

        public void PreUpdate()
        {
            Console.WriteLine("Processing messages");
        }

        public void Update(float deltaTime)
        {
            OnPreUpdate();
            
            OnUpdate(deltaTime, GetEntities());
            
            OnPostUpdate();
        }


        private void OnEntityCreated(uint entity)
        {
            ref var index = ref _mapping[entity];
            if (index == -1)
            {
                index = Interlocked.Increment(ref _count) - 1;
                _entities[index] = entity;
            }
        }

        protected virtual void OnPreUpdate() { }
        protected abstract void OnUpdate(float deltaTime, ReadOnlySpan<uint> entities);
        protected virtual void OnPostUpdate() { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected ReadOnlySpan<uint> GetEntities() => new ReadOnlySpan<uint>(_entities,0, _count);
    }
}
