using System.IO;

namespace Titan.Core.Common
{
    public interface IByteReader
    {
        void Read<T>(Stream stream, out T value) where T : unmanaged;
        void Read<T>(Stream stream, ref T[] values) where T : unmanaged;
        unsafe void Read<T>(Stream stream, void* output, int count) where T : unmanaged;
    }
}