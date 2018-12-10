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
        public Rectangle ClickableZone { get => clickableZone; set => value = clickableZone; }
        public Texture2D textureButton;
        public String label;

        public Button(Rectangle clickableZone, Texture2D textureButton = null, String label = null) //on part du principe que
        {                                                                                          //dans le cas d'un bouton la zone cliquable est de la même taille que la texture?
            ClickableZone = clickableZone;
            this.textureButton = textureButton;
            this.label = label;
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
            sb.Draw(hitboxTexture, clickableZone, Color.White * 0.5f);
#endif
            if (textureButton != null)
            {
                sb.Draw(hitboxTexture, clickableZone, Color.White * 0.5f);
            }
            if (label != null)
            {
                //sb.DrawString(label, label, new Vector2(textX, basePosition.Y * (1 + i))
            }
        }
    }
}
