using System;
using System.Collections.Generic;

namespace Titan.ECS
{
    internal class WorldConfiguration
    {
        public uint MaxEntities { get; set; }
        public uint DefaultPoolSize { get; set; }

        public IEnumerable<(Type type, uint size)> Components() => _components;
        private readonly IList<(Type type, uint size)> _components = new List<(Type type, uint size)>();
        public void AddComponent(Type type) => _components.Add((type, DefaultPoolSize));
        public void AddComponent(Type type, uint size) => _components.Add((type, size));
    }
}
