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
        public static List<Click> GetPressedClicks(this MouseState mouseState)
        {
            List<Click> pressedClicks = new List<Click>();
            for (int i = 0; i < Enum.GetNames(typeof(Click)).Length; i++)
            {
                if (IsClickDown(mouseState, (Click)i))
                {
                    pressedClicks.Add((Click)i);
                }
            }
            return pressedClicks;
        }
        public static int GetPressedClickCount(this MouseState mouseState)
        {
            int pressedClicks = 0;
            if (mouseState.LeftButton == ButtonState.Pressed)
                pressedClicks++;
            if (mouseState.RightButton == ButtonState.Pressed)
                pressedClicks++;
            if (mouseState.MiddleButton == ButtonState.Pressed)
                pressedClicks++;
            return pressedClicks;
        }
    }
}
