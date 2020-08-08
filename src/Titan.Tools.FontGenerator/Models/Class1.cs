using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Tools.FontGenerator.Models
{
    public class FontBuilderConfiguration
    {
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public TextRenderingHint RenderingHint { get; set; }
    }
}
