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
            gameObjects.Add(playerGo);

            Player player = playerGo.GetComponent<Player>() as Player;

            foreach (GameObject go in gameObjects)
            {
                go.Awake();
            }

            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(player, new Vector2(1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(player, new Vector2(-1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(player, new Vector2(0, -1)));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(player, new Vector2(0, 1)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            foreach (GameObject go in gameObjects)
            {
                go.Start();
            }
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

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

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

        //private void SpawnEnemies()
        //{
        //    timeSpawn -= DeltaTime;

        //    if (timeSpawn <= 0)
        //    {
        //        timeSpawn = timeBetweenSpawn;

        //        if (EnemyPool.Instance.active.Count < EnemyPool.Instance.maxAmount)
        //        {
        //            Instantiate(EnemyPool.Instance.GetGameObject());
        //            Notify();
        //        }
        //    }
        //}

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
