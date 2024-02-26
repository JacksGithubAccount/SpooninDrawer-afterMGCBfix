using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class BaseInputMapper
    {
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public InputDetector inputDetector;
        public void SetInputDetector(InputDetector inputDetector)
        {
            this.inputDetector = inputDetector;
        }
        public virtual IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            return new List<BaseInputCommand>();
        }
    }
}
