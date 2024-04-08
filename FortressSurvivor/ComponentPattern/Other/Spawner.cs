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
        private Vector2 potentialSpawnLocation;
        private static int waveNumber;
        public static int defaultEnemyAmount = 15;
        public static int enemyMultiplier = waveNumber;
        private int totalEnemyAmount = defaultEnemyAmount * enemyMultiplier;
        public bool WaveStart = false;
        private List<Vector2> spawnLocations = new List<Vector2>();
        public Spawner(GameObject gameObject) : base(gameObject)
        {
            
        }

        public void PossibleSpawnLocation(Vector2 potentialSpawnLocation)
        {
            Vector2 spawn1 = new Vector2(400, 0);
            Vector2 spawn2 = new Vector2(800, 0);
            Vector2 spawn3 = new Vector2(0, -300);
            Vector2 spawn4 = new Vector2(0, -600);
            Vector2 spawn5 = new Vector2(400, 700);
            Vector2 spawn6 = new Vector2(800, 700);
            Vector2 spawn7 = new Vector2(800, -300);
            Vector2 spawn8 = new Vector2(800,-600);
            spawnLocations.Add(spawn1);
            spawnLocations.Add(spawn2);
            spawnLocations.Add(spawn3);
            spawnLocations.Add(spawn4);
            spawnLocations.Add(spawn5);
            spawnLocations.Add(spawn6);
            spawnLocations.Add(spawn7);
            spawnLocations.Add(spawn8);



        }

        public void WaveStarter()
        {
            if (waveNumber ==1)
            {
                int randomNumber = rnd.Next(0, spawnLocations.Count - 1);
                for (int i = 0; i < totalEnemyAmount; i++)
                {
                    // Spawn enemy on specified locations
                }
            }

            if (waveNumber ==2)
            {
                int randomNumber = rnd.Next(0, spawnLocations.Count - 1);
                for (int i = 0; i < totalEnemyAmount; i++)
                {
                    //
                }
            }

            if (waveNumber == 3)
            {
                int randomNumber = rnd.Next(0, spawnLocations.Count - 1);
                for (int i = 0; i < totalEnemyAmount; i++)
                {

                }
            }
        }
    }
}
