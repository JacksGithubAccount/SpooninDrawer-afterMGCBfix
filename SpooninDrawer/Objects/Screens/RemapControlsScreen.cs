using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class RemapControlsScreen : BaseScreen
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
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; }

        public RemapControlsScreen()
        {
            Position = new Vector2(0, 0);
            screenTexture = "Menu/RemapControlsScreen";
            menuLocationArrayX = new int[1] { 390 };
            menuLocationArrayY = new int[3] { 310, 440, 570 };
            menuNavigatorXCap = menuLocationArrayX.Length - 1;
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
        }

        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)x;
            return holder.ToString();
        }
    }
}
