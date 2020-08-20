using System;
using System.ComponentModel;
using Titan.Windows;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    internal class XAudio2SourceVoice : ComPointer, IXAudio2SourceVoice
    {
        public XAudio2SourceVoice(IntPtr handle) 
            : base(handle)
        {   
        }

        public void SubmitSourceBuffer(in Xaudio2Buffer buffer)
        {
            HRESULT result;
            unsafe
            {
                fixed (Xaudio2Buffer* pointer = &buffer)
                {
                    result = IXAudio2SourceVoiceBindings.SubmitSourceBuffer_(Handle, buffer);
                }
            }
        }

        public void Start()
        {
            var result = IXAudio2SourceVoiceBindings.Start_(Handle, 0);
            if (result.Failed)
            {
                throw new Win32Exception($"XAudio2SourceVoice Start failed with code: 0x{result.Code.ToString("X")}");
            }
        }

        public void SetVolume(float volume)
        {
            var result = IXAudio2VoiceBindings.SetVolume_(Handle, volume);
            result.Check(nameof(XAudio2SourceVoice), nameof(SetVolume));
        }
    }
}
