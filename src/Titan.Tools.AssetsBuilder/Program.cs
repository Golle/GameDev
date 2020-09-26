using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.IOC;
using Titan.Tools.AssetsBuilder.Logging;

namespace Titan.Tools.AssetsBuilder
{
    [StructLayout(LayoutKind.Sequential, Size = 256)]
    internal struct TehHeader
    {
        public ushort Version;
        public int VertexSize;
        public int VertexCount;
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 Texture;
    }
    
    internal class Program
    {

        private static IContainer _container = Bootstrapper.CreateContainer();
        static unsafe void Main(string[] args)
        {

            var logger = _container.GetInstance<ILogger>();
            
            logger.WriteLine("HEHEHE");

            var filename = @"F:\Git\GameDev\resources\data\test.dat";

            // Write the file
            {
                
                var vertices = GenerateVertices().ToArray();
                using var file = File.OpenWrite(filename);
                var header = new TehHeader
                {
                    Version = 1,
                    VertexSize = sizeof(Vertex),
                    VertexCount = vertices.Length,
                };

                file.Write(new ReadOnlySpan<byte>(&header, sizeof(TehHeader)));
                
                fixed (void* pointer = vertices)
                {
                    file.Write(new ReadOnlySpan<byte>(pointer, header.VertexCount * header.VertexSize));
                }
            }
            // Read the file
            {
                var timer = Stopwatch.StartNew();
                using var file = File.OpenRead(filename);
                var header = new TehHeader();
                file.Read(new Span<byte>(&header, sizeof(TehHeader)));
                var vertices = new Vertex[header.VertexCount];
                fixed (void* pointer = vertices)
                {
                    file.Read(new Span<byte>(pointer, header.VertexCount * header.VertexSize));
                }
                
                timer.Stop();
                Console.WriteLine($"Finished reading data in: {timer.Elapsed.Milliseconds} ms");
                Console.WriteLine($"Header.Version {header.Version} VertexCount: {header.VertexCount} VertexSize: {header.VertexSize}");
                //foreach (var vertex in vertices)
                //{
                //    Console.WriteLine($"Vertex: {vertex.Position} {vertex.Normal} {vertex.Texture}");
                //}
            }

            Console.WriteLine("Hello World!" + Environment.Version);
        }

        private static IEnumerable<Vertex> GenerateVertices()
        {
            for (var i = 0; i < 1_000_000; ++i)
            {
                yield return new Vertex { Normal = new Vector3(1, 2, 3), Position = new Vector3(3, 2, 1), Texture = new Vector2(4, 5) };
                yield return new Vertex { Normal = new Vector3(10, 20, 30), Position = new Vector3(30, 20, 10), Texture = new Vector2(40, 50) };
                yield return new Vertex { Normal = new Vector3(100, 200, 300), Position = new Vector3(300, 200, 100), Texture = new Vector2(400, 500) };
            }
        }
    }
}
