using System;
using System.Runtime.InteropServices;

namespace Titan.D3D11.Bindings
{
    internal class D3D10BlobBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr GetBufferPointer_(
            [In] IntPtr blob
            );

        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern UIntPtr GetBufferSize_(
            [In] IntPtr blob
        );
    }
}
