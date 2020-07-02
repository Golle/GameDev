using System.Runtime.InteropServices;
using Titan.Graphics.Textures;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Texture2D
    {
        public ITexture2D Texture;
    }
}
