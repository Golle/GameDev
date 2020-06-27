using System;
using System.Numerics;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Camera;
using Titan.Graphics.Layout;
using Titan.Graphics.Models;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer
{
    public class Renderer3Dv2 : IDisposable //: IRendererV2
    {
        private readonly IDevice _device;
        private readonly IConstantBuffer<ConstantBufferValues> _constantBuffer;
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;
        private ICamera _camera;

        public Renderer3Dv2(IDevice device, IBlobReader blobReader, ICameraFactory cameraFactory)
        {
            _device = device;
            
            using var vertexShaderBlob = blobReader.ReadFromFile("Shaders/VertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexShaderBlob);
            using var pixelShaderBlob = blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);
     
           _inputLayout = device.CreateInputLayout(new VertexLayout(4).Append("Position", VertexLayoutTypes.Position3D).Append("Normals", VertexLayoutTypes.Position3D).Append("Textures", VertexLayoutTypes.Texture2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
            _constantBuffer = device.CreateConstantBuffer<ConstantBufferValues>();

            _camera = cameraFactory.CreatePerspectiveCamera();

            _sampler = device.CreateSampler();
        }



        private float _rotationX = 0f;
        private float _rotationZ = 0f;
        private float _rotationY = 0f;
        private ISampler _sampler;

        public void Render(IMesh mesh, in Matrix4x4 modelSpace, ITexture2D texture)
        {
            //_camera.RotateZ(_rotationZ+=0.01f);
            _camera.RotateX(0.3f);
            _camera.RotateY(_rotationY += 0.003f);


            ConstantBufferValues cb = default;
            cb.ViewProjection = _camera.ViewProjection;
            cb.ModelSpace= modelSpace;

            _constantBuffer.Update(cb);

            mesh.VertexBuffer.Bind();
            //mesh.IndexBuffer.Bind();
            _sampler.Bind();
            texture.Bind();
            _constantBuffer.BindToVertexShader();
            _inputLayout.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();

            _device.Draw(mesh.VertexBuffer.NumberOfVertices, 0);
            //_device.DrawIndexed(mesh.IndexBuffer.NumberOfIndices, 0, 0);
        }

        public void Dispose()
        {
            _constantBuffer.Dispose();
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _inputLayout.Dispose();

        }
    }

}
