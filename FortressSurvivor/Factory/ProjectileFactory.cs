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

        public static ProjectileFactory Instance { get { return instance ??= new ProjectileFactory(); } }
        
       
        
        public override GameObject Create()
        {
            GameObject arrow = new GameObject();
            SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();
            sr.SetSprite("arrow1");
            sr.SetLayerDepth(LAYERDEPTH.Player);
            arrow.AddComponent<Collider>();
            arrow.AddComponent<Projectile>();
            GameWorld.Instance.Instantiate(arrow);
            return arrow;
        }

       
    }
}
