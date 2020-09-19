namespace Titan.Core.Configuration
{
    public interface IConfiguration
    {
        bool Debug { get; }
        int FixedUpdateFrequency { get; }
        string ResourcesPath { get; }
        string ShadersPath { get; }
        string FontsPath { get; }
        string SoundsPath { get; }
    }
}
