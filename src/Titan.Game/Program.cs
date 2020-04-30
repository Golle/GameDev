using System;
using Titan.Core.EventSystem;
using Titan.D3D11.Bindings;
using Titan.D3D11.Device;
using Titan.Graphics;
using Titan.Windows.Input;
using Titan.Windows.Window;

namespace Titan.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World from DirectX 11 SDK version {D3D11CommonBindings.D3D11SdkVersion()}");
            var eventManager = new EventManager();
            using var graphicsHandler = new GraphicsHandler(new WindowCreator(eventManager), new D3D11DeviceFactory(), eventManager, new InputManager(eventManager));

            if (graphicsHandler.Initialize("This is a game", 800, 600))
            {
                Console.WriteLine("Init success!");
                graphicsHandler.Run();

            }
        }
    }
}
