using System;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.Xaudio2.Bindings
{
    public static class IXAudio2SourceVoiceBindings
    {
        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT Start_(
            IntPtr handle,
            uint flags = 0,
            uint operationSet = 0
            );


        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT SubmitSourceBuffer_(
            [In] IntPtr handle,
            [In] Xaudio2Buffer* pointer,
            [In] void* pBufferWma = null // XAUDIO2_BUFFER_WMA
        );
    }
}
