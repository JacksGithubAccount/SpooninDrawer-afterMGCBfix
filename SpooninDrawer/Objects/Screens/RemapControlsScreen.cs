using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Content;
using SpooninDrawer.Engine.Input;
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
            RemapSelectConfirm,
            RemapSelectCancel,
            RemapSelectUp,
            RemapSelectDown,
            RemapSelectLeft,
            RemapSelectRight,
            RemapSelectOpenMenu,
            RemapSelectPause,
            BackSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; }

        private InputDetector inputDetector;
        private Resolution displayResolution;
        private Vector2 positionOffset;
        private SpriteFont spriteFont;
        public RemapControlsScreen(SpriteFont font, Vector2 positionOffset, InputDetector inputDetector, Resolution resolution)
        {
            displayResolution = resolution;
            this.inputDetector = inputDetector;
            Position = new Vector2(0, 0);
            if(displayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/RemapControlsScreen1080";
            }
            else if(displayResolution==Resolution.x720) 
            {
                screenTexture = "Menu/RemapControlsScreen720";
            }
            
            menuLocationArrayX = new int[2] { 15, 125 };
            menuLocationArrayY = new int[9] { 150, 200, 250, 300, 350, 400, 450, 500, 550 };
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
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
            ScreenText[0, 8] = new SettingsText(font, RStrings.SettingsBack);

            ScreenText[1, 0] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Confirm).ToString());
            ScreenText[1, 1] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Cancel).ToString());
            ScreenText[1, 2] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveUp).ToString());
            ScreenText[1, 3] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveDown).ToString());
            ScreenText[1, 4] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveLeft).ToString());
            ScreenText[1, 5] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveRight).ToString());
            ScreenText[1, 6] = new SettingsText(font, inputDetector.getKeyforAction(Actions.OpenMenu).ToString());
            ScreenText[1, 7] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Pause).ToString());
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
        public BaseScreen Initialize(Resolution resolution)
        {
            displayResolution = resolution;
            return new RemapControlsScreen(spriteFont, positionOffset, inputDetector, resolution);
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}
