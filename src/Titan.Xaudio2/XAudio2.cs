using System;
using System.Collections.Generic;
using Titan.Core.Assets.Wave;
using Titan.Windows;
using Titan.Xaudio2.Bindings;
using Titan.Xaudio2.Bindings.Models;

namespace Titan.Xaudio2
{
    internal class XAudio2 : ComPointer, IXAudio2
    {
        public XAudio2(IntPtr handle) 
            : base(handle)
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
            result.Check(nameof(XAudio2), nameof(CreateMasteringVoice));
            return new XAudio2MasteringVoice(masteringVoice);
        }


        private IList<IXAudio2VoiceCallbackWrapper> _wrappers = new List<IXAudio2VoiceCallbackWrapper>();
        public IXAudio2SourceVoice CreateSourceVoice(in WaveformatEx format, IXAudio2VoiceCallback callback)
        {
            HRESULT result;
            IntPtr sourceVoice;
            unsafe
            {
                fixed (WaveformatEx* pointer = &format)
                {
                    var ixAudio2VoiceCallbackWrapper = new IXAudio2VoiceCallbackWrapper(callback);
                    _wrappers.Add(ixAudio2VoiceCallbackWrapper);
                    result = IXAudio2Bindings.CreateSourceVoice_(Handle, out sourceVoice, pointer, pCallback: ixAudio2VoiceCallbackWrapper.Pointer.ToPointer());
                }
            }
            result.Check(nameof(XAudio2), nameof(CreateSourceVoice));
            return new XAudio2SourceVoice(sourceVoice);
        }
    }
}
