using System.Runtime.InteropServices;
using Titan.Graphics.Renderer;

namespace Titan.Components
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Mesh_Unnused
    {
        public uint MeshId;
    }


    unsafe struct TestMesh
    {
        private uint elementSize;
        public uint numberOfElements;
        public Vertex* data;

    }



    public interface IMeshRepository
    {
        unsafe Vertex * GetVertices(string identifier, out uint numberOfElements);
    }


    public class Apa<T> where T : struct { }

    public class Appaa
    {
        private Apa<TestMesh> _mesh;
    }
}







/*
 *
 *
 * 1. read game file
 * 2. load models into memory
 *  2.1 create VB+IB for the model
 *  2.2 assign an unique ID to the model
 * 3. Create mesh component
 * 4. Query meshId from model repository
 * 5. Read model data in Render system
 *
 *
 *
 */
