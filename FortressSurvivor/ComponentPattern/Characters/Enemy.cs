using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace FortressSurvivor
{
    public enum DirectionState
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum EnemyState
    {
        WalkTowardsGoal,
        Attack,
    }

    public class Enemy : Component
    {
        private Vector2 direction, nextTarget;
        public Point targetPos;
        public List<GameObject> path { get; set; }
        public int speed = 100;
        private float threshold = 5f;

        public Action onGoalReached;
        public DirectionState directionState = DirectionState.Right;
        public EnemyState enemyState = EnemyState.WalkTowardsGoal;
        private Grid grid;
        private Astar astar;
        private GameObject towerGameObject;
        private Stats towerStats;
        private SpriteRenderer spriteRenderer;
        private Random rnd = new Random();
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

        public void SetStartPositions(Grid grid, GameObject towerObject, Point gridPos)
        {
            this.grid = grid;
            this.towerGameObject = towerObject;
            towerStats = towerGameObject.GetComponent<Stats>();
            GameObject.Transform.GridPosition = gridPos;
        }
        
        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            astar = GameObject.GetComponent<Astar>();
            spriteRenderer.SetSprite(TextureNames.Knight);
            GameObject.Transform.Position = grid.GetCellGameObjectFromPoint(GameObject.Transform.GridPosition).Transform.Position;

            targetPos = grid.TargetPoints[rnd.Next(0, grid.TargetPoints.Count)];
            onGoalReached += OnGoalReached;

            SetPath();
        }

        public override void Update(GameTime gameTime)
        {

            switch (enemyState)
            {
                case EnemyState.WalkTowardsGoal:
                    UpdatePathing(gameTime);
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
            }


        }

        //public override void OnCollisionEnter(Collider collider)
        //{
        //    if (collider.GameObject.GetComponent<Projectile>() != null)
        //    {
        //        EnemyPool.Instance.ReleaseObject(GameObject);
        //        GameWorld.Instance.Destroy(collider.GameObject);
        //    }
        //}


        private void OnGoalReached()
        {
            enemyState = EnemyState.Attack;
            spriteRenderer.SpriteEffects = SpriteEffects.None;
        }

        #region PathFinding
        private void SetPath()
        {
            MakePath();

            if (path.Count > 0)
            {
                SetNextTargetPos(path[0]); // Set the next target
            }
        }

        private void MakePath()
        {
            for (int i = 0; i < 3; i++) 
            {
                if (path != null && path.Count > 0) break;

                path = astar.FindPath(GameObject.Transform.GridPosition, targetPos);
            }
            if (path == null) throw new Exception("Cant find a path");
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
                    GameObject.Transform.GridPosition = path[0].Transform.GridPosition; //So we update the grid position
                    path.RemoveAt(0); // Remove the current target from the path
                    SetNextTargetPos(path[0]); // Set the next target
                }
                else if (path.Count == 1) // If it's the last cell in the path
                {
                    GameObject.Transform.GridPosition = path[0].Transform.GridPosition; //So we update the grid position
                    SetNextTargetPos(path[0]); // Set the last target
                    path.RemoveAt(0); // Remove the goal cell from the path
                }
            }

            // Calculate the direction to the next target
            direction = Vector2.Normalize(nextTarget - position);

            // Move the player towards the next target
            GameObject.Transform.Translate(direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            // Check if the player has reached the goal
            if (path.Count == 0 && Vector2.Distance(position, nextTarget) < threshold)
            {
                if (onGoalReached != null)
                {
                    onGoalReached.Invoke();
                    onGoalReached = null; // Prevent onGoalReached from being called more than once
                }

                path = null;
            }
            UpdateDirection();
        }

        private void SetNextTargetPos(GameObject cellGo)
        {
            nextTarget = cellGo.Transform.Position + new Vector2(0, -Cell.demension / 2);
        }
        #endregion

        private float attackTimer;
        private readonly float attackCooldown = 2f;
        private void Attack()
        {
            attackTimer -= GameWorld.DeltaTime;
            
            if (attackTimer < 0)
            {
                attackTimer = attackCooldown;
                GameObject.GetComponent<Stats>().DealDamage(towerGameObject);
            }
        }

        public void UpdateDirection()
        {
            if (direction.X >= 0)
            {
                directionState = DirectionState.Right;
                spriteRenderer.SpriteEffects = SpriteEffects.None;

            }
            else if (direction.X < 0)
            {
                directionState = DirectionState.Left;
                spriteRenderer.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (direction.Y > 0)
            {
                directionState = DirectionState.Down;
            }
            else if (direction.Y < 0)
            {
                directionState = DirectionState.Up;
            }
        }
    }
}
