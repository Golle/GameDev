#include "common.h"

EXPORT UINT D3D11SdkVersion()
{
    return D3D11_SDK_VERSION;
}

EXPORT HRESULT D3DReadFileToBlob_(
    LPCWSTR pFileName, 
    ID3DBlob ** ppContents
) 
{
    return D3DReadFileToBlob(pFileName, ppContents);
}

EXPORT ULONG ReleaseComObject(IUnknown* unknown) 
{
    return unknown ? unknown->Release() : 0;
}

EXPORT HRESULT QueryInterface_(IUnknown* unknown, const IID &iid, void ** ppObject)
{
    return unknown->QueryInterface(iid, ppObject);
}
