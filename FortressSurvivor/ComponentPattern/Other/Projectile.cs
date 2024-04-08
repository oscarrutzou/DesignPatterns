using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class Projectile : Component
    {
        

        private float speed;

        private Vector2 direction;
        private Stats stats;
        public Projectile(GameObject gameObject) : base(gameObject)
        {
        }

        public Projectile(GameObject gameObject, Stats stats) : base(gameObject)
        {
            this.stats = stats;
            this.speed = 400;
            SetDirection();
        }

        
        private void SetDirection()
        {
            direction = (InputHandler.Instance.mouseOnUI) - GameObject.Transform.Position;
            if (direction != Vector2.Zero) direction.Normalize();

            float angle = (float)Math.Atan2(direction.Y, direction.X);

            // Add π/2 to the angle to rotate 90 degrees clockwise
            angle += MathHelper.PiOver2;

            // Ensure the angle is within the range of 0 to 2π
            if (angle < 0) angle += MathHelper.TwoPi;
            else if (angle > MathHelper.TwoPi) angle -= MathHelper.TwoPi;

            GameObject.Transform.Rotation = angle;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }

        private void Move()
        {
            GameObject.Transform.Translate(direction * speed * GameWorld.DeltaTime);

            Vector2 screenPosition = Vector2.Transform(GameObject.Transform.Position, GameWorld.Instance.worldCam.GetMatrix());
            if (screenPosition.X < 0
                || screenPosition.Y < 0
                || screenPosition.X > GameWorld.Instance.gfxManager.PreferredBackBufferWidth
                || screenPosition.Y > GameWorld.Instance.gfxManager.PreferredBackBufferHeight)
            {
                // Remove the GameObject here
                GameWorld.Instance.Destroy(GameObject);
            }
        }

        private bool colCalled;
        public override void OnCollisionEnter(Collider collider)
        {
            if (colCalled) return;
            if (collider.GameObject.GetComponent<Enemy>() != null)
            {
                GameWorld.Instance.Destroy(GameObject);
                stats.DealDamage(collider.GameObject);
                colCalled = true;
            }
        }


    }
}
