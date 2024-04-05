using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
     public class ProjectileFactory : Factory
    {
        private static ProjectileFactory instance;

        public static ProjectileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectileFactory();
                }
                return instance;
            }
        }
        
       
        
        public override GameObject Create()
        {
            GameObject arrow = new GameObject();
            SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();
            sr.SetSprite("arrow1");
            arrow.AddComponent<Collider>();
            GameWorld.Instance.Instantiate(arrow);
            return arrow;
        }

       
    }
}
