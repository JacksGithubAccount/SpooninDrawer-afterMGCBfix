using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class SettingsScreen : BaseScreen
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
        public SettingsText[] SettingsText { get; }
        public SettingsScreen()
        {
            Position = new Vector2(0, 0);
            screenTexture = "Menu/SettingsScreen";
            menuLocationArrayX = new int[4] { 100, 100, 100, 100 };
            menuLocationArrayY = new int[4] { 250, 275, 300, 325 };
            menuNavigatorXCap = 0;
            menuNavigatorYCap = 3;
            SettingsText = new SettingsText[2];

        }
        public void GetMenuCommand(int x, int y)
        {

        }

    }


}
