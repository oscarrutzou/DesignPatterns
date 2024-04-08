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
        private Random rnd = new Random();

        public override GameObject Create()
        {
            GameObject go = new GameObject();
            ENEMYTYPE type = (ENEMYTYPE)rnd.Next(0, Enum.GetValues(typeof(ENEMYTYPE)).Length);

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

        public GameObject Create(Grid grid, GameObject towerGameObject, Point point)
        {
            GameObject go = new GameObject();

            go.AddComponent<SpriteRenderer>().SetLayerDepth(LAYERDEPTH.Enemies);
            go.AddComponent<Enemy>().SetStartPositions(grid, towerGameObject, point);
            go.AddComponent<Astar>(grid);
            go.AddComponent<Collider>();
            go.AddComponent<Stats>(100, 25);

            return go;
        }

    }
}
