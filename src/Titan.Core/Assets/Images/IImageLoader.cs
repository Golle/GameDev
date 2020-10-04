namespace Titan.Core.Assets.Images
{
    public interface IImageLoader
    {
        ImageAsset LoadFromFile(string filename);
        ImageAsset2 LoadFromFileUnsafe(string filename);
    }
}
