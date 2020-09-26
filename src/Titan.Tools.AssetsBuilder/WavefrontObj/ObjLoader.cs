using System.Threading.Tasks;

namespace Titan.Tools.AssetsBuilder.WavefrontObj
{
    public class ObjLoader
    {
        private readonly ObjParser _parser = new ObjParser();
        public async Task<WavefrontObject> LoadFromFile(string filename)
        {
            return await _parser.ReadFromFile(filename);
        }
    }
}
