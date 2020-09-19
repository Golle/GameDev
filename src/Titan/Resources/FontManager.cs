using System;
using System.IO;
using Titan.Components;
using Titan.Core.Configuration;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Fonts;

namespace Titan.Resources
{
    internal class FontManager : ResourceManager<string, IFont>
    {
        private readonly IFontLoader _fontLoader;
        private readonly IConfiguration _configuration;

        public FontManager(IFontLoader fontLoader, IConfiguration configuration)
        {
            _fontLoader = fontLoader;
            _configuration = configuration;
        }

        protected override IFont Load(in string identifier)
        {
            var filename = Path.Combine(_configuration.Paths.Fonts, identifier);
            if (Path.GetExtension(filename) != ".fnt")
            {
                throw new NotSupportedException($"File format {Path.GetExtension(filename)} is not supported.");
            }

            return _fontLoader.Load(filename);
        }

        protected override void Unload(in string identifier, IFont font)
        {
            font.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, IFont font)
        {
            entity.AddComponent(new FontComponent { Font = font });
        }
    }
}
