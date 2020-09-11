using Titan.D3D11.Device;

namespace Titan.D3D11.Compiler
{
    public interface ID3DCompiler
    {
        ID3DBlob CompileShader(string shaderCode, string entryPoint, string target, uint flags1 = 0, uint flags2 = 0);
        ID3DBlob CompileShaderFromFile(string filename, string entryPoint, string target, uint flags1 = 0, uint flags2 = 0);
    }
}
