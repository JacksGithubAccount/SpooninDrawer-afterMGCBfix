using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpooninDrawer.Engine.States;

namespace SpooninDrawer.Engine.Objects
{
    public class BaseTextObject : BaseGameObject
    {
        protected SpriteFont _font;
        protected Color _color = Color.White;

        public BaseTextObject() { }
        public BaseTextObject(SpriteFont font)
        {
            _font = font;            
        }

        public string Text { get; set; }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (!Active)
            {
                return;
            }
            spriteBatch.DrawString(_font, Text, _position, _color);
        }
    }
}
