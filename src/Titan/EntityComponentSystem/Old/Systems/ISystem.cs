using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Titan.EntityComponentSystem.Configuration;
using Titan.EntityComponentSystem.Entities;
using Titan.Systems.Components;
using Titan.Systems.TransformSystem;

namespace Titan.EntityComponentSystem.Systems
{
    internal class TestSystem12 : ISystem1
    {
        //private readonly IComponentMapper<Transform3D> _transform;
        private readonly IComponentMapper<TestComponent1> _testComponent1;
        private IList<uint> _entities = new List<uint>();

        public TestSystem12(IComponentMapperProvider componentMapperProvider)
        {
            _testComponent1 = componentMapperProvider.Get<TestComponent1>();
        }

        public void Update(uint entityId, float deltaTime)
        {
            //ref var transform = ref _transform[entityId];
            //var testComponent 

        }
    }

    internal interface ISystem1
    {
        //void Update(float deltaTime);
    }

    internal interface IComponentMapperProvider
    {
        IComponentMapper<T> Get<T>() where T : unmanaged;
    }

    internal interface IComponentMapper<T> where T : unmanaged
    {
        ref T this[uint entityId] { get; } 
    }

    internal class ComponentMapper<T> : IComponentMapper<T> where T : unmanaged
    
    {
        public ref T this[uint entityId] => throw new NotImplementedException();
    }


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

        private IDictionary<ulong, ulong> _entityComponentSignatures = new Dictionary<ulong, ulong>(10_000);


        private IList<ISystem> _systems = new List<ISystem>();

        public void OnEntityRemoved(Entity entity)
        {
            var signature = entity.ComponentSignature;
            foreach (var system in _systems)
            {
                if (system.IsMatch(signature))
                {
                    system.RemoveEntity(entity);
                }
            }
            _entityComponentSignatures.Remove(entity.Id);
        }

        public void OnEntityCreated(Entity entity)
        {
            var signature = entity.ComponentSignature;
            foreach (var system in _systems)
            {
                if (system.IsMatch(signature))
                {
                    system.AddEntity(entity);
                }
            }

            _entityComponentSignatures.Add(entity.Id, entity.ComponentSignature);
        }

        public void OnComponentRemoved(Entity entity)
        {

        }
        public void OnComponentAdded(Entity entity)
        {
            foreach (var system in _systems)
            {
                if (system.IsMatch(entity.ComponentSignature))
                {
                    system.AddEntity(entity);
                }
            }
        }

        public void Register(ISystem system)
        {
            
        }
    }


    internal interface ISystem
    {
        ulong Signature { get; }
        bool IsMatch(ulong componentSignature);
        void RemoveEntity(Entity entity);
        void AddEntity(Entity entity);
    }


    //internal abstract class SystemBase<T1> : ISystem
    //{
    //    //public Type[] Signature { get; } = {typeof(T1)};
    //    protected abstract void Update(float deltaTime, T1 t1);
    //    public ulong Signature { get; }
    //    public bool IsMatch(ulong signature)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void OnEntityRemoved(Entity entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal abstract class SystemBase<T1, T2> : ISystem
    //{
    //    //public Type[] Signature { get; } = { typeof(T1), typeof(T2) };
    //    protected abstract void Update(float deltaTime ,T1 t1, T2 t2);
    //    public ulong Signature { get; }
    //    public bool IsMatch(ulong signature)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void OnEntityRemoved(Entity entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



    internal readonly struct SystemSignature
    {
        public ulong Signature { get; }
        public SystemSignature(params Type[] types)
        {
            Signature = 0;
            foreach (var type in types)
            {
                Signature |= type.ComponentMask();
            }
        }

        public bool IsMatch(ulong signature)
        {
            return (Signature & signature) == Signature;
        }
    }


    internal abstract class SystemBase<T1, T2, T3> : ISystem
    {
        protected abstract void Update(float deltaTime, T1 t1, T2 t2, T3 t3);
        private readonly Container[] _components = new Container[100000];
        private readonly Queue<uint> _freeIndexes = new Queue<uint>(100);
        private readonly SystemSignature _systemSignature = new SystemSignature(typeof(T1), typeof(T2), typeof(T3));
        private uint _count;

        public ulong Signature => _systemSignature.Signature;
        public bool IsMatch(ulong componentSignature) => _systemSignature.IsMatch(componentSignature);

        public void AddEntity(Entity entity)
        {
            if (!_freeIndexes.TryDequeue(out var index))
            {
                index = _count++;
            }
            
            ref var container = ref _components[index];
            container.Active = true;
            container.Id = entity.Id;
            container.First = entity.GetComponent<T1>();
            container.Second = entity.GetComponent<T2>();
            container.Third = entity.GetComponent<T3>();
        }

        public void RemoveEntity(Entity entity)
        {
            for (var i = 0u; i < _count; i++)
            {
                ref var component = ref _components[i];
                if (component.Id == entity.Id)
                {
                    component.Active = false;
                    _freeIndexes.Enqueue(i);
                    break;
                }
            }
        }

        internal void Update(float deltaTime)
        {
            for (var i = 0; i < _count; i++)
            {
                ref var container = ref _components[i];
                if (container.Active) // TODO: measure performance of this compared to re-ordering the list when something changes (Array.Sort ?)
                {
                    Update(deltaTime, container.First, container.Second, container.Third);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)] // TODO: check if Pack can optimize memory usage (Pack = 1,2,4 or 8)
        private struct Container
        {
            public ulong Id;
            public T1 First;
            public T2 Second;
            public T3 Third;
            public bool Active;
        }
    }

}
