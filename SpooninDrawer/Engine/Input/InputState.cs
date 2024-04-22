using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class InputState
    {
        public KeyboardState keyboardState;
        public MouseState mouseState;
        public GamePadState gamePadState;
        
        public InputState(KeyboardState keyboardState, MouseState mouseState, GamePadState gamePadState) : this(keyboardState, mouseState)
        { 
            this.gamePadState = gamePadState;
        }
        public InputState(KeyboardState keyboardState, MouseState mouseState)
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
        }
    }
}
