using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class SettingsScreen : BaseGameObject, BaseScreen
    {
        enum titleCommands
        {

        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public SettingsScreen()
        {
            Position = new Vector2(0, 0);
            screenTexture = "Menu/SettingsScreen";
            menuLocationArrayX = new int[4] { 445, 445, 445, 445 };
            menuLocationArrayY = new int[4] { 310, 410, 490, 590 };
            menuNavigatorXCap = 0;
            menuNavigatorYCap = 3;
        }
        public void GetMenuCommand(int x, int y)
        {

        }

    }


}
