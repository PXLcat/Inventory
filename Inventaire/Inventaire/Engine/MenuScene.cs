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
    public class MenuScene : Scene
    {
        private DrawTileFromSheet background;
        public Player player;
        public List<Button> menuDroite;
        private int selectedMenuDroite;

        private Point handCursorPosition;

        public Cursor arrow;
        public Cursor hand;

        public MenuScene(MainGame mG) : base(mG)
        {

            
        }

        public override void Load()
        {
            base.Load();

            background = new DrawTileFromSheet("UIpackSheet_transparent", 11, 19, 64, 64, 8); //à terme changer le compte des lignes/colonnes ?
            arrow = new Cursor(4, 19, Vector2.Zero);
            hand = new Cursor(5, 13, Vector2.Zero);//pertinence d'être un curseur?

            player = Player.Instance;
            player.Load();

            selectedMenuDroite = 0;

            menuDroite = new List<Button>();
            menuDroite.Add(new Button(mainGame, new Rectangle(600, 30, 170, 35),buttonType: Button.ButtonType.ITEMS, label:"Items"));
            menuDroite.Add(new Button(mainGame, new Rectangle(600, 70, 170, 35), label: "Equipement")); //moche le décalage à la main?

            handCursorPosition = Point.Zero; //TODO peut poser problème plus tard?

        }

        public override void Unload()
        {
            Debug.WriteLine("Unload MenuScene");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (playerInputs.Contains(InputType.SINGLE_DOWN))
            {
                if (selectedMenuDroite == menuDroite.Count - 1)
                {
                    selectedMenuDroite = 0;
                }
                else
                {
                    selectedMenuDroite++;
                }
            }
            if (playerInputs.Contains(InputType.SINGLE_UP))//conflit si les deux à la fois?
            {
                if (selectedMenuDroite == 0)
                {
                    selectedMenuDroite = menuDroite.Count - 1;
                }
                else
                {
                    selectedMenuDroite--;
                }
            }

            handCursorPosition = new Point(menuDroite[selectedMenuDroite].ClickableZone.X-background.tileWidth,
                menuDroite[selectedMenuDroite].ClickableZone.Y-5);//chiffre magique


            foreach (Button button in menuDroite)
            {
                button.Update(playerInputs, cursorPosition);
            }

            if (playerInputs.Contains(InputType.SINGLE_ENTER))
            {
                menuDroite[selectedMenuDroite].OnClick();
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null); //SamplerState.PointClamp => Permet de resize du pixel art sans blur


            background.DrawGrid(mainGame.spriteBatch, 9, 5, 9, 9, new Vector2(10, 10));
            background.DrawGrid(mainGame.spriteBatch, 9, 5, 3, 9, new Vector2(586, 10));
            
            DrawCharactersSummaries(new Vector2(50,50)); //ATTENTION CALCULS
            DrawRightMenu(); //ATTENTION CALCULS

            if (mainGame.gameState.currentInputMethod == InputMethod.MOUSE)
            {
                background.DrawCursor(mainGame.spriteBatch, arrow, cursorPosition);
            }
            else if (mainGame.gameState.currentInputMethod == InputMethod.KEYBOARD)
            {
                background.DrawCursor(mainGame.spriteBatch, hand, handCursorPosition);
            }


            base.Draw(gameTime);

            mainGame.spriteBatch.End();
        }

        public void DrawCharactersSummaries(Vector2 basePosition) {
            int textX = (int)basePosition.X + 120; //taille de l'avatar + des miettes
            for (int i = 0; i < player.playersCharacters.Count; i++)
            {
                Vector2 avatarPosition = new Vector2(basePosition.X, basePosition.Y + i*180);
                mainGame.spriteBatch.Draw(player.playersCharacters[i].avatar, avatarPosition, null, Color.White,0f,Vector2.Zero,2,SpriteEffects.None, 1);
                StringBuilder sb = new StringBuilder(); //TODO le sb serait pas à faire dans le Update?
                sb.AppendLine(player.playersCharacters[i].name);
                sb.Append("  "); //La tabulation ("\t") ne marche pas ?!
                sb.Append(player.playersCharacters[i].currentHP);
                sb.Append("/");
                sb.Append(player.playersCharacters[i].maxHP.ToString());
                sb.AppendLine(" HP");

                if (player.playersCharacters[i].characterStatus != Character.Status.NONE)
                {
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX, (basePosition.Y + i * 180) + 60), 0, 14);
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX + background.tileWidth, (basePosition.Y + i * 180) + 60), 1, 14);
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX + background.tileWidth*2, (basePosition.Y + i * 180) + 60 ), 2, 14);

                    Vector2 buttonSize = new Vector2(background.tileWidth * 3, background.tileHeight);
                    Vector2 fromFrameOffset = Fonts.Instance.GetOffsetToCenterText(buttonSize, Fonts.Instance.kenPixel16, player.playersCharacters[i].characterStatus.ToString());

                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, player.playersCharacters[i].characterStatus.ToString(), 
                                                    new Vector2(textX + fromFrameOffset.X, (basePosition.Y + i * 180) + 60 + fromFrameOffset.Y), Color.Black);
  
                }

                mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, sb.ToString(), new Vector2(textX , (basePosition.Y + i * 180)) 
                    , Color.Black);
                

            }
        }
        public void DrawRightMenu()
        {
            foreach (Button item in menuDroite)
            {
                item.Draw(mainGame.spriteBatch);
            }
        }
    }
}
