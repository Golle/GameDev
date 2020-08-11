namespace Titan.Core.Assets.Angelfont.Model
{
    public class AngelfontInfo
    {
        //info face="Segoe UI Light" size=82 bold=0 italic=0 charset="" unicode=0 stretchH=100 smooth=1 aa=1 padding=3,3,3,3 spacing=-2,-2
        public string Face { get; set; }
        public int Size { get; set; }
        public int Unicode { get; set; }
        public int Italic { get; set; }
        public int Bold { get; set; }
        public string Charset { get; set; }
        public int StretchH { get; set; }
        public int Smooth { get; set; }
        public int AntiAliasing { get; set; }
        public Padding Padding { get; set; }
        public Spacing Spacing { get; set; }
    }
}
