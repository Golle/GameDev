namespace Titan.Core.Configuration
{
    public interface IConfiguration
    {
        bool Debug { get; }
        int FixedUpdateFrequency { get; }
    }
}
