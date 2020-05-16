using System.Numerics;

namespace Titan.Graphics.Renderer
{
    public class RendereableModel
    {
        public ref readonly Vector3 Position => ref _position;
        public ref readonly Vertex[] Vertices => ref _vertices;
        public ref readonly short[] Indices => ref _indices;
        private Vector3 _position;
        
        private readonly Vertex[] _vertices;
        private readonly short[] _indices;

        public RendereableModel(Vertex[] vertices, short[] indices, Vector3 position)
        {
            _vertices = vertices;
            _indices = indices;
            _position = position;
        }

        public void SetPosition(in Vector3 position) => _position = position;
    }
}
