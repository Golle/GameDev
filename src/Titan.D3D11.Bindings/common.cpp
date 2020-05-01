#include "common.h"

EXTERN_C DLLEXPORT UINT D3D11SdkVersion()
{
    return D3D11_SDK_VERSION;
}

EXTERN_C DLLEXPORT HRESULT D3DReadFileToBlob_(
    LPCWSTR pFileName, 
    ID3DBlob ** ppContents
) 
{
    return D3DReadFileToBlob(pFileName, ppContents);
}

EXTERN_C DLLEXPORT ULONG ReleaseComObject(IUnknown* unknown) 
{
    return unknown ? unknown->Release() : 0;
}

EXTERN_C DLLEXPORT HRESULT QueryInterface_(IUnknown* unknown, const IID &iid, void ** ppObject)
{
    return unknown->QueryInterface(iid, ppObject);
}
