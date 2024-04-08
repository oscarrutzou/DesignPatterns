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
        public int maxAmount = 45;

        protected override GameObject CreateObject()
        {
            return EnemyFactory.Instance.Create(); 
        }


        protected override void CleanUp(GameObject gameObject)
        {
           
        }

    }
}
