cbuffer PerObject : register(b0)
{
    matrix WorldMatrix;
    matrix InverseTransposeWorldMatrix;
    matrix WorldViewProjectionMatrix;
}

struct AppData
{
    float4 Position : POSITION;
    float4 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 PositionWS   : TEXCOORD1;
    float4 NormalWS     : TEXCOORD2;
    float2 TexCoord     : TEXCOORD0;             // l h v n
    float3 TMatrix      : TEXCOORD3;
    float4 Color        : TEXCOORD4;
    float4 Position     : SV_Position;
};

VertexShaderOutput vertexShader(AppData IN)
{
    VertexShaderOutput OUT;

    float3 tangent;

    float3 c1 = float3(IN.Normal.xyz) * float3(0.0, 0.0, 1.0);
    float3 c2 = float3(IN.Normal.xyz) * float3(0.0, 1.0, 0.0);

    if (length(c1) > length(c2))
    {
        tangent = c1;
    }
    else 
    {
        tangent = c2;
    }

    float3 color = lerp(float3(0.0f, 0.0f, 1.0f), float3(1.0f, 0.0f, 0.0f), (IN.Position.z + 100.0f)/200.0f);
    OUT.Color = float4(color, 1.0f);


    OUT.TMatrix = tangent;
    OUT.Position = mul(WorldViewProjectionMatrix, IN.Position);
    OUT.PositionWS = mul(WorldMatrix, IN.Position);
    OUT.NormalWS = mul(InverseTransposeWorldMatrix, IN.Normal);
    OUT.TexCoord = IN.TexCoord;

    return OUT;
}