using System.Numerics;
using System.Runtime.InteropServices;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Camera;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer
{
    internal class SpriteBatchRenderer : ISpriteBatchRenderer
    {
        private struct Camera
        {
            public Matrix4x4 Transform;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct Vertex2D
        {
            public Vector2 Position;
            public Vector2 TextureCoordinates;
            public Color Color;
        }

        private readonly IDevice _device;
        private const uint MaxTextures = 32;
        private const uint MaxSprites = 10900;
        private const uint MaxIndices = 6 * MaxSprites;
        private const uint MaxVertices = 4 * MaxSprites;
        private readonly IVertexBuffer _buffer;
        private readonly IIndexBuffer _indices;
        private readonly Vertex2D[] _vertices = new Vertex2D[MaxVertices];
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;

        private uint _numberOfVertices = 0u;
        private uint _numberOfIndices = 0u;
        private uint _numberOfTextures = 0u;

        private ICamera _camera;
        private IConstantBuffer<Camera> _cameraBuffer;
        private ITexture2D[] _textures = new ITexture2D[MaxTextures];
        private ISampler _sampler;
        private IBlendState _blendState;

        private IDeviceContext _context;

        public SpriteBatchRenderer(IDevice device, IBlobReader blobReader, ICameraFactory cameraFactory)
        {
            _device = device;
            _context = _device.ImmediateContext;
            _camera = cameraFactory.CreateOrhographicCamera();

            _buffer = device.CreateVertexBuffer<Vertex2D>(MaxVertices);
            _indices = device.CreateIndexBuffer(CreateIndices());
            _sampler = device.CreateSampler(true);
            _blendState = device.CreateBlendState();
            _cameraBuffer = device.CreateConstantBuffer(new Camera {Transform = _camera.ViewProjection});
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader2D.cso");
            _vertexShader = device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader2D.cso");
            _pixelShader = device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(3).Append("Position", VertexLayoutTypes.Position2D).Append("Textures", VertexLayoutTypes.Texture2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);

        }

        private static short[] CreateIndices()
        {
            var indices = new short[MaxIndices];
            var vertexIndex = 0;
            for (var i = 0; i < MaxIndices; i += 6)
            {
                indices[i] = (short) vertexIndex;
                indices[i+1] = (short) (3 + vertexIndex);
                indices[i+2] = (short) (1 + vertexIndex);
                indices[i+3] = (short) (3 + vertexIndex);
                indices[i+4] = (short) (2 + vertexIndex);
                indices[i+5] = (short) (1 + vertexIndex);
                vertexIndex += 4;
            }
            return indices;
        }

        public void Push(ITexture2D texture2D, in TextureCoordinates textureCoordinates, in Vector2 position, in Vector2 size, in Color color)
        {
            var textureIndex = GetTextureIndex(texture2D);

            var index = _numberOfVertices;

            var topLeft = textureCoordinates.TopLeft;
            var bottomRight = textureCoordinates.BottomRight;

            // Bottom left
            _vertices[index].Position = position;
            _vertices[index].Color = color;
            _vertices[index++].TextureCoordinates = new Vector2(topLeft.X, bottomRight.Y);

            // Bottom right
            _vertices[index].Position = new Vector2(position.X + size.X, position.Y);
            _vertices[index].Color = color;
            _vertices[index++].TextureCoordinates = bottomRight;

            // Top right
            _vertices[index].Position = new Vector2(position.X + size.X, position.Y + size.Y);
            _vertices[index].Color = color;
            _vertices[index++].TextureCoordinates = new Vector2(bottomRight.X, topLeft.Y);

            // Top left
            _vertices[index].Position = new Vector2(position.X, position.Y + size.Y);
            _vertices[index].Color = color;
            _vertices[index].TextureCoordinates = topLeft; 

            _numberOfVertices += 4;
            _numberOfIndices += 6;
        }

        private uint GetTextureIndex(ITexture2D texture2D)
        {
            for (var i = 0u; i < _numberOfTextures; ++i)
            {
                if (_textures[i] == texture2D)
                {
                    return i;
                }
            }
            _textures[_numberOfTextures] = texture2D;
            return _numberOfTextures++;
        }

        public void Flush() => _context.UpdateResourceData(_buffer, _vertices, 0);

        public void Render()
        {
            
            if (_numberOfIndices > 0u)
            {
                _device.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                
                _cameraBuffer.BindToVertexShader();
                
                _context.SetVertexBuffer(_buffer);
                _context.SetIndexBuffer(_indices);
                
                _inputLayout.Bind();
                _vertexShader.Bind();
                _pixelShader.Bind();
                _sampler.Bind();
                _textures[0].Bind();
                _blendState.Bind();


                _context.DrawIndexed(_numberOfIndices, 0, 0);
                
            }
            _numberOfIndices = 0u;
            _numberOfTextures = 0u;
            _numberOfVertices = 0u;
        }

        public void Dispose()
        {
            _buffer?.Dispose();
            _indices?.Dispose();
            _vertexShader?.Dispose();
            _pixelShader?.Dispose();
            _inputLayout?.Dispose();
            _blendState?.Dispose();
            _sampler?.Dispose();
            _cameraBuffer?.Dispose();
        }
    }
}
