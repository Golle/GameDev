using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Titan.Windows;

namespace Titan.Xaudio2.Bindings
{
    [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
    public class IXAudio2VoiceCallbackWrapper : IDisposable
    {
        private readonly IXAudio2VoiceCallback _callback;
        private IntPtr _nativePointer;

        private readonly OnVoiceProcessingPassStartDelegate _startCallback;
        private readonly OnVoiceProcessingPassEnd _endCallback;
        private readonly OnStreamEnd _streamEndCallback;
        private readonly OnBufferStart _bufferStartCallback;
        private readonly OnBufferEnd _bufferEndCallback;
        private readonly OnLoopEnd _loopEndCallback;
        private readonly OnVoiceError _voiceErrorCallback;

        public IntPtr Pointer => _nativePointer;

        public IXAudio2VoiceCallbackWrapper(IXAudio2VoiceCallback callback)
        {
            if (callback == null)
            {
                return;
            }
            
            _callback = callback;
            // Store callbacks to prevent them from being garbage collected
            _startCallback = (_, bytesRequired) => _callback.OnVoiceProcessingPassStart(bytesRequired);
            _endCallback = _ => _callback.OnVoiceProcessingPassEnd();
            _streamEndCallback = _ =>  _callback.OnStreamEnd();
            _bufferStartCallback = (_, context) => _callback.OnBufferStart(context);
            _bufferEndCallback = (_, context) => _callback.OnBufferEnd(context);
            _loopEndCallback = (_, context) => _callback.OnLoopEnd(context);
            _voiceErrorCallback = (_, context, error) => _callback.OnVoiceError(context, error);

            // Allocate memory for the C++ interface
            _nativePointer = Marshal.AllocHGlobal(IntPtr.Size * 8); // 7 methods + vtbl
            
            // Write pointer to vtbl
            Marshal.WriteIntPtr(_nativePointer, IntPtr.Add(_nativePointer, IntPtr.Size));

            // Write the functions to the vtable
            WriteFunctionPointer(1, _startCallback);
            WriteFunctionPointer(2, _endCallback);
            WriteFunctionPointer(3, _streamEndCallback);
            WriteFunctionPointer(4, _bufferStartCallback);
            WriteFunctionPointer(5, _bufferEndCallback);
            WriteFunctionPointer(6, _loopEndCallback);
            WriteFunctionPointer(7, _voiceErrorCallback);
        }

        private void WriteFunctionPointer(int offset, Delegate @delegate) => Marshal.WriteIntPtr(IntPtr.Add(_nativePointer, IntPtr.Size * offset), Marshal.GetFunctionPointerForDelegate(@delegate));

        public void Dispose()
        {
            if (_nativePointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativePointer);
                _nativePointer = IntPtr.Zero;
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        internal delegate void OnVoiceProcessingPassStartDelegate(IntPtr handle, uint bytesRequired);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnVoiceProcessingPassEnd(IntPtr handle);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnStreamEnd(IntPtr handle);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        
        internal delegate void OnBufferStart(IntPtr handle, IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnBufferEnd(IntPtr handle, IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnLoopEnd(IntPtr handle, IntPtr pBufferContext);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void OnVoiceError(IntPtr handle, IntPtr pBufferContext, HRESULT error);
    }
}
