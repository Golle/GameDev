namespace Titan.Core.Assets.Images
{
    public interface IImageLoader
    {
        ImageAsset LoadFromFile(string filename);
    }
}
