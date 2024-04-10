//using FortressSurvivor.CommandPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace FortressSurvivor
{
    public class OscarTestScene : Scene
    {
        private Grid grid;
        private GameObject gridGameobject, astarGameobject, playerGameObject, spawnerGameObject, towerGameObject;
        private Astar astar;
        private Stats towerStats;

        public override void Initialize()
        {
            playerGameObject = SpawnPlayer();
            Instantiate(playerGameObject);


            gridGameobject = new GameObject();
            grid = gridGameobject.AddComponent<Grid>();
            grid.GenerateGrid(Vector2.Zero, 20, 20);
            SetCenterGridUnwalkable();
            SetGridTargetPoints();

            astarGameobject = new GameObject();
            astar = new Astar(astarGameobject, grid);

            InitSpawner();
            InitCurrency();


            InitCommands();
        }


        #region Init and Start 
        private void InitCurrency()
        {
            GameObject towerHealth = new()
            {
                Type = GameObjectTypes.Gui
            };
            towerHealth.AddComponent<SpriteRenderer>();
            towerHealth.AddComponent<TowerHealthUI>(towerGameObject);
            Instantiate(towerHealth);
        }

        private void InitSpawner()
        {
            spawnerGameObject = new GameObject();
            spawnerGameObject.AddComponent<Spawner>(grid, towerGameObject);
        }

        private void InitCommands()
        {
            Player player = playerGameObject.GetComponent<Player>() as Player;
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(player, new Vector2(1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(player, new Vector2(-1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(player, new Vector2(0, -1)));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(player, new Vector2(0, 1)));
            InputHandler.Instance.AddButtonDownCommand(Keys.F, new WaveCommand(spawnerGameObject));
            //InputHandler.Instance.AddButtonDownCommand(Keys.K, new UpgradeCommand(player));

            InputHandler.Instance.AddUpdateCommand(Keys.Q, new AstarTestCommand(astar, grid));
            InputHandler.Instance.AddButtonDownCommand(ButtonState.Pressed, new ShootCommand(player));
            //InputHandler.Instance.AddButtonDownCommand(Keys.Space, new ShootCommand(player));
        }

        /// <summary>
        /// Sets the center cells to unwalkable so the ai dosent go though the castle.
        /// </summary>
        private void SetCenterGridUnwalkable()
        {
            for (int i = 8; i < 12; i++)
            {
                for (int j = 8; j < 12; j++)
                {
                    Cell cell = grid.Cells[new Point(i, j)].GetComponent<Cell>();
                    cell.isValid = false;
                    grid.Cells[new Point(i, j)].GetComponent<SpriteRenderer>().Color = Color.Black;
                }
            }
        }

        private void SetGridTargetPoints()
        {
            int min = 7;
            int maxInclusive = 12;
            for (int i = min;i <= maxInclusive; i++) 
            {
                for (int j = 7; j <= maxInclusive; j++)
                {
                    if ((i == min || i == maxInclusive || j == min || j == maxInclusive) && !(i == min && j == min) && !(i == min && j == maxInclusive) && !(i == maxInclusive && j == min) && !(i == maxInclusive && j == maxInclusive))
                    {
                        Point targetPoint = new Point(i, j);
                        grid.TargetPoints.Add(targetPoint);
                        grid.Cells[targetPoint].GetComponent<SpriteRenderer>().Color = Color.HotPink;
                    }
                }
            }
        }
        private GameObject SpawnPlayer()
        {
            GameObject fogOfWar = new GameObject();
            fogOfWar.AddComponent<SpriteRenderer>();
            fogOfWar.AddComponent<FogOfWar>();
            Instantiate(fogOfWar);

            towerGameObject = new GameObject();
            towerGameObject.AddComponent<SpriteRenderer>().SetSprite(TextureNames.Pixel);
            int collisionBoxDem = Cell.demension * (int)Cell.scaleSize.X * 4;
            towerGameObject.AddComponent<Collider>().SetCollisionBox(collisionBoxDem, collisionBoxDem);
            towerStats = towerGameObject.AddComponent<Stats>(1000, 0);
            Instantiate(towerGameObject);

            GameObject playerGo = new GameObject();
            playerGo.Transform.Scale = new Vector2(4, 4);
            playerGo.AddComponent<SpriteRenderer>();
            playerGo.AddComponent<Animator>();
            playerGo.AddComponent<Player>(fogOfWar, towerGameObject);
            playerGo.AddComponent<Collider>();
            playerGo.AddComponent<Stats>();
            return playerGo;
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (towerStats.health <= 0) GameWorld.Instance.Exit();
            base.Update(gameTime);
        }
        public override void DrawInWorld(SpriteBatch spriteBatch)
        {
            base.DrawInWorld(spriteBatch);
        }

        public override void DrawOnScreen(SpriteBatch spriteBatch)
        {
            base.DrawOnScreen(spriteBatch);
        }

    }
}
