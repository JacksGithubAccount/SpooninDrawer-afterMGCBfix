using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Content;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class RemapControlsScreen : BaseScreenwithButtons, iBaseScreen
    {
        enum titleCommands
        {
            BackSelect
        }
        enum secondColumnCommands
        {
            RemapSelectConfirm = 1,
            RemapSelectCancel = 2,
            RemapSelectUp = 3,
            RemapSelectDown = 4,
            RemapSelectLeft = 5,
            RemapSelectRight = 6,
            RemapSelectOpenMenu = 7,
            RemapSelectPause = 8
        }
        enum thirdColumnCommands
        {
            placeholder,
            RemapMouseSelectConfirm,
            RemapMouseSelectCancel,
            RemapMouseSelectUp,
            RemapMouseSelectDown,
            RemapMouseSelectLeft,
            RemapMouseSelectRight,
            RemapMouseSelectOpenMenu,
            RemapMouseSelectPause,
        }
        enum fourthColumnCommands
        {
            placeholder,
            RemapButtonSelectConfirm,
            RemapButtonSelectCancel,
            RemapButtonSelectUp,
            RemapButtonSelectDown,
            RemapButtonSelectLeft,
            RemapButtonSelectRight,
            RemapButtonSelectOpenMenu,
            RemapButtonSelectPause,
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; set; }
        public bool hasButtons { get; }
        private InputDetector inputDetector;
        private Resolution displayResolution;
        private Vector2 positionOffset;
        private SpriteFont spriteFont;

        public RemapControlsScreen(SpriteFont font, Vector2 positionOffset, InputDetector inputDetector, Resolution resolution)
        {
            displayResolution = resolution;
            this.inputDetector = inputDetector;
            Position = new Vector2(0, 0);
            if (displayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/RemapControlsScreen1080";
            }
            else if (displayResolution == Resolution.x720)
            {
                screenTexture = "Menu/RemapControlsScreen720";
            }

            menuLocationArrayX = new int[4] { 15, 200, 375, 550 };
            menuLocationArrayY = new int[10] { 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[menuLocationArrayX.Length, menuLocationArrayY.Length];
            ScreenText[0, 0] = new SettingsText(font, RStrings.ControlCommand);
            ScreenText[0, 1] = new SettingsText(font, RStrings.ControlConfirm);
            ScreenText[0, 2] = new SettingsText(font, RStrings.ControlCancel);
            ScreenText[0, 3] = new SettingsText(font, RStrings.ControlUp);
            ScreenText[0, 4] = new SettingsText(font, RStrings.ControlDown);
            ScreenText[0, 5] = new SettingsText(font, RStrings.ControlLeft);
            ScreenText[0, 6] = new SettingsText(font, RStrings.ControlRight);
            ScreenText[0, 7] = new SettingsText(font, RStrings.ControlOpenMenu);
            ScreenText[0, 8] = new SettingsText(font, RStrings.ControlPause);
            ScreenText[0, 9] = new SettingsText(font, RStrings.SettingsBack);

            ScreenText[1, 0] = new SettingsText(font, RStrings.ControlKeyboard);
            ScreenText[1, 1] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Confirm).ToString());
            ScreenText[1, 2] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Cancel).ToString());
            ScreenText[1, 3] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveUp).ToString());
            ScreenText[1, 4] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveDown).ToString());
            ScreenText[1, 5] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveLeft).ToString());
            ScreenText[1, 6] = new SettingsText(font, inputDetector.getKeyforAction(Actions.MoveRight).ToString());
            ScreenText[1, 7] = new SettingsText(font, inputDetector.getKeyforAction(Actions.OpenMenu).ToString());
            ScreenText[1, 8] = new SettingsText(font, inputDetector.getKeyforAction(Actions.Pause).ToString());

            for (int superi = 0; superi < 9; superi++)
            {
                if (ScreenText[0, superi].Text.Replace(" ", String.Empty) == inputDetector.getActionforClick(Click.LeftClick).ToString())
                {
                    ScreenText[2, superi] = new SettingsText(font, RStrings.ControlLeftClick);
                }
                else if (ScreenText[0, superi].Text.Replace(" ", String.Empty) == inputDetector.getActionforClick(Click.MiddleClick).ToString())
                {
                    ScreenText[2, superi] = new SettingsText(font, RStrings.ControlMiddleClick);
                }
                else if (ScreenText[0, superi].Text.Replace(" ", String.Empty) == inputDetector.getActionforClick(Click.RightClick).ToString())
                {
                    ScreenText[2, superi] = new SettingsText(font, RStrings.ControlRightClick);
                }
                else if (ScreenText[0, superi].Text.Replace(" ", String.Empty) == inputDetector.getActionforClick(Click.ScrollUp).ToString())
                {
                    //ScreenText[2, superi] = new SettingsText(font, RStrings.ControlScrollUp);
                }
                else if (ScreenText[0, superi].Text.Replace(" ", String.Empty) == inputDetector.getActionforClick(Click.ScrollDown).ToString())
                {
                    //ScreenText[2, superi] = new SettingsText(font, RStrings.ControlScrollDown);
                }
                else
                    ScreenText[2, superi] = new SettingsText(font, "");
            }
            ScreenText[2, 0] = new SettingsText(font, RStrings.ControlMouse);
            ScreenText[3, 0] = new SettingsText(font, RStrings.ControlController);

            ScreenText[3, 1] = new SettingsText(font, inputDetector.getButtonforAction(Actions.Confirm).ToString());
            ScreenText[3, 2] = new SettingsText(font, inputDetector.getButtonforAction(Actions.Cancel).ToString());
            ScreenText[3, 3] = new SettingsText(font, inputDetector.getButtonforAction(Actions.MoveUp).ToString());
            ScreenText[3, 4] = new SettingsText(font, inputDetector.getButtonforAction(Actions.MoveDown).ToString());
            ScreenText[3, 5] = new SettingsText(font, inputDetector.getButtonforAction(Actions.MoveLeft).ToString());
            ScreenText[3, 6] = new SettingsText(font, inputDetector.getButtonforAction(Actions.MoveRight).ToString());
            ScreenText[3, 7] = new SettingsText(font, inputDetector.getButtonforAction(Actions.OpenMenu).ToString());
            ScreenText[3, 8] = new SettingsText(font, inputDetector.getButtonforAction(Actions.Pause).ToString());

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
            hasButtons = true;
            ButtonWidth = 160;
            ButtonHeight = 35;
            CreateRectangles(menuLocationArrayX, menuLocationArrayY, new int[10] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, new int[4] { menuNavigatorYCap + 1, menuNavigatorYCap, menuNavigatorYCap, menuNavigatorYCap });
            for (int b = 0; b < menuNavigatorYCap; b++)
            {
                SetRectangletoReadOnly(ButtonRectangles[b][0]);
            }
            for (int c = 0; c <= menuNavigatorXCap[0]; c++)
                SetRectangletoReadOnly(ButtonRectangles[0][c]);
            for(int d = 3; d <= 6; d++)
            {
                SetRectangletoReadOnly(ButtonRectangles[d][2]);
            }                
        }

        public iBaseScreen Initialize(Resolution resolution)
        {
            displayResolution = resolution;
            return new RemapControlsScreen(spriteFont, positionOffset, inputDetector, resolution);
        }
        public string GetMenuCommand(int x, int y)
        {
            if (x == 0)
            {
                var holder = (titleCommands)x;
                return holder.ToString();
            }
            else if (x == 1)
            {
                var holder = (secondColumnCommands)y;
                return holder.ToString();
            }
            else if (x == 2)
            {
                var holder = (thirdColumnCommands)y;
                return holder.ToString();
            }
            else if (x == 3)
            {
                var holder = (fourthColumnCommands)y;
                return holder.ToString();
            }
            else
                return "";
        }
    }
}
