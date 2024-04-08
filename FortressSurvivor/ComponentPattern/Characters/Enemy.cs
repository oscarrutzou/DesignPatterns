using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public enum DirectionState
    {
        Left,
        Right,
        Up,
        Down
    }

    public class Enemy : Component
    {
        private Vector2 direction, nextTarget;
        private List<Cell> path;
        public int speed = 100;
        private float threshold = 5f;

        public Action onGoalReached;
        public DirectionState directionState = DirectionState.Right;
        public Point gridPosition;

        //private Dictionary<DirectionState, Animation> animationsDict = new Dictionary<DirectionState, Animation>()
        //{
        //    {DirectionState.Left, GlobalAnimations.animations[AnimNames.WizardLeft] },
        //    {DirectionState.Right, GlobalAnimations.animations[AnimNames.WizardRight] },
        //    {DirectionState.Up, GlobalAnimations.animations[AnimNames.WizardUp] },
        //    {DirectionState.Down, GlobalAnimations.animations[AnimNames.WizardDown] }
        //};


        public Enemy(GameObject gameObject) : base(gameObject)
        {
        }

        public Enemy(GameObject gameObject, Grid grid, Point gridPos) : base(gameObject)
        {
            gridPosition = gridPos;
            //GameObject.Transform.Position = grid.GetCellFromPoint(gridPos).positionCentered + new Vector2(0, -Cell.demension / 2);
            GameObject.Transform.Position = grid.GetCellGameObjectFromPoint(gridPos).Transform.Position;
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("knight");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnCollisionEnter(Collider collider)
        {
            base.OnCollisionEnter(collider);
        }

        //public void Move(Vector2 velocity)
        //{
        //    if (velocity != Vector2.Zero)
        //    {
        //        velocity.Normalize();
        //    }
        //    //Check for the grid if its inside the castle cells
        //    velocity *= speed;
        //    GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        //    GameWorld.Instance.worldCam.Move(velocity * GameWorld.DeltaTime);
        //}
    }
}
