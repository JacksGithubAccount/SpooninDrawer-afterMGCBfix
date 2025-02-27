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
    public class CollidableGameObject : BaseGameObject
    {
        protected int BBPosX;
        protected int BBPosY;
        protected int BBWidth;
        protected int BBHeight;
        public override Vector2 Position
        {
            get { return _position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX + 1, bb.Position.Y + deltaY + 1);
                    BBPosX = (int)bb.Position.X; BBPosY = (int)bb.Position.Y;
                    _rectangle = bb.Rectangle;
                }
            }
        }

        protected Rectangle _rectangle;

        public bool Collidable;

        public CollidableGameObject(Rectangle rect)
        {
            _rectangle = rect;
            BBPosX = rect.X; BBPosY = rect.Y;
            BBWidth = rect.Width;
            BBHeight = rect.Height;
            Collidable = true;
            AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(BBPosX, BBPosY), BBWidth, BBHeight));
        }
        public CollidableGameObject(Rectangle rect, Texture2D texture): this(rect)
        {
            _texture = texture;
        }        
        public Rectangle GetRectangle()
        {
            return _rectangle;
        }
        public bool IsPointinTile(float X, float Y)
        {
            if (X > BBPosX && X < BBPosX + BBWidth)
            {
                if (Y > BBPosY && Y < BBPosY + BBHeight)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool IsCollide(Rectangle rectangle)
        {
            bool collided = false;
            foreach(var bb in BoundingBoxes)
            {
                collided = bb.Rectangle.Intersects(rectangle);
                if (collided)
                    break;
            }
            return collided;
        }

    }
}