using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class KeyboardPositionHandler
    {
        private KeyboardState keyboardState;
        private BaseScreenwithButtons screen;
        private Vector2 KeyPosition;

        public KeyboardPositionHandler(KeyboardState keyboardState, BaseScreenwithButtons screen, Vector2 position)
        {
            this.keyboardState = keyboardState;
            this.screen = screen;

        }
        

        
    }
}
