namespace Titan.Tools.FontGenerator.Angelfont.Model
{
    public class Common
    {
        //common lineHeight=114 base=89 scaleW=512 scaleH=512 pages=1 packed=0 // pages can be read from pages array
        public int LineHeight { get; set; }
        public int Base { get; set; }
        public int ScaleHeight { get; set; }
        public int ScaleWidth { get; set; }
        public bool Packed { get; set; } = true;
    }
}
