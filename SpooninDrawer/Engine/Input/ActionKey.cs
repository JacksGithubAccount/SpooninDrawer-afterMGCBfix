using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.States.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SpooninDrawer.Engine.Input
{
    class ActionKey
    {
        public Keys key;
        public Actions action;
        public InputType type;
        

        public ActionKey(Keys inputKey, Actions keyAction)
        {
            key = inputKey;
            action = keyAction;
        }
        public ActionKey(ActionKey actionKey)
        {
            key = actionKey.key;
            action = actionKey.action;
            type = InputType.NoInput;
        }
        public ActionKey(ActionKey actionKey, InputType inputType)
        {
            key = actionKey.key;
            action = actionKey.action;
            type = inputType;
        }
        public ActionKey(Keys inputKey, Actions keyAction, InputType inputType)
        {
            key = inputKey;
            action = keyAction;
            type = inputType;
        }
        public void setKeyAction(Keys inputKey, Actions keyAction)
        {
            key = inputKey;
            action = keyAction;
        }
        public void setKey(Keys inputKey)
        {
            key = inputKey;
        }
    }
}
