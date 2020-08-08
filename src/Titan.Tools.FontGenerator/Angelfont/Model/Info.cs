namespace Titan.Tools.FontGenerator.Angelfont.Model
{
    public class Info
    {
        //info face="Segoe UI Light" size=82 bold=0 italic=0 charset="" unicode=0 stretchH=100 smooth=1 aa=1 padding=3,3,3,3 spacing=-2,-2
        public string Face { get; set; }
        public int Size { get; set; }
        public bool Unicode { get; set; }
        public bool Italic { get; set; }
        public bool Bold { get; set; }
        public string Charset { get; set; }
        public int StretchH { get; set; }
        public bool Smooth { get; set; }
        public int AntiAliasing { get; set; }
        public Padding Padding { get; set; }
        public Spacing Spacing { get; set; }


    }
}
