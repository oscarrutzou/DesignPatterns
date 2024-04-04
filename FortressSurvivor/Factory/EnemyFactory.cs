using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public enum ENEMYTYPE
    {
        Slow,
        Medium,
        //Fast
    }

    public class EnemyFactory : Factory
    {
        private static EnemyFactory instance;
        public static EnemyFactory Instance { get { return instance ??= new EnemyFactory(); } }

        public override GameObject Create()
        {
            GameObject go = new GameObject();
            return go;
        }

        public GameObject CreateEnemy(ENEMYTYPE type)
        {
            GameObject go = new GameObject();
            //SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            //sr.SetLayerDepth(LAYERDEPTH.Enemy);

            //go.AddComponent<Collider>();

            //switch (type)
            //{
            //    case ENEMYTYPE.Slow:
            //        sr.SetSprite("enemyBlack1");
            //        go.AddComponent<Enemy>(new DownEnemyState<Enemy>());
            //        break;
            //    case ENEMYTYPE.Medium:
            //        sr.SetSprite("enemyGreen1");
            //        go.AddComponent<Enemy>(new ChangeDirectionEnemyState<Enemy>());
            //        break;
            //}

            //switch (type)

            return go;
        }
    }
}
