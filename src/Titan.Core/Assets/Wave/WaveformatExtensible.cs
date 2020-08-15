using System;
using System.Runtime.InteropServices;

namespace Titan.Core.Assets.Wave
{
    [StructLayout(LayoutKind.Explicit)]
    public struct WaveformatExtensible
    {
        [FieldOffset(0)]
        public WaveformatEx Format;
        [FieldOffset(144)]
        public ushort wValidBitsPerSample;       /* bits of precision  */
        [FieldOffset(144)]
        public ushort wSamplesPerBlock;          /* valid if wBitsPerSample==0 */
        [FieldOffset(144)]
        public ushort wReserved;                 /* If neither applies, set to zero. */
        [FieldOffset(160)]
        public uint dwChannelMask;      /* which channels are */
        /* present in stream  */
        [FieldOffset(192)]
        public Guid SubFormat;
    }
}
