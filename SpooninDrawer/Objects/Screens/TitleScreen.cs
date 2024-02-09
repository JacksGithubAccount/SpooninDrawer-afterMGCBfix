﻿using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.States.Splash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class TitleScreen : BaseScreen
    {
        enum titleCommands
        {
            GameSelect,
            LoadSelect,
            SettingsSelect,
            ExitSelect
        }
        //holds information for title screen
        public string screenTexture { get; }
        public int[] menuLocationArrayX { get; }
        public int[] menuLocationArrayY { get; }
        public int menuNavigatorXCap { get; }
        public int menuNavigatorYCap { get; }

        //menuLocationArray = new int[] { {445, 310}, {445,410 },{445, 490},{445, 590} };


        public TitleScreen()
        {
            screenTexture = "Menu/TitleScreen";
            menuLocationArrayX = new int[4] { 445, 445, 445, 445 };
            menuLocationArrayY = new int[4] { 310, 410, 490, 590 };
            menuNavigatorXCap = 0;
            menuNavigatorYCap = 3;
        }

        public string GetMenuCommand(int x, int y)
        {
            var holder = (titleCommands)y;
            return holder.ToString();
        }

    }
}
