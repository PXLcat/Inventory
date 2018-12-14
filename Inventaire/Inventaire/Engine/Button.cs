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
        private Rectangle clickableZone;
        public Texture2D textureButton;
        public String label;
        public SpriteFont font;
        public ButtonType buttonType;

        public Rectangle ClickableZone { get => clickableZone; set => clickableZone = value; }
        public bool isHovered;
        public bool isClicked;

        public Button(Rectangle clickableZone, Texture2D textureButton = null, String label = null, SpriteFont font = null) //on part du principe que
        {                                                                                          //dans le cas d'un bouton la zone cliquable est de la même taille que la texture?
            ClickableZone = clickableZone;
            this.textureButton = textureButton;
            this.label = label;
            if (font == null)
            {
                this.font = Fonts.Instance.kenPixel16;
            }
            else
            {
                this.font = font;
            }

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
        }

        public void OnClick()// ou entrer menu
        {
            switch (buttonType)
            {
                case ButtonType.ITEMS:
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
            Texture2D hitboxTexture = new Texture2D(sb.GraphicsDevice, 1, 1);
            hitboxTexture.SetData(new[] { Color.Red });
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
                    Vector2 textOutlinePos = new Vector2(ClickableZone.X + 5, ClickableZone.Y + 3);
                    sb.DrawString(font, label, new Vector2(textOutlinePos.X - 1, textOutlinePos.Y)
                       , Color.Black);
                    sb.DrawString(font, label, new Vector2(textOutlinePos.X + 1, textOutlinePos.Y)
                       , Color.Black);
                    sb.DrawString(font, label, new Vector2(textOutlinePos.X, textOutlinePos.Y - 1)
                       , Color.Black);
                    sb.DrawString(font, label, new Vector2(textOutlinePos.X, textOutlinePos.Y + 1)
                       , Color.Black);

                    if (isClicked)
                    {
                        sb.DrawString(font, label, new Vector2(textOutlinePos.X, textOutlinePos.Y)
                           , Color.White);
                    }
                    else
                    {
                        sb.DrawString(font, label, new Vector2(textOutlinePos.X, textOutlinePos.Y)
                           , Color.Gray);
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
