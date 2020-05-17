using System;
using System.Collections.Generic;
using System.Linq;
using Titan.Core.EventSystem;
using Titan.Systems.Components;
using Titan.Systems.Components.Events;

namespace Titan.Systems.EntitySystem
{
    internal class Entity : IEntity
    {
        private readonly IComponentSystem _componentSystem;
        private readonly IEventManager _eventManager;
        private readonly EntityManager _entityManager;


        private readonly IList<Entity> _children = new List<Entity>();
        private readonly IList<IComponent> _components = new List<IComponent>();
        public IEnumerable<IComponent> Components => _components;
        public IEnumerable<IEntity> Children => _children;

        public bool Enabled { get; private set; }
        public Entity(IComponentSystem componentSystem, IEventManager eventManager, EntityManager entityManager)
        {
            _componentSystem = componentSystem;
            _eventManager = eventManager;
            _entityManager = entityManager;
        }

        public void Attach(Entity entity)
        {
            _children.Add(entity);
            // do more stuff?
        }

        public T AddComponent<T>(ComponentId id) where T : IComponent
        {
            var component = (T)_componentSystem.Create(id);
            if (component == null)
            {
                throw new InvalidOperationException($"Failed to create component with id {id} and type {typeof(T).Name}");
            }
            _components.Add(component);
            _eventManager.Publish(new ComponentAddedEvent(component));
            return component;
        }

        public T GetComponent<T>(ComponentId id) where T : IComponent
        {
            for (var i = 0; i < _components.Count; ++i)
            {
                if (_components[i].Id == id)
                {
                    return (T)_components[i];
                }
            }
            return default;
        }

        public void SetActive(bool active)
        {
            Enabled = active;
            // disable/enable components
        }

        public void Destroy()
        {
            for (var i = 0; i < _children.Count; ++i)
            {
                _children[i].Destroy();
            }

            for (var i = 0; i < _components.Count; ++i)
            {
                _eventManager.Publish(new ComponentRemovedEvent(_components[i]));
            }
            _children.Clear();
            _components.Clear();
        }
    }
}
