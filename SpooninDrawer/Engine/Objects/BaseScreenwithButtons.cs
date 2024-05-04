using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects
{
    public abstract class BaseScreenwithButtons
    {
        protected int ButtonWidth;
        protected int ButtonHeight;
        public List<Rectangle[]> ButtonRectangles { get; }




        protected BaseScreenwithButtons() { ButtonRectangles = new List<Rectangle[]>(); }
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers)
        {
            int[] xRectangleLimits = new int[yRectangleNumbers.Length];
            for (int i = 0; i < yRectangleNumbers.Length; i++)
            {
                xRectangleLimits[i] = xRectangleNumbers.Length - 1;
            }
            CreateRectangles(xRectangleNumbers, yRectangleNumbers, xRectangleLimits);
        }
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers, int[] xRectangleLimits)
        {
            for (int y = 0; y < yRectangleNumbers.Length; y++)
            {
                ButtonRectangles.Add(new Rectangle[xRectangleLimits[y] + 1]);
                for (int x = 0; x <= xRectangleLimits[y]; x++)
                {
                    ButtonRectangles[y][x] = (new Rectangle(xRectangleNumbers[x], yRectangleNumbers[y], ButtonWidth, ButtonHeight));
                }
            }
        }
    }
}
