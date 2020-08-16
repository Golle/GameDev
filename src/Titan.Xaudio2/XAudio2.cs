using System;
using System.ComponentModel;
using Titan.Core.Assets.Wave;
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


        private IXAudio2VoiceCallbackWrapper _callback;
        public IXAudio2SourceVoice CreateSourceVoice(in WaveformatEx format)
        {
            var callback = _callback ??= new IXAudio2VoiceCallbackWrapper(new TestCallback()); // TODO: move this to its own handler/wrapper class
            HRESULT result;
            IntPtr sourceVoice;
            unsafe
            {
                fixed (WaveformatEx* pointer = &format)
                {
                    result = IXAudio2Bindings.CreateSourceVoice_(Handle, out sourceVoice, pointer, pCallback: callback.Pointer.ToPointer());
                }
            }
            if (result.Failed)
            {
                throw new Win32Exception($"XAudio2 CreateSourceVoice failed with code: 0x{result.Code.ToString("X")}");
            }
            return new XAudio2SourceVoice(sourceVoice);
        }
    }

    class TestCallback : IXAudio2VoiceCallback
    {
        public void OnVoiceProcessingPassStart(uint bytesRequired)
        {
            //Console.WriteLine("OnVoiceProcessingPassStart");
        }

        public void OnVoiceProcessingPassEnd()
        {
            //Console.WriteLine("OnVoiceProcessingPassEnd");
        }

        public void OnStreamEnd()
        {
            Console.WriteLine("OnStreamEnd");
        }

        public void OnBufferStart(IntPtr pBufferContext)
        {
            Console.WriteLine("OnBufferStart");
        }

        public void OnBufferEnd(IntPtr pBufferContext)
        {
            Console.WriteLine("OnBufferEnd");
        }

        public void OnLoopEnd(IntPtr pBufferContext)
        {
            Console.WriteLine("OnLoopEnd");
        }

        public void OnVoiceError(IntPtr pBufferContext, HRESULT error)
        {
            Console.WriteLine("OnVoiceError");
        }
    }
}
