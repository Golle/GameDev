using Titan.Core.Ioc;

namespace Titan.ECS.World
{
    public class WorldConfiguration
    {
        public string Name { get;  }
        public ComponentConfiguration[] Components { get; }
        public IContainer Container { get; }
        public WorldConfiguration(string name, ComponentConfiguration[] components, IContainer container)
        {
            Name = name;
            Components = components;
            Container = container;
        }
    }
}
