using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Text
{
    public class RollCreditsText : BaseTextObject
    {
        public RollCreditsText(SpriteFont font, string text)
        {
            _font = font;
            Text = text;
            _color = Color.White;
            Position = new Vector2(0, 0);
        }
    }
}
