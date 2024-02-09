using SpooninDrawer.Engine.Input;
using SpooninDrawer.States.Dev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Dev
{
    public class DevInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class GameExit : DevInputCommand { }
        public class PlayerMoveLeft : DevInputCommand { }
        public class PlayerMoveRight : DevInputCommand { }
        public class PlayerMoveUp : DevInputCommand { }
        public class PlayerMoveDown : DevInputCommand { }
        public class PlayerStopsMoving : DevInputCommand { }
        public class PlayerAction : DevInputCommand { }
    }
}
