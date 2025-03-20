using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.States.Dev;
using SpooninDrawer.Engine.States.Gameplay;
using SpooninDrawer.States.Splash;

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
            if (!inputDetector.IsActionPressedforKey(Actions.MoveRight) && !inputDetector.IsActionPressedforKey(Actions.MoveLeft) && !inputDetector.IsActionPressedforKey(Actions.MoveUp) && !inputDetector.IsActionPressedforKey(Actions.MoveDown) && !currentGamePadState.IsConnected)
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }else if (!inputDetector.IsActionPressedforKey(Actions.MoveRight) && !inputDetector.IsActionPressedforKey(Actions.MoveLeft) && !inputDetector.IsActionPressedforKey(Actions.MoveUp) && !inputDetector.IsActionPressedforKey(Actions.MoveDown) && 
                !inputDetector.IsActionPressedforButton(Actions.MoveRight) && !inputDetector.IsActionPressedforButton(Actions.MoveLeft) && !inputDetector.IsActionPressedforButton(Actions.MoveUp) && !inputDetector.IsActionPressedforButton(Actions.MoveDown))
            {
                commands.Add(new GameplayInputCommand.PlayerStopsMoving());
            }
            if (inputDetector.IsActioninputtedbyTypeforKey(Actions.Pause, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.Pause());
            }
            return commands;
        }
        public override IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            previousMouseState = currentMouseState;
            inputDetector.update(previousMouseState);
            currentMouseState = state;
            var commands = new List<GameplayInputCommand>();

            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Confirm, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerAction());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Confirm, InputType.Release))
            {
                commands.Add(new GameplayInputCommand.PlayerReturnToTitle());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Cancel, InputType.Release))
            {
                commands.Add(new GameplayInputCommand.PlayerCancel());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.MoveRight, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveRight());
            }
            else if (inputDetector.IsActioninputtedbyTypeforClick(Actions.MoveLeft, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveLeft());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.MoveUp, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveUp());
            }
            else if (inputDetector.IsActioninputtedbyTypeforClick(Actions.MoveDown, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerMoveDown());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.OpenMenu, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.PlayerOpenMenu());
            }
            if (inputDetector.IsActioninputtedbyTypeforClick(Actions.Pause, InputType.Press))
            {
                commands.Add(new GameplayInputCommand.Pause());
            }
            return commands;
        }
        public override IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            previousGamePadState = currentGamePadState;
            inputDetector.update(previousGamePadState);
            currentGamePadState = state;
            var commands = new List<GameplayInputCommand>();
            if (state.IsConnected)
            {

                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Confirm, InputType.Press))
                {
                    commands.Add(new GameplayInputCommand.PlayerAction());
                }
                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Confirm, InputType.Release))
                {
                    commands.Add(new GameplayInputCommand.PlayerReturnToTitle());
                }
                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.OpenMenu, InputType.Press))
                {
                    commands.Add(new GameplayInputCommand.PlayerOpenMenu());
                }
                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Cancel, InputType.Press))
                {
                    commands.Add(new GameplayInputCommand.PlayerCancel());
                }
                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.V, InputType.Press))
                {
                    commands.Add(new GameplayInputCommand.PlayerV());
                }
                if (inputDetector.IsActionPressedforButton(Actions.MoveRight))
                {
                    commands.Add(new GameplayInputCommand.PlayerMoveRight());
                }
                else if (inputDetector.IsActionPressedforButton(Actions.MoveLeft))
                {
                    commands.Add(new GameplayInputCommand.PlayerMoveLeft());
                }
                if (inputDetector.IsActionPressedforButton(Actions.MoveUp))
                {
                    commands.Add(new GameplayInputCommand.PlayerMoveUp());
                }
                else if (inputDetector.IsActionPressedforButton(Actions.MoveDown))
                {
                    commands.Add(new GameplayInputCommand.PlayerMoveDown());
                }
                //if (!inputDetector.IsActionPressedforButton(Actions.MoveRight) && !inputDetector.IsActionPressedforButton(Actions.MoveLeft) && !inputDetector.IsActionPressedforButton(Actions.MoveUp) && !inputDetector.IsActionPressedforButton(Actions.MoveDown))
                //{
                //    commands.Add(new GameplayInputCommand.PlayerStopsMoving());
                //}
                if (inputDetector.IsActioninputtedbyTypeforButton(Actions.Pause, InputType.Press))
                {
                    commands.Add(new GameplayInputCommand.Pause());
                }
            }
            return commands;
        }
    }
}
