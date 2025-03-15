using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.States
{
    public class BaseGameStateEvent
    {
        public class Nothing : BaseGameStateEvent { }
        public class DialogNext : BaseGameStateEvent { }
        public class FootSteps : BaseGameStateEvent { }
        public class GameQuit : BaseGameStateEvent { }
    }
}
