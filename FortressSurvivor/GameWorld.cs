using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public class GameWorld : Game
    {

        private static GameWorld instance;

        public static GameWorld Instance { get { return instance ??= new GameWorld(); } }

        public Dictionary<ScenesNames, Scene> scenes { get; private set; }
        public Scene currentScene;
        public Camera worldCam { get; set; }
        public Camera uiCam { get; private set; } //Static on the ui
        public static float DeltaTime { get; private set; }
        public GraphicsDeviceManager gfxManager { get; private set; }
        private SpriteBatch _spriteBatch;
        
        private List<IObserver> observers = new List<IObserver>();



        private GameWorld()
        {
            gfxManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Fortress Survivor";
        }

        protected override void Initialize()
        {


            ResolutionSize(1280, 720);
            //Fullscreen();
            worldCam = new Camera(new Vector2(gfxManager.PreferredBackBufferWidth / 2, gfxManager.PreferredBackBufferHeight / 2), true);
            uiCam = new Camera(Vector2.Zero, false);

            //GlobalTextures.LoadContent();
            //GlobalAnimations.LoadContent();

            GenerateScenes();
            currentScene = scenes[ScenesNames.OscarTestScene];
            currentScene.Initialize();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputHandler.Instance.Execute();
            currentScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Draw in world objects
            _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise,
                transformMatrix: worldCam.GetMatrix());

            currentScene.DrawInWorld(_spriteBatch);
            _spriteBatch.End();

            //Draw on screen objects
            _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise,
                transformMatrix: uiCam.GetMatrix());

            currentScene.DrawOnScreen(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //private void CheckCollision()
        //{
        //    foreach (GameObject go1 in gameObjects)
        //    {
        //        foreach (GameObject go2 in gameObjects)
        //        {
        //            if (go1 == go2) continue;

        //            // Doesn't check between enemies
        //            Enemy enemy1 = go1.GetComponent<Enemy>();
        //            Enemy enemy2 = go2.GetComponent<Enemy>();
        //            if (enemy1 != null && enemy2 != null) continue;

        //            Collider col1 = go1.GetComponent<Collider>();
        //            Collider col2 = go2.GetComponent<Collider>();

        //            // Broad Phase: Check base collision box
        //            if (col1 != null && col2 != null && col1.CollisionBox.Intersects(col2.CollisionBox))
        //            {
        //                // Narrow Phase: Check individual rectangles
        //                foreach (RectangleData recData1 in col1.rectanglesData.Value)
        //                {
        //                    foreach (RectangleData recData2 in col2.rectanglesData.Value)
        //                    {
        //                        if (recData1.Rectangle.Intersects(recData2.Rectangle))
        //                        {
        //                            go1.OnCollisionEnter(col2);
        //                            go2.OnCollisionEnter(col1);
        //                            return; // Exit early after the first collision
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void GenerateScenes()
        {
            scenes = new Dictionary<ScenesNames, Scene>();
            scenes[ScenesNames.GameScene] = new GameScene();
            scenes[ScenesNames.OscarTestScene] = new OscarTestScene();
            scenes[ScenesNames.ErikTestScene] = new ErikTestScene();
            scenes[ScenesNames.StefanTestScene] = new StefanTestScene();
            scenes[ScenesNames.AsserTestScene] = new AsserTestScene();
        }

        public void ResolutionSize(int width, int height)
        {
            gfxManager.HardwareModeSwitch = true;
            gfxManager.PreferredBackBufferWidth = width;
            gfxManager.PreferredBackBufferHeight = height;
            gfxManager.IsFullScreen = false;
            gfxManager.ApplyChanges();
        }

        public void Fullscreen()
        {
            gfxManager.HardwareModeSwitch = false;
            gfxManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            gfxManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            gfxManager.IsFullScreen = true;
            gfxManager.ApplyChanges();
        }

        public void Instantiate(GameObject go) => currentScene.Instantiate(go);

        public void Destroy(GameObject go) => currentScene.Destroy(go);

    }
}
