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

            
            background.DrawGrid(mainGame.spriteBatch, 9, 5, 9, 9, new Vector2(10, 10));
            background.DrawGrid(mainGame.spriteBatch, 9, 5, 3, 9, new Vector2(586, 10));
            

            mainGame.spriteBatch.DrawString(kenPixelBlocks, "test", new Vector2(100, 100), Color.Black);

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
