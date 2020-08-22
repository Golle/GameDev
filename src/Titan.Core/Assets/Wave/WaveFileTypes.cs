using System;

namespace Titan.Core.Assets.Wave
{
    [Flags]
    internal enum WaveFileTypes : uint
    {
        Invalid = 0,
        WAVE = ('W') | ('A' << 8) | ('V' << 16) | ('E' << 24),
        XWMA = ('X') | ('W' << 8) | ('M' << 16) | ('A' << 24),
        DPDS = ('d') | ('p' << 8) | ('d' << 16) | ('s' << 24),
    }
}
