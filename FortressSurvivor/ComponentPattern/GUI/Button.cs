using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortressSurvivor
{
    internal class Button : Component
    {
        private SpriteRenderer spriteRenderer;

        public Button(GameObject gameObject, Rectangle bounds, Texture2D texture) : base(gameObject)
        {
            Bounds = bounds;
            Texture = texture;
        }

        public Rectangle Bounds { get; set; }
        public bool IsClicked { get; private set; }
        public Texture2D Texture { get; set; }
        public bool IsHovered { get; private set; }

        public override void Start()
        {
            
            //spriteRenderer = GameObject.AddComponent<SpriteRenderer>();
        
            //spriteRenderer.SetLayerDepth(LAYERDEPTH.Button);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            IsHovered = mouseRectangle.Intersects(Bounds);

            if (IsHovered && mouseState.LeftButton == ButtonState.Pressed)
            {
                IsClicked = true;
            }
            else
            {
                IsClicked = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
