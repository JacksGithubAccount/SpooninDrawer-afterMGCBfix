using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class DialogScreen: BaseScreenwithButtons, iBaseScreen
    {
        enum titleCommands
        {
            Next,
            Auto,
            Log,
            Skip
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; set; }
        public BaseTextObject[,] ScreenText { get; set; }
        public bool hasButtons { get; }
        private SpriteFont spriteFont;

        public DialogScreen(SpriteFont font, Vector2 position, bool doesScreenNeedButtons)
        {
            spriteFont = font;
            Position = position;



            if (doesScreenNeedButtons)
            {
                hasButtons = true;
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
