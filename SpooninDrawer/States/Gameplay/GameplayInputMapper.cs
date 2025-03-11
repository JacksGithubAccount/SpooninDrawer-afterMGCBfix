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
        public GameplayInputMapper()
        {
            inputDetector = new InputDetector();
        }
        public GameplayInputMapper(InputDetector inputDetector)
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

            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Confirm, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerAction());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Confirm, InputType.Release))
            {
                commands.Add(new GameplayInputCommand.PlayerReturnToTitle());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.OpenMenu, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerOpenMenu());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Cancel, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerCancel());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.V, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerV());
            }
            if (inputDetector.IsActionPressedforKey(Actions.MoveRight))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (inputDetector.IsActionPressedforKey(Actions.MoveLeft))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            if (inputDetector.IsActionPressedforKey(Actions.MoveUp))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            else if (inputDetector.IsActionPressedforKey(Actions.MoveDown))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (!inputDetector.IsActionPressedforKey(Actions.MoveRight) && !inputDetector.IsActionPressedforKey(Actions.MoveLeft) && !inputDetector.IsActionPressedforKey(Actions.MoveUp) && !inputDetector.IsActionPressedforKey(Actions.MoveDown))
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Pause, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.Pause());
            }
            return commands;
        }
    }
}
