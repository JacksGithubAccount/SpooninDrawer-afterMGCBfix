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
            MoveArrowRight,
            Controls = 2,
            BackSelect = 4
        }
        enum secondColumnCommands
        {
            Fullscreen,
            Resolution1080,            
            Volume = 3
        }
        enum thirdColumnCommands
        {
            Windows,
            Resolution720
        }
        enum fourthColumnCommands
        {
            Borderless
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; }
        private Resolution DisplayResolution;
        private Vector2 positionOffset;
        private SpriteFont spriteFont;
        public string volumeBar { get; }
        public string volumeBarArrow { get; }
        public string volumeBarFill { get; }
        public float volume;
        public SettingsScreen(SpriteFont font, Vector2 positionOffset, Resolution resolution)
        {
            spriteFont = font;
            this.positionOffset = positionOffset;
            DisplayResolution = resolution;
            Position = new Vector2(0, 0);
            volumeBar = "Menu/VolumeBar";
            volumeBarArrow = "Menu/VolumeBarArrow";
            volumeBarFill = "Menu/VolumeBarFill";
            if (DisplayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/SettingsScreen1080";
                menuLocationArrayX = new int[4] { 100, 300, 500, 700 };
                menuLocationArrayY = new int[5] { 250, 300, 350, 400, 450 };
            }
            else if (DisplayResolution == Resolution.x720)
            {
                screenTexture = "Menu/SettingsScreen720";
                menuLocationArrayX = new int[4] { 100, 300, 500, 700 };
                menuLocationArrayY = new int[5] { 250, 300, 350, 400, 450 };
            }
            menuNavigatorXCap = menuLocationArrayX.Length - 1;
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[menuLocationArrayX.Length, menuLocationArrayY.Length];
            ScreenText[0, 0] = new SettingsText(font, RStrings.SettingsScreenSettings);
            ScreenText[0, 1] = new SettingsText(font, RStrings.SettingsResolution);
            ScreenText[0, 2] = new SettingsText(font, RStrings.SettingsControls);
            ScreenText[0, 3] = new SettingsText(font, RStrings.SettingsVolume);
            ScreenText[0, 4] = new SettingsText(font, RStrings.SettingsBack);
            ScreenText[1, 0] = new SettingsText(font, RStrings.SettingsFullScreen);
            ScreenText[1, 1] = new SettingsText(font, RStrings.SettingsResolution1080);
            ScreenText[1, 3] = new SettingsText(font, volume.ToString());

            ScreenText[2, 0] = new SettingsText(font, RStrings.SettingsWindowScreen);
            ScreenText[2, 1] = new SettingsText(font, RStrings.SettingsResolution720);

            ScreenText[3, 0] = new SettingsText(font, RStrings.SettingsBorderlessScreen);

            int i = 0;
            int j = 0;
            foreach (SettingsText settingText in ScreenText)
            {
                if (settingText == null)
                {
                    i++;
                    continue;
                    //break;
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
        public BaseScreen Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
            return new SettingsScreen(spriteFont, positionOffset, resolution);
        }
        public string GetMenuCommand(int x, int y)
        {
            if (x == 0)
            {
                //sets command to make arrow go right if back/control isnt select
                if (y < 4 && y != 2)
                {
                    y = 0;
                }
                var holder = (titleCommands)y;
                return holder.ToString();
            }
            else if (x == 1 && y!=4)
            {
                var holder = (secondColumnCommands)y;
                return holder.ToString();
            }else if(x==2 && y!= 4)
            {
                var holder = (thirdColumnCommands)y;
                return holder.ToString();
            }else if(x == 3 && y != 4)
            {
                var holder = (fourthColumnCommands)y;
                return holder.ToString();
            }
            else { return ""; }
        }

    }


}
