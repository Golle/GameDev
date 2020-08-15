using System.Runtime.InteropServices;

namespace Titan.Xaudio2.Bindings
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Xaudio2Buffer
    {
        public uint Flags;
        public uint AudioBytes;
        public unsafe byte* AudioData;
        public uint PlayBegin;
        public uint PlayLength;
        public uint LoopBegin;
        public uint LoopLength;
        public uint LoopCount;
        public unsafe void* Context;
    }
}
