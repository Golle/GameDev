using Titan.Tools.AssetsBuilder.WavefrontObj;

namespace Titan.Tools.AssetsBuilder.Converters
{
    internal class ModelConverter : IModelConverter
    {
        private readonly ObjParser _objParser;
        public ModelConverter(ObjParser objParser)
        {
            _objParser = objParser;
        }

        public Mesh ConvertFromObj(string filename)
        {
            var obj = _objParser.ReadFromFile(filename).Result; // remove async api

            return CreateMesh(obj);
        }

        private static Mesh CreateMesh(WavefrontObject obj)
        {
            var builder = new MeshBuilder(obj);
            foreach (var objGroup in obj.Groups)
            {
                foreach (var face in objGroup.Faces)
                {
                    builder.SetMaterial(face.Material);
                    // TODO: add support for Concave faces (triangles done this way might overlap)

                    // RH
                    // 1st face => vertex 0, 1, 2
                    // 2nd face => vertex 0, 2, 3
                    // 3rd face => vertex 0, 4, 5
                    // 4th face => ...
                    
                     var vertices = face.Vertices;
                    // more than 3 vertices per face, we need to triangulate the face to be able to use it in the engine.
                    for (var i = 2; i < vertices.Length; ++i)
                    {
                        builder.AddVertex(vertices[0]);
                        //Flip the order to convert from RH to LH (d3d) 
                        builder.AddVertex(vertices[i]);
                        builder.AddVertex(vertices[i - 1]);
                    }
                }
            }
            return builder.Build();
        }
    }
}
