using System.Numerics;

namespace Titan.Core.Assets.WavefrontObj
{
    public class WavefrontObject
    {
        public Vector3[] Vertices { get; }
        public Vector3[] Normals { get; }
        public Vector2[] Textures { get; }
        public Face[] Faces { get; }
        public WavefrontObject(Vector3[] vertices, Vector3[] normals, Vector2[] textures, Face[] faces)
        {
            Vertices = vertices;
            Normals = normals;
            Textures = textures;
            Faces = faces;
        }
    }
}
