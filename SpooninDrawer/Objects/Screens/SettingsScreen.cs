using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Content;
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
            //does nothing
            FullScreen,
            Resolution,
            Keybind,
            Volume,
            //end of does nothing
            BackSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[] ScreenText { get; }
        public SettingsScreen(SpriteFont font, Vector2 positionOffset)
        {
            Position = new Vector2(0, 0);
            screenTexture = "Menu/SettingsScreen";
            menuLocationArrayX = new int[5] { 100, 200, 100, 100, 100 };
            menuLocationArrayY = new int[5] { 250, 300, 350, 400, 450 };
            menuNavigatorXCap = 1;
            menuNavigatorYCap = 4;
            ScreenText = new BaseTextObject[5];
            ScreenText[0] = new SettingsText(font, RStrings.SettingsFullScreen);
            ScreenText[1] = new SettingsText(font, RStrings.SettingsResolution);
            ScreenText[2] = new SettingsText(font, RStrings.SettingsKeybinds);
            ScreenText[3] = new SettingsText(font, RStrings.SettingsVolume);
            ScreenText[4] = new SettingsText(font, RStrings.SettingsBack);
            int i = 0;
            int j = 0;
            foreach (SettingsText settingText in ScreenText)
            {
                settingText.Position = new Vector2(menuLocationArrayX[j] + positionOffset.X, menuLocationArrayY[i] + positionOffset.Y);
                settingText.zIndex = 3;
                i++;
            }
        }
        public void GetMenuCommand(int x, int y)
        {

        }

    }


}
