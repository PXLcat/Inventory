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
        List<Item> selectedInventory;

        public Point cursorLocation;

        public Cursor arrow;
        public Cursor hand;

        public Point selectedMenuOrigin;
        public Point nonSelectedMenuOrigin;

        public Vector2 category1TitleOrigin;
        public Vector2 category2TitleOrigin;

        public Button backToMenu;

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

            backToMenu = new Button(mainGame, new Rectangle(720, 5, 64, 64), background, 9, 10, Button.ButtonType.BACK_TO_MENU);

            menuSelected = 1;
            selectedInventory = player.inventory; //on passe le chemin ou la valeur? ça ira quand on modifiera le contenu de l'inventaire?
            selectedItem = 0;

        }
        public override void Update(GameTime gameTime)
        {
            List<InputType> playerInputs = Input.DefineInputs(ref mainGame.gameState.oldMouseState, ref mainGame.gameState.oldKbState); //on mettrait pas ça dans la classe mère et le base.Update a ?
            if (playerInputs.Contains(InputType.SINGLE_DOWN))
            {
                if (selectedItem == selectedInventory.Count-1)
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
                    selectedItem = selectedInventory.Count-1;
                }
                else
                {
                    selectedItem--;
                }
            }
            if (playerInputs.Contains(InputType.SINGLE_RIGHT)|| playerInputs.Contains(InputType.SINGLE_LEFT))
            {
                if (menuSelected == 1)
                {
                    menuSelected = 2;
                    selectedInventory = player.keyItemsInventory;//un peu moche
                    selectedItem = 0;
                }
                else
                {
                    menuSelected = 1;
                    selectedInventory = player.inventory;
                    selectedItem = 0;
                }
            }


            backToMenu.Update(playerInputs,cursorLocation); //faire une liste à terme si + de boutons

            cursorLocation = Mouse.GetState().Position;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //___FOND________________

            mainGame.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(category1TitleOrigin.X, category1TitleOrigin.Y), //refaire ce bloc avec selectedInventory
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

            backToMenu.Draw(mainGame.spriteBatch);
            //redondance beurk dans les ternaires? demander à Gaët si il verrait ça autrement

            mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "Items", new Vector2(category1TitleOrigin.X+20, category1TitleOrigin.Y+ (menuSelected == 1 ? 20:10)), Color.Black);
            mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "Key Items", new Vector2(category2TitleOrigin.X + 20, category2TitleOrigin.Y + (menuSelected == 2 ? 20 : 10)), Color.Black);

            background.DrawGrid(mainGame.spriteBatch, 9, 5, 12, 8, new Vector2(14, 75));
            //______________________

            for (int i = 0; i < selectedInventory.Count; i++)
            { //Avoir une méthode draw dans Items?

                if (selectedInventory[i].itemNumber>1)
                {
                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, "x" + selectedInventory[i].itemNumber.ToString(),
                        new Vector2(itemsListOrigin.X, itemsListOrigin.Y + i * 35 + (i > selectedItem ?30:0)), Color.Black);
                }
                Vector2 itemNamePosition = new Vector2(itemsListOrigin.X + 65, itemsListOrigin.Y + i * 35 + (i > selectedItem ? 30 : 0));
                Vector2 itemIconPosition = new Vector2(itemNamePosition.X - 50, itemNamePosition.Y - 20);//new dans Draw

                switch (selectedInventory[i].itemType)
                {
                    case ItemType.POTION:
                        background.DrawTiled(mainGame.spriteBatch, 1, 1, itemIconPosition, 9, 13); //externaliser
                        break;
                    case ItemType.EQUIPEMENT:
                        throw new NotImplementedException();
                        break;
                    case ItemType.KEY_ITEM:
                        background.DrawTiled(mainGame.spriteBatch, 1, 1, itemIconPosition, 10, 13);
                        break;
                    case ItemType.DEFAULT:
                        background.DrawTiled(mainGame.spriteBatch, 1, 1, itemIconPosition, 11, 13);
                        break;
                    default:
                        break;
                }

                mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, selectedInventory[i].name, 
                    itemNamePosition, Color.Black); //new dans Draw pas bien

                if (selectedItem == i)
                {
                    Fonts.Instance.DrawOutlined(itemNamePosition, Fonts.Instance.kenPixel16, selectedInventory[i].name);

                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, selectedInventory[i].description, 
                        new Vector2(itemsListOrigin.X + 100, itemsListOrigin.Y + i * 35 + 30), Color.Gray);
                }
            }



            background.DrawCursor(mainGame.spriteBatch, arrow, cursorLocation);

            base.Draw(gameTime);
            mainGame.spriteBatch.End();
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload InventoryScene");
            base.Unload();
        }

    }
}
