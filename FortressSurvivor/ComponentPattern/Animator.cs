using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace FortressSurvivor
{
    internal class Animator: Component
    {
        public int CurrentIndex { get; private set; }
        private float timeElapsed;
        private SpriteRenderer spriteRenderer;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;

        public Animator(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            if (spriteRenderer == null)
                throw new Exception($"No spriteRenderer on gameObject, and therefore its not possible to Animate");
        }

        public override void Update(GameTime gameTime)
        {
            if (currentAnimation == null) return;

            timeElapsed += GameWorld.DeltaTime;
            CurrentIndex = (int)(timeElapsed * currentAnimation.FPS);

            if (CurrentIndex > currentAnimation.Sprites.Length - 1)
            {
                timeElapsed = 0;
                CurrentIndex = 0;
            }
            spriteRenderer.Sprite = currentAnimation.Sprites[CurrentIndex];
        }

        /// <summary>
        /// Use the normal GlobalAnimation and GlobalTextures from other projects next time. 
        /// Ineffecient to make a new Animation each time
        /// </summary>
        /// <param name="animName"></param>
        /// <param name="spritesNames"></param>
        /// <param name="fps"></param>
        /// <returns></returns>
        private Animation BuildAnimation(string animName, string[] spritesNames, float fps)
        {
            Texture2D[] sprites = new Texture2D[spritesNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = GameWorld.Instance.Content.Load<Texture2D>(spritesNames[i]);
            }

            return new Animation(animName, sprites, fps);
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);
        }

        public void PlayAnimation(string animationName)
        {
            try
            {
                currentAnimation = animations[animationName];
            }
            catch (Exception)
            {
                throw new Exception($"Cant find the animation called {animationName} in Animations Dict");
            }
        }
    }
}
