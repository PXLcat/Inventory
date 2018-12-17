using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Item
    {
        // public Texture2D icon; //avoir l'image à partir d'un DrawTileFromSheet 

        public int originColumn;
        public int originRow;

        public String name;
        public bool unique;
        public bool keyItem;
        public int itemNumber;

        public Item(string name, bool unique = false, bool keyItem = false, int itemNumber = 1)
        {
            this.name = name;
            this.unique = unique;
            this.keyItem = keyItem;
            this.itemNumber = itemNumber;
        }

        public void OnUse()
        {

        }
    }
    enum ItemType
    {

    }
}
