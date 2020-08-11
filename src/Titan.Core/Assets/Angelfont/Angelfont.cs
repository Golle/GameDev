using Titan.Core.Assets.Angelfont.Model;

namespace Titan.Core.Assets.Angelfont
{
    public class Angelfont
    {
        public AngelfontInfo Info { get; set; }
        public AngelfontCommon Common { get; set; }
        public AngelfontPage[] Pages { get; set; }
        public AngelfontCharacter[] Characters { get; set; }
        public AngelfontKerning[] Kernings { get; set; }
    }
}
