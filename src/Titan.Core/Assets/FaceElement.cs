namespace Titan.Core.Assets
{
    public readonly struct FaceElement
    {
        public readonly int Vertex;
        public readonly int Texture;
        public readonly int Normal;
        public FaceElement(int vertex, int texture, int normal)
        {
            Vertex = vertex;
            Texture = texture;
            Normal = normal;
        }
    }
}
