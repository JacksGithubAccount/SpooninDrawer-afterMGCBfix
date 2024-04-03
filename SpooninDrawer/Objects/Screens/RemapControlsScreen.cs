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

        public RemapControlsScreen(SpriteFont font, Vector2 positionOffset)
        {
            Position = new Vector2(0, 0);
            screenTexture = "Menu/RemapControlsScreen";
            menuLocationArrayX = new int[1] { 15 };
            menuLocationArrayY = new int[8] { 50, 100,150, 200,250,300,350,400 };
            menuNavigatorXCap = menuLocationArrayX.Length - 1;
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[menuLocationArrayX.Length, menuLocationArrayY.Length];
            ScreenText[0, 0] = new SettingsText(font, RStrings.ControlConfirm);
            ScreenText[0, 1] = new SettingsText(font, RStrings.ControlCancel);
            ScreenText[0, 2] = new SettingsText(font, RStrings.ControlUp);
            ScreenText[0, 3] = new SettingsText(font, RStrings.ControlDown);
            ScreenText[0, 4] = new SettingsText(font, RStrings.ControlLeft);
            ScreenText[0, 5] = new SettingsText(font, RStrings.ControlRight);
            ScreenText[0, 6] = new SettingsText(font, RStrings.ControlOpenMenu);
            ScreenText[0, 7] = new SettingsText(font, RStrings.ControlPause);

            int i = 0;
            int j = 0;
            foreach (SettingsText settingText in ScreenText)
            {
                if (settingText == null)
                {
                    i++;
                    continue;
                }
                if (i >= menuLocationArrayY.Length)
                {
                    j++;
                    i = 0;
                }
                settingText.Position = new Vector2(menuLocationArrayX[j] + positionOffset.X, menuLocationArrayY[i] + positionOffset.Y);
                settingText.zIndex = 3;
                i++;
            }
        }
            public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)x;
            return holder.ToString();
        }
    }
}
