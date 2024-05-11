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
                commands.Add(FindConfirm(commandState));


            }
            if (RemapChecker && currentKeyboardState.GetPressedKeyCount() == 0 && previousKeyboardState.GetPressedKeyCount() != 0 && !screenTransition)
            {
                screenTransition = true;
                Keys inputKey = previousKeyboardState.GetPressedKeys()[0];
                inputDetector.RemapKey(inputKey, RemapActionHolder);
                //reloads remap controls screen to update the new keybinds
                commands.Add(new BackSelect());
                commands.Add(new BackSelect());
                commands.Add(new RemapControlSelect());
                commands.Add(new RemapControlDone());
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

            if (RemapChecker && currentMouseState.GetPressedClickCount() == 0 && previousMouseState.GetPressedClickCount() != 0 && !screenTransition)
            {
                screenTransition = true;
                Click inputClick = previousMouseState.GetPressedClicks()[0];
                inputDetector.RemapClick(inputClick, RemapActionHolder);
                //reloads remap controls screen to update the new keybinds
                commands.Add(new BackSelect());
                commands.Add(new BackSelect());
                commands.Add(new RemapControlSelect());
                commands.Add(new RemapControlDone());
                RemapChecker = false;

            }

            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Confirm, InputType.Release) && !screenTransition)
            {
                if (splashState.GetMousePositionHandler().IsMouseOverButton())
                {
                    string commandState = splashState.GetCommandStateforMouse();
                    screenTransition = true;
                    commands.Add(FindConfirm(commandState));
                }
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
        private SplashInputCommand FindConfirm(string commandState)
        {
            switch (commandState)
            {
                case "GameSelect":
                    return new GameSelect();

                case "LoadSelect":
                    return new LoadSelect();

                case "SettingsSelect":
                    return new SettingsSelect();

                case "ExitSelect":
                    return new ExitSelect();

                case "ResumeSelect":
                    return new ResumeSelect();
                case "CheckMenuSelect":
                    return new CheckMenuSelect();

                case "BackSelect":
                    return new BackSelect();

                case "MoveArrowRight":
                    return new MenuMoveRight();

                case "Fullscreen":
                    return new SetFullScreen();

                case "Windows":
                    return new SetWindowScreen();

                case "Borderless":
                    return new SetBorderlessScreen();

                case "Resolution1080":
                    return new SetResolution1080();

                case "Resolution720":
                    return new SetResolution720();

                case "Controls":
                    return new RemapControlSelect();

                //cases to handle remapping controls
                case "RemapSelectConfirm":

                    RemapChecker = true;
                    RemapActionHolder = Actions.Confirm;
                    return new RemapControlConfirm();
                case "RemapSelectCancel":

                    RemapChecker = true;
                    RemapActionHolder = Actions.Cancel;
                    return new RemapControlConfirm();
                case "RemapSelectUp":

                    RemapChecker = true;
                    RemapActionHolder = Actions.MoveUp;
                    return new RemapControlConfirm();
                case "RemapSelectDown":

                    RemapChecker = true;
                    RemapActionHolder = Actions.MoveDown;
                    return new RemapControlConfirm();
                case "RemapSelectLeft":
                    RemapChecker = true;
                    RemapActionHolder = Actions.MoveLeft;
                    return new RemapControlConfirm();
                case "RemapSelectRight":

                    RemapChecker = true;
                    RemapActionHolder = Actions.MoveRight;
                    return new RemapControlConfirm();
                case "RemapSelectOpenMenu":

                    RemapChecker = true;
                    RemapActionHolder = Actions.OpenMenu;
                    return new RemapControlConfirm();
                case "RemapSelectPause":

                    RemapChecker = true;
                    RemapActionHolder = Actions.Pause;
                    return new RemapControlConfirm();

                //remap end
                case "VolumeBGM":
                    return new SettingVolumeBGMSelect();

                case "VolumeSE":
                    return new SettingVolumeSESelect();

            }
            return new BackSelect();
        }
    }
}
