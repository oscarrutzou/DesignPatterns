using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class HealthDamage : Component
    {
        public int health;
        public int damage;
        public HealthDamage(GameObject gameObject) : base(gameObject)
        {
            health = 100;
            damage = 15;

        }
    }
}
