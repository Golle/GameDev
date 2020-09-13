using System.Numerics;
using Titan.D3D11.Compiler;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Models;
using Titan.Graphics.RendererOld;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer.Passes
{
    internal class RenderMeshPass : IRenderPass
    {
        private readonly IVertexShader _vertexShader;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;
        private readonly IConstantBuffer<PerFrameContantBuffer> _perFrameConstantBuffer;
        private readonly IConstantBuffer<LightsConstantBuffer> _lightsConstantBuffer;
        private readonly IConstantBuffer<PerObjectContantBuffer> _perObjectConstantBuffer;
        private readonly ISampler _sampler;

        public RenderMeshPass(IDevice device, ID3DCompiler compiler)
        {
            using var vertexShaderBlob = new Blob(compiler.CompileShaderFromFile(@"F:\Git\GameDev\src\Titan.D3D11.Bindings\VertexShader.hlsl", "main", "vs_5_0"));
            using var pixelShaderBlob = new Blob(compiler.CompileShaderFromFile(@"F:\Git\GameDev\src\Titan.D3D11.Bindings\PixelShader.hlsl", "main", "ps_5_0"));
            
            _vertexShader = device.CreateVertexShader(vertexShaderBlob);
            _pixelShader = device.CreatePixelShader(pixelShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(4).Append("Position", VertexLayoutTypes.Position3D).Append("Normal", VertexLayoutTypes.Position3D).Append("Texture", VertexLayoutTypes.Texture2D).Append("Color", VertexLayoutTypes.Float4Color), vertexShaderBlob);
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>();
            _lightsConstantBuffer = device.CreateConstantBuffer<LightsConstantBuffer>();
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>();
            _sampler = device.CreateSampler();
        }


        public void SetCamera(IDeviceContext context, in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            context.UpdateConstantBuffer(_perFrameConstantBuffer, new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            context.SetVertexShaderConstantBuffer(_perFrameConstantBuffer);
        }

        public void Begin(IDeviceContext context)
        {
            context.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            context.UpdateConstantBuffer(_lightsConstantBuffer, new LightsConstantBuffer());
            context.SetPixelShaderConstantBuffer(_lightsConstantBuffer);
            context.SetPixelShaderSampler(_sampler);
            context.SetInputLayout(_inputLayout);
            context.SetVertexShader(_vertexShader);
            context.SetPixelShader(_pixelShader);
        }

        public void Render(IDeviceContext context, IMesh mesh, in Matrix4x4 worldMatrix, ITexture2D texture)
        {
            context.UpdateConstantBuffer(_perObjectConstantBuffer, new PerObjectContantBuffer { World = Matrix4x4.Transpose(worldMatrix) });
            context.SetVertexShaderConstantBuffer(_perObjectConstantBuffer, 1);

            context.SetPixelShaderResource(texture);

            context.SetVertexBuffer(mesh.VertexBuffer);

            context.Draw(mesh.VertexBuffer.NumberOfVertices, 0);
        }

        public void Dispose()
        {
            _vertexShader?.Dispose();
            _pixelShader?.Dispose();
            _inputLayout?.Dispose();
            _perFrameConstantBuffer?.Dispose();
            _lightsConstantBuffer?.Dispose();
            _perObjectConstantBuffer?.Dispose();
            _sampler?.Dispose();
        }
    }
}
