using System.IO;

namespace Titan.Tools.AssetsBuilder.Files
{
    internal interface IByteWriter
    {
        void Write<T>(Stream stream, in T[] data) where T : unmanaged;
        void Write<T>(Stream stream, in T data) where T : unmanaged;
    }
}
