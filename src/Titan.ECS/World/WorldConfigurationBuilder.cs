using System;
using System.Collections.Generic;
using System.Linq;
using Titan.Core.Ioc;
using Titan.ECS.Systems;

namespace Titan.ECS.World
{
    public class WorldConfigurationBuilder
    {
        private readonly string _name;
        private readonly uint _maxNumberOfEntities;

        private readonly IList<ISystem> _systems = new List<ISystem>();
        private readonly IList<ComponentConfiguration> _components = new List<ComponentConfiguration>();
        private IContainer? _container;

        public WorldConfigurationBuilder(string name, uint maxNumberOfEntities)
        {
            _name = name;
            _maxNumberOfEntities = maxNumberOfEntities;
            _container = null;
        }

        public WorldConfigurationBuilder WithContainer(IContainer container)
        {
            if (_container != null)
            {
                throw new InvalidOperationException("Container has already been registered");
            }
            _container = container;
            return this;
        }
        public WorldConfigurationBuilder WithComponent<T>(uint capacity)
        {
            if (_components.Any(c => c.Type == typeof(T)))
            {
                throw new InvalidOperationException($"Component of type {typeof(T)} has already been registered.");
            }
            _components.Add(new ComponentConfiguration(typeof(T), capacity));
            return this;
        }

        public WorldConfigurationBuilder WithSystem<TConcrete>() where TConcrete : class, ISystem
        {
            if (_container == null)
            {
                throw new InvalidOperationException("No container has been registered.");
            }
            _container.Register<TConcrete>();
            return this;
        }

        public WorldConfigurationBuilder WithSystem<TInterface, TConcrete>() where TConcrete : TInterface where TInterface : ISystem
        {
            if (_container == null)
            {
                throw new InvalidOperationException("No container has been registered.");
            }
            _container.Register<TInterface, TConcrete>();
            return this;
        }

        public WorldConfigurationBuilder WithSystem(ISystem system)
        {
            if (_systems.Any(s => s.GetType() == system.GetType()))
            {
                throw new InvalidOperationException($"System of type {system.GetType()} has already been registered.");
            }
            _systems.Add(system);
            return this;
        }

        public WorldConfiguration Build()
        {
            if (_container == null)
            {
                throw new InvalidOperationException("No container has been registered.");
            }
            return new WorldConfiguration(_name, _components.ToArray(), _container, _maxNumberOfEntities);
        }
    }
}
