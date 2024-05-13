using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Extensions;
using SpooninDrawer.States.Dev;
using SpooninDrawer.States.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public enum Actions
    {
        Confirm,
        Cancel,
        Attack,
        Interact,
        OpenMenu,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Pause,
        NoInput
    }
    public enum InputType
    {
        NoInput,
        Press,
        Release,
        Hold
    }
    public enum InputDevice
    {
        Keyboard,
        Mouse,
        Gamepad
    }
    public class InputDetector
    {
        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;
        private GamePadState oldGamePadState;
        private List<ActionKey> keyboardControls;
        private List<ActionKey> actionKeys;
        private List<ActionClick> mouseControls;
        private List<ActionClick> actionClicks;
        private Action mouseAction;

        public InputDetector()
        {
            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One);

            //player input is added to this list which is then checked when an action is called and removed when player is no longer inputting that input
            actionKeys = new List<ActionKey>();
            actionClicks = new List<ActionClick>();

            mouseControls = new List<ActionClick> 
            {
                new ActionClick(Click.LeftClick, Actions.Confirm),
                new ActionClick(Click.RightClick, Actions.Cancel),
                new ActionClick(Click.MiddleClick, Actions.OpenMenu),
                new ActionClick(Click.ScrollUp, Actions.MoveUp),
                new ActionClick(Click.ScrollDown, Actions.MoveDown),
                new ActionClick(Click.None, Actions.MoveLeft),
                new ActionClick(Click.None, Actions.MoveRight),
                new ActionClick(Click.None, Actions.Interact),
                new ActionClick(Click.None, Actions.Attack),
                new ActionClick(Click.None, Actions.OpenMenu),
                new ActionClick(Click.None, Actions.Pause)
            };
            //contains the controls for  player input
            keyboardControls = new List<ActionKey>
            {
                new ActionKey(Keys.A, Actions.MoveLeft),
                new ActionKey(Keys.D, Actions.MoveRight),
                new ActionKey(Keys.W, Actions.MoveUp),
                new ActionKey(Keys.S, Actions.MoveDown),
                new ActionKey(Keys.Z, Actions.Confirm),
                new ActionKey(Keys.Z, Actions.Interact),
                new ActionKey(Keys.X, Actions.Cancel),
                new ActionKey(Keys.Z, Actions.Attack),
                new ActionKey(Keys.C, Actions.OpenMenu),
                new ActionKey(Keys.P, Actions.Pause)
            };                                     
        }
        public Actions DoesKeyExistinControls(Keys keyToCheck, Actions actionToRemap)
        {
            Actions crossAction = Actions.NoInput;
            if (keyboardControls.Exists(x => x.key == keyToCheck))
            {
                List<ActionKey> checkAKey = keyboardControls.FindAll(x => x.key == keyToCheck);
                if (checkAKey.Count >= 1)
                {
                    if (checkAKey.Exists(x => x.action == Actions.Confirm) && actionToRemap == Actions.Cancel)
                    {
                        return Actions.Confirm;
                    }
                    else if (checkAKey.Exists(x => x.action == Actions.Cancel) && actionToRemap == Actions.Confirm)
                    {
                        return Actions.Cancel;
                    }
                    else
                    {
                        checkAKey.RemoveAll(x => x.action == Actions.Cancel && x.action == Actions.Confirm);
                        if (checkAKey.Count >= 1)
                        {
                            crossAction = checkAKey[0].action;
                        }
                    }
                }
                return crossAction;
            }
            else
                return actionToRemap;
        }
        public Actions DoesClickExistinControls(Click clickToCheck, Actions actionToRemap)
        {
            Actions crossAction = Actions.NoInput;
            if (mouseControls.Exists(x => x.click == clickToCheck))
            {
                List<ActionClick> checkAClick = mouseControls.FindAll(x => x.click == clickToCheck);
                if (checkAClick.Count >= 1)
                {
                    if (checkAClick.Exists(x => x.action == Actions.Confirm) && actionToRemap == Actions.Cancel)
                    {
                        return Actions.Confirm;
                    }
                    else if (checkAClick.Exists(x => x.action == Actions.Cancel) && actionToRemap == Actions.Confirm)
                    {
                        return Actions.Cancel;
                    }
                    else
                    {
                        checkAClick.RemoveAll(x => x.action == Actions.Cancel && x.action == Actions.Confirm);
                        if (checkAClick.Count >= 1)
                        {
                            crossAction = checkAClick[0].action;
                        }
                    }
                }
                return crossAction;
            }
            else
                return actionToRemap;
        }
        public bool IsAnyButtonInputTyped(InputType inputType)
        {
            return actionKeys.Exists(x => x.type == inputType);
        }
        public void RemapKey(Keys remappedKey, Actions selectedAction)
        {
            keyboardControls.Find(x => x.action == selectedAction).setKey(remappedKey);
        }
        public void RemapClick(Click remappedClick, Actions selectedAction)
        {
            mouseControls.Find(x => x.action == selectedAction).setClick(remappedClick);
        }
        public Keys getKeyforAction(Actions selectedAction)
        {
            return keyboardControls.Find(x => x.action == selectedAction).key;
        }
        public Click getClickforAction(Actions selectedAction)
        {
            return mouseControls.Find(x => x.action == selectedAction).click;
        }
        public Actions getActionforClick(Click selectedClick)
        {
            Actions returnAction = Actions.NoInput;
            if (IsClickExist(selectedClick))
                returnAction = mouseControls.Find(x => x.click == selectedClick).action;
            return returnAction;
        }
        public bool IsActionPressedforKey(Actions selectedAction)
        {
            if (actionKeys.Exists(x => x.action == selectedAction))
            {
                return true;
            }
            else
                return false;
        }
        public bool IsClickExist(Click selectedClick)
        {
            if(mouseControls.Exists(x => x.click == selectedClick)) { return true; }
            return false;
        }
        public bool IsActionPressedforClick(Actions selectedAction)
        {
            if (actionClicks.Exists(x => x.action == selectedAction))
            {
                return true;
            }
            else
                return false;
        }
        public bool IsActioninputtedbyTypeforKey(Actions selectedAction, InputType inputType)
        {
            ActionKey actionCheck = new ActionKey(keyboardControls.Find(x => x.action == selectedAction));
            actionCheck.type = inputType;
            if (actionKeys.Exists(x => x.action == selectedAction && x.type == inputType))
            {
                return true;
            }
            else
                return false;
        }
        public bool IsActioninputtedbyTypeforClick(Actions selectedAction, InputType inputType)
        {
            ActionClick actionCheck = new ActionClick(mouseControls.Find(x => x.action == selectedAction));
            actionCheck.type = inputType;
            if (actionClicks.Exists(x => x.action == selectedAction && x.type == inputType))
            {
                return true;
            }
            else
                return false;
        }

        public void resetKeystoDefault()
        {
            RemapKey(Keys.Left, Actions.MoveLeft);
            RemapKey(Keys.Right, Actions.MoveRight);
            RemapKey(Keys.Up, Actions.MoveUp);
            RemapKey(Keys.Down, Actions.MoveDown);
            RemapKey(Keys.Z, Actions.Confirm);
            RemapKey(Keys.Z, Actions.Interact);
            RemapKey(Keys.X, Actions.Cancel);
            RemapKey(Keys.Z, Actions.Attack);
            RemapKey(Keys.C, Actions.OpenMenu);
            RemapKey(Keys.P, Actions.Pause);
            RemapClick(Click.LeftClick, Actions.Confirm);
            RemapClick(Click.RightClick, Actions.Cancel);
            RemapClick(Click.MiddleClick, Actions.OpenMenu);
            RemapClick(Click.ScrollUp, Actions.MoveUp);
            RemapClick(Click.ScrollDown, Actions.MoveDown);
        }

        public void PressButton(KeyboardState keyState, Actions action)
        {
            Keys checkKey = keyboardControls.Find(x => x.action == action).key;
            ActionKey tempActionKey = new ActionKey(keyboardControls.Find(x => x.action == action));

            if (keyState.IsKeyDown(checkKey) && oldKeyboardState.IsKeyDown(checkKey))
            {
                tempActionKey.type = InputType.Hold;
                if (!actionKeys.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Hold))//(actionKeys.Contains(new ActionKey(controls.Find(x => x.action == action), InputType.Hold)))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Hold);
                }
            }
            if (keyState.IsKeyDown(checkKey) && oldKeyboardState.IsKeyUp(checkKey))
            {
                tempActionKey.type = InputType.Press;
                if (!actionKeys.Contains(tempActionKey))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Press))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Press);
                }
            }
            if (keyState.IsKeyUp(checkKey) && oldKeyboardState.IsKeyDown(checkKey))
            {
                tempActionKey.type = InputType.Release;
                if (!actionKeys.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionKeys.Add(tempActionKey);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Release))//(actionKeys.Contains(new ActionKey(controls.Find(x => x.action == action), InputType.Hold)))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Release);
                }
            }
        }
        
        public void MouseClick(MouseState mouseState, Actions action)
        {
            Click checkClick = mouseControls.Find(x => x.action == action).click;
            ActionClick tempActionClick = new ActionClick(mouseControls.Find(x => x.action == action));
            
            if (mouseState.IsClickDown(checkClick) && oldMouseState.IsClickDown(checkClick))
            {
                tempActionClick.type = InputType.Hold;
                if (!actionClicks.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionClicks.Add(tempActionClick);
                }
            }
            else
            {
                if (actionKeys.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionKeys.RemoveAll(x => x.action == action && x.type == InputType.Hold);
                }
            }
            if (mouseState.IsClickDown(checkClick) && oldMouseState.IsClickUp(checkClick))
            {
                tempActionClick.type = InputType.Press;
                if (!actionClicks.Contains(tempActionClick))
                {
                    actionClicks.Add(tempActionClick);
                }
            }
            else
            {
                if (actionClicks.Exists(x => x.action == action && x.type == InputType.Press))
                {
                    actionClicks.RemoveAll(x => x.action == action && x.type == InputType.Press);
                }
            }
            if (mouseState.IsClickUp(checkClick) && oldMouseState.IsClickDown(checkClick))
            {
                tempActionClick.type = InputType.Release;
                if (!actionClicks.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionClicks.Add(tempActionClick);
                }
            }
            else
            {
                if (actionClicks.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionClicks.RemoveAll(x => x.action == action && x.type == InputType.Release);
                }
            }
        }
        public void SetOldKeyboardState(KeyboardState keyboardState)
        {
            oldKeyboardState = keyboardState;
        }
        public void SetOldMouseState(MouseState mouseState)
        {
            oldMouseState = mouseState;
        }
        public void update(KeyboardState keyState)
        {
            for (int c = 0; c < keyboardControls.Count; c++)
            {
                PressButton(keyState, keyboardControls[c].action);
            }

            oldKeyboardState = keyState;
        }
        public void update(MouseState mouseState)
        {
            for (int c = 0; c < mouseControls.Count; c++)
            {
                MouseClick(mouseState, mouseControls[c].action);
            }


            oldMouseState = mouseState;
        }
    }
}
