using Microsoft.Xna.Framework;

namespace FortressSurvivor
{
    internal class Cell : Component
    {
        public static int demension = 16;
        public static Vector2 scaleSize = new Vector2(4, 4);

        public int cost = 1;
        public int G;
        public int H;
        public int F => G + H;

        public Point gridPosition;

        public Cell Parent { get; set; }


        public Cell(GameObject gameObject, Grid grid, Point point) : base(gameObject)
        {
            this.gridPosition = point;
            gameObject.Transform.Position = grid.startPostion + new Vector2(gridPosition.X * demension * scaleSize.X + demension * scaleSize.X / 2, gridPosition.Y * demension * scaleSize.Y + demension * scaleSize.Y / 2);
            GameObject.Transform.Scale = scaleSize;
        }

        public void Reset()
        {
            Parent = null;
            G = 0;
            H = 0;
        }

    }
}
