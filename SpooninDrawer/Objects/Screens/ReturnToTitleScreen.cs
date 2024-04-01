using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class ReturnToTitleScreen : BaseScreen
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
        //public SplashImage splashImage { get; }
        public BaseTextObject[,] ScreenText { get; }

        public ReturnToTitleScreen() : this(0, 0) { }
        public ReturnToTitleScreen(int positionx, int positiony)
        {
            Position = new Vector2(positionx/3, positiony/3);
            screenTexture = "Menu/ReturnToTitleScreen";
            menuLocationArrayX = new int[2] { 5 + (int)Position.X, 155 + (int)Position.X };
            menuLocationArrayY = new int[2] { 130 + (int)Position.Y, 130 + (int)Position.Y };
            menuNavigatorXCap = 1;
            menuNavigatorYCap = 0;
        }

        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)x;
            return holder.ToString();
        }
    }
}
