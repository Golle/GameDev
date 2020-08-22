using System;
using System.Runtime.InteropServices;

namespace Titan.Xaudio2.Bindings
{
    [StructLayout(LayoutKind.Explicit, Size = 44)]
    public struct Xaudio2Buffer
    {
        [FieldOffset(0)]
        public uint Flags;
        [FieldOffset(4)]
        public uint AudioBytes;
        [FieldOffset(8)]
        public unsafe byte* AudioData;
        [FieldOffset(16)]
        public uint PlayBegin;
        [FieldOffset(20)]
        public uint PlayLength;
        [FieldOffset(24)]
        public uint LoopBegin;
        [FieldOffset(28)]
        public uint LoopLength;
        [FieldOffset(32)]
        public uint LoopCount;
        [FieldOffset(36)]
        public IntPtr Context;
    }
}
