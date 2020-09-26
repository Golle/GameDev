using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.IOC;
using Titan.Tools.AssetsBuilder.Converters;
using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder
{
    
    
    internal class Program
    {
        private static readonly IContainer Container = Bootstrapper.CreateContainer();
        static unsafe void Main(string[] args)
        {
            var filename = @"F:\Git\GameDev\resources\data\test.dat";

            var result = Container
                .GetInstance<IModelConverter>()
                .ConvertFromObj(@"F:\Git\GameDev\resources\sponza\sponza.obj");
            
            
            Container.GetInstance<IModelExporter>()
                .Write(filename, result, true);


            // Write the file
            {

                
                var vertices = GenerateVertices().ToArray();
                var indices = new ushort[] {1, 2, 3, 4, 5, 6, 7, 8, 10};


            }
            // Read the file
            {
                //using var file = File.OpenRead(filename);
                //byteReader.Read<Header>(file, out var header);
                //var vertices = new Vertex[header.VertexCount];
                //byteReader.Read(file, ref vertices);
                ////var indices = new ushort[header.IndexCount];
                ////byteReader.Read(file, ref indices);
                //var handle = Marshal.AllocHGlobal(header.IndexCount * header.IndexSize);
                //try
                //{
                //    byteReader.Read<ushort>(file, handle.ToPointer(), header.IndexCount);
                //}
                //finally
                //{
                //    Marshal.FreeHGlobal(handle);
                //}
                
            }

            Console.WriteLine("Hello World!" + Environment.Version);
        }

        private static IEnumerable<Vertex> GenerateVertices()
        {
            for (var i = 0; i < 1; ++i)
            {
                yield return new Vertex { Normal = new Vector3(1, 2, 3), Position = new Vector3(3, 2, 1), Texture = new Vector2(4, 5) };
                yield return new Vertex { Normal = new Vector3(10, 20, 30), Position = new Vector3(30, 20, 10), Texture = new Vector2(40, 50) };
                yield return new Vertex { Normal = new Vector3(100, 200, 300), Position = new Vector3(300, 200, 100), Texture = new Vector2(400, 500) };
            }
        }
    }
}
