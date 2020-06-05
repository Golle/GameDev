cbuffer CBuf
{
	matrix transform;
};

struct VS_OUTPUT {
	float2 Tex : TexCoord;
	float4 Pos: SV_Position;
};

VS_OUTPUT main(float2 pos : Position, float2 tex: TexCoord)
{
	VS_OUTPUT output;
	
	output.Pos = mul(float4(pos, 0.0f, 1.0f), transform);
	output.Tex = tex;

	return output;
}
