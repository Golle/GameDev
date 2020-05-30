using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Camera;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;

namespace Titan.Graphics.Renderer
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ConstantBufferValues
    {
        public Matrix4x4 ViewProjection;
    }

    internal class Renderer3D : IRenderer
    {
        private readonly IDevice _device;
        private readonly IVertexBuffer<Vertex> _vertexBuffer;
        private readonly IIndexBuffer _indexBuffer;
        private readonly IConstantBuffer<ConstantBufferValues> _constantBuffer;
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;

        private readonly Vertex[] _vertices = new Vertex[100000];
        private readonly short[] _indices = new short[100000];
        private int _numberOfVertices = 0;
        private int _numberOfIndices = 0;
        public Renderer3D(IDevice device, IBlobReader blobReader)
        {
            _device = device;
            _vertexBuffer = device.CreateVertexBuffer<Vertex>(100000);
            _indexBuffer = device.CreateIndexBuffer(100000);
            
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);
            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("POSITION", VertexLayoutTypes.Position3D).Append("COLOR", VertexLayoutTypes.Float4Color), vertexShaderBlob);

            _constantBuffer = device.CreateConstantBuffer<ConstantBufferValues>();
        }

        
        public void Push(RendereableModel model)
        {
            var indexOffset = _numberOfVertices;
            foreach (var index in model.Indices)
            {
                _indices[_numberOfIndices++] = (short)(index + indexOffset);
            }

            Array.Copy(model.Vertices,0, _vertices, _numberOfVertices, model.Vertices.Length);
            _numberOfVertices += model.Vertices.Length;
        }

        public void Flush()
        {
            _vertexBuffer.SetData(_vertices, _numberOfVertices);
            _indexBuffer.SetData(_indices, _numberOfIndices);

        }

        public void BeginScene(ICamera camera)
        {
            _device.BeginRender();

            ConstantBufferValues cb = default;
            cb.ViewProjection = camera.ViewProjection;
            _constantBuffer.Update(cb);

            _vertexBuffer.Bind();
            _indexBuffer.Bind();
            _constantBuffer.BindToVertexShader();
            _inputLayout.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();


            
            _device.DrawIndexed((uint)_numberOfIndices, 0, 0);


        }

        public void EndScene()
        {
            _device.EndRender();

            _numberOfVertices = 0;
            _numberOfIndices = 0;
        }

        public void Dispose()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _constantBuffer.Dispose();
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _inputLayout.Dispose();

        }
    }

    public interface IRenderer : IDisposable
    {

        void Push(RendereableModel model);
        void Flush();
        void BeginScene(ICamera camera);
        void EndScene();
    }
}
