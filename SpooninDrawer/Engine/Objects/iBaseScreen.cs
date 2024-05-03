using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects
{
    public enum Resolution
    {
        x1080,
        x720
    }
    public interface iBaseScreen
    {
        string screenTexture { get; }
        //location of buttons
        int[] menuLocationArrayX { get; }
        int[] menuLocationArrayY { get; }
        //how far you can scroll buttons
        int[] menuNavigatorXCap { get; }
        int menuNavigatorYCap { get; }
        Vector2 Position { get; }
        BaseTextObject[,] ScreenText { get; }
        bool hasButtons { get; }
        //Rectangle[][] ButtonRectangles { get; }
        //protected static int ButtonX;
        //protected static int ButtonY;
        //protected static int ButtonsAmount;

        iBaseScreen Initialize(Resolution resolution) { return this; }
        //used for getting command for SplashInputMapper to know what context the enter button is doing
        string GetMenuCommand(int x, int y) { return ""; }
        virtual string GetMenuCommand(Vector2 position) { return GetMenuCommand((int)position.X, (int)position.Y); }
    }
}
