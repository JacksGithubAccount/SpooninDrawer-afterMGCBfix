using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Objects;
using System.IO;
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
using SpooninDrawer.Engine.Sound;
using Microsoft.Xna.Framework.Audio;
using SpooninDrawer.States.Gameplay;
using static SpooninDrawer.Objects.Screens.SettingsScreen;
using SpooninDrawer.Engine.SaveData;

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
        private int[] menuNavigatorXCap;
        private int menuNavigatorYCap;
        private MousePositionHandler mousePositionHandler;
        private KeyboardPositionHandler keyboardPositionHandler;
        iBaseScreen currentScreen;
        iBaseScreen previousScreen;
        private PopupScreen popupScreen;
        private Vector2 popupPosition;
        BaseGameState StoredState;
        private Stack<iBaseScreen> ScreenStack;

        private const string TestFont = "Fonts/TestText";
        private const string MenuFontString = "Fonts/MenuFont";

        private const string BeepSound = "Sounds/beep";

        private const string Soundtrack1 = "Music/FutureAmbient_3";
        private const string Soundtrack2 = "Music/FutureAmbient_4";

        SettingsManager settingsManager;
        SettingsDataManager settingsDataManager;
        SettingsData data;

        private SpriteFont MenuFont;
        TestText _testText;

        private bool VolumeBGMControl = false;
        private bool VolumeSEControl = false;

        public SplashState(Resolution resolution)
        {
            _displayResolution = resolution;
            currentScreen = new TitleScreen(_displayResolution);
        }
        public SplashState(iBaseScreen Screen, Resolution resolution)
        {
            _displayResolution = resolution;
            currentScreen = Screen;
        }
        public SplashState(iBaseScreen Screen, BaseGameState BeforeState, Resolution resolution, SoundManager soundManager) : this(Screen, resolution)
        {
            StoredState = BeforeState;
            _soundManager = soundManager;
        }
        public override void LoadContent(ContentManager content)
        {
            data = new SettingsData();
            settingsDataManager = new SettingsDataManager(data);            
            if (!settingsDataManager.DoesSettingsDataTextExist())
                settingsDataManager.CreateFile();
            data = settingsDataManager.LoadSettingsData(data);
            settingsManager = new SettingsManager(data, _graphics);
            _soundManager.UnloadAllSound();
            MenuFont = LoadFont(MenuFontString);
            ScreenStack = new Stack<iBaseScreen>();
            _testText = new TestText(LoadFont(TestFont));
            _testText.Position = new Vector2(10.0f, 10.0f);
            _testText.zIndex = 3;
            ChangeScreen(currentScreen);
            AddGameObject(_testText);
            _menuArrow = new MenuArrowSprite(LoadTexture(titleScreenArrow));
            _menuArrow.zIndex = 2;
            AddGameObject(_menuArrow);

            _menuArrow.Position = new Vector2(menuLocationArrayX[0], menuLocationArrayY[0]);
            if (currentScreen.hasButtons)
                mousePositionHandler = new MousePositionHandler((BaseScreenwithButtons)currentScreen);
            keyboardPositionHandler = new KeyboardPositionHandler();

            var beepSound = LoadSound(BeepSound);
            //var missileSound = LoadSound(MissileSound);
            _soundManager.RegisterSound(new SplashEvents.SplashMoveArrow(), beepSound);

            var track1 = LoadSound(Soundtrack1).CreateInstance();
            var track2 = LoadSound(Soundtrack2).CreateInstance();
            _soundManager.SetSoundtrack(new List<SoundEffectInstance>() { track1, track2 });

            popupPosition = new Vector2(_graphicsDevice.Viewport.Width / 3, _graphicsDevice.Viewport.Height / 3);
            popupScreen = new PopupScreen(MenuFont, popupPosition);
        }
        public void ChangeScreen(iBaseScreen screen)
        {
            ChangeScreen(screen, true);
        }
        public void ChangeScreen(iBaseScreen screen, bool hidePreviousScreenText)
        {
            previousScreen = currentScreen ?? new EmptyScreen();
            if (hidePreviousScreenText)
            {
                RemoveScreenText(previousScreen);
            }
            currentScreen = screen;
            ScreenStack.Push(currentScreen);
            SetScreenPoints(screen);
            AddScreenText(currentScreen);
            SplashImage currentSplash = new SplashImage(LoadTexture(screenTexture));
            currentSplash.Position = screen.Position;
            menuNavigatorX = 0;
            menuNavigatorY = 0;
            if (currentScreen.hasButtons && keyboardPositionHandler is not null)
            {
                Vector2 menuArrowChecker = keyboardPositionHandler.CheckReadOnlyPositionAtScreenLoad((BaseScreenwithButtons)currentScreen, menuNavigatorX, menuNavigatorY);
                menuNavigatorX = (int)Math.Round(menuArrowChecker.X);
                menuNavigatorY = (int)Math.Round(menuArrowChecker.Y);
            }
            BaseGameObject holder = getScreenExist(currentSplash.getTextureName());
            BaseGameObject previousholder = getScreenExist(previousScreen.screenTexture);
            if (currentScreen.hasButtons)
                mousePositionHandler?.SetScreen((BaseScreenwithButtons)currentScreen);

            if (screen.GetType() == typeof(SettingsScreen))
            {
                SettingsScreen setScreen = (SettingsScreen)screen;
                AddSettingScreenAdditions(setScreen);
            }
            if (previousScreen.GetType() == typeof(SettingsScreen))
            {
                RemoveSettingScreenAdditions((SettingsScreen)previousScreen);
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
                iBaseScreen screen = ScreenStack.Pop();
                RemoveScreenText(screen);
                ScreenStack.TryPeek(out currentScreen);
                SetScreenPoints(currentScreen);
                AddScreenText(currentScreen);
                if (currentScreen.GetType() == typeof(SettingsScreen))
                {
                    SettingsScreen setScreen = (SettingsScreen)currentScreen;
                    AddSettingScreenAdditions(setScreen);
                }
                menuNavigatorX = 0;
                menuNavigatorY = 0;
                if (currentScreen.hasButtons && keyboardPositionHandler is not null)
                {
                    Vector2 menuArrowChecker = keyboardPositionHandler.CheckReadOnlyPositionAtScreenLoad((BaseScreenwithButtons)currentScreen, menuNavigatorX, menuNavigatorY);
                    menuNavigatorX = (int)Math.Round(menuArrowChecker.X);
                    menuNavigatorY = (int)Math.Round(menuArrowChecker.Y);
                }
                BaseGameObject toRemove = getScreenExist(screen.screenTexture);
                RemoveGameObject(toRemove);
                if (currentScreen.hasButtons)
                    mousePositionHandler.SetScreen((BaseScreenwithButtons)currentScreen);
                if (screen.GetType() == typeof(SettingsScreen))
                {
                    RemoveSettingScreenAdditions((SettingsScreen)screen);
                }
            }
        }
        public void ReloadAllScreens()
        {
            Stack<iBaseScreen> LoadStack = new Stack<iBaseScreen>();
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
        private void SetScreenPoints(iBaseScreen screen)
        {
            this.screenTexture = screen.screenTexture;
            this.menuLocationArrayX = screen.menuLocationArrayX;
            this.menuLocationArrayY = screen.menuLocationArrayY;
            this.menuNavigatorXCap = screen.menuNavigatorXCap;
            this.menuNavigatorYCap = screen.menuNavigatorYCap;
        }
        private void RemoveScreenText(iBaseScreen screen)
        {

            if (screen.ScreenText != null)
            {
                foreach (BaseTextObject text in screen.ScreenText)
                {
                    RemoveGameObject(text);
                }
            }
        }
        private void AddScreenText(iBaseScreen screen)
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
        private void AddSettingScreenAdditions(SettingsScreen setScreen)
        {
            SplashImage volumeBGMBar1 = new SplashImage(LoadTexture(setScreen.volumeBar));
            setScreen.volumeBarLength = volumeBGMBar1.Width;
            SplashImage volumeBGMBarArrow = new SplashImage(LoadTexture(setScreen.volumeBarArrow));
            SplashImageLoadingBar volumeBGMBarFill = new SplashImageLoadingBar(LoadTexture(setScreen.volumeBarFill), setScreen.GetVolume(VolumeType.BGM));
            SplashImage volumeSEBar1 = new SplashImage(LoadTexture(setScreen.volumeBar));
            SplashImage volumeSEBarArrow = new SplashImage(LoadTexture(setScreen.volumeBarArrow));
            SplashImageLoadingBar volumeSEBarFill = new SplashImageLoadingBar(LoadTexture(setScreen.volumeBarFill), setScreen.GetVolume(VolumeType.SE));
            volumeBGMBar1.Position = setScreen.volumeBGMBarPosition;
            volumeBGMBarFill.Position = setScreen.volumeBGMBarFillPosition;
            volumeBGMBarArrow.Position = volumeBGMBarFill.GetEndofBarPosition() - new Vector2(volumeBGMBarArrow.Width / 2, volumeBGMBarArrow.Height / 2);
            volumeSEBar1.Position = setScreen.volumeSEBarPosition;
            volumeSEBarFill.Position = setScreen.volumeSEBarFillPosition;
            volumeSEBarArrow.Position = volumeSEBarFill.GetEndofBarPosition() - new Vector2(volumeSEBarArrow.Width / 2, volumeSEBarArrow.Height / 2);
            volumeBGMBar1.zIndex = 2;
            volumeBGMBarArrow.zIndex = 4;
            volumeBGMBarFill.zIndex = 3;
            volumeSEBar1.zIndex = 2;
            volumeSEBarArrow.zIndex = 4;
            volumeSEBarFill.zIndex = 3;
            volumeBGMBar1.Activate();
            volumeBGMBarArrow.Activate();
            volumeBGMBarFill.Activate();
            volumeSEBar1.Activate();
            volumeSEBarArrow.Activate();
            volumeSEBarFill.Activate();
            AddGameObject(volumeBGMBar1);
            AddGameObject(volumeBGMBarArrow);
            AddGameObject(volumeBGMBarFill);
            AddGameObject(volumeSEBar1);
            AddGameObject(volumeSEBarArrow);
            AddGameObject(volumeSEBarFill);
            setScreen.volumeBGMText.zIndex = 2;
            setScreen.volumeBGMText.Activate();
            AddGameObject(setScreen.volumeBGMText);
            setScreen.volumeSEText.zIndex = 2;
            setScreen.volumeSEText.Activate();
            AddGameObject(setScreen.volumeSEText);

        }
        private void RemoveSettingScreenAdditions(SettingsScreen screen)
        {
            List<BaseGameObject> volumeBar1 = getAllScreenExist(screen.volumeBar);
            List<BaseGameObject> volumeBar2 = getAllScreenExist(screen.volumeBarArrow);
            List<BaseGameObject> volumeBar3 = getAllScreenExist(screen.volumeBarFill);
            foreach (BaseGameObject volumeBar in volumeBar1)
            {
                volumeBar.Deactivate();
                RemoveGameObject(volumeBar);
            }
            foreach (BaseGameObject volumeBar in volumeBar2)
            {
                volumeBar.Deactivate();
                RemoveGameObject(volumeBar);
            }
            foreach (BaseGameObject volumeBar in volumeBar3)
            {
                volumeBar.Deactivate();
                RemoveGameObject(volumeBar);
            }
            screen.volumeBGMText.Deactivate();
            RemoveGameObject(screen.volumeBGMText);
            screen.volumeSEText.Deactivate();
            RemoveGameObject(screen.volumeSEText);
        }
        private void ChangeVolume(float volume, VolumeType volumeType, VolumeChangeType volumeChangeType)
        {
            if (currentScreen.GetType() == typeof(SettingsScreen))
            {
                SettingsScreen settingsScreen = (SettingsScreen)currentScreen;
                settingsScreen.VolumeChange(volume, volumeType, volumeChangeType);
                if (volumeType == VolumeType.BGM)
                {
                    _soundManager.ChangeVolumeBGM(settingsScreen.GetVolume(volumeType));
                    try
                    {
                        SplashImageLoadingBar volumeBarFill = (SplashImageLoadingBar)getScreenExist(settingsScreen.volumeBarFill);
                        volumeBarFill.UpdateLoadingBar(settingsScreen.GetVolume(volumeType));
                        BaseGameObject volumeBarArrow = getScreenExist(settingsScreen.volumeBarArrow);
                        volumeBarArrow.Position = volumeBarFill.GetEndofBarPosition() - new Vector2(volumeBarArrow.Width / 2, volumeBarArrow.Height / 2);
                    }
                    catch { /*do nothing*/}
                    data.VolumeBGMValue = settingsScreen.GetVolume(volumeType);
                }
                else if (volumeType == VolumeType.SE)
                {
                    _soundManager.ChangeVolumeSE(settingsScreen.GetVolume(volumeType));
                    try
                    {
                        SplashImageLoadingBar volumeBarFill = (SplashImageLoadingBar)getAllScreenExist(settingsScreen.volumeBarFill)[1];
                        volumeBarFill.UpdateLoadingBar(settingsScreen.GetVolume(volumeType));
                        BaseGameObject volumeBarArrow = getAllScreenExist(settingsScreen.volumeBarArrow)[1];
                        volumeBarArrow.Position = volumeBarFill.GetEndofBarPosition() - new Vector2(volumeBarArrow.Width / 2, volumeBarArrow.Height / 2);
                    }
                    catch { /*do nothing*/}
                    data.VolumeSEValue = settingsScreen.GetVolume(volumeType);
                }


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
        public void ChangePopupDescriptionText(string desription)
        {
            popupScreen.ScreenText[0, 0].Text = desription;
        }
        public bool IsCurrentScreenHasButtons()
        {
            return currentScreen.hasButtons;
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
                if (!VolumeBGMControl && !VolumeSEControl)
                {
                    if (cmd is SplashInputCommand.MenuMoveUp)
                    {
                        NotifyEvent(new SplashEvents.SplashMoveArrow());
                        menuNavigatorY = keyboardPositionHandler.CheckKeyboardforMove(currentScreen, menuNavigatorX, menuNavigatorY, new Vector2(0, -1));
                    }
                    if (cmd is SplashInputCommand.MenuMoveDown)
                    {
                        NotifyEvent(new SplashEvents.SplashMoveArrow());
                        menuNavigatorY = keyboardPositionHandler.CheckKeyboardforMove(currentScreen, menuNavigatorX, menuNavigatorY, new Vector2(0, 1));
                    }
                    if (cmd is SplashInputCommand.MenuMoveLeft)
                    {
                        NotifyEvent(new SplashEvents.SplashMoveArrow());
                        menuNavigatorX = keyboardPositionHandler.CheckKeyboardforMove(currentScreen, menuNavigatorX, menuNavigatorY, new Vector2(-1, 0));
                    }
                    if (cmd is SplashInputCommand.MenuMoveRight)
                    {
                        NotifyEvent(new SplashEvents.SplashMoveArrow());
                        menuNavigatorX = keyboardPositionHandler.CheckKeyboardforMove(currentScreen, menuNavigatorX, menuNavigatorY, new Vector2(1, 0));
                    }
                    if (cmd is SplashInputCommand.SetFullScreen)
                    {
                        _graphics.IsFullScreen = true;
                        _graphics.HardwareModeSwitch = true;
                        _graphics.ApplyChanges();
                        data.ScreenSettingsValue = SettingsDataManager.FullScreenText;
                    }
                    if (cmd is SplashInputCommand.SetWindowScreen)
                    {
                        _graphics.IsFullScreen = false;
                        _graphics.ApplyChanges();
                        data.ScreenSettingsValue = SettingsDataManager.WindowText;
                    }
                    if (cmd is SplashInputCommand.SetBorderlessScreen)
                    {
                        settingsManager.SetBorderlessScreen(_displayResolution);
                        data.ScreenSettingsValue = SettingsDataManager.BorderlessText;
                    }
                    if (cmd is SplashInputCommand.SetResolution1080)
                    {
                        _displayResolution = Resolution.x1080;
                        _graphics.PreferredBackBufferWidth = 1920;
                        _graphics.PreferredBackBufferHeight = 1080;
                        _graphics.ApplyChanges();
                        ReloadAllScreens();
                        data.ResolutionValue = _displayResolution;
                    }
                    if (cmd is SplashInputCommand.SetResolution720)
                    {
                        _displayResolution = Resolution.x720;
                        _graphics.PreferredBackBufferWidth = 1280;
                        _graphics.PreferredBackBufferHeight = 720;
                        _graphics.ApplyChanges();
                        ReloadAllScreens();
                        data.ResolutionValue = _displayResolution;
                    }
                    if (cmd is SplashInputCommand.RemapControlSelect)
                    {
                        ChangeScreen(new RemapControlsScreen(MenuFont, new Vector2(_menuArrow.Width / 2, _menuArrow.Height / 3), InputManager.GetInputDetector(), _displayResolution));
                    }
                    if (cmd is SplashInputCommand.RemapControlConfirm)
                    {
                        ChangeScreen(new RemapControlEnterButtonScreen(MenuFont, new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), InputManager.GetInputDetector()));

                    }
                    if (cmd is SplashInputCommand.RemapControlDone)
                    {
                        //removes pop ups from stack and refreshes remap screen to display up to date information
                        RemoveScreen();
                        RemoveScreen();
                        RemoveScreen();
                        ChangeScreen(new RemapControlsScreen(MenuFont, new Vector2(_menuArrow.Width / 2, _menuArrow.Height / 3), InputManager.GetInputDetector(), _displayResolution));
                        ChangeScreen(new RemapControlDoneScreen(MenuFont, new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), InputManager.GetInputDetector()));
                        data.KeyboardControls = InputManager.GetInputDetector().GetKeyboardControls();
                        data.MouseControls = InputManager.GetInputDetector().GetMouseControls();
                    }
                    if (cmd is SplashInputCommand.RemapControlDuplicate)
                    {
                        ChangeScreen(popupScreen);
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
                        settingsDataManager.SaveSettingsData();
                        if (!devState)
                        {
                            SwitchState(new GameplayState(_displayResolution, _soundManager));
                        }
                        else
                        {
                            SwitchState(new DevState());
                        }
                    }
                    if (cmd is SplashInputCommand.LoadSelect)
                    {
                        ChangeScreen(popupScreen, true); //testing popup windows
                    }
                    if (cmd is SplashInputCommand.SettingsSelect)
                    {
                        ChangeScreen(new SettingsScreen(MenuFont, new Vector2(_menuArrow.Width / 2, _menuArrow.Height / 3), _displayResolution, _soundManager.GetVolumeBGM(), _soundManager.GetVolumeSE()));
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
                        settingsDataManager.SaveSettingsData();
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
                    if (cmd is SplashInputCommand.SettingVolumeBGMSelect)
                    {
                        VolumeBGMControl = true;
                    }
                    if (cmd is SplashInputCommand.SettingVolumeSESelect)
                    {
                        VolumeSEControl = true;
                    }
                    if (cmd is SplashInputCommand.MouseVolumeBGM)
                    {
                        SettingsScreen setScreen = (SettingsScreen)currentScreen;
                        ChangeVolume((mousePositionHandler.GetMouseState().Position.X - setScreen.volumeBGMBarPosition.X) / setScreen.volumeBarLength, VolumeType.BGM, VolumeChangeType.set);
                    }
                    if (cmd is SplashInputCommand.MouseVolumeSE)
                    {
                        SettingsScreen setScreen = (SettingsScreen)currentScreen;
                        ChangeVolume((mousePositionHandler.GetMouseState().Position.X - setScreen.volumeBGMBarPosition.X) / setScreen.volumeBarLength, VolumeType.SE, VolumeChangeType.set);

                    }
                    KeepArrowinBound(ref menuNavigatorX, menuNavigatorXCap, ref menuNavigatorY, menuNavigatorYCap);
                }
                else
                {
                    if (VolumeBGMControl)
                    {
                        if (cmd is SplashInputCommand.MenuMoveLeft || cmd is SplashInputCommand.MenuHoldLeft)
                        {
                            ChangeVolume(-0.01f, VolumeType.BGM, VolumeChangeType.incremental);
                        }
                        if (cmd is SplashInputCommand.MenuMoveRight || cmd is SplashInputCommand.MenuHoldRight)
                        {
                            ChangeVolume(0.01f, VolumeType.BGM, VolumeChangeType.incremental);
                        }
                        if (cmd is SplashInputCommand.BackSelect)
                        {
                            VolumeBGMControl = false;
                        }
                        if (cmd is SplashInputCommand.SettingVolumeBGMSelect)
                        {
                            VolumeBGMControl = false;
                        }
                    }
                    else if (VolumeSEControl)
                    {
                        if (cmd is SplashInputCommand.MenuMoveLeft || cmd is SplashInputCommand.MenuHoldLeft)
                        {
                            ChangeVolume(-0.01f, VolumeType.SE, VolumeChangeType.incremental);
                        }
                        if (cmd is SplashInputCommand.MenuMoveRight || cmd is SplashInputCommand.MenuHoldRight)
                        {
                            ChangeVolume(0.01f, VolumeType.SE, VolumeChangeType.incremental);
                        }
                        if (cmd is SplashInputCommand.BackSelect)
                        {
                            VolumeSEControl = false;
                        }
                        if (cmd is SplashInputCommand.SettingVolumeSESelect)
                        {
                            VolumeSEControl = false;
                        }
                    }
                }
            });
        }
        public string GetCommandStateforKey()
        {
            string holder = currentScreen.GetMenuCommand(menuNavigatorX, menuNavigatorY);
            return holder;
        }
        public string GetCommandStateforMouse()
        {
            return currentScreen.GetMenuCommand(mousePositionHandler.GetScreenPosition());
        }
        private void KeepArrowinBound(ref int currentArrowPosition, int maxArrowPosition)
        {
            if (currentArrowPosition > maxArrowPosition)
            {
                currentArrowPosition = 0;
            }
            else if (currentArrowPosition < 0)
            {
                currentArrowPosition = maxArrowPosition;
            }
        }
        private void KeepArrowinBound(ref int currentArrowPositionX, int[] maxArrowPositionX, ref int currentArrowPositionY, int maxArrowPositionY)
        {
            KeepArrowinBound(ref currentArrowPositionY, maxArrowPositionY);
            if (maxArrowPositionX.Length == maxArrowPositionY + 1)
            {
                if (currentArrowPositionX > maxArrowPositionX[currentArrowPositionY])
                {
                    currentArrowPositionX = 0;
                }
                else if (currentArrowPositionX < 0)
                {
                    currentArrowPositionX = maxArrowPositionX[currentArrowPositionY];
                }
            }
            else
            {
                KeepArrowinBound(ref currentArrowPositionX, maxArrowPositionX[0]);
            }
        }
        public override void UpdateGameState(GameTime gameTime)
        {

            if (mousePositionHandler.IsMouseOverButton())
            {
                Vector2 holder = mousePositionHandler.GetButtonUnderMouse();
                menuNavigatorX = (int)holder.X;
                menuNavigatorY = (int)holder.Y;
            }
            _menuArrow.Update(gameTime);
            HandleInput(gameTime);
        }
        public MousePositionHandler GetMousePositionHandler() { return mousePositionHandler; }
        protected override void SetInputManager()
        {
            InputManager = new InputManager(new SplashInputMapper(this, mousePositionHandler));
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            if (currentScreen.hasButtons)
            {
                BaseScreenwithButtons holder = (BaseScreenwithButtons)currentScreen;
                foreach (SplashRectangle[] rect in holder.ButtonRectangles)
                {
                    foreach (SplashRectangle rect2 in rect)
                    {
                        spriteBatch.Draw(blankTexture, new Vector2(rect2.Rectangle.X, rect2.Rectangle.Y), rect2.Rectangle, Color.Crimson * 0.5f);
                    }
                }
            }
        }
    }
}
