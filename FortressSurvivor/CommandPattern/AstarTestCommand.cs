using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FortressSurvivor
{
    public class AstarTestCommand : ICommand
    {
        private Astar astar;
        private Grid grid;

        public AstarTestCommand(Astar astar, Grid grid)
        {
            this.astar = astar;
            this.grid = grid;
        }

        public void Execute()
        {
            grid.GetCellGameObjectFromPoint(new Point(2,2)).GetComponent<Cell>().isValid = false;
            astar.FindPath(new Point(0, 0), new Point(3, 3));
        }
    }
}
