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
        private Grid grid;

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
            this.grid = grid;
            gridPosition = gridPos;
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("knight");
            //sr.Origin = Vector2.Zero;
            GameObject.Transform.Position = grid.GetCellGameObjectFromPoint(gridPosition).Transform.Position;

        }

        public override void Update(GameTime gameTime)
        {
            UpdatePathing(gameTime);

            if (path == null)
            {

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnCollisionEnter(Collider collider)
        {
            base.OnCollisionEnter(collider);
        }


        //public void ChangeAnimation(DirectionState directionState)
        //{
        //    animation = animationsDict[directionState];

        //    animation.shouldPlay = true;
        //    animation.frameRate = 8f;
        //    animation.isLooping = true;
        //}

        //public override void Update()
        //{
        //    UpdatePathing();

        //    if (path == null && animation.shouldPlay == true)
        //    {
        //        RightStoppedAnimation();
        //    }

        //}

        //private void RightStoppedAnimation()
        //{
        //    animation = animationsDict[DirectionState.Right];
        //    animation.currentFrame = 0;
        //    animation.shouldPlay = false;
        //}

        public void UpdateDirection()
        {
            if (direction.X >= 0)
            {
                directionState = DirectionState.Right;
            }
            else if (direction.X < 0)
            {
                directionState = DirectionState.Left;
            }
            else if (direction.Y > 0)
            {
                directionState = DirectionState.Down;
            }
            else if (direction.Y < 0)
            {
                directionState = DirectionState.Up;
            }

            //ChangeAnimation(directionState);
        }

        public void SetPlayerPath(List<Cell> path)
        {
            this.path = path;
            if (path.Count > 0)
            {
                SetNextTargetPos(path[0]); // Set the next target
            }
        }
        private void UpdatePathing(GameTime gameTime)
        {
            if (path == null)
                return;
            Vector2 position = GameObject.Transform.Position;

            // If the player has reached the next target, update the next target
            if (Vector2.Distance(position, nextTarget) < threshold)
            {
                if (path.Count > 1) // Check if there's another cell in the path
                {
                    gridPosition = path[0].gridPosition; //So we update the grid position
                    path.RemoveAt(0); // Remove the current target from the path
                    SetNextTargetPos(path[0]); // Set the next target
                }
                else if (path.Count == 1) // If it's the last cell in the path
                {
                    gridPosition = path[0].gridPosition; //So we update the grid position
                    SetNextTargetPos(path[0]); // Set the last target
                    path.RemoveAt(0); // Remove the goal cell from the path
                }
            }

            // Calculate the direction to the next target
            direction = Vector2.Normalize(nextTarget - position);

            // Move the player towards the next target
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if the player has reached the goal
            if (path.Count == 0 && Vector2.Distance(position, nextTarget) < threshold)
            {
                if (onGoalReached != null)
                {
                    onGoalReached.Invoke();
                    onGoalReached = null; // Prevent onGoalReached from being called more than once
                    path = null;
                }
            }
            UpdateDirection();
        }

        private void SetNextTargetPos(Cell cell)
        {
            nextTarget = cell.GameObject.Transform.Position + new Vector2(0, -Cell.demension / 2);
        }
    }
}
