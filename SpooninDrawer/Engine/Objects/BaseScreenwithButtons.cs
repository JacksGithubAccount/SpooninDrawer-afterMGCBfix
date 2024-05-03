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
        protected int ButtonsAmount;
        public List<Rectangle[]> ButtonRectangles { get; }




        protected BaseScreenwithButtons() { ButtonRectangles = new List<Rectangle[]>(); }
        public void CreateRectangles(int[] xRectangleNumbers, int[] yRectangleNumbers)
        {
            for (int x = 0; x < xRectangleNumbers.Length; x++)
            {
                ButtonRectangles.Add(new Rectangle[yRectangleNumbers.Length]);
                for (int y = 0; y < yRectangleNumbers.Length; y++)
                {
                    ButtonRectangles[x][y] = (new Rectangle(xRectangleNumbers[x], yRectangleNumbers[y], ButtonWidth, ButtonHeight));
                }
            }            
        }
    }    
}
