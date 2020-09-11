using System;
using Titan.D3D11.Bindings;
using Titan.D3D11.Device;

namespace Titan.D3D11.Compiler
{
    internal class D3DCompiler : ID3DCompiler
    {
        public ID3DBlob CompileShader(string shaderCode, string entryPoint, string target, uint flags1 = 0, uint flags2 = 0)
        {
            var result = D3DCompilerBindings.D3DCompile_(shaderCode, (UIntPtr) shaderCode.Length, null, IntPtr.Zero, IntPtr.Zero, entryPoint, target, 0, 0, out var code, out var error);
            result.Check(nameof(D3DCompiler));
            return new D3DBlob(code);
        }
        //"vs_4_0_level_9_3"
        public ID3DBlob CompileShaderFromFile(string filename, string entryPoint, string target, uint flags1 = 0, uint flags2 = 0)
        {
            var result = D3DCompilerBindings.D3DCompileFromFile_(filename, IntPtr.Zero, IntPtr.Zero, entryPoint, target , 0, 0, out var code, out var error);
            result.Check(nameof(D3DCompiler));
            return new D3DBlob(code);
        }
    }
}
