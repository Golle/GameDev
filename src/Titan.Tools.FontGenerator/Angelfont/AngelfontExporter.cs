using Titan.Tools.FontGenerator.Angelfont.Model;

namespace Titan.Tools.FontGenerator.Angelfont
{


    public class AngelfontDescription
    {
        public Info Info { get; set; }
        public Common Common { get; set; }
        public Page[] Pages { get; set; }
        public Character[] Characters { get; set; }
        public Kerning[] Kernings { get; set; }
    }


    internal class AngelfontExporter
    {


    }
}
