namespace Titan.Core.Assets.Fonts
{
    public interface IFontAssetLoader
    {
        FontAsset LoadFromFile(string filename);
    }
}
