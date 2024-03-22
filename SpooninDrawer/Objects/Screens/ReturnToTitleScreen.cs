using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class ReturnToTitleScreen : BaseGameObject, BaseScreen
    {
        enum titleCommands
        {
            ExitSelect,
            BackSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }

        public ReturnToTitleScreen()
        {
            screenTexture = "Menu/ReturnToTitleScreen";
            menuLocationArrayX = new int[2] { 5, 155 };
            menuLocationArrayY = new int[2] { 130, 130 };
            menuNavigatorXCap = 1;
            menuNavigatorYCap = 0;
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}
