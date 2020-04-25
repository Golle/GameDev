using System;
using Titan.D3D11;
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

            while (true)
            {
                Message message = default;
                if (window.GetMessage(ref message))
                {
                    Console.WriteLine($"Message {message.Value}");
                }
            }
        }
    }
}
