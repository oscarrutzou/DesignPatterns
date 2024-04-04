using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FortressSurvivor
{
    internal class Grid
    {
        public Vector2 startPostion { get; private set; }

        private GameObject parent;
        public Dictionary<Point, GameObject> Cells { get; private set; } = new Dictionary<Point, GameObject>();
        private int width, height;
        
        private int mapW, mapH;

        private bool isCentered = true;
        private int demension => Cell.demension;

        public void GenerateGrid(GameObject parent, Vector2 startPos, int width, int height)
        {
            #region Set Params
            this.parent = parent;
            this.width = width;
            this.height = height;

            if (isCentered)
            {
                startPos = new Vector2(startPos.X - mapW * demension / 2, startPos.Y - mapH * demension / 2);
            }
            this.startPostion = startPos;
            #endregion

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Point point = new Point(x, y);
                    GameObject cellGo = new GameObject();
                    cellGo.AddComponent<Cell>(this, point);
                    cellGo.AddComponent<SpriteRenderer>();
                    Cells.Add(point, cellGo);
                    GameWorld.Instance.Instantiate(cellGo);
                }
            }

        }
        public GameObject GetCellGameObject(Vector2 pos)
        {
            if (pos.X < startPostion.X || pos.Y < startPostion.Y)
            {
                return null; // Position is negative, otherwise it will make a invisable tile in the debug, since it cast to int, then it gets rounded to 0 and results in row and column
            }

            int gridX = (int)((pos.X - startPostion.X) / Cell.demension);
            int gridY = (int)((pos.Y - startPostion.Y) / Cell.demension);

            if (0 <= gridX && gridX < width && 0 <= gridY && gridY < height)
            {
                return Cells[new Point(gridX, gridY)];
            }

            return null; // Position is out of bounds
        }

        public Vector2 PosFromGridPos(Point point) => Cells[point].Transform.Position;

        public GameObject GetCellGameObjectFromPoint(Point point) => GetCellGameObject(PosFromGridPos(point));
    }
}
