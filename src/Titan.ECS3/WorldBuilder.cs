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
}
