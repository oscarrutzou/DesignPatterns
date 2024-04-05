using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor.ComponentPattern
{
    public class Currency : Component, IObserver
    {
        private string text;
        private SpriteFont font;

        public Currency(GameObject gameObject) : base(gameObject)
        {
           
        }

        public Currency(GameObject gameObject, string currencyText) : base(gameObject)
        {
            this.text = currencyText;
            GameWorld.Instance.Attach(this);
        }

        public override void Awake()
        {
            base.Awake();
            font = GameWorld.Instance.Content.Load<SpriteFont>("text");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(text))
            {
                spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
            }
        }

        int enemiesKilled = 0;
        public void Update()
        {
            enemiesKilled++;
            text = enemiesKilled.ToString();
        }
    }
}
