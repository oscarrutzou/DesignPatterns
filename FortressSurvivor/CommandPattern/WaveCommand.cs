using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    internal class WaveCommand : ICommand
    {
        private Spawner spawner;

        public WaveCommand(GameObject spawner)
        {
            this.spawner = spawner.GetComponent<Spawner>();
        }

        public void Execute()
        {
            spawner.SpawnWave();
        }
    }
}
