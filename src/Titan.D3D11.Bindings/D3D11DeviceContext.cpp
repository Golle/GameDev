#include "common.h"

EXPORT void OMSetRenderTargets_(
	ID3D11DeviceContext*	context,
	UINT					numViews,
	ID3D11RenderTargetView* const* ppRenderTargetViews,
	ID3D11DepthStencilView* pDepthStencilView
) {
	context->OMSetRenderTargets(numViews, ppRenderTargetViews, pDepthStencilView);
}


EXPORT void ClearRenderTargetView_(
	ID3D11DeviceContext* context,
	ID3D11RenderTargetView* pRenderTargetView,
	const FLOAT ColorRGBA[4]
) 
{
	context->ClearRenderTargetView(pRenderTargetView, ColorRGBA);
}

EXPORT void ClearDepthStencilView_(
	ID3D11DeviceContext* context,
	ID3D11DepthStencilView* pDepthStencilView,
	UINT ClearFlags,
	FLOAT Depth,
	UINT8 Stencil
)
{
	context->ClearDepthStencilView(pDepthStencilView, ClearFlags, Depth, Stencil);
}

EXPORT void IASetVertexBuffers_(
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

EXPORT void Draw_(
	ID3D11DeviceContext* context,
	UINT vertexCount,
	UINT startVertexLocation)
{
	context->Draw(vertexCount, startVertexLocation);
}

EXPORT void VSSetShader_(
	ID3D11DeviceContext* context,
	ID3D11VertexShader* pVertexShader,
	ID3D11ClassInstance* const* ppClassInstances,
	UINT                NumClassInstances
) 
{
	context->VSSetShader(pVertexShader, ppClassInstances, NumClassInstances);
}

EXPORT void PSSetShader_(
	ID3D11DeviceContext* context,
	ID3D11PixelShader* pPixelShader,
	ID3D11ClassInstance* const* ppClassInstances,
	UINT                NumClassInstances
)
{
	context->PSSetShader(pPixelShader, ppClassInstances, NumClassInstances);
}

EXPORT void RSSetViewports_(
	ID3D11DeviceContext* context,
	UINT numViewports,
	const D3D11_VIEWPORT* pViewports
)
{
	context->RSSetViewports(numViewports, pViewports);
}

EXPORT void IASetPrimitiveTopology_(
	ID3D11DeviceContext* context,
	D3D_PRIMITIVE_TOPOLOGY topology
)
{
	context->IASetPrimitiveTopology(topology);
}

EXPORT void IASetInputLayout_(
	ID3D11DeviceContext* context,
	ID3D11InputLayout* pInputLayout
)
{
	context->IASetInputLayout(pInputLayout);
}

EXPORT void IASetIndexBuffer_(
	ID3D11DeviceContext* context,
	ID3D11Buffer* indexBuffer,
	DXGI_FORMAT format,
	UINT offset
)
{
	context->IASetIndexBuffer(indexBuffer, format, offset);
}

EXPORT void VSSetConstantBuffers_(
	ID3D11DeviceContext* context,
	UINT startSlot,
	UINT numBuffers, 
	ID3D11Buffer * const * ppConstantBuffers
)
{
	context->VSSetConstantBuffers(startSlot, numBuffers, ppConstantBuffers);
}

EXPORT void PSSetConstantBuffers_(
	ID3D11DeviceContext* context,
	UINT startSlot,
	UINT numBuffers, 
	ID3D11Buffer * const * ppConstantBuffers
)
{
	context->PSSetConstantBuffers(startSlot, numBuffers, ppConstantBuffers);
}

EXPORT void DrawIndexed_(
	ID3D11DeviceContext* context,
	UINT indexCount,
	UINT startIndexLocation,
	INT  baseVertexLocation
) 
{
	context->DrawIndexed(indexCount, startIndexLocation, baseVertexLocation);
}

EXPORT void OMSetDepthStencilState_(
	ID3D11DeviceContext* context,
	ID3D11DepthStencilState* pDepthStencilState,
	UINT stencilRef
) 
{
	context->OMSetDepthStencilState(pDepthStencilState, stencilRef);
}

EXPORT void UpdateSubresource_(
	ID3D11DeviceContext* context,
	ID3D11Resource* pDstResource,
	UINT DstSubresource,
	const D3D11_BOX* pDstBox,
	const void* pSrcData,
	UINT SrcRowPitch,
	UINT SrcDepthPitch
)
{
	context->UpdateSubresource(pDstResource, DstSubresource, pDstBox, pSrcData, SrcRowPitch, SrcDepthPitch);
}

EXPORT HRESULT Map_(
	ID3D11DeviceContext*	context,
	ID3D11Resource*			pResource,
	UINT                    subresource,
	D3D11_MAP               mapType,
	UINT                    mapFlags,
	D3D11_MAPPED_SUBRESOURCE* pMappedResource
)
{
	return context->Map(pResource, subresource, mapType, mapFlags, pMappedResource);
}
EXPORT void Unmap_(
	ID3D11DeviceContext* context,
	ID3D11Resource* pResource,
	UINT           subresource
) {
	context->Unmap(pResource, subresource);
}
