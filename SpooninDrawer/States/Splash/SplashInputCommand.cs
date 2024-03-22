
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.States.Gameplay;

namespace SpooninDrawer.States.Splash
{
    public class SplashInputCommand : BaseInputCommand
    {
        // Out of Game Commands
        public class GameSelect : SplashInputCommand { }
        public class LoadSelect : SplashInputCommand { }
        public class SettingsSelect : SplashInputCommand { }
        public class BackSelect : SplashInputCommand { }
        public class ExitSelect : SplashInputCommand { }
        public class ResumeSelect : SplashInputCommand { }
        public class MenuMoveLeft : SplashInputCommand { }
        public class MenuMoveRight : SplashInputCommand { }
        public class MenuMoveUp : SplashInputCommand { }
        public class MenuMoveDown : SplashInputCommand { }
        public class TestMenuButton : SplashInputCommand { }
        public class TestMenuButton2 : SplashInputCommand { }
        public class CheckMenuSelect : SplashInputCommand { }
    }
}
