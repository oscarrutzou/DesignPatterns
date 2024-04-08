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
            spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.TopLeft, Color.Red);

            // View how to use the uicam properties
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.TopLeft, Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.TopCenter, Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.TopRight - new Vector2(40, 0), Color.Red);

            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.LeftCenter, Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.Center, Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.RightCenter - new Vector2(40, 0), Color.Red);

            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.BottomLeft - new Vector2(0, 40), Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.BottomCenter - new Vector2(0, 40), Color.Red);
            //spriteBatch.DrawString(font, money.ToString(), GameWorld.Instance.uiCam.BottomRight - new Vector2(40, 40), Color.Red);
        }

        public void Update()
        {
            money += 10;
        }
    }
}
