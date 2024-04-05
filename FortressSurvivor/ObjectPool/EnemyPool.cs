using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class EnemyPool : ObjectPool
    {

        private static EnemyPool instance;

        public static EnemyPool Instance { get { return instance ??= new EnemyPool(); } }

        protected override GameObject CreateObject()
        {
            return new GameObject();
        }

        protected override void CleanUp(GameObject gameObject)
        {
           
        }

    }
}
