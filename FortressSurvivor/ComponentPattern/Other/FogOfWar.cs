using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class FogOfWar : Component
    {

       
        public FogOfWar(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("FogofWar");
            
        }
    }


}
