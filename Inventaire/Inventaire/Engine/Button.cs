using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Inventaire.Engine
{
    public class Button : IClickable
    {
        private MainGame mG;

        //si le bouton a une image individuelle
        public Texture2D textureButton;

        //si l'image du bouton vient d'une spritesheet
        DrawTileFromSheet sourceTileSheet;
        int originColumn, originRow;

        public String label;
        public SpriteFont font;
        public ButtonType buttonType;

        public Rectangle ClickableZone { get; set; }
        public bool isHovered;
        public bool isClicked;

#if DEBUG
        public Color[] debugTextureColor;
        Texture2D hitboxTexture;
#endif

        /// <summary>
        /// Bouton qui ne vient pas d'une Tilesheet
        /// </summary>
        /// <param name="mG"></param>
        /// <param name="clickableZone"></param>
        /// <param name="textureButton"></param>
        /// <param name="buttonType"></param>
        /// <param name="label"></param>
        /// <param name="font"></param>
        public Button(MainGame mG, Rectangle clickableZone, Texture2D textureButton = null,
            ButtonType buttonType = ButtonType.NONE,  String label = null, SpriteFont font = null) //on part du principe que
        {                                                                                          //dans le cas d'un bouton la zone cliquable est de la même taille que la texture?
            ClickableZone = clickableZone;
            this.textureButton = textureButton;
            this.label = label;
            this.font = font ?? Fonts.Instance.kenPixel16;
            this.mG = mG;

#if DEBUG
            mG.spriteBatch = new SpriteBatch(mG.GraphicsDevice);
            hitboxTexture = new Texture2D(this.mG.spriteBatch.GraphicsDevice, 1, 1); // à terme, rendre la texture de hitbox générale?
            hitboxTexture.SetData(new[] { Color.Red });
#endif
        }

        /// <summary>
        /// Bouton qui vient d'une Tilesheet
        /// </summary>
        /// <param name="sourceTileSheet"></param>
        public Button(MainGame mG, Rectangle clickableZone, DrawTileFromSheet sourceTileSheet,
            int originColumn, int originRow, ButtonType buttonType = ButtonType.NONE, String label = null, SpriteFont font = null)
        {
            ClickableZone = clickableZone;
            this.sourceTileSheet = sourceTileSheet;
            this.originColumn = originColumn;
            this.originRow = originRow;
            this.buttonType = buttonType;
            this.label = label;
            this.font = font ?? Fonts.Instance.kenPixel16;
            this.mG = mG;

#if DEBUG //attention redondance
            mG.spriteBatch = new SpriteBatch(mG.GraphicsDevice);
            hitboxTexture = new Texture2D(this.mG.spriteBatch.GraphicsDevice, 1, 1); // à terme, rendre la texture de hitbox générale?
            hitboxTexture.SetData(new[] { Color.Red });
#endif
        }


        public void Update(List<InputType> playerInputs, Point cursorLocation)
        {//faudra vraiment apprendre à utiliser les eventHandler un jour
            if (ClickableZone.Contains(cursorLocation))
            {
                isHovered = true;
                if (playerInputs.Contains(InputType.LEFT_CLICK))
                {
                    isClicked = true;
                }
                else
                {
                    isClicked = false;
                }
            }
            else
            {
                isHovered = false;
                isClicked = false;
            }
            if (isClicked)
            {
                OnClick();
            }
        }

        public void OnClick()// ou entrer menu
        {
            switch (buttonType)
            {
                case ButtonType.ITEMS:
                    mG.gameState.ChangeScene(Gamestate.SceneType.INVENTORY);
                    break;
                case ButtonType.INVENTORY:
                    break;
                case ButtonType.NONE:
                    break;
                case ButtonType.BACK_TO_MENU:
                    mG.gameState.ChangeScene(Gamestate.SceneType.MENU);
                    break;
                default:
                    break;
            }
        }

        public void OnHover()// ou sélection menu
        {
            throw new NotImplementedException();
        }
        public void Draw(SpriteBatch sb) //TODO la classe contient déjà un mainGame : plutôt utiliser celui là?
        {
#if DEBUG
            sb.Draw(hitboxTexture, ClickableZone, Color.White * 0.5f);
#endif

            if (textureButton != null) //cas bouton d'un image individuelle
            {
                sb.Draw(textureButton, ClickableZone, Color.White * 0.5f);
            }
            else if (sourceTileSheet !=null)
            {
                sourceTileSheet.DrawTiled(mG.spriteBatch, 1, 1, new Vector2(ClickableZone.X, ClickableZone.Y), originColumn, originRow);
            }
            if (label != null)
            {
                if (isHovered)
                {
                    Vector2 textOutlinePos = new Vector2(ClickableZone.X + 5, ClickableZone.Y + 3); //new dans un Draw, c'est mal!
                    Fonts.Instance.DrawOutlined(textOutlinePos, Fonts.Instance.kenPixel16, label);

                    if (isClicked)
                    {
                        sb.DrawString(font, label, new Vector2(textOutlinePos.X, textOutlinePos.Y)
                           , Color.White);
                    }


                }
                else
                {
                    sb.DrawString(font, label, new Vector2(ClickableZone.X + 5, ClickableZone.Y + 3), Color.Black);
                }
                
            }
        }
        //TODO : Draw Centered qui utiliserait Fronts.GetOffsetToCenterText
        public enum ButtonType
        {
            ITEMS,
            INVENTORY,
            BACK_TO_MENU,
            NONE
        }
    }
}
