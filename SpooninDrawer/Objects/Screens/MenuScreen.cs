﻿using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    internal class MenuScreen : BaseScreen
    {
        enum titleCommands
        {
            ResumeSelect,
            SettingsSelect,
            ExitSelect
        }
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }
        public MenuScreen()
        {
            screenTexture = "Menu/MenuScreen";
            menuLocationArrayX = new int[3] { 390, 390, 390 };
            menuLocationArrayY = new int[3] { 310, 440, 570 };
            menuNavigatorXCap = 0;
            menuNavigatorYCap = 2;
        }
        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }
    }
}