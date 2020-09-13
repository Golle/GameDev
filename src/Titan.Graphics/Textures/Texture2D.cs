using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Textures
{
    internal class Texture2D : ITexture2D
    {
        public uint Width { get; }
        public uint Height { get; }
        public IntPtr TextureHandle => _texture.Handle;
        public IntPtr NativeHandle => _textureView.Handle;

        private readonly ID3D11Texture2D _texture;
        private readonly ID3D11ShaderResourceView _textureView;

        public Texture2D(ID3D11Texture2D texture, ID3D11ShaderResourceView textureView, uint width, in uint height)
        {
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
    }
}
