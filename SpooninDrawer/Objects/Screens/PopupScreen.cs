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
    public class PopupScreen : BaseScreenwithButtons, iBaseScreen
    {
        enum titleCommands
        {
            RemapAcceptDuplicateSwap,
            RemapBackSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; set; }
        public bool hasButtons { get; }
        private Vector2 positionOffset = new Vector2(9, 16);
        private SpriteFont spriteFont;
        public string DescriptionString = "Test string";
        public string YesString = "Yes";
        public string NoString = "No";

        public PopupScreen(SpriteFont font, Vector2 position) : this(font, position, true) { }
        public PopupScreen(SpriteFont font, Vector2 position, string description, string option1, string option2) : this(font, position, true, description, option1, option2) { }
        public PopupScreen(SpriteFont font, Vector2 position, bool doesScreenNeedButtons, string description, string option1, string option2) : this(font, position, doesScreenNeedButtons)
        {
            ScreenText[0, 0].Text = description;
            ScreenText[0, 1].Text = option1;
            ScreenText[1, 1].Text = option2;
        }
        public PopupScreen(SpriteFont font, Vector2 position, bool doesScreenNeedButtons)
        {
            spriteFont = font;
            Position = position;
            screenTexture = "Menu/RemapControlConfirm";

            menuLocationArrayX = new int[2] { 5 + (int)Position.X, 155 + (int)Position.X };
            menuLocationArrayY = new int[2] { 50 + (int)Position.Y, 110 + (int)Position.Y };

            //textLocation = new Vector2(position.X / 3, position.Y / 3);
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[2, 2];
            ScreenText[0, 0] = new SettingsText(font, ref DescriptionString);
            ScreenText[0, 1] = new SettingsText(font, YesString);
            ScreenText[1, 1] = new SettingsText(font, NoString);
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
            if (doesScreenNeedButtons)
            {
                hasButtons = true;
                ButtonWidth = 50;
                ButtonHeight = 25;
                CreateRectangles(menuLocationArrayX, menuLocationArrayY);
                ButtonRectangles[0][0].ReadOnly = true;
                ButtonRectangles[0][1].ReadOnly = true;
            }
        }
        public iBaseScreen Initialize()
        {
            return new PopupScreen(spriteFont, Position);
        }
        public iBaseScreen Initialize(Resolution resolution)
        {
            return Initialize();
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)x;
            return holder.ToString();
        }
    }
}
