using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Camera;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer
{
    public interface ISpriteBatchRenderer : IDisposable
    {
        void Push(ITexture2D texture2D, in Vector2 position, in Vector2 size, in Color color);
        void Flush();

        void Render();
    }


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
            //public Vector2 TextureCoordinates;
            public Color Color;
        }

        private readonly IDevice _device;
        private const uint MaxSprites = 10000;
        private const uint MaxIndices = 6 * MaxSprites;
        private const uint MaxVertices = 4 * MaxSprites;
        private readonly IVertexBuffer<Vertex2D> _buffer;
        private readonly IIndexBuffer _indices;
        private readonly Vertex2D[] _vertices = new Vertex2D[MaxVertices];
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;

        private uint _numberOfVertices = 0u;
        private uint _numberOfIndices = 0u;
        private ICamera _camera;
        private IConstantBuffer<Camera> _cameraBuffer;

        public SpriteBatchRenderer(IDevice device, IBlobReader blobReader, ICameraFactory cameraFactory)
        {
            _device = device;
            _camera = cameraFactory.CreateOrhographicCamera();

            _buffer = device.CreateVertexBuffer<Vertex2D>(MaxVertices);
            _indices = device.CreateIndexBuffer(CreateIndices());
            _cameraBuffer = device.CreateConstantBuffer<Camera>(new Camera {Transform = _camera.ViewProjection});
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader2D.cso");
            _vertexShader = device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader2D.cso");
            _pixelShader = device.CreatePixelShader(pixelShaderBlob);
            
            //_inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("Position", VertexLayoutTypes.Position2D).Append("TextureCoordinates", VertexLayoutTypes.Texture2D), vertexShaderBlob);
            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("Position", VertexLayoutTypes.Position2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);

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

        public void Push(ITexture2D texture2D, in Vector2 position, in Vector2 size, in Color color)
        {
            var index = _numberOfVertices;

            _vertices[index].Position = position;
            //_vertices[index++].TextureCoordinates = texture2D.TopLeft;
            _vertices[index++].Color = color;

            _vertices[index].Position = new Vector2(position.X + size.X, position.Y);
            //_vertices[index++].TextureCoordinates = new Vector2(texture2D.BottomRight.X, texture2D.TopLeft.Y);
            _vertices[index++].Color = color;

            _vertices[index].Position = new Vector2(position.X + size.X, position.Y + size.Y);
            //_vertices[index++].TextureCoordinates = texture2D.BottomRight;
            _vertices[index++].Color = color;

            _vertices[index].Position = new Vector2(position.X, position.Y + size.Y);
            //_vertices[index].TextureCoordinates = new Vector2(texture2D.TopLeft.X, texture2D.BottomRight.Y);
            _vertices[index].Color = color;

            _numberOfVertices += 4;
            _numberOfIndices += 6;
        }

        public void Flush()
        {
           _buffer.SetData(_vertices, 0); // replace this with map/unmap
           _numberOfVertices = 0u;
        }

        public void Render()
        {
            _device.BeginRender();

            _cameraBuffer.BindToVertexShader();
            _buffer.Bind();
            _indices.Bind();
            _inputLayout.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();

            _device.DrawIndexed(_numberOfIndices, 0, 0);
            _device.EndRender();
            _numberOfIndices = 0u;
        }

        public void Dispose()
        {
            _device?.Dispose();
            _buffer?.Dispose();
            _indices?.Dispose();
            _vertexShader?.Dispose();
            _pixelShader?.Dispose();
            _inputLayout?.Dispose();
        }
    }
}