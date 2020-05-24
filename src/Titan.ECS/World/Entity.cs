using System.Collections.Generic;
using Titan.ECS.Components;
using Titan.ECS.Entities;

namespace Titan.ECS.World
{
    /// <summary>
    /// Wrapper class for an entity (might delete later depending on what I'll be using it for)
    /// </summary>
    internal class Entity : IEntity
    {
        private readonly IEntityManager _entityManager;
        private readonly IComponentManager _componentManager;
        private readonly IList<Component> _components = new List<Component>();

        private uint _entityId;
        public Entity(IEntityManager entityManager, IComponentManager componentManager)
        {
            _entityManager = entityManager;
            _componentManager = componentManager;
        }
        public void Init()
        {
            _entityId = _entityManager.Create();
        }

        public void AddComponent<T>() where T : unmanaged
        {
            _components.Add(_componentManager.Create<T>(_entityId));
        }

        public void AddComponent<T>(in T initialData) where T : unmanaged
        {
            _components.Add(_componentManager.Create(_entityId, initialData));
        }

        public void Destroy()
        {
            _entityManager.Free(_entityId);
            foreach (var component in _components)
            {
                _componentManager.Free(component);
            }
        }
    }
}
