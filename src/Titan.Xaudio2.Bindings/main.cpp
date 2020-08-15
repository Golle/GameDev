#include "main.h"

EXPORT HRESULT XAudio2Create_(
    IXAudio2** ppXAudio2, 
    UINT32 flags
)
{
    return XAudio2Create(ppXAudio2, flags, XAUDIO2_DEFAULT_PROCESSOR);
}

EXPORT HRESULT CreateMasteringVoice_(
    IXAudio2* ppXAudio2,
    IXAudio2MasteringVoice** ppMasteringVoice,
    UINT32 inputChannels,
    UINT32 inputSampleRate,
    UINT32 flags,
    LPCWSTR szDeviceId,
    const XAUDIO2_EFFECT_CHAIN* pEffectChain,
    AUDIO_STREAM_CATEGORY streamCategory) 
{
    return ppXAudio2->CreateMasteringVoice(ppMasteringVoice, inputChannels, inputSampleRate, flags, szDeviceId, pEffectChain, streamCategory);
}

