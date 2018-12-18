using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Factory //attention pas thread safe
    {
        private static Factory instance = null;
        public MainGame mG;
        

        public static Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                }
                return instance;
            }
        }

        public void SetMainGame(MainGame mG)
        {
            this.mG = mG;
        }

        public Texture2D LoadTexture(String assetPath) => mG.Content.Load<Texture2D>(assetPath);

        private Factory()
        {
        }

        public List<Character> GetCharacters()
        {
            List<Character> charactersList = new List<Character>();
            charactersList.Add((new Character("Bidule", 20)));
            charactersList[0].avatar = mG.Content.Load<Texture2D>("ciale5050cadre");

            

            return charactersList;
        }

        public void Load() {
            Fonts.Instance.Load(mG);
        }
    }

}
