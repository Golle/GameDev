using System;
using Titan.Components;
using Titan.Components.UI;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Fonts;

namespace Titan.Resources
{
    internal class FontManager : ResourceManager<string, IFont>
    {
        private readonly IFontLoader _fontLoader;
        public FontManager(IFontLoader fontLoader)
        {
            _fontLoader = fontLoader;
        }

        protected override IFont Load(in string identifier)
        {
            var mesh = _fontLoader.Load(identifier);
            Console.WriteLine($"Font: {identifier} loaded");
            return mesh;
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
