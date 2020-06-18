using System.Diagnostics;

namespace Titan.ECS3
{
    public class WorldBuilder
    {
        private readonly uint _maxEntities;

        public WorldBuilder(uint maxEntities = 10_000)
        {
            Debug.Assert(maxEntities > 0, "maxEntities > 0");
            _maxEntities = maxEntities;
        }

        public IWorld Build()
        {
            return new World(_maxEntities);
        }
    }
}
