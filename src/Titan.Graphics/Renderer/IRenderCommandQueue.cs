using System;
using System.Collections.Generic;
using Titan.Graphics.Buffers;

namespace Titan.Graphics.Renderer
{

    // POC for render queue
    internal class RenderQueue<T> where T : struct, IJob
    {

        private IList<T> _jobs = new List<T>();

        public void Submit(in T job)
        {
            _jobs.Add(job);
        }

        public void Execute()
        {
            foreach (var job in _jobs)
            {
                job.Execute();
            }
        }
    }

    public readonly struct SpriteBatchJob : IJob
    {
        private readonly IIndexBuffer _indexBuffer;
        private readonly IVertexBuffer _vertexBuffer;

        public SpriteBatchJob(IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer)
        {
            _vertexBuffer = vertexBuffer;
            _indexBuffer = indexBuffer;
        }
        public void Execute()
        {
            throw new NotImplementedException();
            //_vertexBuffer.Bind();
            //_indexBuffer.Bind();
        }
    }


    class Test
    {
        public Test()
        {
            var queue = new RenderQueue<SpriteBatchJob>();
            

            queue.Submit(new SpriteBatchJob(null, null));
        }
    }

    internal interface IJob
    {

        void Execute();
    }

    internal interface IRenderCommandQueue
    {

        void Push(in RenderCommand renderCommand);

        void Push(in RenderCommandFn func);
    }

    public class RenderCommand
    {
        public unsafe IntPtr * Vertexbuffers;
        public uint NumberOfVertexBuffers;
        public IntPtr IndexBuffer;
        public unsafe IntPtr* ConstantBuffers;
        public uint NumberOfConstantBuffers;
    }

    internal delegate void RenderCommandFn();

    
}
