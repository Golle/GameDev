namespace Titan.Core.Assets.Images
{
    public class ImageAsset
    {
        public uint Height { get; }
        public uint Width { get; }
        public byte[] Pixels { get; }
        public ImageAsset(uint height, uint width, byte[] pixels)
        {
            Height = height;
            Width = width;
            Pixels = pixels;
        }
    }
}
