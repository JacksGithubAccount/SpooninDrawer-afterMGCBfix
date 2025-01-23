using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Objects;
using SpooninDrawer.Engine.States;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Audio;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.States.Gameplay;
using System.Reflection.Metadata;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using TiledSharp;
using SpooninDrawer.Engine.Map;
using SpooninDrawer.States.Splash;
using MonoGame.Extended.Screens;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.Engine.Sound;
using SpooninDrawer.Objects.Gameplay;
using SpooninDrawer.Statics;
using System.Runtime.Serialization.Formatters;

namespace SpooninDrawer.Engine.States.Gameplay
{
    public enum GameplayStateStates
    {
        MainGameState,
        DialogState,
        SpooninDrawerState
    }
    public class GameplayState : BaseGameState
    {
        private const float CAMERA_SPEED = 10.0f;

        private const string BackgroundTexture = "Sprites/Barren";
        private const string PlayerFighter = "Sprites/Animations/PlayerSpriteSheet";
        private const string PlayerAnimationTurnLeft = "Animations/Player/left_walk";
        private const string PlayerAnimationTurnRight = "Animations/Player/right_walk";
        private const string PlayerAnimationIdle = "Animations/Player/idle";
        private const string TiledMapTest = "TiledMaps/testain";
        private const string TilesetTest = "TiledMaps/incrediblybadmspainttileset";
        //rivate const string ExplosionTexture = "Sprites/explosion";


        private const string TextFont = "Fonts/TestText";
        private const string GameOverFont = "Fonts/GameOver";

        private const string BulletSound = "Sounds/bullet";
        //private const string MissileSound = "Sounds/missileSound";

        private const string Soundtrack1 = "Music/FutureAmbient_1";
        private const string Soundtrack2 = "Music/FutureAmbient_2";

        private const int StartingPlayerLives = 3;
        private int _playerLives = StartingPlayerLives;

        private const int MaxExplosionAge = 600; // 10 seconds
        private const int ExplosionActiveLength = 75; // emit particles for 1.2 seconds and let them fade out for 10 seconds

        private Texture2D _missileTexture;
        private Texture2D _exhaustTexture;
        private Texture2D _bulletTexture;
        private Texture2D _explosionTexture;
        private Texture2D _chopperTexture;
        private Texture2D _screenBoxTexture;

        private PlayerSprite _playerSprite;

        public bool paused = false;
        private bool _playerDead;
        private bool _gameOver = false;

        private bool _isShootingBullets;
        private bool _isShootingMissile;
        private TimeSpan _lastBulletShotAt;
        private TimeSpan _lastMissileShotAt;

        private const string StatsFont = "Fonts/Stats";
        private StatsObject _statsText;
        private GameTime gameTime;
        private PopupManager PopupManager;
        
        private MinigameManager MinigameManager;

        TmxMap _map;
        Texture2D _tileSet;
        TiledMap _tiledMap;
        TiledMapTile _tiledMapTile;
        TiledMapRenderer _tiledMapRenderer;
        TiledMapLayer _tiledMapLayer;
        private List<CollidableGameObject> colliders;
        private TilemapManager _tilemapManager;

        public Player player1;
        private InteractableManager interactableManager;

        private OrthographicCamera _camera;

        private BaseGameState menuGameState;
        public bool menuActivate = false;

        private GameplayStateStates CurrentGameplayStateStates;

        public GameplayState(Resolution resolution, SoundManager soundManager)
        {
            _displayResolution = resolution;
            _soundManager = soundManager;
        }
        public override void Initialize(ContentManager contentManager, GameWindow window, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            base.Initialize(contentManager, window, graphicsDevice, graphicsDeviceManager);
            player1 = new Player();
            interactableManager = new InteractableManager();
            ChangeGameStateState(GameplayStateStates.MainGameState);
        }
        public override void LoadContent(ContentManager content)
        {
            gameTime = new GameTime();
            _soundManager.UnloadAllSound();
            if (paused) { paused = false; }
            _debug = true;
            //_explosionTexture = LoadTexture(ExplosionTexture);

            _map = new TmxMap("Content/TiledMaps/testain.tmx");
            _tileSet = content.Load<Texture2D>(TilesetTest + "_0"); // TilesetTest + "_1" + _map.Tilesets[0].Name.ToString() "Content/TileSets/incrediblybadmspainttileset.png"
            int tileWidth = _map.Tilesets[0].TileWidth;
            int tileHeight = _map.Tilesets[0].TileHeight;
            int tileSetTilesWide = _tileSet.Width / tileWidth;
            _tilemapManager = new TilemapManager(_map, _tileSet, tileSetTilesWide, tileWidth, tileHeight);
            _tiledMap = LoadTiledMap(TiledMapTest);
            _tiledMapRenderer = GetTiledMapRenderer(_tiledMap);
            _tiledMapLayer = _tiledMap.GetLayer("Collision");
            colliders = new List<CollidableGameObject>();
            foreach (var o in _map.ObjectGroups["Collision"].Objects)
            {
                colliders.Add(new CollidableGameObject(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height)));
            }

            var turnLeftAnimation = LoadAnimation(PlayerAnimationTurnLeft);
            var turnRightAnimation = LoadAnimation(PlayerAnimationTurnRight);
            var idelAnimation = LoadAnimation(PlayerAnimationIdle);
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter), turnLeftAnimation, turnRightAnimation, idelAnimation);
            _playerSprite.blank = blankTexture;

            var viewportAdapter = new DefaultViewportAdapter(_graphicsDevice);
            _camera = new OrthographicCamera(viewportAdapter);

            _statsText = new StatsObject(LoadFont(StatsFont));
            _statsText.Position = new Vector2(10, 10);
            if (_debug)
            {
                AddGameObject(_statsText);
            }
            menuGameState = new SplashState(new MenuScreen(_displayResolution), this, _displayResolution, _soundManager);
            menuGameState.Initialize(content, _window, _graphicsDevice, _graphics);
            menuGameState.LoadContent(content);

            // load sound effects and register in the sound manager
            var bulletSound = LoadSound(BulletSound);
            //var missileSound = LoadSound(MissileSound);
            _soundManager.RegisterSound(new GameplayEvents.PlayerTest(), bulletSound);

            // load soundtracks into sound manager
            var track1 = LoadSound(Soundtrack1).CreateInstance();
            var track2 = LoadSound(Soundtrack2).CreateInstance();
            _soundManager.SetSoundtrack(new List<SoundEffectInstance>() { track1, track2 });

            interactableManager.LoadContent(content);
            AddGameObject(interactableManager.GetItem());
            colliders.Add(interactableManager.Drawer);
            AddGameObject(interactableManager.Drawer);

            var font = LoadFont(TextFont);
            PopupManager = new PopupManager(font, _playerSprite.Position, _camera, _displayResolution);
            PopupManager.InteractableItemPopupBox.BoxTexture = LoadTexture("Menu/InteractPopupBox");
            PopupManager.AddInventoryPopupBox.BoxTexture = LoadTexture("Menu/InteractPopupBox");
            PopupManager.SetPopupBoxTextures(LoadTexture("Menu/InteractPopupBox"), LoadTexture("Menu/InteractPopupBox"));
            PopupManager.DialogBox.BoxTexture = LoadTexture(PopupManager.DialogBox.TexturePath);
            AddGameObject(PopupManager.InteractableItemPopupBox);
            AddGameObject(PopupManager.AddInventoryPopupBox);
            AddGameObject(PopupManager.DialogBox);

            MinigameManager = new MinigameManager(_playerSprite.Position, _camera, _displayResolution);
            foreach (var item in MinigameManager.DrawerFrames)
            {
                item.SetTexture(LoadTexture(item.TexturePath));
                AddGameObject(item);
            }


            ResetGame();
        }
        //it's handleinput method but specifically for pause so this doesn't pause when you pause the game

        public override void HandleInput(GameTime gameTime)
        {
            if (CurrentGameplayStateStates != GameplayStateStates.SpooninDrawerState)
            {
                InputManager.GetCommands(cmd =>
                {
                    if (cmd is GameplayInputCommand.GameExit)
                    {
                        NotifyEvent(new BaseGameStateEvent.GameQuit());
                    }
                    if (cmd is GameplayInputCommand.PlayerOpenMenu)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            paused = !paused;
                            menuActivate = true;
                        }

                    }
                    if (cmd is GameplayInputCommand.Pause)
                    {
                        paused = !paused;
                    }

                    if (cmd is GameplayInputCommand.PlayerMoveLeft && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            _playerSprite.MoveLeft();
                            KeepPlayerInBounds();
                        }

                    }

                    if (cmd is GameplayInputCommand.PlayerMoveRight && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            _playerSprite.MoveRight();
                            KeepPlayerInBounds();
                        }

                    }

                    if (cmd is GameplayInputCommand.PlayerMoveUp && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            _playerSprite.MoveUp();
                            KeepPlayerInBounds();
                        }

                    }

                    if (cmd is GameplayInputCommand.PlayerMoveDown && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            _playerSprite.MoveDown();
                            KeepPlayerInBounds();
                        }

                    }

                    if (cmd is GameplayInputCommand.PlayerStopsMoving && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            _playerSprite.StopMoving();
                        }

                    }

                    if (cmd is GameplayInputCommand.PlayerAction && !_playerDead)
                    {
                        if (CurrentGameplayStateStates == GameplayStateStates.MainGameState)
                        {
                            //mc action
                            NotifyEvent(new GameplayEvents.PlayerTest());


                            if (!interactableManager.IsInteractableEmpty())
                            {
                                if (interactableManager.GetInteractable().GetType() == typeof(Item))
                                {
                                    //adds top most item to inventory
                                    Item temp = interactableManager.AddToInventory(player1);
                                    AddGameObject(PopupManager.ActivateAddInventoryPopupBox(temp.ToString(), gameTime));
                                    interactableManager.RemoveInteractableItem(temp);
                                    RemoveGameObject(temp);
                                }
                                else if (interactableManager.GetInteractable().GetType() == typeof(InteractableOverworldObject))
                                {
                                    InteractableOverworldObject holder = (InteractableOverworldObject)interactableManager.GetInteractable();
                                    if (holder.State[0])
                                    {
                                        interactableManager.InteractWithObject(_playerSprite.CenterPosition);
                                    }
                                    //drawer and spoon check
                                    if (holder == interactableManager.Drawer && player1.Inventory.Exists(x => x.item == interactableManager.Spoon))
                                    {
                                        MinigameManager.StartDrawerFrame();
                                        ChangeGameStateState(GameplayStateStates.SpooninDrawerState);
                                    }
                                    else
                                    {
                                        //tyest
                                        PopupManager.ActivateDialogBox(StoredDialog.glasses);
                                        ChangeGameStateState(GameplayStateStates.DialogState);
                                    }
                                }
                            }
                        }
                        else if (CurrentGameplayStateStates == GameplayStateStates.DialogState)
                        {
                            if (!PopupManager.DialogBox.IsTextFinishDisplay())
                            {
                                PopupManager.ChangeDialogDisplayTextSpeedFast();
                            }
                            else
                            {
                                if (PopupManager.DialogBox.IsThereNextDialogBox())
                                {
                                    PopupManager.DialogBox.ContinueText();
                                }
                                else if (!StoredDialog.bigChungusBool)
                                {
                                    PopupManager.ActivateDialogBox(StoredDialog.bigChungus);
                                    StoredDialog.bigChungusBool = true;
                                }
                                else
                                {
                                    PopupManager.DeactivateDialogBox();
                                    ChangeGameStateState(GameplayStateStates.MainGameState);
                                }
                            }
                        }

                    }
                });
            }
            else if (CurrentGameplayStateStates == GameplayStateStates.SpooninDrawerState)
            {                
                InputManager.GetCommands(cmd =>
                {                        
                    if (cmd is GameplayInputCommand.GameExit)
                    {
                        NotifyEvent(new BaseGameStateEvent.GameQuit());
                    }
                    if (cmd is GameplayInputCommand.PlayerOpenMenu)
                    {
                        MinigameManager.Deactivate();
                        ChangeGameStateState(GameplayStateStates.MainGameState);
                    }
                    if (cmd is GameplayInputCommand.Pause)
                    {
                        paused = !paused;
                    }

                    if (cmd is GameplayInputCommand.PlayerMoveLeft && !_playerDead)
                    {

                    }
                    if (cmd is GameplayInputCommand.PlayerMoveRight && !_playerDead)
                    {

                    }
                    if (cmd is GameplayInputCommand.PlayerMoveUp && !_playerDead)
                    {

                    }
                    if (cmd is GameplayInputCommand.PlayerMoveDown && !_playerDead)
                    {

                    }
                    if (cmd is GameplayInputCommand.PlayerStopsMoving && !_playerDead)
                    {

                    }
                    if (cmd is GameplayInputCommand.PlayerAction && !_playerDead)
                    {
                        MinigameManager.RandomDrawerFrame();
                    }
                    if (cmd is GameplayInputCommand.PlayerCancel && !_playerDead)
                    {
                        MinigameManager.RandomDrawerFrame();
                    }
                });
            }
        }

        public override void UpdateGameState(GameTime gameTime)
        {

            if (!paused)
            {
                HandleInput(gameTime);
                _tiledMapRenderer.Update(gameTime);
                _playerSprite.Update(gameTime);
                _camera.Position = _playerSprite.Position - _camera.Origin;
                interactableManager.Update(gameTime);
                PopupManager.Update(gameTime, _camera);
                DetectCollisions();
            }
            if (_debug)
            {
                _statsText.Update(gameTime);
            }
            if (menuActivate) { menuGameState.UpdateGameState(gameTime); }
            // get rid of bullets and missiles that have gone out of view
        }
        public Matrix getCameraViewMatrix()
        {
            var transformMatrix = _camera.GetViewMatrix();
            return transformMatrix;
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            _tilemapManager.Draw(spriteBatch);
            base.Render(spriteBatch);

            _playerSprite.Render(spriteBatch);

            interactableManager.Render(spriteBatch);

            if (_gameOver)
            {
                // draw black rectangle at 30% transparency
                var screenBoxTexture = GetScreenBoxTexture(spriteBatch.GraphicsDevice);
                var viewportRectangle = new Rectangle(0, 0, _viewportWidth, _viewportHeight);
                spriteBatch.Draw(screenBoxTexture, viewportRectangle, Color.Black * 0.3f);
            }
            //draws colliders for map
            /*foreach (var box in colliders)
            {
                spriteBatch.Draw(_tileSet, box.GetRectangle(), Color.Purple);
            }*/

            if (menuActivate)
            {
                menuGameState.Render(spriteBatch);
            }

        }

        private Texture2D GetScreenBoxTexture(GraphicsDevice graphicsDevice)
        {
            if (_screenBoxTexture == null)
            {
                _screenBoxTexture = new Texture2D(graphicsDevice, 1, 1);
                _screenBoxTexture.SetData<Color>(new Color[] { Color.White });
            }

            return _screenBoxTexture;
        }


        private void DetectCollisions()
        {
            var playerMapCollisionDetector = new AABBCollisionDetector<CollidableGameObject, PlayerSprite>(colliders);
            playerMapCollisionDetector.DetectCollisions(_playerSprite, (mapTile, player) =>
            {
                if (mapTile.Collidable)
                    _playerSprite.HandleMapCollision(mapTile);
            }, () =>
            {
                _playerSprite.makeMoveAgainfromCollision();
            });
            var playerInteractableCollisionDetector = new AABBCollisionDetector<BaseGameObject, PlayerSprite>(_interactableGameObjects);
            playerInteractableCollisionDetector.DetectCollisions(_playerSprite, (interactable, player) =>
            {
                if (interactable.Interactable)
                {
                    PopupManager.InteractableItemPopupBox.Activate();
                    interactableManager.AddInteractableItem(interactable);
                }
            }, () =>
            {
                PopupManager.InteractableItemPopupBox.Deactivate();
                interactableManager.ClearInteractables();
            });
        }

        private void ResetGame()
        {
            AddGameObject(_playerSprite);

            // position the player in the middle of the screen, at the bottom, leaving a slight gap at the bottom
            var playerXPos = _viewportWidth / 2 - _playerSprite.Width / 2;
            var playerYPos = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(playerXPos, playerYPos);

            _playerDead = false;
        }


        private void GameOver()
        {
            var font = LoadFont(GameOverFont);
            var gameOverText = new GameOverText(font);
            var textPositionOnScreen = new Vector2(460, 300);

            gameOverText.Position = textPositionOnScreen;
            AddGameObject(gameOverText);
            _gameOver = true;
        }

        public void returnToTitle(Resolution resolution)
        {
            _displayResolution = resolution;
            paused = false;
            menuActivate = false;
            _soundManager.UnloadAllSound();
            SwitchState(new SplashState(_displayResolution));
        }
        private void ChangeGameStateState(GameplayStateStates gameplayStateState)
        {
            CurrentGameplayStateStates = gameplayStateState;
        }
        private List<T> CleanObjects<T>(List<T> objectList) where T : BaseGameObject
        {
            List<T> listOfItemsToKeep = new List<T>();
            foreach (T item in objectList)
            {
                var offScreen = item.Position.Y < -50;

                if (offScreen || item.Destroyed)
                {
                    RemoveGameObject(item);
                }
                else
                {
                    listOfItemsToKeep.Add(item);
                }
            }

            return listOfItemsToKeep;
        }



        private void KeepPlayerInBounds()
        {
            if (_playerSprite.Position.X < _camera.BoundingRectangle.Left)
            {
                _playerSprite.Position = new Vector2(0, _playerSprite.
               Position.Y);
            }
            if (_playerSprite.Position.X + _playerSprite.Width >
            _camera.BoundingRectangle.Right)
            {
                _playerSprite.Position = new Vector2(
                _camera.BoundingRectangle.Right - _playerSprite.
               Width,
                _playerSprite.Position.Y);
            }
            if (_playerSprite.Position.Y < _camera.BoundingRectangle.Top)
            {
                _playerSprite.Position = new Vector2(
                _playerSprite.Position.X,
                _camera.BoundingRectangle.Top);
            }
            if (_playerSprite.Position.Y + _playerSprite.Height > _camera.BoundingRectangle.Bottom)
            {
                _playerSprite.Position = new Vector2(
                _playerSprite.Position.X,
                _camera.BoundingRectangle.Bottom - _playerSprite.
               Height);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());

        }
    }
}
