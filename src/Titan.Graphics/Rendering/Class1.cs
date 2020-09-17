using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Titan.D3D11;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Renderer.Passes;
using Titan.Graphics.Shaders;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Rendering
{

    internal class RenderQueue
    {
        public IList<IRenderPass> _renderPasses = new List<IRenderPass>();

        public RenderQueue(IDevice device)
        {
            var pass1 = new ClearBackBufferPass();
            pass1.Build(device);
            _renderPasses.Add(pass1);


            var pass2 = 


            var pass3 = new FullScreenRenderPass();
            pass3.SetInput(0, );

        }

        public void Render()
        {

        }

    }


    internal interface IRenderPass : IDisposable
    {

        void Begin(IDeviceContext context);
        void End(IDeviceContext context);
        void Render(IDeviceContext context);
        void Build(IDevice device);
        void SetInput(uint index, ITexture2D texture2D);
        void SetOutout(uint index, IRenderTarget renderTarget);
        void SetDepthStencil(IDepthStencil depthStencil);
    }

    internal class ClearBackBufferPass : IRenderPass
    {
        private IRenderTarget _backBuffer;
        private IDepthStencil _depthStencil;

        public void Begin(IDeviceContext context)
        {
        }

        public void End(IDeviceContext context)
        {
        }

        public void Render(IDeviceContext context)
        {
            context.ClearRenderTarget(_backBuffer, Color.Blue);
            context.ClearDepthStencil(_depthStencil);
        }

        public void Build(IDevice device)
        {
            _backBuffer = device.BackBuffer;
            _depthStencil = device.DepthStencil;
        }

        public void SetInput(uint index, ITexture2D texture2D)
        {
        }

        public void SetOutout(uint index, IRenderTarget renderTarget)
        {
        }

        public void SetDepthStencil(IDepthStencil depthStencil)
        {
        }

        public void Dispose()
        {
        }
    }


    internal abstract class RenderPass : IRenderPass
    {
        protected IList<IShaderResourceView> Inputs = new List<IShaderResourceView>(10);
        protected IList<IRenderTarget> Outputs = new List<IRenderTarget>(10);
        protected IDepthStencil DepthStencil;


        public void Begin(IDeviceContext context)
        {
            context.SetVertexShaderResources(Inputs.ToArray());
            context.SetRenderTargets(Outputs.ToArray(), DepthStencil);
        }

        public void End(IDeviceContext context)
        {
            // no idea?
        }

        public abstract void Render(IDeviceContext context);
        public abstract void Build(IDevice device);

        public void SetInput(uint index, ITexture2D texture2D)
        {
            Inputs[(int) index] = texture2D;
        }

        public void SetOutout(uint index, IRenderTarget renderTarget)
        {
            Outputs[(int)index] = renderTarget;
        }

        public void SetDepthStencil(IDepthStencil depthStencil)
        {
            DepthStencil = depthStencil;
        }

        public virtual void Dispose()
        {
            // who is the owner?
        }
    }

    internal class FullScreenRenderPass : RenderPass
    {
        private IIndexBuffer _indexBuffer;
        private IVertexBuffer<FullScreenVertex> _vertexBuffer;
        private IInputLayout _inputLayout;
        private ISampler _sampler;
        private IPixelShader _pixelShader;
        private IVertexShader _vertexShader;

        public override void Build(IDevice device)
        {
            _indexBuffer = device.CreateIndexBuffer(new short[] { 0, 3, 1, 3, 2, 1 });
            _vertexBuffer = device.CreateVertexBuffer(CreateCube());

            using var pixelShaderBlob = new Blob(device.TEMPORARYCompiler.CompileShaderFromFile(@"F:\Git\GameDev\resources\shaders\BackBufferPixelShader.hlsl", "main", "ps_5_0"));
            _pixelShader = device.CreatePixelShader(pixelShaderBlob);
            using var vertexShaderBlob = new Blob(device.TEMPORARYCompiler.CompileShaderFromFile(@"F:\Git\GameDev\resources\shaders\BackBufferVertexShader.hlsl", "main", "vs_5_0"));
            _vertexShader = device.CreateVertexShader(vertexShaderBlob);

            _inputLayout = device.CreateInputLayout(new VertexLayout(2).Append("POSITION", VertexLayoutTypes.Position2D).Append("TEXCOORD", VertexLayoutTypes.Position2D), vertexShaderBlob);
            _sampler = device.CreateSampler();

        }

        public override void Render(IDeviceContext context)
        {
            context.SetVertexBuffer(_vertexBuffer);
            context.SetIndexBuffer(_indexBuffer);
            context.SetVertexShader(_vertexShader);
            context.SetPixelShader(_pixelShader);
            context.SetInputLayout(_inputLayout);
            context.SetPixelShaderSampler(_sampler);


            context.DrawIndexed(6, 0, 0);
        }

        private static FullScreenVertex[] CreateCube()
        {

            return new[]
            {
                new FullScreenVertex{Position = new Vector2(-1,-1)/2, TextureCoordinates = new Vector2(0,1)},
                new FullScreenVertex{Position = new Vector2(1,-1)/2, TextureCoordinates = new Vector2(1,1)},
                new FullScreenVertex{Position = new Vector2(1,1)/2, TextureCoordinates = new Vector2(1,0) },
                new FullScreenVertex{Position = new Vector2(-1,1)/2, TextureCoordinates = new Vector2(0,0) }
            };
        }

        private struct FullScreenVertex
        {
            public Vector2 Position;
            public Vector2 TextureCoordinates;
        }

        public override void Dispose()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            base.Dispose();
        }
    }
}
