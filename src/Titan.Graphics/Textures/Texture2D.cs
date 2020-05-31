using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Texture2D : ITexture2D
    {
        private readonly ID3D11Texture2D _texture;
        public Texture2D(ID3D11Texture2D texture)
        {
            _texture = texture;
        }

        public void Dispose()
        {
            _texture.Dispose();
        }
    }
}
