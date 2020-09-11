using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Shaders
{
    internal class PixelShader : IPixelShader
    {
        private readonly ID3D11PixelShader _shader;
        public IntPtr NativeHandle => _shader.Handle;

        public PixelShader(ID3D11PixelShader shader)
        {
            _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public void Dispose()
        {
            _shader.Dispose();
        }
    }
}
