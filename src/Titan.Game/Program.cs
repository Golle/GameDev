using System;
using Titan.D3D11;
using Titan.D3D11.Bindings;
using Titan.D3D11.Device;
using Titan.Windows.Window;

namespace Titan.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World from DirectX 11 SDK version {D3D11CommonBindings.D3D11SdkVersion()}");

            var window = new WindowCreator()
                .CreateWindow(new CreateWindowArguments
                {
                    Title = "My first .Net 5 window",
                    Height = 600,
                    Width = 800,
                    Y = 300,
                    X = 300
                });
            
            window.ShowWindow();

            var device = new D3D11DeviceFactory()
                .Create(new CreateDeviceArguments
                {
                    Adapter = IntPtr.Zero,
                    RefreshRate = 144,
                    Window = window,
                    Debug = true
                });

            using var backBuffer = device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Texture2D);
            using var renderTargetsView = device.CreateRenderTargetView(backBuffer);

            device.Context.SetRenderTargets(renderTargetsView);
            using var infoQueue = device.CreateInfoQueue();
            var red = new Color
            {
                Red = 1f,
                Alpha = 1f
            };
            
            Message message = default;
            while (true)
            {
                if (window.GetMessage(ref message))
                {
                    Console.WriteLine($"Message {message.Value}");
                }
                device.Context.ClearRenderTargetView(renderTargetsView, red);
                if (red.Blue > 1f)
                    red.Blue = 0f;
                if (red.Green > 1f)
                    red.Green = 0f;
                device.SwapChain.Present();
            }
        }
    }
}
