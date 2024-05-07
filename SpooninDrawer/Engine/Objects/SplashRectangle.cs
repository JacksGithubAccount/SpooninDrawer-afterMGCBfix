using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects
{
    public class SplashRectangle
    {
        public Rectangle Rectangle;
        public bool ReadOnly = false;
        public SplashRectangle() : this(new Rectangle()) { }
        public SplashRectangle(bool isReadOnly) : this(new Rectangle(), isReadOnly) { }
        public SplashRectangle(Rectangle rectangle) : this(rectangle, false) { }
        public SplashRectangle(Rectangle rectangle, bool isReadOnly)
        {
            Rectangle = rectangle;
            ReadOnly = isReadOnly;
        }
    }
}
