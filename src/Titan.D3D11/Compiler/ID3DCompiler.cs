using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Device;

namespace Titan.D3D11.Compiler
{
    public interface ID3DCompiler
    {
        ID3DBlob CompileShader(string shaderCode);
        ID3DBlob CompileShaderFromFile(string filename);
    }

    internal unsafe class D3DCompiler : ID3DCompiler
    {
        public ID3DBlob CompileShader(string shaderCode)
        {
            var result = D3DCompilerBindings.D3DCompile_(shaderCode, (UIntPtr) shaderCode.Length, null, IntPtr.Zero, IntPtr.Zero, "main", "vs_4_0_level_9_3", 0, 0, out var code, out var error);
            result.Check(nameof(D3DCompiler));
            return new D3DBlob(code);
        }

        public ID3DBlob CompileShaderFromFile(string filename)
        {
            var result = D3DCompilerBindings.D3DCompileFromFile_(filename, IntPtr.Zero, IntPtr.Zero, "main", "vs_4_0_level_9_3", 0, 0, out var code, out var error);
            result.Check(nameof(D3DCompiler));
            return new D3DBlob(code);
        }
    }
}
