using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Device;
using Titan.Graphics;
using Titan.Windows.Window;

namespace Titan.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World from DirectX 11 SDK version {D3D11CommonBindings.D3D11SdkVersion()}");
            using var graphicsHandler = new GraphicsHandler(new WindowCreator(), new D3D11DeviceFactory());
            if (graphicsHandler.Initialize("This is a game", 800, 600))
            {
                Console.WriteLine("Init success!");
                graphicsHandler.Run();

            }
        }
    }
}
