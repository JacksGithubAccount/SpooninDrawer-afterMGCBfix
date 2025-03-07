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
        private float transparency = 1.0f;
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
            spriteBatch.DrawString(_font, Text, _position, _color * transparency);
        }
        public virtual Vector2 MeasureString()
        {
            return _font.MeasureString(Text);
        }
        public void SetTransparency(float transparency)
        {
            this.transparency = transparency;
        }
        public float GetTransparency() { return transparency; }
        public void TransparencyUp()
        {
            if (transparency < 1.0f)
                transparency += 0.01f;
        }
        public void TransparencyDown()
        {
            if (transparency > 0.0f)
                transparency -= 0.01f;
        }
    }
}
