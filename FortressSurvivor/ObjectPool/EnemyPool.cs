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

        private static Random rnd = new Random();
        public int maxAmount = 15;

        public override GameObject CreateObject()
        {
            return EnemyFactory.Instance.Create(); 
        }

        public GameObject CreateObject(Grid grid, Point point) => EnemyFactory.Instance.Create(grid, point);

        public override void CleanUp(GameObject gameObject)
        {
           
        }

    }
}
