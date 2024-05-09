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
        /*private KeyboardState keyboardState;
        private BaseScreenwithButtons screen;
        private Vector2 KeyPosition;

        public KeyboardPositionHandler(KeyboardState keyboardState, BaseScreenwithButtons screen, Vector2 position)
        {
            this.keyboardState = keyboardState;
            this.screen = screen;

        }
        public void SetScreen(BaseScreenwithButtons screen)
        {
            this.screen = screen;
        }*/

        public int CheckKeyboardforMove(BaseScreenwithButtons screen, int xPosition, int yPosition, Vector2 direction)
        {
            if (xPosition + (int)direction.X >= screen.ButtonRectangles.Count())
            {
                return 0;
            }
            else
            {
                foreach (SplashRectangle[] rect in screen.ButtonRectangles)
                {
                    if (yPosition + (int)direction.Y > rect.Length)
                    {
                        return 0;
                    }
                }
            }

            if (screen.ButtonRectangles[xPosition + (int)direction.X][yPosition + (int)direction.Y].ReadOnly)
            {
                if (direction.X != 0)
                    CheckKeyboardforMove(screen, xPosition + 1, yPosition, direction);
                else
                    CheckKeyboardforMove(screen, xPosition, yPosition + 1, direction);
            }
            else
            {
                if (direction.X != 0)
                    return xPosition + (int)direction.X;
                else
                    return yPosition + (int)direction.Y;
            }
            return 0;
        }

    }
}
