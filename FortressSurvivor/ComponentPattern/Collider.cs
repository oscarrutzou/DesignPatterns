﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public class Collider : Component
    {
        private SpriteRenderer spriteRenderer;
        private Texture2D texture;

        private Rectangle _collider;
        public Rectangle CollisionBox
        {
            get
            {
                return _collider;
            }
        }

        public Lazy<List<RectangleData>> rectanglesData = new Lazy<List<RectangleData>>();

        public Collider(GameObject gameObject) : base(gameObject)
        {
        }
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            _collider = new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X - (spriteRenderer.Sprite.Width * GameObject.Transform.Scale.X) / 2),
                        (int)(GameObject.Transform.Position.Y - (spriteRenderer.Sprite.Height * GameObject.Transform.Scale.Y) / 2),
                        spriteRenderer.Sprite.Width * (int)GameObject.Transform.Scale.X,
                        spriteRenderer.Sprite.Height * (int)GameObject.Transform.Scale.X
                    );
            texture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");

            rectanglesData = new Lazy<List<RectangleData>>(() => CreateRectangles());

            if (spriteRenderer == null) new Exception("The collision need a spriteRenderer to work");
            var value = rectanglesData.Value;
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePixelCollider();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(CollisionBox, spriteBatch);

            if (rectanglesData.IsValueCreated)
            {
                foreach (RectangleData rectangleData in rectanglesData.Value)
                {
                    DrawRectangle(rectangleData.Rectangle, spriteBatch);
                }
            }
        }


        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
        private List<RectangleData> CreateRectangles()
        {
            texture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");
            List<Color[]> lines = new List<Color[]>();
            List<RectangleData> pixels = new List<RectangleData>();

            Vector2 cellVec = Vector2.Zero;
            Cell cell = GameObject.GetComponent<Cell>() as Cell;
            if (cell != null)
            {
                //Fix this:DDDD
                //Set the cell to the right bottom 
                cellVec = new Vector2(Cell.demension * GameObject.Transform.Scale.X / 2 + (Cell.demension / 2), Cell.demension * GameObject.Transform.Scale.Y / 2 + (Cell.demension / 2));

                //Move one cell up and left
                cellVec -= new Vector2(Cell.demension * GameObject.Transform.Scale.X, Cell.demension * GameObject.Transform.Scale.Y);
            }

            for (int y = 0; y < spriteRenderer.Sprite.Height; y++)
            {
                Color[] colors = new Color[spriteRenderer.Sprite.Width];
                spriteRenderer.Sprite.GetData(0, new Rectangle(0, y, spriteRenderer.Sprite.Width, 1), colors, 0, spriteRenderer.Sprite.Width);
                lines.Add(colors);
            }

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x].A != 0)
                    {
                        if ((x == 0) || (x == lines[y].Length) || (x > 0 && lines[y][x - 1].A == 0) || (x < lines[y].Length - 1 && lines[y][x + 1].A == 0) || (y == 0) || (y > 0 && lines[y - 1][x].A == 0) || (y < lines.Count - 1 && lines[y + 1][x].A == 0))
                        {
                            RectangleData rd = new RectangleData(
                                (int)((x * GameObject.Transform.Scale.X) + cellVec.X),
                                (int)((y * GameObject.Transform.Scale.Y) + cellVec.Y));
                            pixels.Add(rd);
                        }
                    }
                }
            }

            return pixels;
        }

        private void UpdatePixelCollider()
        {
            if (rectanglesData.IsValueCreated)
            {
                for (int i = 0; i < rectanglesData.Value.Count; i++)
                {
                    rectanglesData.Value[i].UpdatePosition(GameObject, spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);
                }
            }
        }
    }
}
