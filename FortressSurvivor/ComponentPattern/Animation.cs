using Microsoft.Xna.Framework.Graphics;

namespace FortressSurvivor
{
    public class Animation
    {
        public string Name { get; private set; }
        public Texture2D[] Sprites { get; private set; }
        public float FPS { get; private set; }
        public Animation(string name, Texture2D[] sprites, float fPS)
        {
            Name = name;
            Sprites = sprites;
            FPS = fPS;
        }
    }
}
