﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor.CommandPattern
{
    internal class WaveCommand : ICommand
    {
        private Spawner spawner;
        public void Execute()
        {
            spawner.WaveStarter();
        }
    }
}