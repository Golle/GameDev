using System.Numerics;
using System.Runtime.InteropServices;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TextureCoordinates
    {
        public Vector2 TopLeft;
        public Vector2 BottomRight;

        public static readonly TextureCoordinates Default =  new TextureCoordinates{BottomRight = new Vector2(1,1), TopLeft = new Vector2(0,0)};
    }
}
