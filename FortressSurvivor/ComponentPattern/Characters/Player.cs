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
        private GameObject fogOfWarGo, areaWorldColliderGo;
        private Collider collider;

        private bool canShoot = true;
        private float lastShot = 0;
        readonly float shootTimer = 1;
        private Vector2 previousPos;

        public Player(GameObject gameObject) : base(gameObject)
        {
        }

        public Player(GameObject gameObject, GameObject followObject, GameObject areaWorldColliderGo) : base(gameObject)
        {
            fogOfWarGo = followObject;
            this.areaWorldColliderGo = areaWorldColliderGo;
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

            collider = GameObject.GetComponent<Collider>();
        }


        public override void Update(GameTime gameTime)
        {
            fogOfWarGo.Transform.Position = GameObject.Transform.Position;

            lastShot += GameWorld.DeltaTime;

            if (lastShot > shootTimer)
            {
                canShoot = true;
            }
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            //Check for the grid if its inside the castle cells
            CheckNewPosIsInsideTower(velocity * speed * GameWorld.DeltaTime);
        }

        private void CheckNewPosIsInsideTower(Vector2 newPos)
        {
            Vector2 prePos = GameObject.Transform.Position;
            GameObject.Transform.Translate(newPos);

            if (!areaWorldColliderGo.GetComponent<Collider>().CollisionBox.Contains(collider.CollisionBox)){
                GameObject.Transform.Position = prePos;
            }
            else
            {
                GameObject.Transform.Translate(newPos);
                GameWorld.Instance.worldCam.Move(newPos);
            }
        }

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


        public override void OnCollisionEnter(Collider collider)
        {
            if (areaWorldColliderGo == collider.GameObject)
            {

            }
        }
    }
}
