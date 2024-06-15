using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Extensions;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.States.Dev;
using SpooninDrawer.States.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpooninDrawer.States.Splash.SplashInputCommand;
using static System.Collections.Specialized.BitVector32;

namespace SpooninDrawer.States.Splash
{
    public class SplashInputMapper : BaseInputMapper
    {

        SplashState splashState;
        private bool RemapChecker;
        private Actions RemapActionHolder;
        //used to stop the screen from activating the selected option when Z/enter is pressed and changing screens
        private bool screenTransition = false;
        private MousePositionHandler mousePositionHandler;
        private InputDevice remapDevice;
        private bool remapDuplicatePopup = false;
        public SplashInputMapper(SplashState currentSplashState, MousePositionHandler mousePositionHandler) : this(currentSplashState, new InputDetector())
        {
            this.mousePositionHandler = mousePositionHandler;
        }
        public SplashInputMapper(SplashState currentSplashState, InputDetector inputDetector)
        {

            splashState = currentSplashState;
            this.inputDetector = inputDetector;
            RemapChecker = false;
        }
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            previousKeyboardState = currentKeyboardState;
            inputDetector.update(previousKeyboardState);
            currentKeyboardState = state;
            var commands = new List<SplashInputCommand>();


            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Confirm, InputType.Release) && !screenTransition)
            {
                string commandState = splashState.GetCommandStateforKey();
                screenTransition = true;
                //commands.Add(FindConfirm(commandState));
                FindConfirm(commandState, commands);


            }
            if (RemapChecker && currentKeyboardState.GetPressedKeyCount() == 0 && previousKeyboardState.GetPressedKeyCount() != 0 && !screenTransition && remapDevice == InputDevice.Keyboard)
            {
                screenTransition = true;
                Keys inputKey = previousKeyboardState.GetPressedKeys()[0];
                Actions duplicateRemapActions = inputDetector.DoesKeyExistinControls(inputKey, RemapActionHolder);
                //if key is used for another action, this if swaps the keys for the two actions
                if (duplicateRemapActions != RemapActionHolder)
                {
                    remapDuplicatePopup = true;
                    Keys switchingKey = inputDetector.getKeyforAction(RemapActionHolder);
                    splashState.ChangePopupDescriptionText(inputKey.ToString() + " is already mapped to " + duplicateRemapActions.ToString());
                    commands.Add(new RemapControlDuplicate());
                    inputDetector.HoldRemap(switchingKey, duplicateRemapActions);
                    inputDetector.HoldRemap(inputKey, RemapActionHolder);
                }
                if (!remapDuplicatePopup)
                {
                    commands.Add(new RemapControlDone());
                    inputDetector.RemapKey(inputKey, RemapActionHolder);

                }
                RemapChecker = false;
            }
            if (state.IsKeyDown(Keys.T) && HasBeenPressed(Keys.T))
            {
                commands.Add(new TestMenuButton());
            }
            if (state.IsKeyDown(Keys.R) && HasBeenPressed(Keys.R))
            {
                commands.Add(new TestMenuButton2());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Cancel, InputType.Press))
            {
                commands.Add(new BackSelect());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveUp, InputType.Press))
            {
                commands.Add(new MenuMoveUp());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveDown, InputType.Press))
            {
                commands.Add(new MenuMoveDown());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveLeft, InputType.Press))
            {
                commands.Add(new MenuMoveLeft());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveRight, InputType.Press))
            {
                commands.Add(new MenuMoveRight());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveLeft, InputType.Hold))
            {
                commands.Add(new MenuHoldLeft());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.MoveRight, InputType.Hold))
            {
                commands.Add(new MenuHoldRight());
            }
            if (currentKeyboardState.GetPressedKeyCount() == 0 && previousKeyboardState.GetPressedKeyCount() == 0)
            {
                screenTransition = false;
            }
            return commands;
        }
        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            previousMouseState = currentMouseState;
            inputDetector.update(previousMouseState);
            currentMouseState = state;
            var commands = new List<SplashInputCommand>();

            if (RemapChecker && currentMouseState.GetPressedClickCount() == 0 && previousMouseState.GetPressedClickCount() != 0 && !screenTransition && remapDevice == InputDevice.Mouse)
            {
                screenTransition = true;
                Click inputClick = previousMouseState.GetPressedClicks()[0];
                Actions duplicateRemapActions = inputDetector.DoesClickExistinControls(inputClick, RemapActionHolder);
                //if key is used for another action, this if swaps the keys for the two actions
                if (duplicateRemapActions != RemapActionHolder)
                {
                    remapDuplicatePopup = true;
                    Click switchingClick = inputDetector.getClickforAction(RemapActionHolder);
                    splashState.ChangePopupDescriptionText(inputClick.ToString() + " is already mapped to " + duplicateRemapActions.ToString());
                    commands.Add(new RemapControlDuplicate());
                    inputDetector.HoldRemap(switchingClick, duplicateRemapActions);
                    inputDetector.HoldRemap(inputClick, RemapActionHolder);
                }
                if (!remapDuplicatePopup)
                {
                    inputDetector.RemapClick(inputClick, RemapActionHolder);
                    commands.Add(new RemapControlDone());
                }
                RemapChecker = false;

            }
            if (!RemapChecker)
            {
                if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Confirm, InputType.Release) && !screenTransition)
                {
                    if (splashState.GetMousePositionHandler().IsMouseOverButton() || !splashState.IsCurrentScreenHasButtons())
                    {
                        string commandState = splashState.GetCommandStateforMouse();
                        screenTransition = true;
                        FindConfirm(commandState, commands);

                        switch (commandState)
                        {
                            case "MouseVolumeBGM":
                                commands.Add(new MouseVolumeBGM());
                                break;
                            case "MouseVolumeSE":
                                commands.Add(new MouseVolumeSE());
                                break;
                        }
                    }
                }
                if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Confirm, InputType.Hold))
                {
                    if (splashState.GetMousePositionHandler().IsMouseOverButton() || !splashState.IsCurrentScreenHasButtons())
                    {
                        string commandState = splashState.GetCommandStateforMouse();
                        screenTransition = true;
                        //FindConfirm(commandState, commands);

                        switch (commandState)
                        {
                            case "MouseVolumeBGM":
                                commands.Add(new MouseVolumeBGM());
                                break;
                            case "MouseVolumeSE":
                                commands.Add(new MouseVolumeSE());
                                break;
                        }
                    }
                }
                if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Cancel, InputType.Press))
                {
                    commands.Add(new BackSelect());
                }
            }
            return commands;
        }

        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            previousGamePadState = currentGamePadState;
            inputDetector.update(previousGamePadState);
            currentGamePadState = state;
            var commands = new List<SplashInputCommand>();


            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Confirm, InputType.Release) && !screenTransition)
            {
                string commandState = splashState.GetCommandStateforButton();
                screenTransition = true;
                FindConfirm(commandState, commands);


            }
            if (RemapChecker && currentGamePadState.GetPressedButtonCount() == 0 && previousGamePadState.GetPressedButtonCount() != 0 && !screenTransition && remapDevice == InputDevice.Gamepad)
            {
                screenTransition = true;
                Buttons inputButton = previousGamePadState.GetPressedButtons()[0];
                Actions duplicateRemapActions = inputDetector.DoesButtonExistinControls(inputButton, RemapActionHolder);
                //if key is used for another action, this if swaps the keys for the two actions
                if (duplicateRemapActions != RemapActionHolder)
                {
                    remapDuplicatePopup = true;
                    Buttons switchingButton = inputDetector.getButtonforAction(RemapActionHolder);
                    splashState.ChangePopupDescriptionText(inputButton.ToString() + " is already mapped to " + duplicateRemapActions.ToString());
                    commands.Add(new RemapControlDuplicate());
                    inputDetector.HoldRemap(switchingButton, duplicateRemapActions);
                    inputDetector.HoldRemap(inputButton, RemapActionHolder);
                }
                if (!remapDuplicatePopup)
                {
                    commands.Add(new RemapControlDone());
                    inputDetector.RemapButton(inputButton, RemapActionHolder);

                }
                RemapChecker = false;
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Cancel, InputType.Press))
            {
                commands.Add(new BackSelect());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveUp, InputType.Press))
            {
                commands.Add(new MenuMoveUp());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveDown, InputType.Press))
            {
                commands.Add(new MenuMoveDown());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveLeft, InputType.Press))
            {
                commands.Add(new MenuMoveLeft());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveRight, InputType.Press))
            {
                commands.Add(new MenuMoveRight());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveLeft, InputType.Hold))
            {
                commands.Add(new MenuHoldLeft());
            }
            if (inputDetector.IsActioninputtedbyTypeforButton(Actions.MoveRight, InputType.Hold))
            {
                commands.Add(new MenuHoldRight());
            }
            if (currentGamePadState.GetPressedButtonCount() == 0 && previousGamePadState.GetPressedButtonCount() == 0)
            {
                screenTransition = false;
            }
            return commands;
        }
        private bool isKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        private bool IsKeyTriggered(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key)) &&
                (!previousKeyboardState.IsKeyDown(key));
        }
        private bool HasBeenPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
        public void ResetRemaps()
        {
            inputDetector.resetKeystoDefault();
        }
        private void FindConfirm(string commandState, List<SplashInputCommand> commands)
        {
            switch (commandState)
            {
                case "GameSelect":
                    commands.Add(new GameSelect());
                    break;
                case "LoadSelect":
                    commands.Add(new LoadSelect());
                    break;
                case "SettingsSelect":
                    commands.Add(new SettingsSelect());
                    break;
                case "ExitSelect":
                    commands.Add(new ExitSelect());
                    break;
                case "ResumeSelect":
                    commands.Add(new ResumeSelect());
                    break;
                case "CheckMenuSelect":
                    commands.Add(new CheckMenuSelect());
                    break;
                case "BackSelect":
                    commands.Add(new BackSelect());
                    break;
                case "MoveArrowRight":
                    commands.Add(new MenuMoveRight());
                    break;
                case "Fullscreen":
                    commands.Add(new SetFullScreen());
                    break;
                case "Windows":
                    commands.Add(new SetWindowScreen());
                    break;
                case "Borderless":
                    commands.Add(new SetBorderlessScreen());
                    break;
                case "Resolution1080":
                    commands.Add(new SetResolution1080());
                    break;
                case "Resolution720":
                    commands.Add(new SetResolution720());
                    break;
                case "Controls":
                    commands.Add(new RemapControlSelect());
                    break;
                //cases to handle remapping controls
                case "RemapSelectConfirm":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.Confirm;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectCancel":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.Cancel;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectUp":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.MoveUp;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectDown":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.MoveDown;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectLeft":
                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.MoveLeft;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectRight":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.MoveRight;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectOpenMenu":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.OpenMenu;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapSelectPause":

                    RemapChecker = true;
                    remapDevice = InputDevice.Keyboard;
                    RemapActionHolder = Actions.Pause;
                    commands.Add(new RemapControlConfirm());
                    break;
                //mouse
                case "RemapMouseSelectConfirm":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.Confirm;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectCancel":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.Cancel;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectUp":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.MoveUp;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectDown":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.MoveDown;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectLeft":
                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.MoveLeft;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectRight":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.MoveRight;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectOpenMenu":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.OpenMenu;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapMouseSelectPause":

                    RemapChecker = true;
                    remapDevice = InputDevice.Mouse;
                    RemapActionHolder = Actions.Pause;
                    commands.Add(new RemapControlConfirm());
                    break;
                case "RemapAcceptDuplicateSwap":
                    remapDuplicatePopup = false;
                    inputDetector.ConfirmRemap();
                    commands.Add(new RemapControlDone());
                    break;
                case "RemapBackSelect":
                    remapDuplicatePopup = false;
                    commands.Add(new BackSelect());
                    commands.Add(new BackSelect());
                    break;
                //remap end
                case "VolumeBGM":
                    commands.Add(new SettingVolumeBGMSelect());
                    break;
                case "VolumeSE":
                    commands.Add(new SettingVolumeSESelect());
                    break;
            }
        }
    }
}
