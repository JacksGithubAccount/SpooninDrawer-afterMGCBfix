﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects.Animations;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework.Content;
using SpooninDrawer.Extensions;

namespace SpooninDrawer.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        //public Vector2 CurrentUpSpeed { get; private set; }

        public float PlayerSpeed { get; set; } //velocity in units per seconds, so 600 units per second (10.0 times 60)

        private const int BB1PosX = 29;
        private const int BB1PosY = 2;
        private const int BB1Width = 57;
        private const int BB1Height = 147;

        private const int BB2PosX = 2;
        private const int BB2PosY = 77;
        private const int BB2Width = 111;
        private const int BB2Height = 37;

        private Rectangle LookLeftRect;
        private Rectangle LookRightRect;
        private Rectangle LookUpRect;
        private Rectangle LookDownRect;

        private Animation _idleAnimation = new Animation(false);
        private Animation _turnLeftAnimation = new Animation(false);
        private Animation _turnRightAnimation = new Animation(false);
        private Animation _leftToCenterAnimation = new Animation(false);
        private Animation _rightToCenterAnimation = new Animation(false);
        private const int AnimationSpeed = 3;
        private const int AnimationCellWidth = 116;
        private const int AnimationCellHeight = 152;

        private Animation _currentAnimation;
        private Rectangle _idleRectangle;

        private bool _isIdle = false;
        private bool _movingLeft = false;
        private bool _movingRight = false;
        private bool _movingUp = false;
        private bool _movingDown = false;
        private bool _stopLeft = false;
        private bool _stopRight = false;
        private bool _stopUp = false;
        private bool _stopDown = false;
        private bool flipAnimationHorizontal = false;

        public bool _MustStop = false;
        public Vector2 _MoveDirection;
        public float startingPositionX = 344.0f;
        public float startingPositionY = 444.0f;

        public List<Rectangle> Collided;

        public Texture2D blank;
        public Vector2 CenterPosition;
        public override int Height => AnimationCellHeight;
        public override int Width => AnimationCellWidth;

        public PlayerSprite(Texture2D texture, AnimationData turnLeftAnimation, AnimationData turnRightAnimation, AnimationData idleAnimation) : base(texture)
        {
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB1PosX, BB1PosY), BB1Width, BB1Height));
            //AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB2PosX, BB2PosY), BB2Width, BB2Height));

            _idleRectangle = new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight);

            _idleAnimation = new Animation(idleAnimation);
            _turnLeftAnimation = new Animation(turnLeftAnimation);
            _turnRightAnimation = new Animation(turnRightAnimation);
            _leftToCenterAnimation = _turnLeftAnimation.ReverseAnimation;
            _rightToCenterAnimation = _turnRightAnimation.ReverseAnimation;
            Position = new Vector2(startingPositionX, startingPositionY);
            CenterPosition = Position;
            PlayerSpeed = 10.0f;
            _MoveDirection = Position;
            Collided = new List<Rectangle>();
            //CurrentUpSpeed = _playerNormalUpSpeed;
        }
        //moved to content file
        /*public PlayerSprite(Texture2D texture)
        {
            _texture = texture;
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB1PosX, BB1PosY), BB1Width, BB1Height));
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BB2PosX, BB2PosY), BB2Width, BB2Height));

            _idleRectangle = new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight);
            _idleAnimation.MakeLoop();
            _idleAnimation.AddFrame(new Rectangle(0, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(116, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(232, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _idleAnimation.AddFrame(new Rectangle(464, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _turnLeftAnimation.MakeLoop();
            _turnLeftAnimation.AddFrame(new Rectangle(0, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(116, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(232, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(348, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnLeftAnimation.AddFrame(new Rectangle(464, 152, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _turnRightAnimation.AddFrame(new Rectangle(348, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(464, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(580, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);
            _turnRightAnimation.AddFrame(new Rectangle(696, 0, AnimationCellWidth, AnimationCellHeight), AnimationSpeed);

            _leftToCenterAnimation = _turnLeftAnimation.ReverseAnimation;
            _rightToCenterAnimation = _turnRightAnimation.ReverseAnimation;
        }*/
        public bool IsMovingLeft()
        {
            return _movingLeft;
        }
        public bool IsMovingRight()
        {
            return _movingRight;
        }
        public bool IsMovingUp()
        {
            return _movingUp;
        }
        public bool IsMovingDown()
        {
            return _movingDown;
        }
        public void StopMoving()
        {
            if (_movingLeft)
            {
                _currentAnimation = _leftToCenterAnimation;
                _movingLeft = false;
            }

            if (_movingRight)
            {
                _currentAnimation = _rightToCenterAnimation;
                _movingRight = false;
            }
            if (!_movingLeft && !_movingRight && !_isIdle)
            {
                _currentAnimation = _idleAnimation;
                _isIdle = true;
                _idleAnimation.Reset();
            }
            //mapCollided = false;
        }

        public bool MoveLeft()
        {
            _movingLeft = true;
            _movingRight = false;
            flipAnimationHorizontal = true;
            _isIdle = false;
            _currentAnimation = _turnLeftAnimation;
            _leftToCenterAnimation.Reset();
            _turnRightAnimation.Reset();
            if (!_stopLeft)
            {
                _MoveDirection.X -= PlayerSpeed;
            }
            //used to play footstep sound effect
            return frameAsBool(3);
        }

        public bool MoveRight()
        {
            _movingRight = true;
            _movingLeft = false;
            flipAnimationHorizontal = false;
            _isIdle = false;
            _currentAnimation = _turnRightAnimation;
            _rightToCenterAnimation.Reset();
            _turnLeftAnimation.Reset();
            if (!_stopRight)
            {
                _MoveDirection.X += PlayerSpeed;
            }
            return frameAsBool(3);
        }

        public bool MoveUp()
        {
            _movingUp = true;
            _movingDown = false;
            if (!_movingLeft)
            {
                _isIdle = false;
                _currentAnimation = _turnRightAnimation;
                _rightToCenterAnimation.Reset();
                _turnLeftAnimation.Reset();
            }
            if (!_stopUp)
            {
                _MoveDirection.Y -= PlayerSpeed;
            }
            return frameAsBool(3);
        }

        public bool MoveDown()
        {
            _movingUp = false;
            _movingDown = true;
            if (!_movingLeft)
            {
                _isIdle = false;
                _currentAnimation = _turnRightAnimation;
                _rightToCenterAnimation.Reset();
                _turnLeftAnimation.Reset();
            }
            if (!_stopDown)
            {
                _MoveDirection.Y += PlayerSpeed;
            }
            return frameAsBool(3);
        }
        public bool frameAsBool(int framenumber)
        {
            if (_currentAnimation.AnimationAge() == framenumber)
            {
                return true;
            }
            else
                return false;
        }
        public void HandleMapCollision(CollidableGameObject MapTile)
        {
            Engine.Objects.BoundingBox tempBB = BoundingBoxes[0];
            Vector2 newPosition = new Vector2(Position.X, Position.Y);

            foreach (var bb in BoundingBoxes)
            {
                foreach (var MapTileBB in MapTile.BoundingBoxes)
                {
                    if (!Collided.Contains(MapTileBB.Rectangle))
                        Collided.Add(MapTileBB.Rectangle);

                    if (bb.CollidesWith(MapTileBB))
                    {
                        tempBB = bb;
                    }
                }
            }
            if (_movingLeft)
            {
                LookLeftRect = new Rectangle((int)tempBB.Position.X, (int)tempBB.Position.Y + 3, -10, (int)tempBB.Height - 10);
                if (MapTile.IsCollide(LookLeftRect))
                {
                    _stopLeft = true;
                }
                else if (!LookLeftRect.Intersects(Collided))
                    _stopLeft = false;
            }
            else if (_movingRight)
            {
                LookRightRect = new Rectangle((int)tempBB.Position.X + (int)tempBB.Width, (int)tempBB.Position.Y + 3, 5, (int)tempBB.Height - 10);
                if (MapTile.IsCollide(LookRightRect))
                {
                    _stopRight = true;
                }
                else if (!LookRightRect.Intersects(Collided))
                    _stopRight = false;
            }
            if (_movingUp)
            {
                LookUpRect = new Rectangle((int)tempBB.Position.X + 5, (int)tempBB.Position.Y, (int)tempBB.Width - 13, -10);
                if (MapTile.IsCollide(LookUpRect))
                {
                    _stopUp = true;
                }
                else if (!LookUpRect.Intersects(Collided))
                    _stopUp = false;
            }
            else if (_movingDown)
            {
                LookDownRect = new Rectangle((int)tempBB.Position.X + 5, (int)tempBB.Position.Y + (int)tempBB.Height, (int)tempBB.Width - 13, 5);
                if (MapTile.IsCollide(LookDownRect))
                {
                    _stopDown = true;
                }
                else if (!LookDownRect.Intersects(Collided))
                    _stopDown = false;

            }
            Position = newPosition;

        }
        public void makeMoveAgainfromCollision()
        {
            _stopLeft = false;
            _stopRight = false;
            _stopDown = false;
            _stopUp = false;
            Collided.Clear();
        }

        public void Update(GameTime gametime)
        {
            PlayerSpeed = (float)(600 * gametime.ElapsedGameTime.TotalSeconds);
            Position = _MoveDirection;
            startingPositionX = Position.X;
            startingPositionY = Position.Y;
            CenterPosition.X = Position.X + AnimationCellWidth / 2;
            CenterPosition.Y = Position.Y + AnimationCellHeight / 2;
            if (_currentAnimation != null)
            {
                _currentAnimation.Update(gametime);
            }
        }

        public override void Render(SpriteBatch spriteBatch)
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
            if (flipAnimationHorizontal)
            {
                var effects = SpriteEffects.FlipHorizontally;
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, effects, 1f);
            }
            else
            {
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
                /*foreach (var box in BoundingBoxes)
                {
                    spriteBatch.Draw(_texture, box.Position, box.Rectangle, Color.Red);
                }
                spriteBatch.Draw(blank, LookLeftRect, Color.Blue);
                 spriteBatch.Draw(blank, LookRightRect, Color.Blue);
                spriteBatch.Draw(blank, LookUpRect, Color.Blue);
                spriteBatch.Draw(blank, LookDownRect, Color.Blue);
                spriteBatch.Draw(_texture, _MoveDirection, new Rectangle((int)Position.X, (int)Position.Y, (int)_MoveDirection.X, (int)_MoveDirection.Y), Color.Red);
                */
            }
        }
    }
}