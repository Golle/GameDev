namespace Titan.Resources
{
    internal interface IImageLoader
    {
        ImageAsset LoadFromFile(string filename);
    }
}
