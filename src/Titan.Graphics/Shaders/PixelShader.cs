using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Shaders
{
    internal class PixelShader : IPixelShader
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11PixelShader _shader;

        public PixelShader(ID3D11DeviceContext context, ID3D11PixelShader shader)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public void Bind()
        {
            _context.SetPixelShader(_shader);
        }

        public void Dispose()
        {
            _shader.Dispose();
        }
    }
}
