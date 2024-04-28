using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace SpooninDrawer.Extensions
{
    public static class RectangleExtension
    {
        //extension for rectangle used in collision detecting with more than one object
        public static bool Intersects(this Rectangle rect, List<Rectangle> rectangles)
        {
            foreach (Rectangle rectangle in rectangles)
            {
                if( rect.Intersects(rectangle))
                    return true;
            }
            return false;
        }
        //extension for rectangle used in mouse detection
        public static bool Intersects(this Rectangle rect, Point position)
        {
            if (position.X < rect.Right && rect.Left < position.X && position.Y < rect.Bottom)
            {
                return rect.Top < position.Y;
            }

            return false;
        }
    }
}
