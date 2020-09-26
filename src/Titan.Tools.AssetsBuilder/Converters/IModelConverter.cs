using Titan.Tools.AssetsBuilder.Data;
using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder.Converters
{
    internal interface IModelConverter
    {
        Mesh[] ConvertFromObj(string filename);
    }


    internal class Mesh
    {
        public Vertex[] Vertices { get; }
        public ushort[] Indices { get; }
        public Mesh(Vertex[] vertices, ushort[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }
    }
}
