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

namespace SpooninDrawer.States.Splash
{
    public class SplashInputMapper : BaseInputMapper
    {

        SplashState splashState;
        public SplashInputMapper(SplashState currentSplashState)
        {
            splashState = currentSplashState;
            inputDetector = new InputDetector();
        }
        public SplashInputMapper(SplashState currentSplashState, InputDetector inputDetector)
        {
            splashState = currentSplashState;
            this.inputDetector = inputDetector;
        }
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            previousKeyboardState = currentKeyboardState;
            inputDetector.update(previousKeyboardState);
            currentKeyboardState = state;
            var commands = new List<SplashInputCommand>();


            if (inputDetector.IsActioninputtedbyType(Actions.Confirm, InputType.Release))
            {
                string commandState = splashState.GetCommandState();
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
                }
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
    }
}
