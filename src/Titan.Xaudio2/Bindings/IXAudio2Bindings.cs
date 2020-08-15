using System;
using System.Runtime.InteropServices;
using Titan.Core.Assets.Wave;
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


        [DllImport(Constants.XAudio2Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern unsafe HRESULT CreateSourceVoice_(
            [In] IntPtr ppXAudio2Handle,
            [Out] out IntPtr sourceVoice, //IXAudio2SourceVoice
            [In] WaveformatEx* sourceFormat,
            [In] uint flags = 0,
            [In] float maxFrequencyRatio = 2.0f,
            [In] void* pCallback = null,       //IXAudio2VoiceCallback
            [In] void* pSendList = null,       // XAUDIO2_VOICE_SENDS
            [In] void* pEffectChain = null);   // XAUDIO2_EFFECT_CHAIN
    }
}


  
