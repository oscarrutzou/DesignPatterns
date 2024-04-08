using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class HealthDamage : Component
    {
        public int health;
        public int damage;

        public HealthDamage(GameObject gameObject) : base(gameObject)
        {
        }

        public HealthDamage(GameObject gameObject, int health, int damage) : base(gameObject)
        {
            this.health = health;
            this.damage = damage;
        }

        public void DealDamage(GameObject damageGo, int damage)
        {
            HealthDamage damageGoHealth = damageGo.GetComponent<HealthDamage>();
            damageGoHealth.TakeDamage(damage);
        }

        private void TakeDamage(int damage)
        {
            health -= damage;
            //Delete or add to pool.
            if (health > 0) return;

            Enemy enemy = GameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

            }
            else
            {
                GameWorld.Instance.Destroy(GameObject);
            }
            //else if (Game)
            //{

            //}
        }
    }
}
