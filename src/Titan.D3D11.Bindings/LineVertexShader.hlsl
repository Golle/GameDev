cbuffer PerFrameBuffer : register(b0)
{
    matrix View;
    matrix ViewProjection;
};

struct VS_INPUT
{
    float3 Position : Position;
    float4 Color : Color;
};


struct VS_OUTPUT
{
    float4 Color : Color;
    float4 Position : SV_Position;
};

VS_OUTPUT main(VS_INPUT input)
{
    VS_OUTPUT output;
    
    output.Position = mul(float4(input.Position, 1.0f), ViewProjection);
    output.Color = input.Color;
    
    return output;
}
