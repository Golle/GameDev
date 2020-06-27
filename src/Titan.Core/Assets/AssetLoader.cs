using System;
using System.IO;
using Titan.Core.Assets.WavefrontObj;

namespace Titan.Core.Assets
{
    internal class AssetLoader : IAssetLoader
    {
        private readonly IObjLoader _objLoader;

        public AssetLoader(IObjLoader objLoader)
        {
            _objLoader = objLoader;
        }

        public IModel3D LoadModel(string filename)
        {
            if (Path.GetExtension(filename) != ".obj")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            // TODO: use the configured path from the engine when reading models

            return _objLoader.LoadFromFile(filename);
        }
    }
}
