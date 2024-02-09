using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.States;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.States.Gameplay;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.States.Dev;
using Microsoft.Xna.Framework.Content;

namespace SpooninDrawer.States.Splash
{
    public class SplashState : BaseGameState
    {   
        private bool devState = true; //true to turn on Dev state, false for gameplay state

        private string screenTexture;
        private const string titleScreenArrow = "Menu/TitleScreenArroww";
        private MenuArrowSprite _menuArrow;
        private int[] menuLocationArrayX;
        private int[] menuLocationArrayY;
        private int menuNavigatorX = 0;
        private int menuNavigatorY = 0;
        private int menuNavigatorXCap;
        private int menuNavigatorYCap;
        BaseScreen currentScreen;
        BaseScreen previousScreen;

        private const string TestFont = "Fonts/TestText";
        TestText _testText;

        public override void LoadContent(ContentManager content)
        {
            _testText = new TestText(LoadFont(TestFont));
            _testText.Position = new Vector2(10.0f, 10.0f); 
            _testText.zIndex = 3;
            ChangeScreen(new TitleScreen());
            AddGameObject(_testText);                    
            _menuArrow = new MenuArrowSprite(LoadTexture(titleScreenArrow));
            _menuArrow.zIndex = 2;
            AddGameObject(_menuArrow);

            _menuArrow.Position = new Vector2(menuLocationArrayX[0], menuLocationArrayY[0]);
        }

        public void ChangeScreen(BaseScreen screen)
        {
            previousScreen = currentScreen ?? new EmptyScreen();
            currentScreen = screen;
            this.screenTexture = screen.screenTexture;
            this.menuLocationArrayX = screen.menuLocationArrayX;
            this.menuLocationArrayY = screen.menuLocationArrayY;
            this.menuNavigatorXCap = screen.menuNavigatorXCap;
            this.menuNavigatorYCap = screen.menuNavigatorYCap;
            SplashImage currentSplash = new SplashImage(LoadTexture(screenTexture));
            BaseGameObject holder = getScreenExist(currentSplash.getTextureName());
            BaseGameObject previousholder = getScreenExist(previousScreen.screenTexture);
            if (holder != null) 
            {
                //draws current screen on top of previous screen
                holder.zIndex = 1;
                previousholder.zIndex = 0;
            }
            else
            {
                AddGameObject(currentSplash);
                currentSplash.Activate();
            }        
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime)
        {

            _menuArrow.Position = new Vector2(menuLocationArrayX[menuNavigatorX], menuLocationArrayY[menuNavigatorY]);

            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommand.TestMenuButton)
                {
                    BaseGameObject holder = getScreenExist(screenTexture);
                    holder.Activate();
                }
                if (cmd is SplashInputCommand.TestMenuButton2)
                {
                    BaseGameObject holder = getScreenExist(screenTexture);
                    holder.Deactivate();
                }
                if (cmd is SplashInputCommand.GameSelect)
                {
                    if (!devState)
                    {
                        SwitchState(new GameplayState());
                    }else
                    {
                        SwitchState(new DevState());
                    }
                }
                if (cmd is SplashInputCommand.SettingsSelect) 
                {
                    ChangeScreen(new SettingsScreen());
                }
                if (cmd is SplashInputCommand.BackSelect) 
                {
                    if (previousScreen != null)
                    {
                        ChangeScreen(previousScreen);
                    }
                    else
                    {
                        menuNavigatorY = 4;
                    }
                }
                if (cmd is SplashInputCommand.ExitSelect)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
                if (cmd is SplashInputCommand.MenuMoveUp)
                {
                    menuNavigatorY--;
                }
                if (cmd is SplashInputCommand.MenuMoveDown)
                {
                    menuNavigatorY++;
                }
                KeepArrowinBound(ref menuNavigatorX, menuNavigatorXCap);
                KeepArrowinBound(ref menuNavigatorY, menuNavigatorYCap);

            });
        }
        public string GetCommandState()
        {
            string holder = currentScreen.GetMenuCommand(menuNavigatorX, menuNavigatorY);
            return holder;
        }
        private void KeepArrowinBound(ref int currentArrowPosition, int maxArrowPostion)
        {
            if (currentArrowPosition > maxArrowPostion)
            {
                currentArrowPosition = 0;
            }
            else if (currentArrowPosition < 0)
            {
                currentArrowPosition = maxArrowPostion;
            }
        }

        public override void UpdateGameState(GameTime gameTime) 
        {
            _menuArrow.Update(gameTime);
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper(this));
        }
    }
}
