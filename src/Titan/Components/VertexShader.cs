using System.Runtime.InteropServices;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexShader
    {
        public IVertexShader Shader;
        public IInputLayout InputLayout;
    }
}