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
    //un jour faudra faire des Draw avec argument optionnel (int zoom = 1) parce que vraiment agrandir les spritesheet c'est dégeulasse
    public class DrawTileFromSheet // n'a pas IDrawable. C'est une sheet unique dont on dessine des bouts, pas chaque élément qui est un objet drawable
    {
        private int spacing;
        private int nbRows;
        private int nbColumns; //Compter les colonnes et lignes à partir de 0
        public int tileWidth;
        public int tileHeight;

        Texture2D spriteSheet;

        public DrawTileFromSheet(String assetPath, int nbColumns, int nbRows, int tileWidth, int tileHeight, int spacing = 0)
        {
            Load(assetPath);
            this.nbRows = nbRows;
            this.nbColumns = nbColumns;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.spacing = spacing;
        }

        public void Load(String assetPath)
        {
            spriteSheet = Factory.Instance.LoadTexture(assetPath);
        }

        public Rectangle GetSourceRectangle(int column, int row)
        {

            if (column > nbColumns || row > nbRows)
            {
                throw new Exception("Pas de tile aux coordonnées " + column + ":" + row);
            }
            Rectangle sourceRectangle = new Rectangle(
                column * (tileWidth + spacing), row * (tileHeight + spacing), tileWidth, tileHeight);
            return sourceRectangle;
        }
        /// <summary>
        /// Draws a grid based on a 9-tiles grid on the spritesheet.
        /// ╔══╦══╦══╗
        /// ║A ║  ║  ║
        /// ╠══╬══╬══╣
        /// ║  ║  ║  ║
        /// ╠══╬══╬══╣
        /// ║  ║  ║  ║
        /// ╚══╩══╩══╝
        /// The sheet must have its grid disposed like that
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="leftCornerCoordX">Number of the A tile column</param>
        /// <param name="leftCornerCoordY">Nomber ot the A tile row</param>
        /// <param name="gridColumnsNb">Desired width of the grid, based on tile width.</param>
        /// <param name="gridRowsNb">Desired height  of the grid, based on tile height.</param>
        /// <param name="position">Desired position of the upper left corner of the grid.</param>
        public void DrawGrid(SpriteBatch sb, int leftCornerCoordX, int leftCornerCoordY, int gridColumnsNb, int gridRowsNb, Vector2 position)
        {
            // 1ère ligne
            DrawTiled(sb, 1, 1, position, leftCornerCoordX, leftCornerCoordY);
            DrawTiled(sb, gridColumnsNb - 2, 1, new Vector2(position.X + tileWidth, position.Y), leftCornerCoordX + 1, leftCornerCoordY);
            DrawTiled(sb, 1, 1, new Vector2(position.X + tileWidth * (gridColumnsNb - 1), position.Y), leftCornerCoordX + 2, leftCornerCoordY);
            // Lignes du millieu
            for (int i = 1; i < gridRowsNb - 1; i++)
            {
                DrawTiled(sb, 1, 1, new Vector2(position.X, position.Y + tileHeight * i), leftCornerCoordX, leftCornerCoordY + 1);
                DrawTiled(sb, gridColumnsNb - 2, 1, new Vector2(position.X + tileWidth, position.Y + tileHeight * i), leftCornerCoordX + 1, leftCornerCoordY + 1);
                DrawTiled(sb, 1, 1, new Vector2(position.X + tileWidth * (gridColumnsNb - 1), position.Y + tileHeight * i), leftCornerCoordX + 2, leftCornerCoordY + 1);
            }
            // Dernière ligne
            DrawTiled(sb, 1, 1, new Vector2(position.X, position.Y + tileHeight * (gridRowsNb - 1)), leftCornerCoordX, leftCornerCoordY + 2);
            DrawTiled(sb, gridColumnsNb - 2, 1, new Vector2(position.X + tileWidth, position.Y + tileHeight * (gridRowsNb - 1)), leftCornerCoordX + 1, leftCornerCoordY + 2);
            DrawTiled(sb, 1, 1, new Vector2(position.X + tileWidth * (gridColumnsNb - 1), position.Y + tileHeight * (gridRowsNb - 1)), leftCornerCoordX + 2, leftCornerCoordY + 2);

        }

        public void DrawTiled(SpriteBatch sb, int horizontalTilesNb, int verticalTilesNb, Vector2 position,
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
                        sb.Draw(spriteSheet, new Rectangle((int)position.X + y * tileWidth, (int)position.Y + i * tileHeight, tileWidth, tileHeight),
                            sourceRectangle, Color.White, 0f, new Vector2(tileWidth / 2, tileHeight / 2), SpriteEffects.FlipHorizontally, 0);
                    }
                    else
                    {
                        sb.Draw(spriteSheet, new Rectangle((int)position.X + y * tileWidth, (int)position.Y + i * tileHeight, tileWidth, tileHeight), sourceRectangle, Color.White);
                    }
                }
            }
        }

        public void DrawCursor(SpriteBatch sb, Cursor cursor, Point position, bool horizontalFlip = false)
        {
            Rectangle sourceRectangle = GetSourceRectangle(cursor.originColumn, cursor.originRow);

            if (horizontalFlip)
            {
                sb.Draw(spriteSheet, new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight),
                    sourceRectangle, Color.White, 0f, cursor.hotspot, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                sb.Draw(spriteSheet, new Rectangle((int)position.X, (int)position.Y, tileWidth, tileHeight), 
                    sourceRectangle, Color.White, 0f, cursor.hotspot, SpriteEffects.None, 0);
            }

        }

    }
}
