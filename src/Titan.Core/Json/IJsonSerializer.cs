namespace Titan.Core.Json
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string json);
    }
}