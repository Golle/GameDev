using System.Numerics;
using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Texture2D : ITexture2D
    {
        private readonly ID3D11Texture2D _texture;
        private Vector2 _topLeft = new Vector2(0,0);
        private Vector2 _bottomRight = new Vector2(1,1);

        public Texture2D(ID3D11Texture2D texture)
        {
            _texture = texture;
        }

        public void Dispose()
        {
            _texture.Dispose();
        }

        public ref readonly Vector2 TopLeft => ref _topLeft;

        public ref readonly Vector2 BottomRight => ref _bottomRight;
        public void Bind()
        {
            
        }
    }
}
