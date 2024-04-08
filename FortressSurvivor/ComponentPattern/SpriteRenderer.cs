using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

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
        Button,
        Text,
        CollisionDebug,
    }

    public class SpriteRenderer : Component
    {
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; } = Color.White;

        public Vector2 Origin { get; set; }
        public bool IsCentered = true;
        private float LayerDepth;
        public LAYERDEPTH LayerName { get; private set; } = LAYERDEPTH.Default;
        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {

        }

        public void SetLayerDepth(LAYERDEPTH layerName)
        {
            LayerName = layerName;
            LayerDepth = (float)LayerName / (Enum.GetNames(typeof(LAYERDEPTH)).Length - 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite == null) return;

            Origin = IsCentered ? new Vector2(Sprite.Width / 2, Sprite.Height / 2) : Vector2.Zero;

            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects, LayerDepth);

        }  

        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }

    }
}
