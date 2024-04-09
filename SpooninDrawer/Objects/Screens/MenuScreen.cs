using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    internal class MenuScreen : BaseScreen
    {
        enum titleCommands
        {
            ResumeSelect,
            SettingsSelect,
            CheckMenuSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; }
        public Resolution DisplayResolution { get; }
        public MenuScreen(Resolution resolution)
        {
            DisplayResolution = resolution;
            Position = new Vector2(0, 0);
            if (DisplayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/MenuScreen1080";
                menuLocationArrayX = new int[1] { 625 };
                menuLocationArrayY = new int[3] { 430, 560, 690 };
            }
            else if (DisplayResolution == Resolution.x720)
            {
                screenTexture = "Menu/MenuScreen720";
                menuLocationArrayX = new int[1] { 390 };
                menuLocationArrayY = new int[3] { 310, 440, 570 };
            }
            menuNavigatorXCap = menuLocationArrayX.Length - 1;
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}
