using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects
{
    public interface BaseScreen
    {
        string screenTexture { get; }
        //location of buttons
        int[] menuLocationArrayX { get; }
        int[] menuLocationArrayY { get; }
        //how far you can scroll buttons
        int menuNavigatorXCap { get; }
        int menuNavigatorYCap { get; }
        Vector2 Position { get; }
        BaseTextObject[,] ScreenText { get; }

        //used for getting command for SplashInputMapper to know what context the enter button is doing
        string GetMenuCommand(int x, int y) { return ""; }

    }
}
