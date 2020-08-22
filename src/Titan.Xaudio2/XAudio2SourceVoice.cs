using System;
using System.ComponentModel;
using Titan.Windows;
using Titan.Xaudio2.Bindings;

namespace Titan.Xaudio2
{
    internal class XAudio2SourceVoice : XAudio2Voice, IXAudio2SourceVoice
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
            result.Check(nameof(XAudio2SourceVoice), nameof(SubmitSourceBuffer));
        }

        public void Start()
        {
            var result = IXAudio2SourceVoiceBindings.Start_(Handle, 0);
            result.Check(nameof(XAudio2SourceVoice), nameof(Start));
        }
    }
}
