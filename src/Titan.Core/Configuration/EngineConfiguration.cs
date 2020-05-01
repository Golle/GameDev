namespace Titan.Core.Configuration
{
    internal class EngineConfiguration : IConfiguration
    {
        public bool Debug => true;
        public int FixedUpdateFrequency => 30; // 30 hz
    }
}
