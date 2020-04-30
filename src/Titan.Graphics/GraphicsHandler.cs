using System;
using Titan.D3D11;
using Titan.D3D11.Device;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Graphics
{
    public class GraphicsHandler : IDisposable
    {
        private readonly IWindowCreator _windowCreator;
        private readonly ID3D11DeviceFactory _d3D11DeviceFactory;
        
        private IWindow _window;
        private ID3D11Device _device;
        private ID3D11RenderTargetView _renderTarget;

        public GraphicsHandler(IWindowCreator windowCreator, ID3D11DeviceFactory d3D11DeviceFactory)
        {
            _windowCreator = windowCreator;
            _d3D11DeviceFactory = d3D11DeviceFactory;
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
            _window.ShowWindow();
            while (_window.Update())
            {
                var color = new Color { Alpha = 1f };
                if (_window.Keyboard.IsKeyDown(KeyCode.A))
                {
                    color.Red = 1f;
                }
                if (_window.Keyboard.IsKeyDown(KeyCode.S))
                {
                    color.Green = 1f;
                }
                if (_window.Keyboard.IsKeyDown(KeyCode.D))
                {
                    color.Blue = 1f;
                }
                while (_window.Keyboard.TryGetChar(out var character))
                {
                    Console.Write(character);
                }

                var mouse = _window.Mouse;
                var mousePosition = mouse.Position;
                
                _window.SetTitle($"x: {mousePosition.X} y: {mousePosition.Y}, left: {mouse.LeftButtonDown}, right: {mouse.RightButtonDown}");

                _device.Context.ClearRenderTargetView(_renderTarget, color);
                _device.SwapChain.Present();
            }
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _device.Dispose();
        }
    }
}
