using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public MenuScene(MainGame mG) : base(mG)
        {

        }

        public override void Load()
        {
            base.Load();

            background = new DrawTileFromSheet("UIpackSheet_transparent", 11, 19, 64, 64, 8); //à terme changer le compte des lignes/colonnes ?
            player = Player.Instance;
            player.Load();

            menuDroite = new List<Button>();
            menuDroite.Add(new Button(new Rectangle(300, 100, 50, 10),label:"Items"));

            player.playersCharacters[0].characterStatus = Character.Status.KO;

        }

        public override void Unload()
        {
            Debug.WriteLine("Unload TestScene");
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            //List<InputType> playerInputs = Input.DefineInputs(ref oldKbState);


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null); //SamplerState.PointClamp => Permet de resize du pixel art sans blur


            background.DrawGrid(mainGame.spriteBatch, 9, 5, 9, 9, new Vector2(10, 10));
            background.DrawGrid(mainGame.spriteBatch, 9, 5, 3, 9, new Vector2(586, 10));
            
            DrawCharactersSummaries(new Vector2(50,50));
            DrawRightMenu();

            mainGame.spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawCharactersSummaries(Vector2 basePosition) {
            int textX = (int)basePosition.X + 120; //taille de l'avatar + des miettes
            for (int i = 0; i < player.playersCharacters.Count; i++)
            {
                mainGame.spriteBatch.Draw(player.playersCharacters[i].avatar, basePosition*(i+1),null, Color.White,0f,Vector2.Zero,2,SpriteEffects.None, 1);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(player.playersCharacters[i].name);
                sb.Append("  "); //La tabulation ("\t") ne marche pas ?!
                sb.Append(player.playersCharacters[i].currentHP);
                sb.Append("/");
                sb.Append(player.playersCharacters[i].maxHP.ToString());
                sb.AppendLine(" HP");

                if (player.playersCharacters[i].characterStatus != Character.Status.NONE)
                {
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX, basePosition.Y * (i + 1) + 60 * (i + 1)), 0, 14);
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX + background.tileWidth, basePosition.Y * (i + 1) + 60 * (i + 1)), 1, 14);
                    background.DrawTiled(mainGame.spriteBatch, 1, 1, new Vector2(textX + background.tileWidth*2, basePosition.Y * (i + 1) + 60 * (i + 1)), 2, 14);

                    Vector2 buttonSize = new Vector2(background.tileWidth * 3, background.tileHeight);
                    Vector2 fromFrameOffset = Fonts.Instance.GetOffsetToCenterText(buttonSize, Fonts.Instance.kenPixel16, player.playersCharacters[i].characterStatus.ToString());

                    mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, player.playersCharacters[i].characterStatus.ToString(), 
                                                    new Vector2(textX + fromFrameOffset.X, basePosition.Y * (i + 1) + 60 * (i + 1)+ fromFrameOffset.Y), Color.Black);
  
                }

                mainGame.spriteBatch.DrawString(Fonts.Instance.kenPixel16, sb.ToString(), new Vector2(textX ,basePosition.Y*(1+i)) 
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
