using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class ObserverComponent : Component, IObserver
    {
        public ObserverComponent(GameObject gameObject) : base(gameObject)
        {
        }

        public void Update()
        {
            
        }
    }
}
