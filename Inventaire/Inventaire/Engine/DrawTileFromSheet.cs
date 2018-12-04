using CommonImagery;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class DrawTileFromSheet // n'a pas IDrawable. C'est une sheet unique dont on dessine des bouts, pas chaque élément qui est un objet drawable
    {
        private int spacing;
        private int nbRows;
        private int nbColumns; //Compter les colonnes et lignes à partir de 0
        private int tileWidth;
        private int tileHeight;

        Texture2D spriteSheet;

        public DrawTileFromSheet(String assetPath, int nbRows, int nbColumns, int tileWidth, int tileHeight, int spacing = 0)
        {
            Load(assetPath);
            this.nbRows = nbRows;
            this.nbColumns = nbColumns;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.spacing = spacing;
        }

        public void Load(String assetPath) {
            spriteSheet = Factory.Instance.LoadTexture(assetPath);
        }

        public Rectangle GetSourceRectangle(int column, int row) {

            if (column>nbColumns ||row>nbRows)
            {
                throw new Exception("Pas de tile aux coordonnées "+column+":"+row);
            }
            Rectangle sourceRectangle = new Rectangle(
                column * (tileWidth + spacing), row * (tileHeight + spacing), tileWidth, tileHeight);
            return sourceRectangle;
        }

        public virtual void DrawTiled(SpriteBatch sb, int horizontalTilesNb, int verticalTilesNb, Vector2 position,
            int originColumn, int originRow, bool horizontalFlip = false)
        {
            Rectangle sourceRectangle = GetSourceRectangle(originColumn, originRow);
            for (int i = 0; i < verticalTilesNb; i++)
            {
                for (int y = 0; y < horizontalTilesNb; y++)
                {
                    //n'utilise pas le centre, va se caler sur le point en haut à gauche
                    if (horizontalFlip)
                    {
                        sb.Draw(spriteSheet, position, sourceRectangle, Color.White, 0, new Vector2(tileWidth / 2, tileHeight/2), 1, SpriteEffects.FlipHorizontally, 0);
                    }
                    else
                    {
                        sb.Draw(spriteSheet, position, sourceRectangle, Color.White);
                    }
                }
            }
        }
    }
}
