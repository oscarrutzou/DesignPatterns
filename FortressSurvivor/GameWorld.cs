using FortressSurvivor.CommandPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public class GameWorld : Game, ISubject
    {

        private static GameWorld instance;

        public static GameWorld Instance { get { return instance ??= new GameWorld(); } }



        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destoroyedGameObjects = new List<GameObject>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<IObserver> observers = new List<IObserver>();

        public static float DeltaTime { get; private set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        private SpriteBatch _spriteBatch;
        private Grid grid;
        private GameObject gridGameobject;
        private Texture2D _buttontexture;
        private Button _button;

        private float timeSpawn;
        private float timeBetweenSpawn;

        private GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
 
            Director playerDirector = new Director(new PlayerBuilder());
            GameObject playerGo = playerDirector.Contruct();
            Instantiate(playerGo);

            grid = new Grid();
            gridGameobject = new GameObject();
            grid.GenerateGrid(gridGameobject, new Vector2(Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight / 2), 5, 5);
            //grid.startPostion = new Vector2(Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight / 2);
            //grid.mapW = 5; grid.mapH = 5;

            //GameObject cellGo = new GameObject();
            //SpriteRenderer sr = cellGo.AddComponent<SpriteRenderer>();
            //sr.SetSprite("World\\Tile_overlay");
            //cellGo.AddComponent<Cell>(grid, new Point(0,0));
            //cellGo.AddComponent<Collider>();
            //Instantiate(cellGo);


            Player player = playerGo.GetComponent<Player>() as Player;
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(player, new Vector2(1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(player, new Vector2(-1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(player, new Vector2(0, -1)));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(player, new Vector2(0, 1)));
            InputHandler.Instance.AddButtonDownCommand(Keys.F, new WaveCommand());
            InputHandler.Instance.AddButtonDownCommand(Keys.K, new UpgradeCommand(player));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }
            InputHandler.Instance.Execute();

            CheckCollision();

            CleanUp();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckCollision()
        {
            foreach (GameObject go1 in gameObjects)
            {
                foreach (GameObject go2 in gameObjects)
                {
                    if (go1 == go2) continue;
                    //Dosent check between enemies
                    Enemy enemy1 = go1.GetComponent<Enemy>() as Enemy;
                    Enemy enemy2 = go2.GetComponent<Enemy>() as Enemy;
                    if (enemy1 != null && enemy2 != null) continue; //Shouldnt make collisions between 2 enemies.

                    Collider col1 = go1.GetComponent<Collider>() as Collider;
                    Collider col2 = go2.GetComponent<Collider>() as Collider;

                    //Check base collisionbox
                    if (col1 != null && col2 != null && col1.CollisionBox.Intersects(col2.CollisionBox))
                    {
                        foreach (RectangleData recData1 in col1.rectanglesData.Value)
                        {
                            foreach (RectangleData recData2 in col2.rectanglesData.Value)
                            {
                                if (recData1.Rectangle.Intersects(recData2.Rectangle))
                                {
                                    go1.OnCollisionEnter(col2);
                                    go2.OnCollisionEnter(col1);
                                }
                            }
                        }
                    }   
                }
            }
        }


        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        public void Destroy(GameObject go)
        {
            destoroyedGameObjects.Add(go);
        }

        private void CleanUp()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                gameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();
            }
            for (int i = 0; i < destoroyedGameObjects.Count; i++)
            {
                gameObjects.Remove(destoroyedGameObjects[i]);
            }

            newGameObjects.Clear();
            destoroyedGameObjects.Clear();
        }

        private void SpawnEnemies()
        {
            timeSpawn -= DeltaTime;

            if (timeSpawn <= 0)
            {
                timeSpawn = timeBetweenSpawn;

                if (EnemyPool.Instance.active.Count < EnemyPool.Instance.maxAmount)
                {
                    Instantiate(EnemyPool.Instance.GetObject());
                    Notify();
                }
            }
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Use this method when there is something that happends, like if there is added or removed gameobjects
        /// </summary>
        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}
