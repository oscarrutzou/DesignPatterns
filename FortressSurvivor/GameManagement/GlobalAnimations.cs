using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace FortressSurvivor
{
    public enum AnimNames
    {
        TestAnim,
        WizardRight,
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations
        public static Dictionary<AnimNames, Animation> animations { get; private set; }

        public static void LoadContent()
        {
            animations = new Dictionary<AnimNames, Animation>();
            //Can upload sprite sheets, left to right
            LoadSpriteSheet(AnimNames.WizardRight, "wizardRight", 5, 32);
            //wizardRight
            //How to use. Each animation should be called _0, then _1 and so on, on each texuture.
            //Remember the path should show everything and just delete the number. But keep the "_".
            //LoadIndividualFramesAnimationT(AnimNames.FighterDead, "Persons\\Worker\\FigtherTestDead_", 2);
        }

        private static void LoadSpriteSheet(AnimNames animName, string path, float fps, int dem)
        {
            Texture2D[] sprite = new Texture2D[]{
                GameWorld.Instance.Content.Load<Texture2D>(path)
            };
            animations.Add(animName, new Animation(animName, sprite, fps, dem));
        }

        private static void LoadIndividualFramesAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation

        }
    }
}
