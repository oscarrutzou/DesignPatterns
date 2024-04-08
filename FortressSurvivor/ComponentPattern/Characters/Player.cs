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
        private float speed;
        Animator animator;
        private GameObject fogOfWarGo;
        public Player(GameObject gameObject) : base(gameObject)
        {
        }

        public Player(GameObject gameObject, GameObject followObject) : base(gameObject)
        {
            fogOfWarGo = followObject;
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
        readonly float shootTimer = 1;

        public override void Update(GameTime gameTime)
        {
            fogOfWarGo.Transform.Position = GameObject.Transform.Position;

            lastShot += GameWorld.DeltaTime;

            if (lastShot > shootTimer)
            {
                canShoot = true;
            }
        }
    }
}
