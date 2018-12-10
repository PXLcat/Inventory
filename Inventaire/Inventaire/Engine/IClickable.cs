using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public interface IClickable
    {
        Rectangle ClickableZone { get; set; }

        void OnHover();
        void OnClick();
    }
}
