namespace Titan.Tools.AssetsBuilder.WavefrontObj
{
    public readonly struct ObjVertex
    {
        public readonly int VertexIndex;
        public readonly int TextureIndex;
        public readonly int NormalIndex;
        public ObjVertex(int vertexIndex, int textureIndex, int normalIndex)
        {
            VertexIndex = vertexIndex;
            TextureIndex = textureIndex;
            NormalIndex = normalIndex;
        }
    }
}
