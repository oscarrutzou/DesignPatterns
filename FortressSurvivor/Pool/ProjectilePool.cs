using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor.Pool
{
    internal class ProjectilePool : ObjectPool
    {
        protected override void CleanUp(GameObject gameObject)
        {
            
        }

        protected override GameObject CreateObject()
        {
            throw new NotImplementedException();
        }
    }
}
