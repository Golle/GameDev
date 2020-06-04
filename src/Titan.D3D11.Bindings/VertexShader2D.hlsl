//
cbuffer CBuf
{
	matrix transform;
};
//
//float4 main(float3 pos : POSITION) : SV_Position
//{
//	return mul(float4(pos.x, pos.y, pos.z, 1.0f), transform);
//}
//
//

struct VS_OUTPUT {
	float4 Color : COLOR;
	float4 Pos: SV_Position;
};

VS_OUTPUT main(float2 pos : Position, float4 color : Color)
{
	VS_OUTPUT output;
	output.Pos = mul(float4(pos, 0.0f, 1.0f), transform);
	//output.Pos = float4(pos, 0.0f, 1.0f);
	
	output.Color = color;
	return output;
	/*return mul(float4(pos.x, pos.y, pos.z, 1.0f), transform);*/
}
