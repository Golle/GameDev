namespace Titan.Core.Configuration
{
    internal class Configuration : IConfiguration
    {
        public bool Debug { get; set; }
        public int FixedUpdateFrequency { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public IResourcePaths Paths { get; set; }
        public ISoundConfiguration Sound { get; set; }
        public IRendererConfiguration Renderer { get; set; }
    }
}
