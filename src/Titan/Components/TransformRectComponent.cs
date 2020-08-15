using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Titan.Core.Math;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct TransformRectComponent
    {
        public Vector3 Offset;
        public Size Size;
        public AnchorPoint AnchorPoint;

        internal Vector3 WorldPosition;
        internal BoundingBox2D BoundingBox;
    }

    public enum AnchorPoint
    {
        BottomLeft,// default
        Bottom,
        BottomRight,
        Right,
        TopRight,
        Top,
        TopLeft, 
        Left,
        Center
    }


    public struct BoundingBox2D
    {
        public Vector2 Min;
        public Vector2 Max;
        public Vector2 Center;
        public BoundingBox2D(in Vector2 position, in Size size)
        {
            Min = position;
            Max = new Vector2(position.X + size.Width, position.Y + size.Height);
            Center = (Min + Max) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Intersects(in Vector2 position) => !(position.Y < Min.Y || position.Y > Max.Y || position.X < Min.X || position.X > Max.X);

    }
}
