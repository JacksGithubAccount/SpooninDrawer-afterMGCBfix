using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.Objects.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class MenuArrowSprite : BaseGameObject
    {

        private const int AnimationSpeed = 6;
        private const int AnimationCellWidth = 60;
        private const int AnimationCellHeight = 50;

        private Animation _idleAnimation = new Animation(false);
        private Animation _currentAnimation;
        private Rectangle _idleRectangle;
        public MenuArrowSprite(Texture2D texture)
        {
            _texture = texture;
            _idleRectangle = new Rectangle(0, 0, AnimationCellWidth, AnimationCellHeight);
            _idleAnimation.MakeLoop();
            _idleAnimation.AddFrame(new Rectangle(0, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(AnimationCellWidth, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(AnimationCellWidth * 2, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _currentAnimation = _idleAnimation;
        }

        public void Update(GameTime gametime)
        {
            if (_currentAnimation != null)
            {
                if(Active)
                    _currentAnimation.Update(gametime);
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                var destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, AnimationCellWidth, AnimationCellHeight);
                var sourceRectangle = _idleRectangle;
                if (_currentAnimation != null)
                {
                    var currentFrame = _currentAnimation.CurrentFrame;
                    if (currentFrame != null)
                    {
                        sourceRectangle = currentFrame.SourceRectangle;
                    }
                }
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }
    }
}
