using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Gamestate
    {
        public InputMethod currentInputMethod;

        public KeyboardState oldKbState;
        public MouseState oldMouseState;

        public enum SceneType
        {
            MENU,
            INVENTORY,
            STATUS
        }

        protected MainGame mainGame;
        public Scene CurrentScene { get; set; }

        public Gamestate(MainGame mG)
        {
            mainGame = mG;
        }

        public void ChangeScene(SceneType sT)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            switch (sT)
            {
                case SceneType.MENU:
                    CurrentScene = new MenuScene(mainGame);
                    break;
                case SceneType.INVENTORY:
                    CurrentScene = new InventoryScene(mainGame);
                    break;
                case SceneType.STATUS:
                    break;
                default:
                    break;
            }

            CurrentScene.Load();
        }
    }
}
