using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FortressSurvivor
{
    public class QuitCommand : ICommand
    {

        public void Execute()
        {
            GameWorld.Instance.Exit();
        }
    }
}
