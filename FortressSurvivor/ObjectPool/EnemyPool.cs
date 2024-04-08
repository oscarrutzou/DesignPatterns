using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FortressSurvivor
{
    public class EnemyPool : ObjectPool
    {

        private static EnemyPool instance;

        public static EnemyPool Instance { get { return instance ??= new EnemyPool(); } }


        public void PopulatePool(Grid grid, GameObject towerGameObject, Point point)
        {
            for (int i = 0; i < maxAmount; i++)
            {
                GameObject go = CreateObject(grid, towerGameObject, point);
                ReleaseObject(go);

            }

        }

        public GameObject GetObject(Grid grid, GameObject towerObject, Point point)
        {
            if (active.Count == maxAmount) return null;
            if (inactive.Count == 0)
            {
                return CreateObject(grid, towerObject, point);
            }
            GameObject go = inactive.Pop();
            go.GetComponent<Enemy>().SetStartPositions(grid, towerObject, point);

            active.Add(go);
            return go;
        }

        public override void ReleaseObject(GameObject gameObject)
        {
            active.Remove(gameObject);
            if (active.Count + inactive.Count < maxAmount)
            {
                inactive.Push(gameObject);
            }

            GameWorld.Instance.Destroy(gameObject); //Removes gameobject
            CleanUp(gameObject);
        }

        public override GameObject CreateObject()
        {
            GameObject go = EnemyFactory.Instance.Create();
            active.Add(go);
            return go;
        }

        public GameObject CreateObject(Grid grid, GameObject towerGameObject, Point point)
        {
            GameObject go = EnemyFactory.Instance.Create(grid, towerGameObject, point);
            active.Add(go);
            return go;
        }

        public override void CleanUp(GameObject gameObject)
        {
            gameObject.Transform.GridPosition = Point.Zero;
            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.path = null;
            enemy.enemyState = EnemyState.WalkTowardsGoal;
        }
    }
}
