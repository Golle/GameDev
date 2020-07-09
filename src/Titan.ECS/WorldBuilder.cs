using System;
using System.Diagnostics;

namespace Titan.ECS
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

        public WorldBuilder WithComponent(Type type)
        {
            _configuration.AddComponent(type);
            return this;
        }
        
        public WorldBuilder WithComponent(Type type, uint poolSize)
        {
            _configuration.AddComponent(type, poolSize);
            return this;
        }

        public WorldBuilder WithComponent<T>()
        {
            return WithComponent(typeof(T));
        }

        public WorldBuilder WithComponent<T>(uint poolSize)
        {
            return WithComponent(typeof(T), poolSize);
        }

        public IWorld Build()
        {
            return new World(_configuration);
        }
    }
}
