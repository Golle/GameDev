using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.EntityComponentSystem.Entities;
using Titan.Systems.TransformSystem;

namespace Titan.EntityComponentSystem.Systems
{

    
    internal class Group
    {

    }


    internal class TestSystem : SystemBase<TestComponent1, TestComponent2, Transform3D>
    {
        protected override void Update(float deltaTime, TestComponent1 t1, TestComponent2 t2, Transform3D t3)
        {
            t1.Transform = t1.Transform * t2.Transform * t3.Transform;
        }
    }



    internal class SystemManager
    {

        public void OnEntityCreated(Entity entity)
        {

        }

        public void Register(ISystem system)
        {
            
        }
    }


    internal interface ISystem
    {
        Type[] Signature { get; }
    }


    internal abstract class SystemBase<T1> : ISystem
    {
        public Type[] Signature { get; } = {typeof(T1)};
        protected abstract void Update(float deltaTime, T1 t1);
    }

    internal abstract class SystemBase<T1, T2> : ISystem
    {
        public Type[] Signature { get; } = { typeof(T1), typeof(T2) };
        protected abstract void Update(float deltaTime ,T1 t1, T2 t2);
    }

    internal abstract class SystemBase<T1, T2, T3>
    {
        // TODO: this needs optimization
        public Type[] Signature { get; } = { typeof(T1), typeof(T2), typeof(T3) };
        
        protected abstract void Update(float deltaTime, T1 t1, T2 t2, T3 t3);
        private readonly Container[] _components = new Container[100000];
        private uint _count = 0u;
        internal void OnEntityCreated(Entity entity)
        {
            ref var container = ref _components[_count];
            container.Id = entity.Id;
            container.First = entity.GetComponent<T1>();
            container.Second = entity.GetComponent<T2>();
            container.Third = entity.GetComponent<T3>();
            _count++;
        }

        internal void Update(float deltaTime)
        {
            for (var i = 0; i < _count; i++)
            {
                ref var container = ref _components[i];
                Update(deltaTime, container.First, container.Second, container.Third);
            }
        }
        private struct Container
        {
            public ulong Id;
            public T1 First;
            public T2 Second;
            public T3 Third;
        }
    }
}
