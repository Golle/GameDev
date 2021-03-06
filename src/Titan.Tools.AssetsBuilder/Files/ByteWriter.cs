using System;
using System.IO;

namespace Titan.Tools.AssetsBuilder.Files
{
    internal class ByteWriter : IByteWriter
    {
        public void Write<T>(Stream stream, in T[] data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* dataPtr = data)
                {
                    stream.Write(new ReadOnlySpan<byte>(dataPtr, sizeof(T) * data.Length));
                }
            }
        }

        public void Write<T>(Stream stream, in T data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* dataPtr = &data)
                {
                    stream.Write(new ReadOnlySpan<byte>(dataPtr, sizeof(T)));
                }
            }
        }
    }
}
