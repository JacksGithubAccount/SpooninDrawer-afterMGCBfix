using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.States.Dev;

namespace SpooninDrawer.States.Dev
{
    public class DevInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<DevInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new DevInputCommand.GameExit());
            }

            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new DevInputCommand.PlayerAction());
            }

            if (state.IsKeyDown(Keys.Right))
            {
                commands.Add(new DevInputCommand.PlayerMoveRight());
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                commands.Add(new DevInputCommand.PlayerMoveLeft());
            }
            if (state.IsKeyDown(Keys.Up))
            {
                commands.Add(new DevInputCommand.PlayerMoveUp());
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                commands.Add(new DevInputCommand.PlayerMoveDown());
            }
            if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Down))
            {
                commands.Add(new DevInputCommand.PlayerStopsMoving());
            }

            return commands;
        }
    }
}
