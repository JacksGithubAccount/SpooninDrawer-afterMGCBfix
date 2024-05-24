using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class KeyboardPositionHandler
    {
        public int CheckKeyboardforMove(BaseScreenwithButtons screen, int xPosition, int yPosition, Vector2 direction)
        {
            int finalInt = 0;
            try
            {

                if (screen.ButtonRectangles[yPosition + (int)direction.Y][xPosition + (int)direction.X].ReadOnly)
                {
                    if (direction.X > 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition + 1, yPosition, direction);
                    else if (direction.X < 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition - 1, yPosition, direction);
                    else if (direction.Y > 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition, yPosition + 1, direction);
                    else
                        finalInt = CheckKeyboardforMove(screen, xPosition, yPosition - 1, direction);
                }
                else
                {
                    if (direction.X != 0)
                        return xPosition + (int)direction.X;
                    else
                        return yPosition + (int)direction.Y;
                }
            }
            catch
            {
                if (direction.X > 0) { return xPosition + 1; }
                else if (direction.X < 0) { return xPosition - 1; }
                if (direction.Y > 0) { return yPosition + 1; }
                else if (direction.Y < 0) { return yPosition - 1; }
            }
            return finalInt;
        }
        public Vector2 CheckReadOnlyPositionAtScreenLoad(BaseScreenwithButtons screen, int xPosition, int yPosition)
        {
            if (screen.ButtonRectangles[xPosition][yPosition].ReadOnly)
            {
                if (screen.ButtonRectangles.Count > xPosition + 1)
                {
                    if (!screen.ButtonRectangles[xPosition + 1][yPosition].ReadOnly)
                    {
                        return new Vector2(xPosition + 1, yPosition);
                    }
                }
                else if (screen.ButtonRectangles[xPosition].Length > yPosition + 1)
                {
                    if (!screen.ButtonRectangles[xPosition][yPosition + 1].ReadOnly)

                    {
                        return new Vector2(xPosition, yPosition + 1);

                    }
                }
            }
            return new Vector2(xPosition, yPosition);
        }
    }
}
