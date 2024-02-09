using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.States.Dev;
using SpooninDrawer.Engine.States.Gameplay;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayInputMapper : BaseInputMapper
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        InputDetector inputDetector;
        public GameplayInputMapper()
        {
            inputDetector = new InputDetector();
        }
        public GameplayInputMapper(InputDetector inputDetector)
        {
            this.inputDetector = inputDetector;
        }
        public void SetInputDetector(InputDetector inputDetector)
        {
            this.inputDetector = inputDetector;
        }
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyState)
        {
            previousKeyboardState = currentKeyboardState;
            inputDetector.update(previousKeyboardState);
            currentKeyboardState = keyState;

            var commands = new List<GameplayInputCommand>();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GameplayInputCommand.GameExit());
            }

            if (inputDetector.IsActioninputtedbyType(Actions.Confirm, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerAction());
            }

            if (inputDetector.IsActionPressed(Actions.MoveRight))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (inputDetector.IsActionPressed(Actions.MoveLeft))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            if (inputDetector.IsActionPressed(Actions.MoveUp))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            else if (inputDetector.IsActionPressed(Actions.MoveDown))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (!inputDetector.IsActionPressed(Actions.MoveRight) && !inputDetector.IsActionPressed(Actions.MoveLeft) && !inputDetector.IsActionPressed(Actions.MoveUp) && !inputDetector.IsActionPressed(Actions.MoveDown))
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }
            if (inputDetector.IsActioninputtedbyType(Actions.Pause, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.Pause());
            }
            return commands;
        }
    }
}
