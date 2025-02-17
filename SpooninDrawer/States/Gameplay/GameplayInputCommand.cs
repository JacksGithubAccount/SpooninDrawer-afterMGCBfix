using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public class GameExit : GameplayInputCommand { }
        public class PlayerMoveLeft : GameplayInputCommand { }
        public class PlayerMoveRight : GameplayInputCommand { }
        public class PlayerMoveUp : GameplayInputCommand { }
        public class PlayerMoveDown : GameplayInputCommand { }
        public class PlayerStopsMoving : GameplayInputCommand { }
        public class PlayerAction : GameplayInputCommand { }
        public class PlayerConfirm : GameplayInputCommand { }
        public class PlayerCancel : GameplayInputCommand { }
        public class PlayerInteract : GameplayInputCommand { }
        public class PlayerOpenMenu : GameplayInputCommand { }
        public class PlayerNoInput : GameplayInputCommand { }
        public class Pause : GameplayInputCommand { }
        public class PlayerV : GameplayInputCommand { }
    }
}
