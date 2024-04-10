using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public enum TextureNames
    {
        TestDolphin,
        Knight,
        Arrow,
        FogOfWar,
        Cell,
        Pixel
    }

    // Dictionary of all textures
    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures { get; private set; }
        public static SpriteFont defaultFont { get; private set; }
        //public static SpriteFont defaultFontBigger { get; private set; }

        public static void LoadContent()
        {
            ContentManager content = GameWorld.Instance.Content;
            // Load all textures
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.Knight, content.Load<Texture2D>("knight") },
                {TextureNames.Arrow, content.Load<Texture2D>("arrow1") },
                {TextureNames.FogOfWar, content.Load<Texture2D>("FogofWar") },
                {TextureNames.Cell, content.Load<Texture2D>("World\\16x16White") },
                {TextureNames.Pixel, content.Load<Texture2D>("Pixel") },

            };

            // Load all fonts
            defaultFont = content.Load<SpriteFont>("Font");
            //defaultFontBigger = content.Load<SpriteFont>("Fonts\\FontBigger");
        }
    }
}
