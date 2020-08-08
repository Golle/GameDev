using System.Threading.Tasks;

namespace Titan.Tools.FontGenerator.Angelfont.Serialization
{
    internal interface ISerializer
    {
        public Task SerializeAsync(AngelfontDescription description, string path);
    }
}
