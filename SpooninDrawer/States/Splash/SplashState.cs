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
using MonoGame.Extended.Screens;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using static SpooninDrawer.States.Splash.SplashInputCommand;
using System.Data;

namespace SpooninDrawer.States.Splash
{
    
    public class SplashState : BaseGameState
    {
        private bool devState = false; //true to turn on Dev state, false for gameplay state

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
        BaseGameState StoredState;
        private Stack<BaseScreen> ScreenStack;

        private const string TestFont = "Fonts/TestText";
        private const string MenuFontString = "Fonts/MenuFont";
        private SpriteFont MenuFont;
        TestText _testText;

        private bool VolumeControl = false;
        public SplashState(Resolution resolution)
        {
            _displayResolution = resolution;
            currentScreen = new TitleScreen(_displayResolution);
        }
        public SplashState(BaseScreen Screen, Resolution resolution)
        {
            _displayResolution = resolution;
            currentScreen = Screen;
        }
        public SplashState(BaseScreen Screen, BaseGameState BeforeState, Resolution resolution) : this(Screen, resolution)
        {
            StoredState = BeforeState;
        }
        public override void LoadContent(ContentManager content)
        {
            MenuFont = LoadFont(MenuFontString);
            ScreenStack = new Stack<BaseScreen>();
            _testText = new TestText(LoadFont(TestFont));
            _testText.Position = new Vector2(10.0f, 10.0f);
            _testText.zIndex = 3;
            ChangeScreen(currentScreen);
            AddGameObject(_testText);
            _menuArrow = new MenuArrowSprite(LoadTexture(titleScreenArrow));
            _menuArrow.zIndex = 2;
            AddGameObject(_menuArrow);

            _menuArrow.Position = new Vector2(menuLocationArrayX[0], menuLocationArrayY[0]);
        }

        public void ChangeScreen(BaseScreen screen)
        {
            previousScreen = currentScreen ?? new EmptyScreen();
            RemoveScreenText(previousScreen);
            currentScreen = screen;
            ScreenStack.Push(currentScreen);
            SetScreenPoints(screen);
            AddScreenText(currentScreen);
            SplashImage currentSplash = new SplashImage(LoadTexture(screenTexture));
            currentSplash.Position = screen.Position;
            BaseGameObject holder = getScreenExist(currentSplash.getTextureName());
            BaseGameObject previousholder = getScreenExist(previousScreen.screenTexture);

            if(screen.GetType() == typeof(SettingsScreen)) 
            {
                SettingsScreen setScreen = (SettingsScreen)screen;
                SplashImage volumeBar1 = new SplashImage(LoadTexture(setScreen.volumeBar));
                SplashImage volumeBar2 = new SplashImage(LoadTexture(setScreen.volumeBarArrow));
                SplashImage volumeBar3 = new SplashImage(LoadTexture(setScreen.volumeBarFill));
                volumeBar1.Position = setScreen.volumeBarPosition;
                volumeBar2.Position = setScreen.volumeBarArrowPosition;
                volumeBar3.Position = setScreen.volumeBarFillPosition;
                volumeBar1.zIndex = 2;
                volumeBar2.zIndex = 4;
                volumeBar3.zIndex = 3;
                volumeBar1.Activate();
                volumeBar2.Activate();
                volumeBar3.Activate();
                AddGameObject(volumeBar1);
                AddGameObject(volumeBar2);
                AddGameObject(volumeBar3);
                setScreen.volumeText.zIndex = 2;
                setScreen.volumeText.Activate();
                AddGameObject(setScreen.volumeText);
                
            }
            if(previousScreen.GetType() == typeof(SettingsScreen))
            {
                RemoveSettingScreenAdditions((SettingsScreen)screen);
            }
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
        public void RemoveScreen()
        {
            if (ScreenStack.Count == 1)
            {
                ResumeGameState();
            }
            else
            {
                BaseScreen screen = ScreenStack.Pop();
                RemoveScreenText(screen);
                ScreenStack.TryPeek(out currentScreen);
                SetScreenPoints(currentScreen);
                AddScreenText(currentScreen);
                BaseGameObject toRemove = getScreenExist(screen.screenTexture);
                RemoveGameObject(toRemove);

                if (screen.GetType() == typeof(SettingsScreen))
                {
                    RemoveSettingScreenAdditions((SettingsScreen)screen);
                }
            }
        }
        public void ReloadAllScreens()
        {
            Stack<BaseScreen> LoadStack = new Stack<BaseScreen>();
            for (int i = 0; i <= ScreenStack.Count; i++)
            {
                LoadStack.Push(ScreenStack.Pop());
                RemoveScreenText(LoadStack.Peek());
                BaseGameObject toRemove = getScreenExist(LoadStack.Peek().screenTexture);
                RemoveGameObject(toRemove);
            }
            for (int i = 0; i <= LoadStack.Count; i++)
            {
                ChangeScreen(LoadStack.Pop().Initialize(_displayResolution));
            }
        }
        private void SetScreenPoints(BaseScreen screen)
        {
            this.screenTexture = screen.screenTexture;
            this.menuLocationArrayX = screen.menuLocationArrayX;
            this.menuLocationArrayY = screen.menuLocationArrayY;
            this.menuNavigatorXCap = screen.menuNavigatorXCap;
            this.menuNavigatorYCap = screen.menuNavigatorYCap;
        }
        private void RemoveScreenText(BaseScreen screen)
        {

            if (screen.ScreenText != null)
            {
                foreach (BaseTextObject text in screen.ScreenText)
                {                    
                    RemoveGameObject(text);
                }
            }
        }
        private void AddScreenText(BaseScreen screen)
        {
            if (screen.ScreenText != null)
            {
                foreach (BaseTextObject text in screen.ScreenText)
                {
                    if (text != null)
                        AddGameObject(text);
                }
            }
        }
        private void RemoveSettingScreenAdditions(SettingsScreen screen)
        {
            BaseGameObject volumeBar1 = getScreenExist(screen.volumeBar);
            BaseGameObject volumeBar2 = getScreenExist(screen.volumeBarArrow);
            BaseGameObject volumeBar3 = getScreenExist(screen.volumeBarFill);
            volumeBar1.Deactivate();
            volumeBar2.Deactivate();
            volumeBar3.Deactivate();
            RemoveGameObject(volumeBar1);
            RemoveGameObject(volumeBar2);
            RemoveGameObject(volumeBar3);
            screen.volumeText.Deactivate();
            RemoveGameObject(screen.volumeText);
        }
        private void ChangeVolume(float volume)
        {
            if(currentScreen.GetType() == typeof(SettingsScreen))
            {
                SettingsScreen settingsScreen = (SettingsScreen)currentScreen;               
                settingsScreen.VolumeChange(volume);
                _soundManager.ChangeBGMVolume(settingsScreen.GetVolume());
                //AddGameObject(settingsScreen.volumeText);
                //RemoveGameObject(settingsScreen.volumeText);
                
            }
        }
        public void ResumeGameState()
        {
            if (StoredState != null)
            {
                GameplayState gameState = (GameplayState)StoredState;
                gameState.menuActivate = false;
                gameState.paused = false;
            }
        }

        public override void HandleInput(GameTime gameTime)
        {
            try
            {
                _menuArrow.Activate();
                _menuArrow.Position = new Vector2(menuLocationArrayX[menuNavigatorX], menuLocationArrayY[menuNavigatorY]);
            }
            catch
            {
                _menuArrow.Deactivate();
            }

            InputManager.GetCommands(cmd =>
            {
                if (!VolumeControl)
                {
                    if (cmd is SplashInputCommand.SetFullScreen)
                    {
                        _graphics.IsFullScreen = true;
                        _graphics.HardwareModeSwitch = true;
                        _graphics.ApplyChanges();
                    }
                    if (cmd is SplashInputCommand.SetWindowScreen)
                    {
                        _graphics.IsFullScreen = false;
                        _graphics.ApplyChanges();
                    }
                    if (cmd is SplashInputCommand.SetBorderlessScreen)
                    {
                        _graphics.IsFullScreen = true;
                        if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1920 || GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 1080)
                        {
                            _displayResolution = Resolution.x1080;
                            _graphics.PreferredBackBufferWidth = 1920;
                            _graphics.PreferredBackBufferHeight = 1080;
                        }
                        else if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width == 1280 || GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height == 720)
                        {
                            _displayResolution = Resolution.x720;
                            _graphics.PreferredBackBufferWidth = 1280;
                            _graphics.PreferredBackBufferHeight = 720;
                        }
                        //_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                        //_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                        _graphics.HardwareModeSwitch = false;

                        _graphics.ApplyChanges();
                    }
                    if (cmd is SplashInputCommand.SetResolution1080)
                    {
                        _displayResolution = Resolution.x1080;
                        _graphics.PreferredBackBufferWidth = 1920;
                        _graphics.PreferredBackBufferHeight = 1080;
                        _graphics.ApplyChanges();
                        ReloadAllScreens();
                    }
                    if (cmd is SplashInputCommand.SetResolution720)
                    {
                        _displayResolution = Resolution.x720;
                        _graphics.PreferredBackBufferWidth = 1280;
                        _graphics.PreferredBackBufferHeight = 720;
                        _graphics.ApplyChanges();
                        ReloadAllScreens();
                    }
                    if (cmd is SplashInputCommand.RemapControlSelect)
                    {
                        ChangeScreen(new RemapControlsScreen(MenuFont, new Vector2(_menuArrow.Width / 2, _menuArrow.Height / 3), InputManager.GetInputDetector(), _displayResolution));
                    }
                    if (cmd is SplashInputCommand.RemapControlConfirm)
                    {
                        ChangeScreen(new RemapControlConfirmScreen(MenuFont, new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), InputManager.GetInputDetector()));

                    }
                    if (cmd is SplashInputCommand.RemapControlDone)
                    {
                        ChangeScreen(new RemapControlDoneScreen(MenuFont, new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), InputManager.GetInputDetector()));
                    }
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
                            SwitchState(new GameplayState(_displayResolution));
                        }
                        else
                        {
                            SwitchState(new DevState());
                        }
                    }
                    if (cmd is SplashInputCommand.SettingsSelect)
                    {
                        ChangeScreen(new SettingsScreen(MenuFont, new Vector2(_menuArrow.Width / 2, _menuArrow.Height / 3), _displayResolution));
                    }
                    if (cmd is SplashInputCommand.BackSelect)
                    {
                        if (previousScreen != null)
                        {
                            RemoveScreen();
                        }
                        else
                        {
                            menuNavigatorY = 4;
                        }
                    }
                    if (cmd is SplashInputCommand.ExitSelect)
                    {
                        NotifyEvent(new BaseGameStateEvent.GameQuit());
                        GameplayState gameState = (GameplayState)StoredState;
                        gameState?.returnToTitle(_displayResolution);
                    }
                    if (cmd is SplashInputCommand.ResumeSelect)
                    {
                        ResumeGameState();
                    }
                    if (cmd is SplashInputCommand.CheckMenuSelect)
                    {
                        ChangeScreen(new ReturnToTitleScreen(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height));
                    }
                    if (cmd is SplashInputCommand.SettingVolumeSelect)
                    {
                        VolumeControl = true;
                    }
                    if (cmd is SplashInputCommand.MenuMoveUp)
                    {
                        menuNavigatorY--;
                    }
                    if (cmd is SplashInputCommand.MenuMoveDown)
                    {
                        menuNavigatorY++;
                    }
                    if (cmd is SplashInputCommand.MenuMoveLeft)
                    {
                        menuNavigatorX--;
                    }
                    if (cmd is SplashInputCommand.MenuMoveRight)
                    {
                        menuNavigatorX++;
                    }
                    KeepArrowinBound(ref menuNavigatorX, menuNavigatorXCap);
                    KeepArrowinBound(ref menuNavigatorY, menuNavigatorYCap);
                }
                else
                {
                    if (cmd is SplashInputCommand.MenuMoveLeft)
                    {
                        ChangeVolume(-0.01f);
                    }
                    if (cmd is SplashInputCommand.MenuMoveRight)
                    {
                        ChangeVolume(0.01f);
                    }
                    if (cmd is SplashInputCommand.BackSelect)
                    {
                        VolumeControl = false;
                    }
                    if (cmd is SplashInputCommand.SettingVolumeSelect)
                    {
                        VolumeControl = false;
                    }
                }
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
            HandleInput(gameTime);
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper(this));
        }
    }
}
