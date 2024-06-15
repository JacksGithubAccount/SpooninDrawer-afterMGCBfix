using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Extensions
{
    public static class GamePadStateExtension
    {
        public static List<Buttons> GetPressedButtons(this GamePadState gamePadState)
        {
            List<Buttons> pressedButtons = new List<Buttons>();
            for (int i = 0; i < Enum.GetNames(typeof(Buttons)).Length; i++)
            {
                if (gamePadState.IsButtonDown((Buttons)i))
                {
                    pressedButtons.Add((Buttons)i);
                }
            }
            return pressedButtons;
        }

        public static int GetPressedButtonCount(this GamePadState gamePadState)
        {
            int pressedButtons = 0;
            foreach (var property in gamePadState.Buttons.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                if (type == typeof(ButtonState))
                {
                    ButtonState button = (ButtonState)property.GetValue(gamePadState.Buttons, null);
                    if (button.Equals(ButtonState.Pressed))
                    {
                        pressedButtons++;
                    }
                }
            }
            return pressedButtons;
        }
    }
}
