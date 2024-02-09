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
        int[] menuLocationArrayX { get; }
        int[] menuLocationArrayY { get; }
        int menuNavigatorXCap { get; }
        int menuNavigatorYCap { get; }

        //menuLocationArray = new int[] { {445, 310}, {445,410 },{445, 490},{445, 590} };
        string GetMenuCommand(int x, int y) { return "";  }

    }
}
