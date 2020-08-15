using System;
using System.Runtime.InteropServices;
using Titan.Windows;
using Titan.Xaudio2.Bindings.Models;

namespace Titan.Xaudio2.Bindings
{
    public static class IXAudio2Bindings
    {
        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HRESULT XAudio2Create_(
            [Out] out IntPtr ppXAudio2,
            [In] uint flags
        );

        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateMasteringVoice_(
            [In] IntPtr ppXAudio2Handle,
            [Out] out IntPtr ppMasteringVoice,
            [In] uint inputChannels,
            [In] uint inputSampleRate,
            [In] uint flags,
            [In] string szDeviceId,
            [In] Xaudio2EffectChain* pEffectChain,
            [In] AudioStreamCategory streamCategory);
    }
}

  
