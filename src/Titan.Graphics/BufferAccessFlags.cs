using System;

namespace Titan.Graphics
{
    [Flags]
    public enum BufferAccessFlags
    {
        Default = 0,
        Write = 0x10000,
        Read = 0x20000,
        ReadWrite = Read | Write
    }
}
