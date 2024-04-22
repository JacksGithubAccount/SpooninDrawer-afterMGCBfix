using Microsoft.Xna.Framework.Input;
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
    public class InputDetector
    {
        private KeyboardState oldState;
        private MouseState oldMouseState;
        private List<ActionKey> controls;
        private List<ActionKey> actionKeys;
        public InputDetector()
        {
            oldState = Keyboard.GetState();

            //player input is added to this list which is then checked when an action is called and removed when player is no longer inputting that input
            actionKeys = new List<ActionKey>();


            //contains the controls for  player input
            controls = new List<ActionKey>
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
            if (controls.Exists(x => x.key == keyToCheck))
            {
                List<ActionKey> checkAKey = controls.FindAll(x => x.key == keyToCheck);
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
        public bool IsAnyButtonInputTyped(InputType inputType)
        {
            return actionKeys.Exists(x => x.type == inputType);
        }
        public void Remap(Keys remappedKey, Actions selectedAction)
        {
            controls.Find(x => x.action == selectedAction).setKey(remappedKey);
        }
        public Keys getKeyforAction(Actions selectedAction)
        {
            return controls.Find(x => x.action == selectedAction).key;
        }
        public bool IsActionPressed(Actions selectedAction)
        {
            if (actionKeys.Exists(x => x.action == selectedAction))
            {
                return true;
            }
            else
                return false;
        }
        public bool IsActioninputtedbyType(Actions selectedAction, InputType inputType)
        {
            ActionKey actionCheck = new ActionKey(controls.Find(x => x.action == selectedAction));
            actionCheck.type = inputType;
            if (actionKeys.Exists(x => x.action == selectedAction && x.type == inputType))
            {
                return true;
            }
            else
                return false;
        }

        public void resetKeystoDefault()
        {
            Remap(Keys.Left, Actions.MoveLeft);
            Remap(Keys.Right, Actions.MoveRight);
            Remap(Keys.Up, Actions.MoveUp);
            Remap(Keys.Down, Actions.MoveDown);
            Remap(Keys.Z, Actions.Confirm);
            Remap(Keys.Z, Actions.Interact);
            Remap(Keys.X, Actions.Cancel);
            Remap(Keys.Z, Actions.Attack);
            Remap(Keys.C, Actions.OpenMenu);
            Remap(Keys.P, Actions.Pause);
        }
        public void PressButton(KeyboardState keyState, Actions action)
        {
            Keys checkKey = controls.Find(x => x.action == action).key;
            ActionKey tempActionKey = new ActionKey(controls.Find(x => x.action == action));

            if (keyState.IsKeyDown(checkKey) && oldState.IsKeyDown(checkKey))
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
            if (keyState.IsKeyDown(checkKey) && oldState.IsKeyUp(checkKey))
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
            if (keyState.IsKeyUp(checkKey) && oldState.IsKeyDown(checkKey))
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
            //if (keyState.IsKeyUp(tempActionKey.key))
            //{
            //    if (actionKeys.Exists(x => x.action == action))//(actionKeys.Contains(controls.Find(x => x.action == action)))
            //        actionKeys.RemoveAll(x => x.action == action);
            //}
        }
        public void SetOldKeyboardState(KeyboardState keyboardState)
        {
            oldState = keyboardState;
        }
        public void update(KeyboardState keyState)
        {
            for (int c = 0; c < controls.Count; c++)
            {
                PressButton(keyState, controls[c].action);
            }

            //if (keyState.GetPressedKeys().Length == 0)
            //{
            //    actionKeys.Clear();
            //}
            oldState = keyState;
        }
    }
}
