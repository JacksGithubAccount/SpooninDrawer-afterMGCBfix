using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
    }
}
