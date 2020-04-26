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
            Console.WriteLine($"Hello World from DirectX 11 SDK version {SdkVersion.GetVersion()}");

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
                    Window = window
                });

            using var backBuffer = device.SwapChain.GetBuffer(0, D3D11Resources.D3D11Texture2D);
            using var renderTargetsView = device.CreateRenderTargetView(backBuffer);






            Message message = default;
            while (true)
            {
                if (window.GetMessage(ref message))
                {
                    Console.WriteLine($"Message {message.Value}");

                }
            }
        }
    }
}
