cbuffer CBuf : register(b0)
{
	matrix transform;
};


struct VS_INPUT
{
    float2 Position : Position;
    float2 Texture : Textures;
    float4 Color : Color;
};

struct VS_OUTPUT
{
    float2 Texture : Textures;
    float4 Color : Color;
	float4 Position: SV_Position;
};

VS_OUTPUT main(VS_INPUT input)
{
	VS_OUTPUT output;
	
	output.Position = mul(float4(input.Position, 0.0f, 1.0f), transform);
	output.Texture = input.Texture;
    output.Color = input.Color;

	return output;
}
