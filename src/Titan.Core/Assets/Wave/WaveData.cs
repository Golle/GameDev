using System;
using System.Runtime.InteropServices;

namespace Titan.Core.Assets.Wave
{
    public class WaveData : IDisposable
    {
        public WaveformatEx Format { get; }
        public IntPtr Data { get; private set; }
        public uint Size { get; private set; }

        public WaveData(IntPtr data, uint size, in WaveformatEx format)
        {
            Format = format;
            Data = data;
            Size = size;
        }

        ~WaveData()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            if (Data != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Data);
                Data = IntPtr.Zero;
                Size = 0;
            }
        }
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}
