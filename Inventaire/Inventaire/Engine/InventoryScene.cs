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


            player.playersCharacters[0].characterStatus = Character.Status.NONE;

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
            base.Draw(gameTime);
            mainGame.spriteBatch.End();
        }
    }
}
