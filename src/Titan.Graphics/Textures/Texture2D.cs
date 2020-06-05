using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Texture2D : ITexture2D
    {
        public uint Width { get; }
        public uint Height { get; }

        private readonly ID3D11Texture2D _texture;
        private readonly ID3D11ShaderResourceView _textureView;
        private readonly ID3D11DeviceContext _context;
        public Texture2D(ID3D11DeviceContext context, ID3D11Texture2D texture, ID3D11ShaderResourceView textureView, uint width, in uint height)
        {
            _context = context;
            _texture = texture;
            _textureView = textureView;
            Width = width;
            Height = height;
        }

        public void Dispose()
        {
            _texture.Dispose();
            _textureView.Dispose();
        }
        
        public void Bind(uint startSlot = 0u)
        {
            _context.PSSetShaderResources(startSlot, _textureView);
        }
    }
}
