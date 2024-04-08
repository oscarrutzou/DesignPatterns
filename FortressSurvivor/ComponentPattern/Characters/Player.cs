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
        private GameObject fogOfWar;
        private SpriteRenderer fogOfWarSr;
        public Player(GameObject gameObject) : base(gameObject)
        {
        }

        public Player(GameObject gameObject, GameObject followObject) : base(gameObject)
        {
            fogOfWar = followObject;
            fogOfWarSr = fogOfWar.GetComponent<SpriteRenderer>();
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            //Check for the grid if its inside the castle cells
            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
            GameWorld.Instance.worldCam.Move(velocity * GameWorld.DeltaTime);

         

        }

        public override void Awake()
        {
            speed = 200;
        }

        public override void Start()
        {
            
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("knight");
            sr.SetLayerDepth(LAYERDEPTH.Player);
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
            fogOfWar.Transform.Position = GameObject.Transform.Position + new Vector2(-fogOfWarSr.Sprite.Width / 2, -fogOfWarSr.Sprite.Height / 2);


            lastShot += GameWorld.DeltaTime;

            if (lastShot > shootTimer)
            {
                canShoot = true;
            }
        }
    }
}
