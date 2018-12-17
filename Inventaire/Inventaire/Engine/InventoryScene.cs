using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class InventoryScene : Scene
    {
        private DrawTileFromSheet background;
        public Player player;

        public Point cursorLocation;

        public Cursor arrow;
        public Cursor hand;

        public InventoryScene(MainGame mG) : base(mG)
        {


        }

        public override void Load()
        {
            base.Load();

            background = new DrawTileFromSheet("UIpackSheet_transparent", 11, 19, 64, 64, 8); //à terme changer le compte des lignes/colonnes ?
            arrow = new Cursor(4, 19, new Vector2(0, 0));

            player = Player.Instance;
            player.Load();



        }
        public override void Update(GameTime gameTime)
        {
            List<InputType> playerInputs = Input.DefineInputs(ref oldMouseState); //on mettrait pas ça dans la classe mère et le base.Update a ?

            cursorLocation = Mouse.GetState().Position;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            mainGame.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(20, 10), 9, 4);
            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(20 + background.tileWidth, 10 ), 10, 4);
            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(20 + background.tileWidth * 2, 10), 11, 4);

            background.DrawGrid(mainGame.spriteBatch, 9, 5, 12, 8, new Vector2(14, 75));
            background.DrawCursor(mainGame.spriteBatch, arrow, cursorLocation);

            base.Draw(gameTime);
            mainGame.spriteBatch.End();
        }
    }
}
