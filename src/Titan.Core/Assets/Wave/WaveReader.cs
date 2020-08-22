using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Titan.Core.Assets.Wave
{
    internal class WaveReader : IWaveReader
    {
        public WaveData ReadFromStream(Stream stream)
        {
            var data = IntPtr.Zero;
            var dataSize = 0u;
            WaveformatEx format = default;
            try
            {
                while (true)
                {
                    if (ReadChunk<WaveChunkTypes>(stream, out var chunkType) == 0) break;
                    if (ReadChunk<uint>(stream, out var chunkSize) == 0) break;
                    switch (chunkType)
                    {
                        case WaveChunkTypes.RIFF:
                            if (ReadChunk<WaveFileTypes>(stream, out var fileType) == 0) throw new InvalidOperationException("Failed to read file type");
                            if (fileType != WaveFileTypes.WAVE && fileType != WaveFileTypes.XWMA) throw new InvalidOperationException($"File type is not supported. {fileType}");
                            break;
                        case WaveChunkTypes.DATA:
                            if ((dataSize = ReadBytes(stream, out data, chunkSize)) != chunkSize) throw new InvalidDataException($"The DATA size and read size is a mismatch. Expected: {chunkSize}  Read: {dataSize}");
                            break;
                        case WaveChunkTypes.FMT:
                            if (ReadChunk(stream, out format, chunkSize) != chunkSize) throw new InvalidDataException($"The FMT size and read size is a mismatch.");
                            break;
                        default:
                            // If its a format we don't care about, just skip it and move to the next
                            stream.Seek(chunkSize, SeekOrigin.Current);
                            break;
                    }
                }
            }
            catch
            {
                if (data != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(data);
                }
                throw;
            }
            
            return new WaveData(data, dataSize, format);
        }

        private static uint ReadBytes(Stream stream, out IntPtr data, uint size)
        {
            data = Marshal.AllocHGlobal((int)size);
            unsafe
            {
                return (uint)stream.Read(new Span<byte>(data.ToPointer(), (int)size));
            }
        }

        private static int ReadChunk<T>(Stream stream, out T data, uint size = 0) where T : unmanaged
        {
            unsafe
            {
                fixed (void * a = &data)
                {
                    return stream.Read(new Span<byte>(a, size == 0 ? sizeof(T) : (int)size));
                }
            }
        }
    }
}
