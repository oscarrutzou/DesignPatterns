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
        //public Vector2 PositionCentered => new Vector2(GameObject.Transform.Position.X + Sprite.Width / 2, GameObject.Transform.Position.Y + Sprite.Height / 2);
        public Texture2D Sprite { get; set; }
        public Color Color { get; set; } = Color.White;

        public Vector2 Origin { get; set; }
        public bool IsCentered = true;
        private float LayerDepth;
        public LAYERDEPTH LayerName { get; private set; } = LAYERDEPTH.Default;

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

            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, GameObject.Transform.Rotation, Origin, GameObject.Transform.Scale, SpriteEffects.None, LayerDepth);

        }  

        ///Set the cell to the right bottom
        //        cellVec = new Vector2(Cell.demension * GameObject.Transform.Scale.X / 2 + (Cell.demension / 2), Cell.demension * GameObject.Transform.Scale.Y / 2 + (Cell.demension / 2));

        ////Move one cell up and left
        //cellVec -= new Vector2(Cell.demension* GameObject.Transform.Scale.X, Cell.demension* GameObject.Transform.Scale.Y);

        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }

    }
}
