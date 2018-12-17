using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Player 
    {
        private static Player instance;
        public List<Character> playersCharacters;


        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();

                }
                return instance;
            }
        }

        private Player() { }


        public void Load()
        {
            if (playersCharacters == null)
            {
                playersCharacters = Factory.Instance.GetCharacters();
            }
            

        }



    }
}
