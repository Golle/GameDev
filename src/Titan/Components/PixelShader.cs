using System.Runtime.InteropServices;
using Titan.Graphics.Shaders;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PixelShader
    {
        public IPixelShader Shader;
    }
}