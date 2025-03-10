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
        V,
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
        private List<ActionButton> buttonControls;
        private List<ActionButton> actionButtons;

        //handling remap
        private List<Actions> remapTempActionHolder;
        private List<Keys> remapTempKeyHolder;
        private List<Click> remapTempClickHolder;
        private List<Buttons> remapTempButtonHolder;

        public InputDetector()
        {
            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One);

            //player input is added to this list which is then checked when an action is called and removed when player is no longer inputting that input
            actionKeys = new List<ActionKey>();
            actionClicks = new List<ActionClick>();
            actionButtons = new List<ActionButton>();

            //contains the controls for  player input
            mouseControls = new List<ActionClick>
            {
                new ActionClick(Click.LeftClick, Actions.Confirm),
                new ActionClick(Click.RightClick, Actions.Cancel),
                new ActionClick(Click.MiddleClick, Actions.OpenMenu),
                new ActionClick(Click.None, Actions.MoveUp),
                new ActionClick(Click.None, Actions.MoveDown),
                new ActionClick(Click.None, Actions.MoveLeft),
                new ActionClick(Click.None, Actions.MoveRight),
                //new ActionClick(Click.None, Actions.Interact),
                //new ActionClick(Click.None, Actions.Attack),
                new ActionClick(Click.None, Actions.OpenMenu),
                new ActionClick(Click.None, Actions.Pause)
            };
            keyboardControls = new List<ActionKey>
            {
                new ActionKey(Keys.A, Actions.MoveLeft),
                new ActionKey(Keys.D, Actions.MoveRight),
                new ActionKey(Keys.W, Actions.MoveUp),
                new ActionKey(Keys.S, Actions.MoveDown),
                new ActionKey(Keys.Z, Actions.Confirm),
                //new ActionKey(Keys.Z, Actions.Interact),
                new ActionKey(Keys.X, Actions.Cancel),
                //new ActionKey(Keys.Z, Actions.Attack),
                new ActionKey(Keys.C, Actions.OpenMenu),
                new ActionKey(Keys.P, Actions.Pause),
                new ActionKey(Keys.V, Actions.V),
                new ActionKey(Keys.Escape, Actions.OpenMenu)
            };
            buttonControls = new List<ActionButton>
            {
                new ActionButton(Buttons.LeftThumbstickLeft, Actions.MoveLeft),
                new ActionButton(Buttons.LeftThumbstickRight, Actions.MoveRight),
                new ActionButton(Buttons.LeftThumbstickUp, Actions.MoveUp),
                new ActionButton(Buttons.LeftThumbstickDown, Actions.MoveDown),
                new ActionButton(Buttons.A, Actions.Confirm),
                //new ActionButton(Buttons.A, Actions.Interact),
                new ActionButton(Buttons.B, Actions.Cancel),
                //new ActionButton(Buttons.RightShoulder, Actions.Attack),
                new ActionButton(Buttons.X, Actions.OpenMenu),
                new ActionButton(Buttons.Y, Actions.Pause)
            };

            remapTempActionHolder = new List<Actions>();
            remapTempClickHolder = new List<Click>();
            remapTempKeyHolder = new List<Keys>();
            remapTempButtonHolder = new List<Buttons>();
        }
        public List<ActionClick> GetMouseControls()
        {
            return mouseControls;
        }
        public void SetMouseControls(List<ActionClick> mouseControls)
        {
            this.mouseControls = mouseControls;
        }
        public void SetKeyboardControls(List<ActionKey> keyboardControls)
        {
            this.keyboardControls = keyboardControls;
        }
        public List<ActionKey> GetKeyboardControls()
        {
            return keyboardControls;
        }
        public List<ActionButton> GetButtonControls()
        {
            return buttonControls;
        }
        public void SetButtonControls(List<ActionButton> buttonControls)
        {
            this.buttonControls = buttonControls;
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

        public Actions DoesButtonExistinControls(Buttons buttonToCheck, Actions actionToRemap)
        {
            Actions crossAction = Actions.NoInput;
            if (buttonControls.Exists(x => x.button == buttonToCheck))
            {
                List<ActionButton> checkAButton = buttonControls.FindAll(x => x.button == buttonToCheck);
                if (checkAButton.Count >= 1)
                {
                    if (checkAButton.Exists(x => x.action == Actions.Confirm) && actionToRemap == Actions.Cancel)
                    {
                        return Actions.Confirm;
                    }
                    else if (checkAButton.Exists(x => x.action == Actions.Cancel) && actionToRemap == Actions.Confirm)
                    {
                        return Actions.Cancel;
                    }
                    else
                    {
                        checkAButton.RemoveAll(x => x.action == Actions.Cancel && x.action == Actions.Confirm);
                        if (checkAButton.Count >= 1)
                        {
                            crossAction = checkAButton[0].action;
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
        public void RemapButton(Buttons remappedButton, Actions selectedAction)
        {
            buttonControls.Find(x => x.action == selectedAction).setButton(remappedButton);
        }
        public void HoldRemap(Click remapClick, Actions selectedAction)
        {
            remapTempClickHolder.Add(remapClick);
            remapTempActionHolder.Add(selectedAction);
        }
        public void HoldRemap(Keys remapKey, Actions selectedAction)
        {
            remapTempKeyHolder.Add(remapKey);
            remapTempActionHolder.Add(selectedAction);
        }
        public void HoldRemap(Buttons remapButton, Actions selectedAction)
        {
            remapTempButtonHolder.Add(remapButton);
            remapTempActionHolder.Add(selectedAction);
        }
        public void ConfirmRemap()
        {
            if (remapTempClickHolder.Count > 0)
            {
                for (int i = 0; i < remapTempClickHolder.Count; i++)
                {
                    RemapClick(remapTempClickHolder[i], remapTempActionHolder[i]);
                }
            }
            else if (remapTempKeyHolder.Count > 0)
            {
                for (int i = 0; i < remapTempKeyHolder.Count; i++)
                {
                    RemapKey(remapTempKeyHolder[i], remapTempActionHolder[i]);
                }
            }
            else if (remapTempButtonHolder.Count > 0)
            {
                for (int i = 0; i < remapTempButtonHolder.Count; i++)
                {
                    RemapButton(remapTempButtonHolder[i], remapTempActionHolder[i]);
                }
            }
            remapTempKeyHolder.Clear();
            remapTempClickHolder.Clear();
            remapTempButtonHolder.Clear();
            remapTempActionHolder.Clear();
        }
        public Keys getKeyforAction(Actions selectedAction)
        {
            return keyboardControls.Find(x => x.action == selectedAction).key;
        }
        public Click getClickforAction(Actions selectedAction)
        {
            return mouseControls.Find(x => x.action == selectedAction).click;
        }
        public Buttons getButtonforAction(Actions selectedAction)
        {
            return buttonControls.Find(x => x.action == selectedAction).button;
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
        public bool IsActionPressedforButton(Actions selectedAction)
        {
            if (actionButtons.Exists(x => x.action == selectedAction))
            {
                return true;
            }
            else
                return false;
        }
        public bool IsClickExist(Click selectedClick)
        {
            if (mouseControls.Exists(x => x.click == selectedClick)) { return true; }
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
        public bool IsActioninputtedbyTypeforButton(Actions selectedAction, InputType inputType)
        {
            ActionButton actionCheck = new ActionButton(buttonControls.Find(x => x.action == selectedAction));
            actionCheck.type = inputType;
            if (actionButtons.Exists(x => x.action == selectedAction && x.type == inputType))
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
            RemapClick(Click.None, Actions.MoveLeft);
            RemapClick(Click.None, Actions.MoveRight);
            RemapClick(Click.None, Actions.Interact);
            RemapClick(Click.None, Actions.Attack);
            RemapClick(Click.None, Actions.OpenMenu);
            RemapClick(Click.None, Actions.Pause);
            RemapButton(Buttons.LeftThumbstickLeft, Actions.MoveLeft);
            RemapButton(Buttons.LeftThumbstickRight, Actions.MoveRight);
            RemapButton(Buttons.LeftThumbstickUp, Actions.MoveUp);
            RemapButton(Buttons.LeftThumbstickDown, Actions.MoveDown);
            RemapButton(Buttons.A, Actions.Confirm);
            RemapButton(Buttons.A, Actions.Interact);
            RemapButton(Buttons.B, Actions.Cancel);
            RemapButton(Buttons.RightShoulder, Actions.Attack);
            RemapButton(Buttons.X, Actions.OpenMenu);
            RemapButton(Buttons.Y, Actions.Pause);
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
                if (actionClicks.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionClicks.RemoveAll(x => x.action == action && x.type == InputType.Hold);
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

        public void PressGamePadButton(GamePadState gamePadState, Actions action)
        {
            Buttons checkButton = buttonControls.Find(x => x.action == action).button;
            ActionButton tempActionButton = new ActionButton(buttonControls.Find(x => x.action == action));

            if (gamePadState.IsButtonDown(checkButton) && oldGamePadState.IsButtonDown(checkButton))
            {
                tempActionButton.type = InputType.Hold;
                if (!actionButtons.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionButtons.Add(tempActionButton);
                }
            }
            else
            {
                if (actionButtons.Exists(x => x.action == action && x.type == InputType.Hold))
                {
                    actionButtons.RemoveAll(x => x.action == action && x.type == InputType.Hold);
                }
            }
            if (gamePadState.IsButtonDown(checkButton) && oldGamePadState.IsButtonUp(checkButton))
            {
                tempActionButton.type = InputType.Press;
                if (!actionButtons.Contains(tempActionButton))
                {
                    actionButtons.Add(tempActionButton);
                }
            }
            else
            {
                if (actionButtons.Exists(x => x.action == action && x.type == InputType.Press))
                {
                    actionButtons.RemoveAll(x => x.action == action && x.type == InputType.Press);
                }
            }
            if (gamePadState.IsButtonUp(checkButton) && oldGamePadState.IsButtonDown(checkButton))
            {
                tempActionButton.type = InputType.Release;
                if (!actionButtons.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionButtons.Add(tempActionButton);
                }
            }
            else
            {
                if (actionButtons.Exists(x => x.action == action && x.type == InputType.Release))
                {
                    actionButtons.RemoveAll(x => x.action == action && x.type == InputType.Release);
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
        public void SetOldGamePadState(GamePadState gamePadState)
        {
            oldGamePadState = gamePadState;
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
        public void update(GamePadState gamePadState)
        {
            for (int c = 0; c < buttonControls.Count; c++)
            {
                PressGamePadButton(gamePadState, buttonControls[c].action);
            }

            oldGamePadState = gamePadState;
        }
    }
}
