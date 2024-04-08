using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FortressSurvivor
{
    public class TowerHealthUI : Component
    {
        public int gealth;
        private SpriteFont font;
        private GameObject towerGo;
        private Stats towerHealth;
        public TowerHealthUI(GameObject gameObject) : base(gameObject)
        {
        }
        public TowerHealthUI(GameObject gameObject, GameObject towerGo) : base(gameObject)
        {
            this.towerGo = towerGo;
            towerHealth = towerGo.GetComponent<Stats>();
        }

        public override void Awake()
        {
            base.Awake();
            font = GameWorld.Instance.Content.Load<SpriteFont>("Font");
            GameObject.SetLayerDepth(LAYERDEPTH.Text);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, towerHealth.health.ToString(), GameWorld.Instance.uiCam.TopLeft, Color.Red);
            //spriteBatch.DrawString(font, $"{EnemyPool.Instance.active.Count} + inactive {EnemyPool.Instance.inactive.Count}", GameWorld.Instance.uiCam.TopLeft, Color.Red);

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


    }
}
