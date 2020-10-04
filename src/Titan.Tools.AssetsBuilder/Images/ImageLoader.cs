using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Titan.Tools.AssetsBuilder.Data;

namespace Titan.Tools.AssetsBuilder.Images
{
    internal class ImageLoader
    {
        public Pixel[] LoadImage(in string filename)
        {

            var s = Stopwatch.StartNew();
            using var image = Image.FromFile(filename);
            using var bitmap = new Bitmap(image);
            //bitmap.
            
            var byteCount = bitmap.Width * bitmap.Height * 4;
            var mem = Marshal.AllocHGlobal(byteCount);

            unsafe
            {
                var stream = new UnmanagedMemoryStream((byte*)mem.ToPointer(), byteCount);
                image.Save(stream, image.RawFormat);
            }
            
            
            s.Stop();
            Console.WriteLine($"Loaded '{Path.GetFileName(filename)}' in {s.Elapsed.TotalMilliseconds} ms");
            s.Restart();
            var pixels = new Pixel[bitmap.Width * bitmap.Height];
            var count = 0;
            for (var y = bitmap.Height - 1; y >= 0; --y) // invert the Y order of pixels since its a bitmap
            {
                for (var x = 0; x < bitmap.Width; ++x)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    pixels[count++] = new Pixel
                    {
                        Red = pixel.R,
                        Green = pixel.G,
                        Blue = pixel.B,
                        Alpha = pixel.A,
                    };
                }
            }
            s.Stop();
            Console.WriteLine($"Copied pixels in {s.Elapsed.TotalMilliseconds} ms");
            return pixels;
        }
    }
}
