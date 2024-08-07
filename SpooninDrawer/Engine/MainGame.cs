﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.States;
using SpooninDrawer.Engine.States.Gameplay;
using SpooninDrawer.Objects;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.States;
using SpooninDrawer.States.Dev;
using SpooninDrawer.States.Splash;
using System;

namespace SpooninDrawer.Engine
{
    public class MainGame : Game
    {
        private BaseGameState _currentGameState;
        private bool menuStateBool = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch testSpriteBatch;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;

        private int _DesignedResolutionWidth;
        private int _DesignedResolutionHeight;
        private float _designedResolutionAspectRatio;

        private BaseGameState _firstGameState;
        private BaseGameState _testGameState;

        private int targetFPS = 60;

        public MainGame(int width, int height, BaseGameState firstGameState)
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            _firstGameState = firstGameState;
            _DesignedResolutionWidth = width;
            _DesignedResolutionHeight = height;
            _designedResolutionAspectRatio = width / (float)height;
            _testGameState = new TestCameraState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //controls FPS
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / targetFPS);

            graphics.PreferredBackBufferWidth = _DesignedResolutionWidth;
            graphics.PreferredBackBufferHeight = _DesignedResolutionHeight;
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(graphics.GraphicsDevice, _DesignedResolutionWidth, _DesignedResolutionHeight, false,
                SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();
            //_testGameState.Initialize(Content, Window, GraphicsDevice, graphics);
            base.Initialize();
        }

        /// <summary>
        /// Uses the current window size compared to the design resolution
        /// </summary>
        /// <returns>Scaled Rectangle</returns>
        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5;
            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= _designedResolutionAspectRatio)
            {
                var presentHeight = (int)(Window.ClientBounds.Width / _designedResolutionAspectRatio + variance);
                var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                var presentWidth = (int)(Window.ClientBounds.Height * _designedResolutionAspectRatio + variance);
                var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testSpriteBatch = new SpriteBatch(GraphicsDevice);



            //_testGameState.LoadContent(Content);
            SwitchGameState(_firstGameState);
            //CallGameState(_menuGameState);
            menuStateBool = false;
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }
        private void SwitchGameState(BaseGameState gameState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
                _currentGameState.UnloadContent();
            }
            _currentGameState = gameState;
            _currentGameState.Initialize(Content, Window, GraphicsDevice, graphics);
            _currentGameState.LoadContent(Content);

            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
        {
            switch (e)
            {
                case BaseGameStateEvent.GameQuit _:
                    Exit();
                    break;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            _currentGameState?.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //_currentGameState.HandleInput(gameTime);
            _currentGameState.Update(gameTime);
            _testGameState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Render to the Render Target
            GraphicsDevice.SetRenderTarget(_renderTarget);

            //this makes transparency work on tiled maps
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //attaches camera to player
            if (_currentGameState.GetType() == typeof(GameplayState))
            {
                GameplayState tempState = _currentGameState as GameplayState;
                if (!tempState.menuActivate)
                {
                    var transformMatrix = tempState.getCameraViewMatrix();
                    spriteBatch.Begin(transformMatrix: transformMatrix);
                }
                else //calls begin without matrix when a menu is called after game begins
                    spriteBatch.Begin();
            }
            else
                spriteBatch.Begin();
            //testSpriteBatch.Begin();
            
            _currentGameState.Render(spriteBatch);
            //_testGameState.Render(testSpriteBatch);
            spriteBatch.End();
            //testSpriteBatch.End();

            // Now render the scaled content
            graphics.GraphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}