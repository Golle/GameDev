using System.Runtime.InteropServices;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct D3D11Shader
    {
        public IVertexShader VertexShader;
        public IPixelShader PixelShader;
        public IInputLayout InputLayout;
    }
}
