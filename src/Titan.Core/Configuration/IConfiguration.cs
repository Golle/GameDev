namespace Titan.Core.Configuration
{
    public interface IConfiguration
    {
        bool Debug { get; }
        int FixedUpdateFrequency { get; }
        string Title { get; }
        int Width { get; }
        int Height { get; }
        IResourcePaths Paths { get; }
        ISoundConfiguration Sound { get; }
        IRendererConfiguration Renderer { get; }
    }
}
