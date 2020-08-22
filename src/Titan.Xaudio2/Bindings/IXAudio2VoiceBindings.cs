using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.Xaudio2.Bindings
{
    public static class IXAudio2VoiceBindings
    {
        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT SetVolume_(
            [In] IntPtr handle,
            [In] float volume,
            [In] uint operationSet = 0
        );

        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void GetVolume_(
            [In] IntPtr handle,
            [Out] out float volume
        );
    }
}
