using System.Numerics;
using Titan.D3D11;
using Titan.D3D11.Compiler;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer.Passes
{
    public class RenderToBackbufferPass : IRenderPass
    {
        private readonly IDepthStencil _depthStencil;
        private readonly IRenderTarget _backBuffer;

        private readonly IIndexBuffer _indexBuffer;
        private readonly IVertexBuffer<BackbufferVertex> _vertexBuffer;
        private readonly IPixelShader _pixelShader;
        private readonly IInputLayout _inputLayout;
        private readonly IVertexShader _vertexShader;
        private readonly ISampler _sampler;

        public RenderToBackbufferPass(IDevice device, ID3DCompiler compiler)
        {
            _backBuffer = device.BackBuffer;
            _depthStencil = device.DepthStencil;

            _indexBuffer = device.CreateIndexBuffer(new short[] {0, 3, 1, 3, 2, 1});
            _vertexBuffer = device.CreateVertexBuffer(CreateCube());

            using var pixelShaderBlob = new Blob(compiler.CompileShaderFromFile(@"F:\Git\GameDev\resources\shaders\BackBufferPixelShader.hlsl", "main", "ps_5_0"));
            _pixelShader = device.CreatePixelShader(pixelShaderBlob);
            using var vertexShaderBlob = new Blob(compiler.CompileShaderFromFile(@"F:\Git\GameDev\resources\shaders\BackBufferVertexShader.hlsl", "main", "vs_5_0"));
            _vertexShader = device.CreateVertexShader(vertexShaderBlob);
            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("POSITION", VertexLayoutTypes.Position2D).Append("TEXCOORD", VertexLayoutTypes.Position2D), vertexShaderBlob);
            _sampler = device.CreateSampler();
        }

        public void Render(IDeviceContext context, ITexture2D meshRenderTexture)
        {
            context.SetRenderTarget(_backBuffer, _depthStencil);
            context.ClearRenderTarget(_backBuffer, Color.Red);
            context.ClearDepthStencil(_depthStencil);
            
            context.SetInputLayout(_inputLayout);
            
            context.SetVertexBuffer(_vertexBuffer);
            context.SetIndexBuffer(_indexBuffer);

            context.SetPixelShader(_pixelShader);
            context.SetVertexShader(_vertexShader);

            
            context.SetPixelShaderResource(meshRenderTexture);

            context.DrawIndexed(6, 0, 0);
        }

        public void Dispose()
        {
            _indexBuffer.Dispose();
            _vertexBuffer.Dispose();
            _inputLayout.Dispose();
            _vertexShader.Dispose();
            _pixelShader.Dispose();
            _sampler.Dispose();
        }

        private static BackbufferVertex[] CreateCube()
        {
            
            return new[]
            {
                new BackbufferVertex{Position = new Vector2(-1,-1), TextureCoordinates = new Vector2(0,1)},
                new BackbufferVertex{Position = new Vector2(1,-1), TextureCoordinates = new Vector2(1,1)},
                new BackbufferVertex{Position = new Vector2(1,1), TextureCoordinates = new Vector2(1,0) },
                new BackbufferVertex{Position = new Vector2(-1,1), TextureCoordinates = new Vector2(0,0) }
            };
        }

        private struct BackbufferVertex
        {
            public Vector2 Position;
            public Vector2 TextureCoordinates;
        }
    }
}
