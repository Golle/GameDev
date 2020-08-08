using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Tools.FontGenerator.Angelfont.Serialization
{
    internal class TextSerializer : ISerializer
    {
        public async Task SerializeAsync(AngelfontDescription description, string path)
        {

            char a = (char)120;
            var builder = new StringBuilder();

            var info = description.Info;
            var common = description.Common;
            builder.AppendLine($"info face=\"{info.Face}\" size={info.Size} bold={(info.Bold ? 1 : 0)} italic={(info.Italic ? 1 : 0)} charset=\"{info.Charset}\" unicode={(info.Unicode ? 1 :  0)} stretchH={info.StretchH} smooth={(info.Smooth ? 1 : 0)} aa={info.AntiAliasing } padding={info.Padding.Top},{info.Padding.Bottom},{info.Padding.Left},{info.Padding.Right} spacing={info.Spacing.X},{info.Spacing.Y}");
            builder.AppendLine($"common lineHeight={common.LineHeight} base={common.Base} scaleW={common.ScaleWidth} scaleH={common.ScaleHeight} pages={description.Pages.Length} packed={(common.Packed ? 1 : 0)}");
     
            foreach (var page in description.Pages)
            {
                builder.AppendLine($"page id={page.Id} file=\"{page.File}\"");
            }

            builder.AppendLine($"chars count={description.Characters.Length}");
            foreach (var character in description.Characters)
            {
                builder.AppendLine($"char id={character.Id} x={character.X} y={character.Y}width={character.Width} height={character.Height} xoffset={character.XOffset} yoffset={character.YOffset} xadvance={character.XAdvance} page={character.Page} chnl={(int)character.Channel}");
            }

            builder.AppendLine($"kernings count={description.Kernings.Length}");
            foreach (var kerning in description.Kernings)
            {
                builder.AppendLine($"kerning first={kerning.First} second={kerning.Second} amount={kerning.Amount}");
            }

            var snakeCaseName = description.Info.Face.ToLowerInvariant().Replace(" ", "_");
            await File.WriteAllTextAsync(Path.Combine(path, snakeCaseName + ".fnt"), builder.ToString());
        }
    }
}
