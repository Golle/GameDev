using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.D3D11.Bindings
{
    public static unsafe class D3DCompilerBindings
    {
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3DCompile_(
            [In] string srcData, //LPCVOID
            [In] UIntPtr srcDataSize,
            [In] string sourceName,
            [In] IntPtr defines, // D3D_SHADER_MACRO
            [In] IntPtr include, // ID3DInclude
            [In] string entryPoint,
            [In] string target,
            [In] uint flags1,
            [In] uint flags2,
            [Out] out IntPtr code, // ID3DBlob
            [Out] out IntPtr errorMsg // ID3DBlob
        );
        
        [DllImport(Constants.D3D11Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT D3DCompileFromFile_(
            [In, MarshalAs(UnmanagedType.LPWStr)] string fileName,
            [In] IntPtr defines, // D3D_SHADER_MACRO
            [In] IntPtr include, // ID3DInclude
            [In] string entryPoint,
            [In] string target,
            [In] uint flags1,
            [In] uint flags2,
            [Out] out IntPtr code, // ID3DBlob
            [Out] out IntPtr errorMsg // ID3DBlob
        );
    }
}
