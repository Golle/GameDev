using System;
using System.Diagnostics;
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
            _perFrameConstantBuffer = device.CreateConstantBuffer<PerFrameContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _lightsConstantBuffer = device.CreateConstantBuffer<LightsConstantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _perObjectConstantBuffer = device.CreateConstantBuffer<PerObjectContantBuffer>(BufferUsage.Dynamic, BufferAccessFlags.Write);
            _sampler = device.CreateSampler();
        }


        public void SetCamera(IDeviceContext context, in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            context.MapResource(_perFrameConstantBuffer, new PerFrameContantBuffer { ViewProjection = Matrix4x4.Transpose(viewProjection), View = view });
            context.SetVertexShaderConstantBuffer(_perFrameConstantBuffer);
        }


        private int count = 1000;
        public void Begin(IDeviceContext context)
        {
            context.SetPrimitiveTopology(PrimitiveTopology.TriangleList);

            if (count-- == 0)
            {
                var s = Stopwatch.StartNew();
                context.MapResource(_lightsConstantBuffer, new LightsConstantBuffer());
                s.Stop();
                Console.WriteLine($"Elapsed time for MapResource buffers: {s.Elapsed.Ticks}");

                var a = Stopwatch.StartNew();
                context.SetPixelShaderConstantBuffer(_lightsConstantBuffer);
                a.Stop();
                Console.WriteLine($"Elapsed time for PX buffers: {a.Elapsed.Ticks}");
            }
            else
            {
                context.MapResource(_lightsConstantBuffer, new LightsConstantBuffer());
                context.SetPixelShaderConstantBuffer(_lightsConstantBuffer);
            }

            
            context.SetPixelShaderSampler(_sampler);
            context.SetInputLayout(_inputLayout);
            context.SetVertexShader(_vertexShader);
            context.SetPixelShader(_pixelShader);
        }

        public void Render(IDeviceContext context, IMesh mesh, in Matrix4x4 worldMatrix, ITexture2D texture)
        {
            context.MapResource(_perObjectConstantBuffer, new PerObjectContantBuffer { World = Matrix4x4.Transpose(worldMatrix) });
            context.SetVertexShaderConstantBuffer(_perObjectConstantBuffer, 1);

            context.SetPixelShaderResource(texture);

            context.SetVertexBuffer(mesh.VertexBuffer);
            context.SetIndexBuffer(mesh.IndexBuffer);

            context.DrawIndexed(mesh.IndexBuffer.NumberOfIndices, 0, 0);
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

        public void End(IDeviceContext context)
        {
            
        }
    }
}
