using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Titan.Tools.AssetsBuilder.Files
{
    internal class ByteReader : IByteReader
    {
        public void Read<T>(Stream stream, out T value) where T : unmanaged
        {
            unsafe
            {
                fixed (void* dataPtr = &value)
                {
                    stream.Read(new Span<byte>(dataPtr, sizeof(T)));
                }
            }
        }

        public void Read<T>(Stream stream, ref T[] values) where T : unmanaged
        {
            unsafe
            {
                fixed (void* dataPtr = values)
                {
                    Read<T>(stream, dataPtr, values.Length);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Read<T>(Stream stream, void* output, int count) where T : unmanaged
        {
            stream.Read(new Span<byte>(output, sizeof(T) * count));
        }
    }
}
