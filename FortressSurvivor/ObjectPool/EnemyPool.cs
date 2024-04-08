using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
     class EnemyPool : ObjectPool
    {

        private static EnemyPool instance;

        public static EnemyPool Instance { get { return instance ??= new EnemyPool(); } }

        private static Random rnd = new Random();
        public int maxAmount = 15;

        protected override GameObject CreateObject()
        {
            // There cant be unused enemytypes, otherwise it will still be able to spawn those types that have not been set.
            return EnemyFactory.Instance.CreateEnemy((ENEMYTYPE)rnd.Next(0, Enum.GetValues(typeof(ENEMYTYPE)).Length)); 
        }


        protected override void CleanUp(GameObject gameObject)
        {
           
        }

    }
}
