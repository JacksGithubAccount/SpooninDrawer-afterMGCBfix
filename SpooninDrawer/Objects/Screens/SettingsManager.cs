using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.SaveData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Screens
{
    public class SettingsManager
    {
        GraphicsDeviceManager graphics;
        SettingsData data;
        public SettingsManager(SettingsData data, GraphicsDeviceManager graphics)
        {
            this.data = data;
            this.graphics = graphics;
        }
        public void SetScreenSettings(string ScreenSetting)
        {
            if (ScreenSetting == SettingsDataManager.FullScreenText)
            {
                graphics.IsFullScreen = true;
                graphics.HardwareModeSwitch = true;
            }
            else if (ScreenSetting == SettingsDataManager.WindowText)
            {
                //needs to be done twice for it to work for some reason
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                graphics.IsFullScreen = false;
            }
            else if (ScreenSetting == SettingsDataManager.BorderlessText)
            {
                graphics.IsFullScreen = true;
                if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1920 || GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 1080)
                {
                    //displayResolution = Resolution.x1080;
                    graphics.PreferredBackBufferWidth = 1920;
                    graphics.PreferredBackBufferHeight = 1080;
                }
                else if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1280 || GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 720)
                {
                    //displayResolution = Resolution.x720;
                    graphics.PreferredBackBufferWidth = 1280;
                    graphics.PreferredBackBufferHeight = 720;
                }
                graphics.HardwareModeSwitch = false;
            }
            graphics.ApplyChanges();
        }
        public void SetResolution(Resolution displayResolution)
        {
            if (displayResolution == Resolution.x1080)
            {

                graphics.PreferredBackBufferWidth = 1920;
                graphics.PreferredBackBufferHeight = 1080;

            }
            else if (displayResolution == Resolution.x720)
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }
            graphics.ApplyChanges();
        }



    }
}
