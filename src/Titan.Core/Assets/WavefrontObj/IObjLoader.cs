namespace Titan.Core.Assets.WavefrontObj
{
    public interface IObjLoader
    {
        WavefrontObject LoadFromFile(string filename);
    }
}
