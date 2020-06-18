using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Titan.ECS3
{
    public class WorldBuilder
    {
        private readonly uint _maxEntities;
        private readonly uint _defaultComponentPoolSize;

        private WorldConfiguration _configuration = new WorldConfiguration();

        public WorldBuilder(uint maxEntities = 10_000, uint defaultComponentPoolSize = 10_000)
        {
            Debug.Assert(maxEntities > 0, "maxEntities > 0");
            _configuration.MaxEntities = maxEntities;
            _configuration.DefaultPoolSize = defaultComponentPoolSize;
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
    }
}
