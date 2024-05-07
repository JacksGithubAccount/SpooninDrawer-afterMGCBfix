using Microsoft.Xna.Framework;
using SpooninDrawer.Objects;
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
        public List<SplashRectangle[]> ButtonRectangles { get; }

        protected BaseScreenwithButtons() { ButtonRectangles = new List<SplashRectangle[]>(); }

        //use for single column or row or if need every rectangles
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers)
        {
            int[] xRectangleLimits = new int[yRectangleNumbers.Length];
            for (int i = 0; i < yRectangleNumbers.Length; i++)
            {
                xRectangleLimits[i] = xRectangleNumbers.Length - 1;
            }
            CreateRectangles(xRectangleNumbers, yRectangleNumbers, xRectangleLimits);
        }
        //use for different amount of buttons row-wise
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers, int[] xRectangleLimits)
        {
            for (int y = 0; y < yRectangleNumbers.Length; y++)
            {
                ButtonRectangles.Add(new SplashRectangle[xRectangleLimits[y] + 1]);
                for (int x = 0; x <= xRectangleLimits[y]; x++)
                {
                    ButtonRectangles[y][x] = (new SplashRectangle(new Rectangle(xRectangleNumbers[x], yRectangleNumbers[y], ButtonWidth, ButtonHeight)));
                }
            }
        }
        //use for different amount of buttons row and column wise
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers, int[] xRectangleLimits, int[] yRectangleLimits)
        {
            for (int y = 0; y < yRectangleNumbers.Length; y++)
            {
                ButtonRectangles.Add(new SplashRectangle[xRectangleLimits[y] + 1]);
                for (int x = 0; x <= xRectangleLimits[y]; x++)
                {
                    if (yRectangleLimits[x] != 0 && yRectangleLimits[x] > y)
                        ButtonRectangles[y][x] = new SplashRectangle(new Rectangle(xRectangleNumbers[x], yRectangleNumbers[y], ButtonWidth, ButtonHeight));
                    else
                        ButtonRectangles[y][x] = new SplashRectangle();
                }                
            }
        }
    }
}
