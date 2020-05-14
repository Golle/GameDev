using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Shaders
{
    internal class VertexShader : IVertexShader
    {
        private readonly ID3D11DeviceContext _context;
        private readonly ID3D11VertexShader _shader;

        public VertexShader(ID3D11DeviceContext context, ID3D11VertexShader shader)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public void Bind()
        {
            _context.SetVertexShader(_shader);
        }

        public void Dispose()
        {
            _shader.Dispose();
        }
    }
}
