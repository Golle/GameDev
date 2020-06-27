//
cbuffer CBuf 
{
	matrix transform;
	matrix model;
};

struct VS_OUTPUT {
	float4 Color : COLOR;
	float2 Tex: Textures;
	float4 Pos: SV_Position;
};

VS_OUTPUT main(float3 pos : Position, float3 norm : Normals, float2 tex : Textures, float4 color : Color)
{
	VS_OUTPUT output;

	float4 position = float4(pos, 22.0f);
	position = mul(position, model);
	output.Pos = mul(position, transform);
	//output.Pos = float4(pos, 22.0f);
	output.Color = color;
	output.Tex = tex;
	return output;
	/*return mul(float4(pos.x, pos.y, pos.z, 1.0f), transform);*/
}
