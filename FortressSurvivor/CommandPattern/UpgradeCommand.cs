using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class UpgradeCommand : ICommand
    {
      
        private HealthDamage playerHealthDamage;
        public UpgradeCommand(Player player)
        {
           playerHealthDamage = player.GameObject.GetComponent<HealthDamage>() as HealthDamage;

        }
        public void Execute()
        {
            playerHealthDamage.health += 50;
            playerHealthDamage.damage += 15;
        }
    }
}
