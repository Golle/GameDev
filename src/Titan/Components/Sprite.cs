using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.Graphics.Textures;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Sprite
    {
        public TextureCoordinates TextureCoordinates;

        public Color Color;
        //public uint PixelsPerUnit; // TODO: maybe create a sprite abstraction?
    }
}
