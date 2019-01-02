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
        public Texture2D textureButton;
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


        public Button(MainGame mG, Rectangle clickableZone, Texture2D textureButton = null, ButtonType buttonType = ButtonType.NONE,  String label = null, SpriteFont font = null) //on part du principe que
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
                default:
                    break;
            }
        }

        public void OnHover()// ou sélection menu
        {
            throw new NotImplementedException();
        }
        public void Draw(SpriteBatch sb)
        {
#if DEBUG
            sb.Draw(hitboxTexture, ClickableZone, Color.White * 0.5f);
#endif
            if (textureButton != null)
            {
                sb.Draw(textureButton, ClickableZone, Color.White * 0.5f);
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
            NONE
        }
    }
}
