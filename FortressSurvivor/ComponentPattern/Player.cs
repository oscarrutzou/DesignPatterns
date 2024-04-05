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
        public Player(GameObject gameObject) : base(gameObject)
        {
            int health = 100;
            int damage = 15;

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
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
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

            }
        }
        
        float lastShot = 0;
    }
}
