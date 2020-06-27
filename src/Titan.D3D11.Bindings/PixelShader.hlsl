Texture2D tex : register(t0);

SamplerState splr;


//cbuffer CBuf 
//{
//	float4 face_colors[6];
//};
//
//float4 main(uint tid : SV_PrimitiveID) : SV_TARGET
//{
//	return face_colors[tid / 2];
//}



float4 main(float4 color : COLOR, float2 texCoords : Textures) : SV_TARGET
{
	return tex.Sample(splr, texCoords);
	//return color;
}
