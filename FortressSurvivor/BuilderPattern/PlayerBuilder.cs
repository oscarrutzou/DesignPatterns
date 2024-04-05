using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();
        }

        private void BuildComponents()
        {
            gameObject.AddComponent<Player>();
            gameObject.AddComponent<SpriteRenderer>();
            gameObject.AddComponent<Collider>();
            //Animator animator = gameObject.AddComponent<Animator>();
            //animator.AddAnimation(BuildAnimation("Forward", new string[] { "1fwd", "2fwd", "3fwd" }, 5));

            //animator.PlayAnimation("Forward");
        }

        private Animation BuildAnimation(string animName, string[] spritesNames, float fps)
        {
            Texture2D[] sprites = new Texture2D[spritesNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = GameWorld.Instance.Content.Load<Texture2D>(spritesNames[i]);
            }

            return new Animation(animName, sprites, fps);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
