using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class Player : Component
    {
        private Projectile projectile;
        private float speed;
        Animator animator;
        public Player(GameObject gameObject) : base(gameObject)
        {
            
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);
        }

        public override void Awake()
        {
            speed = 100;
        }

        public override void Start()
        {
            
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("knight");
            sr.SetLayerDepth(LAYERDEPTH.Player);
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight / 2);
        }

        bool canShoot = true;
        

        public void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;
                lastShot = 0;
                GameObject arrow = ProjectileFactory.Instance.Create();
                arrow.Transform.Position = GameObject.Transform.Position;
                GameWorld.Instance.Instantiate(arrow);

            }
        }
        
        float lastShot = 0;
        float shootTimer = 1;

        public override void Update(GameTime gameTime)
        {
            lastShot = GameWorld.Instance.DeltaTime;

            if (lastShot> shootTimer)
            {
                canShoot = true;
            }
        }
    }
}
