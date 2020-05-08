#include "common.h"
#include <DirectXMath.h>
EXPORT UINT D3D11SdkVersion()
{
    auto m = DirectX::XMMatrixPerspectiveLH(1.0f, 3.0f / 4.0f, 0.5f, 10.0f);

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
