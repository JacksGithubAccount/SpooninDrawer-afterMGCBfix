﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.Objects.Animations;
using Animation = SpooninDrawer.Engine.Objects.Animations.Animation;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractableOverworldObject : CollidableGameObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }

        private Animation IdleAnimation = new Animation(false);
        private Animation InteractAnimation = new Animation(false);
        private Animation FinishAnimation = new Animation(false);
        private Animation EndingAnimation = new Animation(false);
        private const int AnimationSpeed = 3;
        private const int AnimationCellWidth = 116;
        private const int AnimationCellHeight = 152;

        private Animation _currentAnimation;
        private Rectangle _idleRectangle => _rectangle;

        private bool Up = false;
        private bool Down = false;
        private bool Left = false;
        private bool Right = false;

        private Action InteractUp;
        private Action InteractDown;
        private Action InteractLeft;
        private Action InteractRight;

        public bool Collidable;

        public InteractableOverworldObject(int ID, string Name, string texturePath, Texture2D texture, AnimationData idle, AnimationData interact, AnimationData finish, AnimationData end) : base(new Rectangle(0, 0, AnimationCellWidth, AnimationCellHeight))
        {
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
            _texture = texture;
            this.ID = ID;
            this.Name = Name;
            TexturePath = texturePath;
            IdleAnimation = new Animation(idle);
            InteractAnimation = new Animation(interact);
            FinishAnimation = new Animation(finish);
            EndingAnimation = new Animation(end);
            _currentAnimation = IdleAnimation;
        }
        public void Interact() { _currentAnimation = InteractAnimation; }
        public void Update(GameTime gametime)
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Update(gametime);
            }
        }
        public void SetInteractableDirections(bool up, bool down, bool left, bool right)
        {
            Up = up; Down = down; Left = left; Right = right;
        }
        public void setInteractions(Action Up, Action Down, Action Left, Action Right)
        {
            InteractUp = Up; 
            InteractDown = Down;
            InteractLeft = Left; 
            InteractRight = Right;
        }
        public void Interact(Vector2 Direction)
        {
            if(Direction.X>0 && Left)
            {
                //Interaction();
            }
        }
        public void InteractOpen()
        {

        }
        public void InteractDoesNotOpen()
        {

        }
        public void InteractClose()
        {

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
                foreach (var box in BoundingBoxes)
                {
                    spriteBatch.Draw(_texture, box.Position, box.Rectangle, Color.Red);
                }
            }
        }
    }
}