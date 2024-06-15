using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class ActionButton
    {
        public Buttons button;
        public Actions action;
        public InputType type;


        public ActionButton(Buttons inputButton, Actions keyAction)
        {
            button = inputButton;
            action = keyAction;
        }
        public ActionButton(ActionButton actionButton)
        {
            button = actionButton.button;
            action = actionButton.action;
            type = InputType.NoInput;
        }
        public ActionButton(ActionButton actionButton, InputType inputType)
        {
            button = actionButton.button;
            action = actionButton.action;
            type = inputType;
        }
        public ActionButton(Buttons inputButton, Actions keyAction, InputType inputType)
        {
            button = inputButton;
            action = keyAction;
            type = inputType;
        }
        public void setButtonAction(Buttons inputButton, Actions keyAction)
        {
            button = inputButton;
            action = keyAction;
        }
        public void setButton(Buttons inputButton)
        {
            button = inputButton;
        }
    }
}