using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
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
        public SplashInputMapper(SplashState currentSplashState) : this(currentSplashState, new InputDetector()) { }
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


            if (inputDetector.IsActioninputtedbyType(Actions.Confirm, InputType.Release) && !screenTransition)
            {
                string commandState = splashState.GetCommandState();
                screenTransition = true;
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
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.Confirm;
                        break;
                    case "RemapSelectCancel":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.Cancel;
                        break;
                    case "RemapSelectUp":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.MoveUp;
                        break;
                    case "RemapSelectDown":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.MoveDown;
                        break;
                    case "RemapSelectLeft":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.MoveLeft;
                        break;
                    case "RemapSelectRight":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.MoveRight;
                        break;
                    case "RemapSelectOpenMenu":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.OpenMenu;
                        break;
                    case "RemapSelectPause":
                        commands.Add(new RemapControlConfirm());
                        RemapChecker = true;
                        RemapActionHolder = Actions.Pause;
                        break;
                }
            }
            if (RemapChecker && currentKeyboardState.GetPressedKeyCount() == 0 && previousKeyboardState.GetPressedKeyCount() !=0 && !screenTransition)
            {
                screenTransition = true;
                Keys inputKey = previousKeyboardState.GetPressedKeys()[0];
                inputDetector.Remap(inputKey, RemapActionHolder);
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
            if (inputDetector.IsActioninputtedbyType(Actions.Cancel, InputType.Press))
            {
                commands.Add(new BackSelect());
            }
            if (inputDetector.IsActioninputtedbyType(Actions.MoveUp, InputType.Press))
            {
                commands.Add(new MenuMoveUp());
            }
            if (inputDetector.IsActioninputtedbyType(Actions.MoveDown, InputType.Press))
            {
                commands.Add(new MenuMoveDown());
            }
            if (inputDetector.IsActioninputtedbyType(Actions.MoveLeft, InputType.Press))
            {
                commands.Add(new MenuMoveLeft());
            }
            if (inputDetector.IsActioninputtedbyType(Actions.MoveRight, InputType.Press))
            {
                commands.Add(new MenuMoveRight());
            }
            if(currentKeyboardState.GetPressedKeyCount() == 0 && previousKeyboardState.GetPressedKeyCount() ==0)
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
    }
}
