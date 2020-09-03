//
//cbuffer CBuf 
//{
//	matrix transform;
//	matrix model;
//    matrix view;
//    float3 lights;
//    float3 lightColors;
//};



cbuffer PerFrameBuffer : register(b0)
{
    matrix View;
    matrix ViewProjection;
};

cbuffer PerObjectBuffer : register(b1)
{
    matrix World;
};


struct VS_INPUT
{
    float3 Position : Position;
    float3 Normal : Normal;
    float2 Texture : Texture;
    float4 Color : Color;
};


struct VS_OUTPUT
{
    float4 Color : Color;
    float3 Normal : Normal;
    float2 Tex : Texture;
    float4 WorldPosition : Position;
    float4 Pos : SV_Position;
};

VS_OUTPUT main(VS_INPUT input)
{
    VS_OUTPUT output;
    
    output.WorldPosition = mul(float4(input.Position, 1.0f), World);
    output.Pos = mul(output.WorldPosition, ViewProjection);
    output.Color = input.Color;
    output.Tex = input.Texture;
    output.Normal = mul(input.Normal, (float3x3) World);
    
    return output;
}
