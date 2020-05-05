#include "common.h"
#include <DirectXMath.h>
EXTERN_C DLLEXPORT UINT D3D11SdkVersion()
{
    auto m = DirectX::XMMatrixPerspectiveLH(1.0f, 3.0f / 4.0f, 0.5f, 10.0f);

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
