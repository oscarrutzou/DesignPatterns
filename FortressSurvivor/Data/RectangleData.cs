using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class RectangleData
    {
        public Rectangle Rectangle { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public RectangleData(int x, int y)
        {
            X = x;
            Y = y;
            this.Rectangle = new Rectangle();
        }

        public void UpdatePosition(GameObject go, int width, int height)
        {
            Rectangle = new Rectangle((int)go.Transform.Position.X + X - width / 2, (int)go.Transform.Position.Y + Y - height / 2, 1, 1);
        }
    }
}
