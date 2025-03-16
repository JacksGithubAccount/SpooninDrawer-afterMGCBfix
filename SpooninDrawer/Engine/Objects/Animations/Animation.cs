using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects.Animations
{
    public class Animation
    {
        private List<AnimationFrame> _frames = new List<AnimationFrame>();
        private int _animationAge = 0;
        private int _lifespan = -1;
        private bool _isLoop = false;

        public Animation(AnimationData data)
        {
            _isLoop = data.IsLooping;
            foreach (var frame in data.Frames)
            {
                AddFrame(
                new Rectangle(frame.X, frame.Y, frame.CellWidth, frame.
                CellHeight),
                data.AnimationSpeed);
            }
        }
        public int Lifespan
        {
            get
            {
                if (_lifespan < 0)
                {
                    _lifespan = 0;
                    foreach (var frame in _frames)
                    {
                        _lifespan += frame.Lifespan;
                    }
                }
                return _lifespan;
            }
        }
        public int AnimationAge()
        {
            return _animationAge;
        }
        public AnimationFrame CurrentFrame
        {
            get
            {
                AnimationFrame currentFrame = null;

                var framesLifespan = 0;
                foreach (var frame in _frames)
                {
                    if (framesLifespan + frame.Lifespan >= _animationAge)
                    {
                        currentFrame = frame;
                        break;
                    }
                    else
                    {
                        framesLifespan += frame.Lifespan;
                    }
                }

                if (currentFrame == null)
                {
                    currentFrame = _frames.LastOrDefault();
                }

                return currentFrame;
            }
        }

        public Animation ReverseAnimation
        {
            get
            {
                var newAnimation = new Animation(_isLoop);
                for (int i = _frames.Count - 1; i >= 0; i--)
                {
                    newAnimation.AddFrame(_frames[i].SourceRectangle, _frames[i].Lifespan);
                }

                return newAnimation;
            }
        }

        public Animation(bool looping)
        {
            _isLoop = looping;
        }

        public void AddFrame(Rectangle sourceRectangle, int lifespan)
        {
            _frames.Add(new AnimationFrame(sourceRectangle, lifespan));
        }

        public void Update(GameTime gametime)
        {
            _animationAge++;

            if (_isLoop && _animationAge > Lifespan)
            {
                _animationAge = 0;
            }
        }

        public void Reset()
        {
            _animationAge = 0;
        }

        public void MakeLoop()
        {
            _isLoop = true;
        }
        public void NoLoop()
        {
            _isLoop = false;
        }
    }
}
