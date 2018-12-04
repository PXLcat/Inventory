using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class MenuScene : Scene
    {
        private SpriteFont kenPixelBlocks;
        private DrawTileFromSheet background;

        public MenuScene(MainGame mG) : base(mG)
        {

        }

        public override void Load()
        {
            base.Load();

            kenPixelBlocks = mainGame.Content.Load<SpriteFont>("KenPixelBlocks");
            background = new DrawTileFromSheet("UIpackSheet_transparent", 11, 19, 64, 64, 8); //à terme changer le compte des lignes/colonnes           
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload TestScene");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            //List<InputType> playerInputs = Input.DefineInputs(ref oldKbState);


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin();

            background.DrawTiled(mainGame.spriteBatch, 3, 2, new Vector2(100, 100), 0, 0);
            background.DrawGrid(mainGame.spriteBatch, 0, 0, 4, 5, new Vector2(200, 200));

            mainGame.spriteBatch.DrawString(kenPixelBlocks, "test", new Vector2(100, 100), Color.White);

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
