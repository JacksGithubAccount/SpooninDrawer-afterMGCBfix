using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class SplashImageLoadingBar : BaseGameObject
    {
        private int LoadingBarWidth = 0;
        public SplashImageLoadingBar(Texture2D texture, float progressNumber)

        {
            _texture = texture;
            LoadingBarWidth = (int)Math.Round(_texture.Width * progressNumber);            
        }
        public void UpdateLoadingBar(float progressNumber)
        {
            LoadingBarWidth = (int)Math.Round(_texture.Width * progressNumber);
        }
        public Vector2 GetEndofBarPosition()
        {
            return new Vector2(_position.X + LoadingBarWidth, _position.Y + _texture.Height/2);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(_texture, _position, new Rectangle(0, 0, LoadingBarWidth, _texture.Height), Color.White);
            }
        }
    }
}
