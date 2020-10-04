using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using Titan.Core.Logging;

namespace Titan.Core.Assets.Images
{

    public class ImageAsset2 : IDisposable
    {
        public uint Height { get; }
        public uint Width { get; }
        public IntPtr Pixels { get; }
        public uint Size { get; }
        public ImageAsset2(uint width, uint height, IntPtr pixels, uint size)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
            Size = size;
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(Pixels);
        }
    }

    internal class ImageLoader : IImageLoader
    {
        //private readonly ILogger _logger;
        //public ImageLoader(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public unsafe ImageAsset2 LoadFromFileUnsafe(string filename)
        {
            var s = Stopwatch.StartNew();
            try
            {
                using var image = Image.FromFile(filename);
                
                using var bitmap = new Bitmap(image);
                
                var size = bitmap.Width * bitmap.Height * sizeof(float);
                var mem = Marshal.AllocHGlobal(size);
                var colorPointer = (byte*) mem.ToPointer();
                using var stream = new UnmanagedMemoryStream(colorPointer, size);
                bitmap.Save(stream, image.RawFormat);
                //s.Stop();
                //Console.WriteLine($"||||||||||ELAasdPSED: {s.Elapsed.TotalMilliseconds} ms " + size );
                //for (var y = bitmap.Height - 1; y >= 0; --y)
                //{
                //    for (var x = 0; x < bitmap.Width; ++x)
                //    {
                //        var pixel = bitmap.GetPixel(x, y);
                        
                //        colorPointer[0] = pixel.R;
                //        colorPointer[1] = pixel.G;
                //        colorPointer[2] = pixel.B;
                //        colorPointer[3] = pixel.A;
                //    }
                //}

                return new ImageAsset2((uint) image.Height, (uint) image.Width, mem, (uint) size);
            }
            catch (IOException)
            {
                //_logger.Error($"Failed to load image from {filename}");
                throw;
            }
            finally
            {
                s.Stop();
                Console.WriteLine($"||||||||||ELAPSED: {s.Elapsed.TotalMilliseconds} ms");
            }

        }

        public ImageAsset LoadFromFile(string filename)
        {
            var s = Stopwatch.StartNew();
            try
            {
                using var image = Image.FromFile(filename);
                using var bitmap = new Bitmap(image);

                var bytes = new List<byte>(bitmap.Width * bitmap.Height * 4);
                for (var y = bitmap.Height - 1; y >= 0; --y)
                {
                    for(var x = 0; x < bitmap.Width; ++x)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        bytes.Add(pixel.R);
                        bytes.Add(pixel.G);
                        bytes.Add(pixel.B);
                        bytes.Add(pixel.A);
                    }
                }
                
                return new ImageAsset((uint) image.Height, (uint) image.Width, bytes.ToArray());
            }
            catch (IOException)
            {
                //_logger.Error($"Failed to load image from {filename}");
                throw;
            }
            finally
            {
                s.Stop();
                Console.WriteLine($"||||||||||ELAPSED: {s.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}
