using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
     class Player : Component
    {
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
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }

        public override void Awake()
        {
            speed = 100;
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("knight");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 3);
        }

        bool canShoot = true;
        

        public void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;
                lastShot = 0;

            }
        }
        
        float lastShot = 0;
    }
}
