﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    internal class MenuScreen : BaseScreenwithButtons, iBaseScreen
    {
        enum titleCommands
        {
            ResumeSelect,
            SettingsSelect,
            CheckMenuSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int[] menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public Vector2 Position { get; }
        public BaseTextObject[,] ScreenText { get; set; }
        public bool hasButtons { get; }
        private Resolution DisplayResolution;

        public MenuScreen(Resolution resolution)
        {
            DisplayResolution = resolution;
            Position = new Vector2(0, 0);
            if (DisplayResolution == Resolution.x1080)
            {
                screenTexture = "Menu/MenuScreen1080";
                menuLocationArrayX = new int[1] { 625 };
                menuLocationArrayY = new int[3] { 430, 560, 690 };
                ButtonWidth = 500;
                ButtonHeight = 100;
            }
            else if (DisplayResolution == Resolution.x720)
            {
                screenTexture = "Menu/MenuScreen720";
                menuLocationArrayX = new int[1] { 420 };
                menuLocationArrayY = new int[3] { 280, 380, 480 };
                ButtonWidth = 500;
                ButtonHeight = 80;
            }
            menuNavigatorXCap = new int[1] { menuLocationArrayX.Length - 1 };
            menuNavigatorYCap = menuLocationArrayY.Length - 1;
            hasButtons = true;
            
            CreateRectangles(menuLocationArrayX, menuLocationArrayY);
        }
        public iBaseScreen Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
            return new MenuScreen(resolution);
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}
