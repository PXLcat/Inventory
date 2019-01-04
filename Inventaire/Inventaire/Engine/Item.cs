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
        public String description;
        public bool unique;
        public int itemNumber;
        public ItemType itemType;

        public Item(string name, string description, bool unique = false, int itemNumber = 1, ItemType itemType = ItemType.DEFAULT)
        {
            this.name = name;
            this.description = description;
            this.unique = unique;
            this.itemNumber = itemNumber;
            this.itemType = itemType;
        }

        public void OnUse()
        {

        }
    }
}
public enum ItemType
{
    POTION,
    EQUIPEMENT,
    KEY_ITEM,
    DEFAULT
}

