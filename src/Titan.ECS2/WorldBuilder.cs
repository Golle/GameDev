using System;
using System.Collections.Generic;

namespace Titan.ECS2
{
    public class WorldBuilder
    {
        // TODO: add methods for components, systems, max entities etc
        private readonly uint _maxEntities;
        private readonly IList<(Type componentType, uint size)> _components = new List<(Type componentType, uint size)>();
        public WorldBuilder(uint maxEntities = 10_000)
        {
            _maxEntities = maxEntities;
        }

        public WorldBuilder WithComponent<T>(uint size = 10000)
        {
            _components.Add((typeof(T), size));
            return this;
        }

        public World Build() => WorldCollection.CreateWorld(_maxEntities, _components);
    }
}
