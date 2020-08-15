using System;
using System.ComponentModel;
using Titan.Windows;
using Titan.Xaudio2.Bindings;
using Titan.Xaudio2.Bindings.Models;

namespace Titan.Xaudio2
{
    internal class XAudio2 : ComPointer, IXAudio2
    {
        public XAudio2(IntPtr handle) : base(handle)
        {
        }

        public IXAudio2MasteringVoice CreateMasteringVoice()
        {

            HRESULT result;
            IntPtr masteringVoice;
            unsafe
            {
                result = IXAudio2Bindings.CreateMasteringVoice_(Handle, out masteringVoice, 0, 0, 0, null, null, AudioStreamCategory.GameEffects);
            }
            if (result.Failed)
            {
                throw new Win32Exception($"XAudio2 CreateMasteringVoice failed with code: 0x{result.Code.ToString("X")}");
            }
            return new XAudio2MasteringVoice(masteringVoice);
        }
    }
}
