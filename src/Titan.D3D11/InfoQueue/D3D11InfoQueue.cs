using System;
using Titan.D3D11.Bindings;

namespace Titan.D3D11.InfoQueue
{
    internal class D3D11InfoQueue : ID3D11InfoQueue
    {
        private readonly IntPtr _handle;

        public D3D11InfoQueue(in IntPtr handle)
        {
            _handle = handle;
        }

        // not sure how to implement the get message function, will do when needed.
        //public void GetMessage();

        public void Dispose()
        {
            D3D11CommonBindings.ReleaseComObject(_handle);
        }
    }
}
