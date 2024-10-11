using Microsoft.Xna.Framework;
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
                //var deltaX = value.X - _position.X;
                //var deltaY = value.Y - _position.Y;
                _position = value;

                foreach (var bb in _boundingBoxes)
                {
                    //bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
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
            return _rectangle.Intersects(rectangle);
        }

    }
}