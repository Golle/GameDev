
struct VS_OUTPUT {
    float2 Texture: TEXCOORD;
    float4 Position: SV_POSITION;
};

struct VS_INPUT {
    float2 Position: POSITION;
    float2 Texture : TEXCOORD;
};

VS_OUTPUT main(VS_INPUT input) {
    VS_OUTPUT output;
    output.Position = float4(input.Position, 0.0, 1.0);
    output.Texture = input.Texture;
    return output;
}