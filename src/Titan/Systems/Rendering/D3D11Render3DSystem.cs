using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Components;
using Titan.Core.Math;
using Titan.D3D11;
using Titan.ECS.Components;
using Titan.ECS.Systems;
using Titan.Graphics;
using Titan.Graphics.Buffers;
using Titan.Graphics.Renderer;
using Titan.Windows.Window;

namespace Titan.Systems.Rendering
{
    internal class D3D11Camera3DSystem : BaseSystem
    {
        private readonly IComponentMapper<Transform3D> _transform;
        private readonly IComponentMapper<Camera> _camera;
        private readonly IConstantBuffer<CameraBuffer> _buffer;

        private CameraBuffer _cameraBuffer;
        public D3D11Camera3DSystem(IComponentManager componentManager, IDevice device, IWindow window) 
            : base(typeof(Transform3D), typeof(Camera))
        {
            _transform = componentManager.GetComponentMapper<Transform3D>();
            _camera = componentManager.GetComponentMapper<Camera>();

            _buffer = device.CreateConstantBuffer<CameraBuffer>();

            var projection = MatrixExtensions.CreatePerspectiveLH(1f, window.Height / (float)window.Width, 0.5f, 10f);
            var view = Matrix4x4.Identity;
            //var viewProjection = Matrix4x4.Transpose(
            //    Matrix4x4.CreateTranslation(0, 0f, 0.5f + 2f) *
            //    projection
            //);

            var viewProjection = Matrix4x4.Transpose(
                Matrix4x4.CreateRotationZ(0.2f) *
                Matrix4x4.CreateRotationX(0.5f) *
                Matrix4x4.CreateTranslation(0, 0f, 0.5f + 2f) *
                projection
            );

            _cameraBuffer.ViewProjection = viewProjection;

        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var camera = ref _camera[entity];
                ref var transform = ref _transform[entity];
                
                _buffer.Update(_cameraBuffer);
                _buffer.BindToVertexShader();
                
                // only use the first camera
                break;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct CameraBuffer
        {
            public Matrix4x4 ViewProjection;
        }
    }


    internal class D3D11Render3DSystem : BaseSystem
    {
        private readonly IDevice _device;
        private readonly IComponentMapper<D3D11Model> _model;
        private readonly IComponentMapper<D3D11Shader> _shader;

        private IComponentMapper<Transform3D> _transform;

        public D3D11Render3DSystem(IComponentManager componentManager, IDevice device) : 
            base(typeof(D3D11Model), typeof(Transform3D))
        {
            _device = device;
            _model = componentManager.GetComponentMapper<D3D11Model>();
            _shader = componentManager.GetComponentMapper<D3D11Shader>();
            _transform = componentManager.GetComponentMapper<Transform3D>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            _device.BeginRender();

            foreach (var entity in Entities)
            {
                ref var shader = ref _shader[entity];
                shader.VertexShader.Bind();
                shader.PixelShader.Bind();
                shader.InputLayout.Bind();

                ref var model = ref _model[entity];
                model.VertexBuffer.Bind();
                model.IndexBuffer.Bind();

                _device.DrawIndexed(model.IndexBuffer.NumberOfIndices, 0, 0);
            }
            _device.EndRender();
        }
    }

    internal class UIRenderSystem : BaseSystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;
        private readonly IComponentMapper<TransformRect> _transform;

        public UIRenderSystem(IComponentManager componentManager, ISpriteBatchRenderer spriteBatchRenderer) 
            : base(typeof(TransformRect))
        {
            _spriteBatchRenderer = spriteBatchRenderer;
            _transform = componentManager.GetComponentMapper<TransformRect>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                ref var rect = ref _transform[entity];
                _spriteBatchRenderer.Push(null, new Vector2(rect.Position.X, rect.Position.Y), new Vector2(rect.Size.Width, rect.Size.Height), new Color(1,0,0));
            }
        }
    }


    internal class SpriteRenderSystem : BaseSystem
    {
        private readonly ISpriteBatchRenderer _spriteBatchRenderer;

        public SpriteRenderSystem(ISpriteBatchRenderer spriteBatchRenderer) :
            base(typeof(Transform3D))
        {
            _spriteBatchRenderer = spriteBatchRenderer;

        }

        protected override void OnUpdate(float deltaTime)
        {
            _spriteBatchRenderer.Push(null, new Vector2(100.0f, 100.0f), new Vector2(200, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(-200.0f, -200.0f), new Vector2(100, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200.0f, -300.0f), new Vector2(100, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(-200.0f, 200.0f), new Vector2(100, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200.0f, 200.0f), new Vector2(100, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(1000.0f, 0.0f), new Vector2(200, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(0.0f, -700.0f), new Vector2(200, 200), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200, 100), new Vector2(200, 300), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200, 200), new Vector2(200, 300), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200, 300), new Vector2(200, 300), new Color(0, 1, 0));
            _spriteBatchRenderer.Push(null, new Vector2(200, 400), new Vector2(200, 300), new Color(0, 1, 0));
        }
    }
}
