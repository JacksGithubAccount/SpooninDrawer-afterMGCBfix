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
    public class RemapControlDoneScreen : BaseScreen
    {
        enum titleCommands
        {
            BackSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; }

        private InputDetector inputDetector;
        private Vector2 textLocation;
        private SpriteFont spriteFont;


        public RemapControlDoneScreen(SpriteFont font, Vector2 position, InputDetector inputDetector)
        {
            this.inputDetector = inputDetector;
            Position = position;
            screenTexture = "Menu/RemapControlConfirm";
            menuLocationArrayX = new int[0] { };
            menuLocationArrayY = new int[0] { };
            textLocation = new Vector2(position.X / 3, position.Y / 3);
            menuNavigatorXCap = menuLocationArrayX.Length - 1;
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[1, 1];
            ScreenText[0, 0] = new SettingsText(font, RStrings.SettingsRemapDone);
            ScreenText[0, 0].Position = position;


            ScreenText[0, 0].Position = textLocation;
            ScreenText[0, 0].zIndex = 3;

        }
        public BaseScreen Initialize()
        {
            return new RemapControlDoneScreen(spriteFont, Position, inputDetector);
        }
        public BaseScreen Initialize(Resolution resolution)
        {
            return new RemapControlDoneScreen(spriteFont, Position, inputDetector);
        }
        public string GetMenuCommand(int x, int y)
        {
            if (y < menuLocationArrayY.Length - 1)
            {
                y = 0;
            }
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}

