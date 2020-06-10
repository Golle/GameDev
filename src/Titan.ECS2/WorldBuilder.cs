namespace Titan.ECS2
{
    public class WorldBuilder
    {
        // TODO: add methods for components, systems, max entities etc

        public IWorld Build() => WorldCollection.CreateWorld();
    }
}
