namespace Titan.Core.Assets.Angelfont.Model
{
    public class AngelfontCommon
    {
        //common lineHeight=114 base=89 scaleW=512 scaleH=512 pages=1 packed=0 // pages can be read from pages array
        public int LineHeight { get; set; }
        public int Base { get; set; }
        public int ScaleHeight { get; set; }
        public int ScaleWidth { get; set; }
        public int Packed { get; set; }
        public int Pages { get; set; }
    }
}
