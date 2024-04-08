using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class UpgradeCommand : ICommand
    {
      
        private Stats playerHealthDamage;
        public UpgradeCommand(Player player)
        {
           playerHealthDamage = player.GameObject.GetComponent<Stats>() as Stats;

        }
        public void Execute()
        {
            playerHealthDamage.health += 50;
            playerHealthDamage.damage += 15;
        }
    }
}
