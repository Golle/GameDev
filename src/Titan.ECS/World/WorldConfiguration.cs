using Titan.Core.Ioc;

namespace Titan.ECS.World
{
    public class WorldConfiguration
    {
        public string Name { get;  }
        public ComponentConfiguration[] Components { get; }
        public IContainer Container { get; }
        public uint MaxNumberOfEntities { get; }
        public WorldConfiguration(string name, ComponentConfiguration[] components, IContainer container, uint maxNumberOfEntities)
        {
            Name = name;
            Components = components;
            Container = container;
            MaxNumberOfEntities = maxNumberOfEntities;
        }
    }
}
