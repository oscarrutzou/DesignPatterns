using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public enum ScenesNames
    {
        //MainMenu,
        //LoadingScreen,
        GameScene,
        OscarTestScene,
        StefanTestScene,
        ErikTestScene,
        AsserTestScene,
        //EndMenu,
    }


    public abstract class Scene
    {
        // We have a data stored on each scene, to make it easy to add and remove gameObjects
        public bool hasFadeOut;
        public bool isPaused;

        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destoroyedGameObjects = new List<GameObject>();

        public abstract void Initialize();

        /// <summary>
        /// The base update on the scene handles all the gameobjects and calls Update on them all. 
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            CleanUp();

            foreach (GameObject gameObject in SceneData.gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }


        public void Instantiate(GameObject gameObject) => newGameObjects.Add(gameObject);

        public void Destroy(GameObject go) => destoroyedGameObjects.Add(go);

        private void CleanUp()
        {
            if (newGameObjects.Count == 0 && destoroyedGameObjects.Count == 0) return; //Shouldnt run since there is no new changes

            for (int i = 0; i < newGameObjects.Count; i++)
            {
                SceneData.gameObjects.Add(newGameObjects[i]);
                AddToCategory(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();
            }
            for (int i = 0; i < destoroyedGameObjects.Count; i++)
            {
                SceneData.gameObjects.Remove(destoroyedGameObjects[i]);
                RemoveFromCategory(destoroyedGameObjects[i]);
            }

            newGameObjects.Clear();
            destoroyedGameObjects.Clear();
        }

        private void AddToCategory(GameObject gameObject)
        {
            switch (gameObject.Type)
            {
                case GameObjectTypes.Cell:
                    SceneData.cells.Add(gameObject.GetComponent<Cell>());
                    break;
                case GameObjectTypes.Gui:
                    SceneData.guis.Add(gameObject);
                    break;
                default:
                    SceneData.defaults.Add(gameObject);
                    break;
            }
        }

        private void RemoveFromCategory(GameObject gameObject)
        {
            switch (gameObject.Type)
            {
                case GameObjectTypes.Cell:
                    SceneData.cells.Remove(gameObject.GetComponent<Cell>());
                    break;
                case GameObjectTypes.Gui:
                    SceneData.guis.Remove(gameObject);
                    break;
                default:
                    SceneData.defaults.Remove(gameObject);
                    break;
            }
        }

        public virtual void DrawInWorld(SpriteBatch spriteBatch)
        {
            //DrawSceenColor();

            //// Draw all GameObjects that is not Gui in the active scene.
            foreach (GameObject gameObject in SceneData.gameObjects)
            {
                if (gameObject.Type != GameObjectTypes.Gui)
                {
                    gameObject.Draw(spriteBatch);
                }
            }
        }

        public virtual void DrawOnScreen(SpriteBatch spriteBatch)
        {
            // Draw all Gui GameObjects in the active scene.
            foreach (GameObject guiGameObject in SceneData.guis)
            {
                guiGameObject.Draw(spriteBatch);
            }
        }


    }
}
