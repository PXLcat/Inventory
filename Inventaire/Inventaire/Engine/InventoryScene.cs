using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class InventoryScene : Scene
    {
        private DrawTileFromSheet background;
        public Player player;

        public Point cursorLocation;

        public Cursor arrow;
        public Cursor hand;

        public Point selectedMenuOrigin;
        public Point nonSelectedMenuOrigin;

        public Vector2 category1TitleOrigin;
        public Vector2 category2TitleOrigin;

        public int menuSelected; //1 = Items , 2 = Key Items 

        public Vector2 itemsListOrigin;

        public int selectedItem;


        public InventoryScene(MainGame mG) : base(mG)
        {


        }

        public override void Load()
        {
            base.Load();

            background = new DrawTileFromSheet("UIpackSheet_transparent", 11, 19, 64, 64, 8); //à terme changer le compte des lignes/colonnes ?
            arrow = new Cursor(4, 19, new Vector2(0, 0));

            player = Player.Instance;
            player.Load();

            selectedMenuOrigin = new Point(9, 4);
            nonSelectedMenuOrigin = new Point(9, 3);

            category1TitleOrigin = new Vector2(25,5);
            category2TitleOrigin = new Vector2(450,5);

            itemsListOrigin = new Vector2(50, 110);

            menuSelected = 1;
            selectedItem = 0;

        }
        public override void Update(GameTime gameTime)
        {
            List<InputType> playerInputs = Input.DefineInputs(ref oldMouseState, ref oldKbState); //on mettrait pas ça dans la classe mère et le base.Update a ?
            if (playerInputs.Contains(InputType.SINGLE_DOWN))
            {
                if (selectedItem == player.inventory.Count-1)
                {
                    selectedItem=0;
                }
                else
                {
                    selectedItem++;
                }
            }
            if (playerInputs.Contains(InputType.SINGLE_UP))//conflit si les deux à la fois?
            {
                if (selectedItem == 0)
                {
                    selectedItem = player.inventory.Count-1;
                }
                else
                {
                    selectedItem--;
                }
            }
            cursorLocation = Mouse.GetState().Position;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //___FOND________________

            mainGame.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(category1TitleOrigin.X, category1TitleOrigin.Y), 
                9, menuSelected==1?selectedMenuOrigin.Y:nonSelectedMenuOrigin.Y);
            background.DrawTiled(mainGame.spriteBatch, 2, 1, new Vector2(category1TitleOrigin.X + background.tileWidth, category1TitleOrigin.Y), 
                10, menuSelected == 1 ? selectedMenuOrigin.Y : nonSelectedMenuOrigin.Y);
            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(category1TitleOrigin.X + background.tileWidth * 3, category1TitleOrigin.Y), 
                11, menuSelected == 1 ? selectedMenuOrigin.Y : nonSelectedMenuOrigin.Y);

            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(category2TitleOrigin.X, category2TitleOrigin.Y), 
                9, menuSelected == 2 ? selectedMenuOrigin.Y : nonSelectedMenuOrigin.Y);
            background.DrawTiled(mainGame.spriteBatch, 2, 1, new Vector2(category2TitleOrigin.X + background.tileWidth, category2TitleOrigin.Y), 
                10, menuSelected == 2 ? selectedMenuOrigin.Y : nonSelectedMenuOrigin.Y);
            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(category2TitleOrigin.X + background.tileWidth * 3, category2TitleOrigin.Y), 
                11, menuSelected == 2 ? selectedMenuOrigin.Y : nonSelectedMenuOrigin.Y);
            //redondance beurk dans les ternaires? demander à Gaët si il verrait ça autrement

            mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "Items", new Vector2(category1TitleOrigin.X+20, category1TitleOrigin.Y+ (menuSelected == 1 ? 20:10)), Color.Black);
            mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "Key Items", new Vector2(category2TitleOrigin.X + 20, category2TitleOrigin.Y + (menuSelected == 2 ? 20 : 10)), Color.Black);

            background.DrawGrid(mainGame.spriteBatch, 9, 5, 12, 8, new Vector2(14, 75));
            //______________________

            for (int i = 0; i < player.inventory.Count; i++)
            { //TODO décalage bas droite pour item sélectionné (ou autre effet graphique onHover) 

                if (player.inventory[i].itemNumber>1)
                {
                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "x" + player.inventory[i].itemNumber.ToString(),
                        new Vector2(itemsListOrigin.X, itemsListOrigin.Y + i * 35 + (i > selectedItem ?30:0)), Color.Black);
                }
                
                mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, player.inventory[i].name, 
                    new Vector2(itemsListOrigin.X+65 , itemsListOrigin.Y + i * 35 + (i > selectedItem ? 30 : 0)), Color.Black);
                if (selectedItem == i)
                {
                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, player.inventory[i].description, 
                        new Vector2(itemsListOrigin.X + 100, itemsListOrigin.Y + i * 50 + 30), Color.Gray);
                }
            }


 
            background.DrawCursor(mainGame.spriteBatch, arrow, cursorLocation);

            base.Draw(gameTime);
            mainGame.spriteBatch.End();
        }
    }
}
