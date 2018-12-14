using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Cursor //mettre en classe interne de DrawTileFromSheet?
    {
        public int originColumn, originRow;
        public Vector2 hotspot;

        public Cursor(int originColumn, int originRow, Vector2 hotspot)
        {
            this.originColumn = originColumn;
            this.originRow = originRow;
            this.hotspot = hotspot;
        }
    }
}
