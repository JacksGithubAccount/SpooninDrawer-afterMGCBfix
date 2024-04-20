using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.States.Splash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class TitleScreen : BaseScreen
    {
        enum titleCommands
        {
            GameSelect,
            LoadSelect,
            SettingsSelect,
            ExitSelect
        }
        //holds information for title screen
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; }
        private Resolution DisplayResolution;

        public TitleScreen(Resolution resolution)
        {
            DisplayResolution = resolution;
            Position = new Vector2(0, 0);
            
            if (DisplayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/TitleScreen1080";
                menuLocationArrayX = new int[1] { 750 };
                menuLocationArrayY = new int[4] { 480, 580, 660, 760 };
            }
            else if(DisplayResolution == Resolution.x720)
            {
                screenTexture = "Menu/TitleScreen720";
                menuLocationArrayX = new int[1] { 445 };
                menuLocationArrayY = new int[4] { 310, 410, 490, 590 };
            }
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1; ;            
        }
        public TitleScreen(SpriteFont font, Resolution resolution) : this(resolution)
        {
            ScreenText = new TestText[0,1];
            ScreenText[0,0] = new TestText(font);
        }
        public BaseScreen Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
            return new TitleScreen(resolution);
        }

        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }

    }
}
