using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.EventSystem;
using Titan.Core.Logging;
using Titan.D3D11;
using Titan.D3D11.Bindings.Models;
using Titan.Graphics;
using Titan.Graphics.Blobs;
using Titan.Graphics.Buffers;
using Titan.Graphics.Layout;
using Titan.Graphics.Shaders;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan
{
    internal class GameEngine
    {
        private readonly IWindow _window;
        private readonly IDevice _device;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;
        private readonly IInputManager _inputManager;
        private readonly IBlobReader _blobReader;

        private IVertexBuffer<MyVertex> _vertexBuffer;
        private IIndexBuffer _indexBuffer;
        private IVertexShader _vertexShader;
        private IPixelShader _pixelShader;
        private IInputLayout _inputLayout;

        public GameEngine(IWindow window, IDevice device, IEventManager eventManager, ILogger logger, IInputManager inputManager, IBlobReader blobReader)
        {
            _window = window;
            _device = device;
            _eventManager = eventManager;
            _logger = logger;
            _inputManager = inputManager;
            _blobReader = blobReader;


            Setup();
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MyVertex
        {
            public float X;
            public float Y;
            public float Z;
            public Color Color;
        }
        private void Setup()
        {
            //var vertices = new MyVertex[]
            //{
            //    new MyVertex {Color = new Color(1f, 0, 0),  X = -1f, Y = -1f, Z = -1f},
            //    new MyVertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = -1f},
            //    new MyVertex {Color = new Color(1f, 0, 1f), X = -1f, Y = 1f, Z = -1f},
            //    new MyVertex {Color = new Color(0f, 1f, 0), X = 1f, Y = 1f, Z = -1f},
            //    new MyVertex {Color = new Color(0f, 1f, 1f),X = -1f, Y = -1f, Z = 1f},
            //    new MyVertex {Color = new Color(1f, 1f, 0), X = 1f, Y = -1f, Z = 1f},
            //    new MyVertex {Color = new Color(1f, 1f, 1f),X = -1f, Y = 1f, Z = 1f},
            //    new MyVertex {Color = new Color(1f, 0, 0),  X = 1f, Y = 1f, Z = 1f},
            //};
            //
            var vertices = new MyVertex[]
            {
                new MyVertex {Color = new Color(1f, 0, 0),  X = 0f, Y = 0.5f, Z = 0f},
                new MyVertex {Color = new Color(1f, 1f, 0), X = 0.5f, Y = -0.5f, Z = 0f},
                new MyVertex {Color = new Color(1f, 0, 1f), X = -0.5f, Y = -0.5f, Z = 0f},
            };
            _vertexBuffer = _device.CreateVertexBuffer(vertices);

            var indices = new short[]
            {
                0, 1, 2
            };

            _indexBuffer = _device.CreateIndexBuffer(indices);

            using var vertexBlob = _blobReader.ReadFromFile("Shaders/VertexShader.cso");
            _vertexShader = _device.CreateVertexShader(vertexBlob);

            using var pixelShaderBlob = _blobReader.ReadFromFile("Shaders/PixelShader.cso");
            _pixelShader = _device.CreatePixelShader(pixelShaderBlob);

            var layout = new VertexLayout(2)
                .Append("POSITION", VertexLayoutTypes.Position3D)
                .Append("COLOR", VertexLayoutTypes.Float4Color);

            _inputLayout = _device.CreateInputLayout(layout, vertexBlob);
        }

        public bool Execute()
        {
            // temporary for testing
            var mousePosition = _inputManager.Mouse.Position;
            _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}");


            if (!_window.Update())
            {
                _logger.Debug("Window kill commands received, exiting Engine.");
                return false;
            }
            _eventManager.Update();

            // Update physics?

            // Execute queued sounds

            
            _device.BeginRender();




            // Draw 3D World
            // Draw the UI/Hud

            
            _vertexBuffer.Bind();
            _indexBuffer.Bind();
            _vertexShader.Bind();
            _pixelShader.Bind();
            _inputLayout.Bind();

            _device.DrawIndexed(_indexBuffer.NumberOfIndices, 0, 0);

            _device.EndRender();

            return true;
        }
    }
}
