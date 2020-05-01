using System;
using System.Diagnostics;
using Titan.Core.EventSystem;
using Titan.Core.GameLoop.Events;
using Titan.D3D11;
using Titan.D3D11.Device;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Graphics
{
    public class GraphicsHandler : IGraphicsHandler
    {
        private readonly IWindowCreator _windowCreator;
        private readonly ID3D11DeviceFactory _d3D11DeviceFactory;
        private readonly IEventManager _eventManager;
        private readonly IInputManager _inputManager;

        private IWindow _window;
        private ID3D11Device _device;
        private ID3D11RenderTargetView _renderTarget;

        public GraphicsHandler(IWindowCreator windowCreator, ID3D11DeviceFactory d3D11DeviceFactory, IEventManager eventManager, IInputManager inputManager)
        {
            _windowCreator = windowCreator;
            _d3D11DeviceFactory = d3D11DeviceFactory;
            _eventManager = eventManager;
            _inputManager = inputManager;
        }

        public bool Initialize(string title, int width, int height)
        {
            _window = _windowCreator.CreateWindow(new CreateWindowArguments
            {
                Height = height, 
                Title = title, 
                Width = width
            });
            _device = _d3D11DeviceFactory
                .Create(new CreateDeviceArguments
                {
                    Adapter = IntPtr.Zero,
                    RefreshRate = 144, 
                    Debug = true,
                    Window = _window
                });

            using (var backBuffer = _device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Texture2D))
            {
                _renderTarget = _device.CreateRenderTargetView(backBuffer);
            }
            
            _device.Context.SetRenderTargets(_renderTarget);
            return true;
        }

        public void Run()
        {
            var lastTicks = Stopwatch.GetTimestamp();
            _window.ShowWindow();
            var frames = 0;
            var lastFps = 0;
            var fpsTimer = 0f;
            while (_window.Update())
            {
                var ticks = Stopwatch.GetTimestamp();
                var elapsedTicks = ticks - lastTicks;
                fpsTimer += elapsedTicks / (float)Stopwatch.Frequency;

                if (fpsTimer > 1f)
                {
                    lastFps = (int)(frames / fpsTimer);
                    fpsTimer = 0f;
                    frames = 0;
                }

                _eventManager.PublishImmediate(new UpdateEvent(elapsedTicks));

                _eventManager.Update();

                var color = new Color { Alpha = 1f };
                if (_inputManager.Keyboard.IsKeyDown(KeyCode.A))
                {
                    color.Red = 1f;
                }
                if (_inputManager.Keyboard.IsKeyDown(KeyCode.S))
                {
                    color.Green = 1f;
                }
                if (_inputManager.Keyboard.IsKeyDown(KeyCode.D))
                {
                    color.Blue = 1f;
                }
                while (_inputManager.Keyboard.TryGetChar(out var character))
                {
                    //Console.Write(character);
                }

                var mouse = _inputManager.Mouse;
                var mousePosition = mouse.Position;
                
                _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}, left: {mouse.LeftButtonDown}, right: {mouse.RightButtonDown}, fps: {lastFps}");

                _device.Context.ClearRenderTargetView(_renderTarget, color);
                _device.SwapChain.Present();
                
                lastTicks = ticks;
                
                frames++;
            }
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _device.Dispose();
        }
    }
}
