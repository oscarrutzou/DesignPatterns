using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class ProjectilePool : ObjectPool
    {
        // Isnt used yet
        private static ProjectilePool instance;

        public static ProjectilePool Instance { get { return instance ??= new ProjectilePool(); } }
        
        public override void CleanUp(GameObject gameObject)
        {
            
        }

        public override GameObject CreateObject()
        {
            return ProjectileFactory.Instance.Create();
        }
    }
}
