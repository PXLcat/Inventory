using Microsoft.Xna.Framework;
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

        public MenuScene(MainGame mG) : base(mG)
        {

        }

        public override void Load()
        {

            base.Load();
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


            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
