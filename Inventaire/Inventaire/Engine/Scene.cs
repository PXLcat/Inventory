using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inventaire.Engine.Gamestate;

namespace Inventaire.Engine
{
    abstract public class Scene
    {
        protected MainGame mainGame;
        public KeyboardState oldKbState;
        public MouseState oldMouseState;
        public int windowWidth;
        public int windowHeight;
        //public Player player;

        //____Affichage de la position de la souris____
        private MouseState mouse;
        public String mouseText;
        public Vector2 mouseTextPos;
        //_____________________________________________

        public Scene(MainGame mG)
        {
            mainGame = mG;
            if (null == Factory.Instance.mG) //moche
            {
                Factory.Instance.SetMainGame(mG);
            }
            Factory.Instance.Load();
            
        }

        public virtual void Load()
        {
            //windowWidth = mainGame.GraphicsDevice.DisplayMode.Width; //Attention, c'est la taille de l'écran, pas de la fenêtre
            //windowHeight = mainGame.GraphicsDevice.DisplayMode.Height;

            //player = Player.Instance;

            windowWidth = mainGame.GraphicsDevice.Viewport.Bounds.Width;
            windowHeight = mainGame.GraphicsDevice.Viewport.Bounds.Height;
            Debug.WriteLine("Window width = " + windowWidth + ", window height = " + windowHeight);

        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime) {

#if DEBUG
            mouse = Mouse.GetState();
            mouseText = mouse.Position.X + ":" + mouse.Position.Y;
            mouseTextPos = new Vector2(windowWidth - Fonts.Instance.kenPixel16.MeasureString(mouseText).X, 0);
#endif

        }

        public virtual void Draw(GameTime gameTime)
        {
#if DEBUG
            mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, mouseText, mouseTextPos, Color.Yellow);
#endif

        }

        public void GoToScene(MainGame mG, SceneType sT)
        {
            mG.gameState.ChangeScene(sT);
        }
    }
}
