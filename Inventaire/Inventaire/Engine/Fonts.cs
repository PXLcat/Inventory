﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventaire.Engine
{
    public class Fonts
    {
        private static Fonts instance;
        MainGame mG;
        public SpriteFont kenPixel16;

        public static Fonts Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Fonts();
                }
                return instance;
            }
        }

        private Fonts()
        {
        }

        public void Load(MainGame mG) {
            this.mG = mG;
            kenPixel16 = mG.Content.Load<SpriteFont>("KenPixel");
        }

        public Vector2 GetOffsetToCenterText(Vector2 frame, SpriteFont font, String text)
        {
            Vector2 textSize = font.MeasureString(text);
            int XOffset = (int)((frame.X - textSize.X) / 2);
            int YOffset = (int)((frame.Y - textSize.Y) / 2);
            Vector2 result = new Vector2(XOffset, YOffset);

            return result;
        }
    }
}