﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor.ComponentPattern
{
    internal class Spawner : Component
    {
        public Spawner(GameObject gameObject) : base(gameObject)
        {
        }
    }
}