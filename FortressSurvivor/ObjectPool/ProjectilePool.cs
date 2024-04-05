using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class ProjectilePool : ObjectPool
    {

        private static ProjectilePool instance;

        public static ProjectilePool Instance { get { return instance ??= new ProjectilePool(); } }
        
        protected override void CleanUp(GameObject gameObject)
        {
            
        }

        protected override GameObject CreateObject()
        {
            return new GameObject();
        }
    }
}
