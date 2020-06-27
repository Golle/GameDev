using System.Numerics;

namespace Titan.Core.Assets
{
    public interface IModel3D
    {
        public Vector3[] Vertices { get; }
        public Vector3[] Normals { get; }
        public Vector2[] Textures { get; }
        public Face[] Faces { get; }
    }
}
