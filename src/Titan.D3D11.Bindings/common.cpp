#include "common.h"

EXTERN_C DLLEXPORT UINT D3D11SDKVersion() 
{
    return D3D11_SDK_VERSION;
}

EXTERN_C DLLEXPORT ULONG ReleaseComObject(IUnknown* unknown) 
{
    return unknown ? unknown->Release() : 0;
}
