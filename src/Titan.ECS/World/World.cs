using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;
using Titan.Core.Logging;
using Titan.ECS.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;

namespace Titan.ECS.World
{
    internal class World : IWorld
    {
        private readonly ISystemsRunner _systemsRunner;
        private readonly IComponentManager _componentManager;
        private readonly IEntityManager _entityManager;
        private readonly ILogger _logger;
        private readonly IComponentMapper<Relationship> _relationship;

        protected IDictionary<uint, List<ulong>> _components = new Dictionary<uint, List<ulong>>(1000);

        public World(IEventManager eventManager, ISystemsRunner systemsRunner, IComponentManager componentManager, IEntityManager entityManager, ILogger logger)
        {
            _systemsRunner = systemsRunner;
            _componentManager = componentManager;
            _entityManager = entityManager;
            _logger = logger;
            _relationship = _componentManager.GetComponentMapper<Relationship>();

            eventManager.Subscribe<UpdateEvent>(OnUpdate);
        }

        //public IEntity CreateEntity()
        //{
        //    // TODO: entities should be pooled
        //    var entity = new Entity(_entityManager, _componentManager);
        //    entity.Init();
        //    entity.AddComponent(Relationship.Default); // initialize with default values, setting everything to EntityId.Invalid
            
        //    return entity;
        //}

        public void AddComponent<T>(uint entity) where T : struct
        {
            _components[entity].Add(_componentManager.Create<T>(entity));
        }

        public void AddComponent<T>(uint entity, in T initialData) where T : struct
        {
            _components[entity].Add(_componentManager.Create<T>(entity, initialData));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetComponent<T>(uint entity) where T : struct => ref _componentManager.GetComponentMapper<T>()[entity];

        public void RemoveComponent<T>(uint entity) where T : struct
        {
            var id = typeof(T).ComponentMask();
            _components[entity].Remove(id);
            _componentManager.Free(entity, id);
        }

        public uint CreateEntity()
        {
            var entity = _entityManager.Create();
            _components[entity] = new List<ulong>();
            AddComponent<Relationship>(entity);

            return entity;
        }

        public void Destroy()
        {
            _logger.Debug("Destroying World");
        }

        public void SetParent(uint entity, uint parentEntity)
        {
            ref var relationship = ref _relationship[entity];
            Debug.Assert(relationship.Parent != EntityId.Invalid, "Parent already set for this entity. Moving parent is not supported yet.");
            relationship.Parent = parentEntity;

            //ref var parent = ref _relationship[parentEntity];
            //if (parent.First == EntityId.Invalid)
            //{
            //    parent.First = entity;
            //    return;
            //}

            //ref var firstChild = ref _relationship[parent.First];


        }

        private void OnUpdate(in UpdateEvent @event)
        {
            //_entityManager.Update();
            //_componentManager.Update();

            _systemsRunner.Update(@event.ElapsedTime);
        }
    }
}
