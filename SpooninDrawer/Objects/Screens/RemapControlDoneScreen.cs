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
    public class RemapControlDoneScreen : iBaseScreen
    {
        enum titleCommands
        {
            Controls
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
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            ScreenText = new BaseTextObject[1, 1];
            ScreenText[0, 0] = new SettingsText(font, RStrings.SettingsRemapDone);
            ScreenText[0, 0].Position = position;


            ScreenText[0, 0].Position = textLocation;
            ScreenText[0, 0].zIndex = 3;
            hasButtons = false;
        }
        public iBaseScreen Initialize()
        {
            return new RemapControlDoneScreen(spriteFont, Position, inputDetector);
        }
        public iBaseScreen Initialize(Resolution resolution)
        {
            return new RemapControlDoneScreen(spriteFont, Position, inputDetector);
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}

