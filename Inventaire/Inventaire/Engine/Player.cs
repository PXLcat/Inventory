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
        public List<Item> inventory;

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
            if (inventory == null)
            {
                inventory = new List<Item>();
                inventory.Add(new Item("Potion", "Rend X PV", itemNumber: 4));
                inventory.Add(new Item("Brosse a dents", "A utiliser 3 fois par jour"));//TODO kenpixel prend pas les accents, trouver une police de base qui les fait (ou vérif dans le xml de la font que c'est les bons caractères de générés)
                inventory.Add(new Item("Noix d'Hazel","Friandise pour lapinets. Rend 2PV."));
            }
            

        }



    }
}
