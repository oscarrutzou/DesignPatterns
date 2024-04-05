using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortressSurvivor
{
    public class OscarTestScene : Scene
    {
        private Grid grid;
        private GameObject gridGameobject;
        private GameObject astarGameobject;
        private Astar astar;
        public override void Initialize()
        {
            Director playerDirector = new Director(new PlayerBuilder());
            GameObject playerGo = playerDirector.Contruct();
            Instantiate(playerGo);


            gridGameobject = new GameObject();
            grid = gridGameobject.AddComponent<Grid>();
            grid.GenerateGrid(Vector2.Zero, 5, 5);

            astarGameobject = new GameObject();
            astar = new Astar(astarGameobject, grid);

            Player player = playerGo.GetComponent<Player>() as Player;
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(player, new Vector2(1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(player, new Vector2(-1, 0)));
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(player, new Vector2(0, -1)));
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(player, new Vector2(0, 1)));

            InputHandler.Instance.AddUpdateCommand(Keys.Q, new AstarTestCommand(astar, grid));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void DrawInWorld(SpriteBatch spriteBatch)
        {
            base.DrawInWorld(spriteBatch);
        }

        public override void DrawOnScreen(SpriteBatch spriteBatch)
        {
            base.DrawOnScreen(spriteBatch);
        }

    }
}
