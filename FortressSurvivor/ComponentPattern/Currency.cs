using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class Currency : Component, IObserver
    {
        public int money;
        private SpriteFont font;

        public Currency(GameObject gameObject) : base(gameObject)
        {
            //Attach
        }

        public override void Awake()
        {
            base.Awake();
            font = GameWorld.Instance.Content.Load<SpriteFont>("Font");
            GameObject.SetLayerDepth(LAYERDEPTH.Text);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, money.ToString(), Vector2.Zero, Color.Red);
        }

        public void Update()
        {
            money += 10;
        }
    }
}
