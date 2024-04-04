using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FortressSurvivor
{
    public enum LAYERDEPTH
    {
        Default,
        WorldBackground,
        Enemies,
        Player,
        WorldForeground,
        UI,
    }

    internal class SpriteRenderer : Component
    {
        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
        }

        public Texture2D Sprite { get; set; }

        public Vector2 Origin { get; set; }
        private float LayerDepth;
        public LAYERDEPTH LayerName { get; private set; } = LAYERDEPTH.Default;

        public override void Start()
        {
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

      
        public void SetLayerDepth(LAYERDEPTH layerName)
        {
            LayerName = layerName;
            LayerDepth = (float)LayerName / (Enum.GetNames(typeof(LAYERDEPTH)).Length - 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color.White, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, LayerDepth);
        }  
        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }

    }
}
