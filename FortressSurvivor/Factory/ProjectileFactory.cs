using Microsoft.Xna.Framework;
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
            arrow.Transform.Scale = new Vector2(0.4f, 0.4f);
            SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();
            sr.SetSprite(TextureNames.Arrow);
            sr.SetLayerDepth(LAYERDEPTH.Player);
            arrow.AddComponent<Collider>().SetCollisionBox(30, 30);
            Stats stats = arrow.AddComponent<Stats>();
            stats.damage = 50;
            arrow.AddComponent<Projectile>(stats);

            return arrow;
        }

       
    }
}
