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
    public class GameplayText : BaseTextObject
    {
        public GameplayText(SpriteFont font, string text)
        {
            _font = font;
            Text = text;
            _color = Color.Black;
            Position = new Vector2(0, 0);
        }
        public GameplayText(SpriteFont font, ref string text)
        {
            _font = font;
            Text = text;
            _color = Color.Black;
            Position = new Vector2(0, 0);
        }
        public GameplayText(SpriteFont font, string text, Vector2 position)
        {
            _font = font;
            Text = text;
            _color = Color.Black;
            Position = position;
        }
    }
}
