using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class EmptyScreen : iBaseScreen
    {
        enum titleCommands
        {
            Empty
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; }
        public bool hasButtons { get; }
        public Resolution DisplayTexture;
        public EmptyScreen()
        {
            hasButtons = false;
            Position = new Vector2(0, 0);
            screenTexture = "Menu/EmptyScreen";
            menuLocationArrayX = new int[1] { 0 };
            menuLocationArrayY = new int[1] { 0 };
            menuNavigatorXCap = new int[0];
            menuNavigatorYCap = 0;
            ScreenText = new BaseTextObject[0, 1];
            ScreenText[0, 0].Text = "";
        }
        public iBaseScreen Initialize()
        {
            return new EmptyScreen();
        }
        public iBaseScreen Initialize(Resolution resolution)
        {
            return new EmptyScreen();
        }
        public string GetMenuCommand(int x, int y)
        {
            return "Empty";
        }
    }
}
