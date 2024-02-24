using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
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
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        InputDetector inputDetector;
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
            currentKeyboardState = state;
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
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
            if (state.IsKeyDown(Keys.Escape) && HasBeenPressed(Keys.Escape))
            {
                commands.Add(new BackSelect());
            }
            if (state.IsKeyDown(Keys.Up) && HasBeenPressed(Keys.Up))
            {
                commands.Add(new MenuMoveUp());
            }
            if (state.IsKeyDown(Keys.Down) && HasBeenPressed(Keys.Down))
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
        private  bool HasBeenPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
