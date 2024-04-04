using FortressSurvivor.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor.Pool
{
    internal class EnemyPool : ObjectPool
    {

        private static EnemyPool instance;

        public static EnemyPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyPool();
                }
                return instance;
            }
        }
        protected override void CleanUp(GameObject gameObject)
        {
           
        }

        protected override GameObject CreateObject()
        {
            return EnemyFactory;
        }
    }
}
