using System.Numerics;
using Titan.D3D11.Compiler;
using Titan.Graphics.Models;
using Titan.Graphics.Renderer.Passes;
using Titan.Graphics.RendererOld;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Renderer
{
    public interface IRenderQueue
    {
        // TODO: this will copy the world transform on each frame, need to rethink this.
        void Submit(IMesh mesh, ITexture2D texture2D, in Matrix4x4 worldTransform);
        void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view);
        void Execute();
    }

    internal class RenderQueue : IRenderQueue
    {
        private RenderJob[] _renderJobs = new RenderJob[10000];
        private uint _numberOfRenderJobs;
        
        private RenderMeshPass _meshPass;
        private IDevice _device;

        public RenderQueue(IDevice device, ID3DCompiler compiler)
        {
            _device = device;
            
            _meshPass = new RenderMeshPass(device, compiler);
        }

        
        public void Submit(IMesh mesh, ITexture2D texture2D, in Matrix4x4 worldTransform)
        {
            ref var renderJob = ref _renderJobs[_numberOfRenderJobs++];
            renderJob.Mesh = mesh;
            renderJob.Texture = texture2D;
            renderJob.WorldTransform = worldTransform;
        }

        public void SetCamera(in Matrix4x4 viewProjection, in Matrix4x4 view)
        {
            _meshPass.SetCamera(_device.ImmediateContext, viewProjection, view);
        }

        public void Execute()
        {

            _meshPass.Begin(_device.ImmediateContext);
            for (var i = 0; i < _numberOfRenderJobs; ++i)
            {
                ref var job = ref _renderJobs[i];
                _meshPass.Render(_device.ImmediateContext, job.Mesh, job.WorldTransform, job.Texture);
            }

            _numberOfRenderJobs = 0;
        }


        private struct RenderJob
        {
            public IMesh Mesh;
            public ITexture2D Texture;
            public Matrix4x4 WorldTransform;
        }
    }
}
