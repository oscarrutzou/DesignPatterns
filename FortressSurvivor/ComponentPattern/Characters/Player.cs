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
        private GameObject fogOfWarGo, areaWorldColliderGo;
        private Collider collider;

        private bool canShoot = true;
        private float lastShot = 0;
        readonly float shootTimer = 1;

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
            //sr.SetSprite(TextureNames.Knight);
            sr.SetLayerDepth(LAYERDEPTH.Player);

            Animator animator = GameObject.GetComponent<Animator>();
            animator.AddAnimation(GlobalAnimations.animations[AnimNames.WizardRight]);
            animator.PlayAnimation(AnimNames.WizardRight);

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
                // Calculate the direction of the collision
                Vector2 collisionDirection = GameObject.Transform.Position - prePos;
                collisionDirection.Normalize();

                // Project the newPos vector onto the collision direction
                float projection = Vector2.Dot(newPos, collisionDirection);

                // Subtract the projection from the newPos vector to get the allowed movement vector
                Vector2 allowedMovement = newPos - projection * collisionDirection;

                // Apply the allowed movement
                GameObject.Transform.Position = prePos + allowedMovement;
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

    }
}
