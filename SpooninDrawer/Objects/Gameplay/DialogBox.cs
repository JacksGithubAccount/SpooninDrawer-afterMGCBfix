using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class DialogBox : BaseScreenwithButtons, BaseGameObject, iBaseScreen
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
        private Resolution DisplayResolution;

        public DialogBox(SpriteFont font, Vector2 position, Resolution resolution, bool doesScreenNeedButtons)
        {
            DisplayResolution = resolution;
            spriteFont = font;
            Position = position;

            if (DisplayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/DialogBox1080";

                //button locations
            }
            else if(DisplayResolution == Resolution.x720)
            {
                screenTexture = "Menu/DialogBox720";
            }

            if (doesScreenNeedButtons)
            {
                hasButtons = true;
            }
        }
        public void Initialize()
        {
        }
        public void Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)x;
            return holder.ToString();
        }
    }
}
