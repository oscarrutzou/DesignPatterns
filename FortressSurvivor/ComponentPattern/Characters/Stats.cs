using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class Stats : Component
    {
        public int health = 100;
        public int damage = 10;

        public Stats(GameObject gameObject) : base(gameObject)
        {
        }

        public Stats(GameObject gameObject, int health, int damage) : base(gameObject)
        {
            this.health = health;
            this.damage = damage;
        }

        public void DealDamage(GameObject damageGo)
        {
            Stats damageGoHealth = damageGo.GetComponent<Stats>();
            damageGoHealth.TakeDamage(damage);
        }

        private void TakeDamage(int damage)
        {
            int newHealth = health - damage;
            
            if (newHealth < 0) health = 0;
            else health = newHealth;

            //Delete or add to pool.
            if (health > 0) return;

            Enemy enemy = GameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                EnemyPool.Instance.ReleaseObject(GameObject);
            }
            else
            {
                GameWorld.Instance.Destroy(GameObject);
            }
        }
    }
}
