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
        public int CheckKeyboardforMove(iBaseScreen screen, int xPosition, int yPosition, Vector2 direction)
        {
            BaseScreenwithButtons screenWithButtons;
            int finalInt = 0;
            if (screen.hasButtons)
            {
                screenWithButtons = (BaseScreenwithButtons)screen;
                finalInt = CheckKeyboardforMove(screenWithButtons, xPosition, yPosition, direction);
            }
            else
            {
                if (direction.X != 0)
                    return xPosition;
                else
                    return yPosition;
            }
            return finalInt;
        }
        public int CheckKeyboardforMove(BaseScreenwithButtons screen, int xPosition, int yPosition, Vector2 direction)
        {            
            int finalInt = 0;
            try
            {

                if (screen.ButtonRectangles[yPosition + (int)direction.Y][xPosition + (int)direction.X].ReadOnly || !screen.ButtonRectangles[yPosition + (int)direction.Y][xPosition + (int)direction.X].IsKeyboardable)
                {
                    if (direction.X > 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition + 1, yPosition, direction);
                    else if (direction.X < 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition - 1, yPosition, direction);
                    else if (direction.Y > 0)
                        finalInt = CheckKeyboardforMove(screen, xPosition, yPosition + 1, direction);
                    else if (direction.Y < 0)
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
                //if out of bounds, then returns to in bounds based on direction
                if (direction.X > 0) 
                {
                    if (screen.ButtonRectangles[yPosition + (int)direction.Y][0].ReadOnly || !screen.ButtonRectangles[yPosition + (int)direction.Y][0].IsKeyboardable)
                    {
                        finalInt = CheckKeyboardforMove(screen, 0, yPosition, direction);
                    }
                    else                    
                        return xPosition + 1;                    
                }
                else if (direction.X < 0) 
                {
                    if (screen.ButtonRectangles[yPosition + (int)direction.Y][screen.ButtonRectangles[yPosition + (int)direction.Y].Length - 1].ReadOnly || !screen.ButtonRectangles[yPosition + (int)direction.Y][screen.ButtonRectangles[yPosition + (int)direction.Y].Length - 1].IsKeyboardable)
                    {
                        finalInt = CheckKeyboardforMove(screen, screen.ButtonRectangles[yPosition + (int)direction.Y].Length - 1, yPosition, direction); ;
                    }
                    else 
                        return xPosition - 1; 
                }
                if (direction.Y > 0)                 
                {
                    if (screen.ButtonRectangles[0][xPosition + (int)direction.X].ReadOnly || !screen.ButtonRectangles[0][xPosition + (int)direction.X].IsKeyboardable)
                    {
                        finalInt = CheckKeyboardforMove(screen, xPosition, 0, direction);
                    }
                    else
                        return yPosition + 1; 
                }
                else if (direction.Y < 0) 
                {
                    //if overmoving back to a spot that doesn't exist, doens't move at all
                    try
                    {
                        if (screen.ButtonRectangles[yPosition + (int)direction.Y].Length < xPosition)
                        {
                            return 0;
                        }
                    }
                    catch
                    {
                        //return 0;
                    }
                    if(xPosition + (int)direction.X > screen.ButtonRectangles[yPosition].Length)
                    if (screen.ButtonRectangles[screen.ButtonRectangles.Count - 1][xPosition + (int)direction.X].ReadOnly || !screen.ButtonRectangles[screen.ButtonRectangles.Count - 1][xPosition + (int)direction.X].IsKeyboardable)
                    {
                        finalInt = CheckKeyboardforMove(screen, xPosition, screen.ButtonRectangles.Count - 1, direction);
                    }
                    else
                        return yPosition - 1;                 
                }
            }
            return finalInt;
        }
        public Vector2 CheckReadOnlyPositionAtScreenLoad(BaseScreenwithButtons screen, int xPosition, int yPosition)
        {
            Vector2 ReturnVector = new Vector2(xPosition, yPosition);
            if (screen.ButtonRectangles[yPosition][xPosition].ReadOnly)
            {
                if (screen.ButtonRectangles[xPosition].Length > yPosition + 1)
                {
                    for (int i = 0; i < screen.ButtonRectangles[yPosition].Count(); i++)
                    {
                        if (!screen.ButtonRectangles[xPosition][i].ReadOnly)
                        {
                            ReturnVector = new Vector2(i, xPosition);
                            return ReturnVector;
                        } 
                    }
                }
                if (screen.ButtonRectangles.Count > xPosition + 1)
                {
                    for (int i = 0; i < screen.ButtonRectangles.Count; i++)
                    {
                        if (!screen.ButtonRectangles[i][yPosition].ReadOnly)
                        {
                            ReturnVector = new Vector2(yPosition, i);
                            return ReturnVector;
                        }
                    }

                }                
            }
            return ReturnVector;
        }
    }
}
