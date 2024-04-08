using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public static class SceneData
    {
        public static List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Still uses the GameObject, just faster to use the reference to the Cell insted of the gameobject.
        /// </summary>
        public static List<Cell> cells = new List<Cell>();
        public static List<Projectile> projectiles = new List<Projectile>();
        public static List<GameObject> guis = new List<GameObject>();
        public static List<GameObject> defaults = new List<GameObject>();
    }
}
