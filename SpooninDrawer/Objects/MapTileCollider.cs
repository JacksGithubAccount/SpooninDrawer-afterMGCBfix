using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class MapTileCollider : BaseGameObject
    {
        private int BBPosX;
        private int BBPosY;
        private int BBWidth;
        private int BBHeight;

        private Rectangle _rectangle;

        public MapTileCollider(Rectangle rect)
        {
            _rectangle = rect;
            BBPosX = rect.X; BBPosY = rect.Y;
            BBWidth = rect.Width;
            BBHeight = rect.Height;
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