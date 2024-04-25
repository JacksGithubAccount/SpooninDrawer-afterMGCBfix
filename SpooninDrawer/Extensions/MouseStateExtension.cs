using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Extensions
{
    public static class MouseStateExtension
    {
        public static bool IsClickDown(this MouseState mouseState, Click click)
        {
            if (click == Click.LeftClick)
            {
                return mouseState.LeftButton == ButtonState.Pressed;
            }
            else if (click == Click.RightClick)
            {
                return mouseState.RightButton == ButtonState.Pressed;
            }
            else if (click == Click.MiddleClick)
            {
                return mouseState.MiddleButton == ButtonState.Pressed;
            }
            else if (click == Click.ScrollUp)
            {
                return mouseState.ScrollWheelValue > 0;
            }
            else if (click == Click.ScrollDown)
            {
                return mouseState.ScrollWheelValue < 0;
            }
            else
            {
                return false;
            }
        }
        public static bool IsClickUp(this MouseState mouseState, Click click)
        {
            if (click == Click.LeftClick)
            {
                return mouseState.LeftButton == ButtonState.Released;
            }
            else if (click == Click.RightClick)
            {
                return mouseState.RightButton == ButtonState.Released;
            }
            else if (click == Click.MiddleClick)
            {
                return mouseState.MiddleButton == ButtonState.Released;
            }
            else if (click == Click.ScrollUp || click == Click.ScrollDown)
            {
                return mouseState.ScrollWheelValue == 0;
            }
            else
            {
                return false;
            }
        }
    }
}
