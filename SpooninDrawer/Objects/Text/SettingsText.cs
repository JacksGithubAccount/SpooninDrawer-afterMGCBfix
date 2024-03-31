using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Text
{
    public class SettingsText : BaseTextObject
    {
        public SettingsText(SpriteFont font, String text)
        {
            _font = font;
            Text = text;
            _color = Color.Black;
            Position = new Vector2(0, 0);
        }
        public SettingsText(SpriteFont font, String text, Vector2 position)
        {
            _font = font;
            Text = text;
            _color = Color.Black;
            Position = position;
        }
    }
}
