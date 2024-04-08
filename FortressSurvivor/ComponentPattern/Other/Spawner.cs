using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FortressSurvivor
{
    internal class Spawner : Component
    {
        private Random rnd = new Random();
        private static int waveNumber = 1;
        public static int defaultEnemyAmount = 5;
        private int totalEnemyAmount = defaultEnemyAmount * waveNumber;
        public bool WaveStart = false;
        private Grid grid;
        private List<Point> spawnLocations = new List<Point>() {
            new Point(0, 9),
            new Point(9, 0),
            new Point(19, 9),
            new Point(9, 19),
        };

        private GameObject tower;

        public Spawner(GameObject gameObject) : base(gameObject)
        {
        }

        public Spawner(GameObject gameObject, Grid grid, GameObject towerObject) : base(gameObject)
        {
            this.grid = grid;
            this.tower = towerObject;
            EnemyPool.Instance.PopulatePool(grid, tower, spawnLocations[0]);
        }



        public void SpawnWave()
        {
            int spawnLocation = 0;
            for (int i = 0; i < totalEnemyAmount; i++)
            {
                if (spawnLocation == spawnLocations.Count) spawnLocation = 0;

                GameObject go = EnemyPool.Instance.GetObject(grid, tower, spawnLocations[spawnLocation]);
                if (go != null) GameWorld.Instance.Instantiate(go);

                spawnLocation++;
            }
        }
    }
}
