using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public enum Click
    {
        LeftClick, 
        RightClick, 
        MiddleClick,
        ScrollUp,
        ScrollDown,
        None
    }
    class ActionClick
    {
        public Click click;
        public Actions action;
        public InputType type;

        public ActionClick(Click inputClick, Actions clickAction)
        {
            click = inputClick;
            action = clickAction;
        }
        public ActionClick(Click inputClick, Actions clickAction, InputType inputType):this(inputClick, clickAction)
        {
            type = inputType;
        }
        public ActionClick(ActionClick actionClick) : this(actionClick, InputType.NoInput) { }
        public ActionClick(ActionClick actionClick, InputType inputType)
        {
            click = actionClick.click;
            action = actionClick.action;
            type = inputType;
        }
        
        public void setClickAction(Click inputClick, Actions keyAction)
        {
            click = inputClick;
            action = keyAction;
        }
        public void setClick(Click inputClick)
        {
            click = inputClick;
        }
    }


}
