using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Content;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.Sound;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class SettingsScreen : BaseScreenwithButtons, iBaseScreen
    {
        public enum VolumeChangeType
        {
            incremental,
            set
        }
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
            VolumeBGM = 3,
            VolumeSE = 4
        }
        enum thirdColumnCommands
        {
            Windows,
            Resolution720,
            MouseVolumeBGM = 3,
            MouseVolumeSE = 4
        }
        enum fourthColumnCommands
        {
            Borderless
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; set; }
        private Resolution DisplayResolution;
        private Vector2 positionOffset;
        private SpriteFont spriteFont;
        public string volumeBar { get; }
        public string volumeBarArrow { get; }
        public string volumeBarFill { get; }
        public int volumeBarLength { get; set; }
        public Vector2 volumeBGMBarPosition { get; }
        public Vector2 volumeBGMBarArrowPosition { get; }
        public Vector2 volumeBGMBarFillPosition { get; }
        public Vector2 volumeSEBarPosition { get; }
        public Vector2 volumeSEBarArrowPosition { get; }
        public Vector2 volumeSEBarFillPosition { get; }
        public bool hasButtons { get; }
        private float volumeBGM = 1.0f;
        private float volumeSE = 1.0f;
        private float maxVolume = 1.0f;
        public SettingsText volumeBGMText;
        public SettingsText volumeSEText;


        public SettingsScreen(SpriteFont font, Vector2 positionOffset, Resolution resolution, float volumeBGM, float volumeSE)
        {
            this.volumeBGM = volumeBGM;
            this.volumeSE = volumeSE;
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
            menuNavigatorXCap = new int[5] { 3, 2, 0, 2, 2 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[menuLocationArrayX.Length, menuLocationArrayY.Length];
            ScreenText[0, 0] = new SettingsText(font, RStrings.SettingsScreenSettings);
            ScreenText[0, 1] = new SettingsText(font, RStrings.SettingsResolution);
            ScreenText[0, 2] = new SettingsText(font, RStrings.SettingsControls);
            ScreenText[0, 3] = new SettingsText(font, RStrings.SettingsVolume);
            ScreenText[0, 4] = new SettingsText(font, RStrings.SettingsBack);
            ScreenText[1, 0] = new SettingsText(font, RStrings.SettingsFullScreen);
            ScreenText[1, 1] = new SettingsText(font, RStrings.SettingsResolution1080);
            ScreenText[1, 3] = new SettingsText(font, RStrings.SettingsVolumeBGM);
            ScreenText[1, 4] = new SettingsText(font, RStrings.SettingsVolumeSE);

            ScreenText[2, 0] = new SettingsText(font, RStrings.SettingsWindowScreen);
            ScreenText[2, 1] = new SettingsText(font, RStrings.SettingsResolution720);

            ScreenText[3, 0] = new SettingsText(font, RStrings.SettingsBorderlessScreen);

            volumeBGMBarPosition = new Vector2(menuLocationArrayX[2], menuLocationArrayY[3]) + positionOffset;
            volumeBGMBarArrowPosition = new Vector2(menuLocationArrayX[3], menuLocationArrayY[3]) + positionOffset;
            volumeBGMBarFillPosition = new Vector2(menuLocationArrayX[2], menuLocationArrayY[3]) + positionOffset;
            volumeSEBarPosition = new Vector2(menuLocationArrayX[2], menuLocationArrayY[4]) + positionOffset;
            volumeSEBarArrowPosition = new Vector2(menuLocationArrayX[3], menuLocationArrayY[4]) + positionOffset;
            volumeSEBarFillPosition = new Vector2(menuLocationArrayX[2], menuLocationArrayY[4]) + positionOffset;
            volumeBGMText = new SettingsText(font, Math.Round(volumeBGM * 100).ToString());
            volumeBGMText.Position = new Vector2(menuLocationArrayX[1] + 50, menuLocationArrayY[3]) + positionOffset; ;
            volumeSEText = new SettingsText(font, Math.Round(volumeSE * 100).ToString());
            volumeSEText.Position = new Vector2(menuLocationArrayX[1] + 50, menuLocationArrayY[4]) + positionOffset; ;

            int k = 0;
            int j = 0;
            foreach (SettingsText settingText in ScreenText)
            {
                if (k >= menuLocationArrayY.Length)
                {
                    j++;
                    k = 0;
                }
                if (settingText == null)
                {
                    k++;
                    continue;
                }
                settingText.Position = new Vector2(menuLocationArrayX[j] + positionOffset.X, menuLocationArrayY[k] + positionOffset.Y);
                settingText.zIndex = 3;
                k++;
            }
            hasButtons = true;
            ButtonWidth = 175;
            ButtonHeight = 40;
            CreateRectangles(menuLocationArrayX, menuLocationArrayY, menuNavigatorXCap);
            ButtonRectangles[3][2] = new SplashRectangle(new Rectangle(585, 410, 305, 20), false, true, false);
            ButtonRectangles[4][2] = new SplashRectangle(new Rectangle(585, 460, 305, 20), false, true, false);
        }
        public iBaseScreen Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
            return new SettingsScreen(spriteFont, positionOffset, resolution, volumeBGM, volumeSE);
        }
        public void VolumeChange(float volume, VolumeType volumeType, VolumeChangeType volumeChangeType)
        {
            if (volumeType == VolumeType.BGM)
            {
                if (volumeBGM <= maxVolume && volumeBGM >= 0.0f)
                {
                    if (volumeChangeType == VolumeChangeType.incremental)
                        volumeBGM += volume;
                    else
                        volumeBGM = volume;
                    if (volumeBGM > maxVolume)
                    {
                        volumeBGM = maxVolume;
                    }
                    if (volumeBGM < 0.0f)
                    {
                        volumeBGM = 0.0f;
                    }
                    volumeBGMText.Text = (Math.Round(volumeBGM * 100)).ToString();
                }
            }
            else if (volumeType == VolumeType.SE)
            {
                if (volumeSE <= maxVolume && volumeSE >= 0.0f)
                {
                    if (volumeChangeType == VolumeChangeType.incremental)
                        volumeSE += volume;
                    else
                        volumeSE = volume;
                    if (volumeSE > maxVolume)
                    {
                        volumeSE = maxVolume;
                    }
                    if (volumeSE < 0.0f)
                    {
                        volumeSE = 0.0f;
                    }
                    volumeSEText.Text = (Math.Round(volumeSE * 100)).ToString();
                }
            }
        }
        public float GetVolume(VolumeType volumeType)
        {
            if (volumeType == VolumeType.BGM)
                return volumeBGM;
            else if (volumeType == VolumeType.SE)
                return volumeSE;
            else
                return 0.0f;
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
            else if (x == 1 && y <= 4)
            {
                var holder = (secondColumnCommands)y;
                return holder.ToString();
            }
            else if (x == 2 && y <= 4)
            {
                var holder = (thirdColumnCommands)y;
                return holder.ToString();
            }
            else if (x == 3 && y != 4)
            {
                var holder = (fourthColumnCommands)y;
                return holder.ToString();
            }
            else { return ""; }
        }

    }


}
