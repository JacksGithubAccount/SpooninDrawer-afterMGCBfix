using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class EmptyScreen : BaseScreen
    {
        enum titleCommands
        {
            Empty
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }

        public EmptyScreen()
        {
            Position = new Vector2(0,0);
            screenTexture = "Menu/EmptyScreen";
            menuLocationArrayX = new int[1] { 0 };
            menuLocationArrayY = new int[1] { 0 };
            menuNavigatorXCap = 0;
            menuNavigatorYCap = 0;
        }
        public string GetMenuCommand(int x, int y)
        {
            return "Empty";
        }
    }
}
