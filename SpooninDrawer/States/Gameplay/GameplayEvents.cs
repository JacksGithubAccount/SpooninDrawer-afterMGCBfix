using SpooninDrawer.Engine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.States.Gameplay
{
    public class GameplayEvents : BaseGameStateEvent
    {
        public class PlayerDies : GameplayEvents { }
    }
}
