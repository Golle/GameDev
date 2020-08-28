using System;
using Titan.D3D11.Device;

namespace Titan.Graphics.Shaders
{
    internal class VertexShader : IVertexShader
    {
        private readonly ID3D11VertexShader _shader;
        public IntPtr NativeHandle => _shader.Handle;
        public VertexShader(ID3D11VertexShader shader)
        {
            _shader = shader ?? throw new ArgumentNullException(nameof(shader));
        }

        public void Dispose()
        {
            _shader.Dispose();
        }
    }
}
