using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
     class Projectile : Component
    {
        

        private float speed;

        private Vector2 velocity;
        private Vector2 direction;

        public Projectile(GameObject gameObject) : base(gameObject)
        {
            this.speed = 500;

            
        }

       

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Move(mouseState);
        }

        private void Move(MouseState mouseState)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            direction = mousePosition - GameObject.Transform.Position;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            velocity = direction * speed;

            //GameObject.Transform.Position += velocity * (float)GameWorld.Instance.DeltaTime;

            GameObject.Transform.Translate(velocity * GameWorld.Instance.DeltaTime);

            if (GameObject.Transform.Position.Y <0 || GameObject.Transform.Position.X <0)
            {
                GameWorld.Instance.Destroy(this.GameObject);
            }
        }
    }
}
