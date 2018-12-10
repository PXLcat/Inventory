using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Inventaire.Engine
{
    public class Button : IClickable
    {
        private Rectangle clickableZone;
        public Texture2D textureButton;
        public String label;
        public SpriteFont font;

        public Rectangle ClickableZone { get => clickableZone; set => clickableZone = value; }

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

        public void OnClick()// ou entrer menu
        {
            throw new NotImplementedException();
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
                sb.Draw(hitboxTexture, ClickableZone, Color.White * 0.5f);
            }
            if (label != null)
            {
                sb.DrawString(font, label, new Vector2(ClickableZone.X + 5, ClickableZone.Y + 3),Color.Black);
            }
        }
    }
}
