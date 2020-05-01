#include "common.h"


EXTERN_C DLLEXPORT void DeviceContextOMSetRenderTargets_(
	ID3D11DeviceContext*	context,
	UINT					numViews,
	ID3D11RenderTargetView* const* ppRenderTargetViews,
	ID3D11DepthStencilView* pDepthStencilView
) {
	context->OMSetRenderTargets(numViews, ppRenderTargetViews, pDepthStencilView);
}


EXTERN_C DLLEXPORT void DeviceContextClearRenderTargetView_(
	ID3D11DeviceContext* context,
	ID3D11RenderTargetView* pRenderTargetView,
	const FLOAT ColorRGBA[4]
) 
{
	context->ClearRenderTargetView(pRenderTargetView, ColorRGBA);
}

EXTERN_C DLLEXPORT void DeviceContextIASetVertexBuffers_(
	ID3D11DeviceContext* context,
	UINT startSlot,
	UINT numBuffers,
	ID3D11Buffer* const* ppVertexBuffers,
	UINT* pStrides,
	const UINT* pOffsets
)
{
	context->IASetVertexBuffers(startSlot, numBuffers, ppVertexBuffers, pStrides, pOffsets);
}

EXTERN_C DLLEXPORT void DeviceContextDraw_(
	ID3D11DeviceContext* context,
	UINT vertexCount,
	UINT startVertexLocation)
{
	context->Draw(vertexCount, startVertexLocation);
}

EXTERN_C DLLEXPORT void DeviceContextVSSetShader_(
	ID3D11DeviceContext* context,
	ID3D11VertexShader* pVertexShader,
	ID3D11ClassInstance* const* ppClassInstances,
	UINT                NumClassInstances
) 
{
	context->VSSetShader(pVertexShader, ppClassInstances, NumClassInstances);
}

EXTERN_C DLLEXPORT void DeviceContextPSSetShader_(
	ID3D11DeviceContext* context,
	ID3D11PixelShader* pPixelShader,
	ID3D11ClassInstance* const* ppClassInstances,
	UINT                NumClassInstances
)
{
	context->PSSetShader(pPixelShader, ppClassInstances, NumClassInstances);
}

EXTERN_C DLLEXPORT void DeviceContextRSSetViewports_(
	ID3D11DeviceContext* context,
	UINT numViewports,
	const D3D11_VIEWPORT* pViewports
)
{
	context->RSSetViewports(numViewports, pViewports);
}

EXTERN_C DLLEXPORT void DeviceContextIASetPrimitiveTopology_(
	ID3D11DeviceContext* context,
	D3D_PRIMITIVE_TOPOLOGY topology
)
{
	context->IASetPrimitiveTopology(topology);
}
