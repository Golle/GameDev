using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Titan.Core.Assets.Angelfont.Model;
using Titan.Core.Common;

namespace Titan.Core.Assets.Angelfont
{
    public interface IAngelfontLoader
    {
        public Angelfont LoadFromPath(string path);

    }

    internal class AngelfontParser : IAngelfontParser
    {
        public Angelfont ParseFromStream(StreamReader reader)
        {
            var result = new Angelfont();
            string? line;

            var page = 0;
            var character = 0;
            var kerning = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.SplitQuotedStrings(' ');
                switch (parts[0])
                {
                    case "info": 
                        result.Info = ParseInfo(parts); 
                        break;
                    case "common": 
                        result.Common = ParseCommon(parts); 
                        result.Pages = new AngelfontPage[result.Common.Pages];
                        break;
                    case "page":
                        result.Pages[page++] = ParsePage(parts);
                        break;
                    case "chars":
                        result.Characters = new AngelfontCharacter[ReadInt(parts, "count")];
                        break;
                    case "char":
                        result.Characters[character++] = ParseCharacter(parts);
                        break;
                    case "kernings": 
                        result.Kernings = new AngelfontKerning[ReadInt(parts, "count")];
                        break;
                    case "kerning":
                        result.Kernings[kerning++] = ParseKerning(parts);
                        break;
                }
            }

            return result;
        }

        private static AngelfontKerning ParseKerning(string[] parts) =>
            new AngelfontKerning
            {
                Second = ReadInt(parts, "second"),
                First = ReadInt(parts, "first"),
                Amount = ReadInt(parts, "amount")
            };

        private static AngelfontCharacter ParseCharacter(string[] parts) =>
            new AngelfontCharacter
            {
                Id = ReadInt(parts, "id"),
                X = ReadInt(parts, "x"),
                Y = ReadInt(parts, "y"),
                Height = ReadInt(parts, "height"),
                Width = ReadInt(parts, "width"),
                Page = ReadInt(parts, "page"),
                XOffset = ReadInt(parts, "xoffset"),
                YOffset = ReadInt(parts, "yoffset"),
                XAdvance = ReadInt(parts, "xadvance"),
                Channel = (AngelfontChannel) ReadInt(parts, "chnl")
            };

        private static AngelfontPage ParsePage(string[] parts) =>
            new AngelfontPage
            {
                File = ReadQuotedString(parts, "file"),
                Id = ReadInt(parts, "id")
            };

        private AngelfontCommon ParseCommon(string[] parts) =>
            new AngelfontCommon
            {
                Base = ReadInt(parts, "base"),
                ScaleHeight = ReadInt(parts, "scaleH"),
                ScaleWidth = ReadInt(parts, "scaleW"),
                Packed = ReadInt(parts, "packed"),
                LineHeight = ReadInt(parts, "lineHeight"),
                Pages = ReadInt(parts, "pages")
            };

        private static AngelfontInfo ParseInfo(string[] parts) =>
            new AngelfontInfo
            {
                Face = ReadQuotedString(parts, "face"),
                Charset = ReadQuotedString(parts, "charset"),
                Size = ReadInt(parts, "size"),
                AntiAliasing = ReadInt(parts, "aa"),
                StretchH = ReadInt(parts, "stretchH"),
                Padding = ReadPadding(parts, "padding"),
                Spacing = ReadSpacing(parts, "spacing"),
                Italic = ReadInt(parts, "italic"),
                Bold = ReadInt(parts, "bold"),
                Smooth = ReadInt(parts, "smooth"),
                Unicode = ReadInt(parts, "unicode")
            };

        private static Spacing ReadSpacing(IEnumerable<string> parts, string name)
        {
            var values = parts.SingleOrDefault(p => p.StartsWith($"{name}="))
                ?.Split("=")[1]
                ?.Split(",")
                ?.Select(v => int.TryParse(v, NumberStyles.Integer, new NumberFormatInfo { NegativeSign = "-" }, out var value) ? value : 0)
                .ToArray();
            return values != null ? new Spacing(values[0], values[1]) : default;
        }

        private static Padding ReadPadding(IEnumerable<string> parts, string name)
        {
            var values = parts.SingleOrDefault(p => p.StartsWith($"{name}="))
                ?.Split("=")[1]
                ?.Split(",")
                .Select(v => int.TryParse(v, NumberStyles.Integer, new NumberFormatInfo { NegativeSign = "-" }, out var value) ? value : 0)
                .ToArray();
            return values != null ? new Padding(values[0], values[1], values[2], values[3]) : default;
        }

        private static int ReadInt(IEnumerable<string> parts, string name)
        {
            var valueStr = parts
                .SingleOrDefault(p => p.StartsWith($"{name}="))
                ?.Split("=", 2)[1];
            return int.TryParse(valueStr, out var value) ? value : 0;
        }

        private static string ReadQuotedString(IEnumerable<string> parts, string name) =>
            parts
                .SingleOrDefault(p => p.StartsWith($"{name}="))
                ?.Split("=", 2)[1]
                .Replace("\"", string.Empty)
            ?? string.Empty;
    }
}
