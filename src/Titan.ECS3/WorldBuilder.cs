using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Titan.ECS3
{
    public class WorldBuilder
    {
        private readonly WorldConfiguration _configuration = new WorldConfiguration();
        public WorldBuilder(uint maxEntities = 10_000, uint defaultComponentPoolSize = 10_000)
        {
            Debug.Assert(maxEntities > 0, "maxEntities > 0");
            _configuration.MaxEntities = maxEntities;
            _configuration.DefaultPoolSize = defaultComponentPoolSize;
        }


        public WorldBuilder WithComponent<T>()
        {
            _configuration.AddComponent(typeof(T));
            return this;
        }

        public WorldBuilder WithComponent<T>(uint poolSize)
        {
            _configuration.AddComponent(typeof(T), poolSize);
            return this;
        }

        public IWorld Build()
        {
            return new World(_configuration);
        }
    }


    internal class WorldConfiguration
    {
        public uint MaxEntities { get; set; }
        public uint DefaultPoolSize { get; set; }

        public IEnumerable<(Type type, uint size)> Components() => _components;
        private IList<(Type type, uint size)> _components = new List<(Type type, uint size)>();
        public void AddComponent(Type type) => _components.Add((type, DefaultPoolSize));
        public void AddComponent(Type type, uint size) => _components.Add((type, size));
    }
}
