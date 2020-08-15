using System;

namespace Titan.Core.Assets.Wave
{
    [Flags]
    internal enum WaveChunkTypes : uint
    {
        Invalid = 0,
        RIFF = ('R') | ('I' << 8) | ('F' << 16) | ('F' << 24),
        DATA = ('d') | ('a' << 8) | ('t' << 16) | ('a' << 24),
        FMT = ('f') | ('m' << 8) | ('t' << 16) | (' ' << 24)
    }
}
