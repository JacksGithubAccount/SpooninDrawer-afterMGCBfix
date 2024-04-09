using SpooninDrawer.Content;
using SpooninDrawer.Engine;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.States;
using SpooninDrawer.Engine.States.Gameplay;
using SpooninDrawer.States.Dev;
using SpooninDrawer.States.Splash;
using System;
using System.Globalization;
using System.Xml;


namespace SpooninDrawer
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private const int WIDTH = 1920;
        private const int HEIGHT = 1080;

        private const string ENGLISH = "en";
        private const string FRENCH = "fr";
        private const string JAPANESE = "ja";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RStrings.Culture = CultureInfo.CurrentCulture;
            //RStrings.Culture = CultureInfo.GetCultureInfo(JAPANESE); //to switch language for RString resource
            using (var game = new MainGame(WIDTH, HEIGHT, new SplashState(Resolution.x1080)))
                game.Run();
        }
    }
}