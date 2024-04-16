using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.States;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.Input;
using Microsoft.Xna.Framework.Audio;
using SpooninDrawer.Engine.Sound;
using SpooninDrawer.Objects.Screens;
using Engine2D.PipelineExtensions;
using MonoGame.Extended.Tiled;
using SpooninDrawer.Engine.Objects.Animations;
using MonoGame.Extended.Tiled.Renderers;
using System.Reflection.Metadata;
using System.IO.Pipes;

namespace SpooninDrawer.Engine.States
{
    public abstract class BaseGameState
    {
        private const string FallbackTexture = "Empty";
        private const string FallbackSong = "EmptySound";

        protected bool _debug = false;
        private ContentManager _contentManager;
        protected GraphicsDevice _graphicsDevice;
        protected GameWindow _window;
        protected int _viewportHeight;
        protected int _viewportWidth;
        protected SoundManager _soundManager = new SoundManager();
        protected GraphicsDeviceManager _graphics;
        protected GraphicsAdapter _graphicsAdapter;
        protected Resolution _displayResolution;

        private const string BlankTexture = "Menu/Blank";
        public Texture2D blankTexture;

        private readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

        protected InputManager InputManager { get; set; }

        public void Initialize(ContentManager contentManager, GameWindow window, GraphicsDevice graphicsDevice)
        {
            _contentManager = contentManager;
            _graphicsDevice = graphicsDevice;
            _viewportHeight = graphicsDevice.Viewport.Height;
            _viewportWidth = graphicsDevice.Viewport.Width;
            
            _window = window;
            SetInputManager();
            blankTexture = contentManager.Load<Texture2D>(BlankTexture);
        }
        public void Initialize(ContentManager contentManager, GameWindow window, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager)
        {
            _contentManager = contentManager;
            _graphicsDevice = graphicsDevice;
            _viewportHeight = graphicsDevice.Viewport.Height;
            _viewportWidth = graphicsDevice.Viewport.Width;
            _window = window;
            SetInputManager();
            blankTexture = contentManager.Load<Texture2D>(BlankTexture);
            _graphics = graphicsDeviceManager;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void HandleInput(GameTime gameTime);
        public abstract void UpdateGameState(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;
        public event EventHandler<BaseGameState> OnStateCalled;
        public event EventHandler<BaseGameStateEvent> OnEventNotification;
        protected abstract void SetInputManager();

        public void UnloadContent()
        {
            _contentManager.Unload();
        }
        public void SetResolution(Resolution resolution)
        {
            _displayResolution = resolution;
        }
        protected TiledMap LoadTiledMap(string tiledMapName)
        {
            return _contentManager.Load<TiledMap>(tiledMapName);
        }
        protected TiledMapRenderer GetTiledMapRenderer(TiledMap tiledMap)
        {
            TiledMapRenderer _tiledMapRenderer = new TiledMapRenderer(_graphicsDevice, tiledMap);
            return _tiledMapRenderer;
        }
        protected AnimationData LoadAnimation(string animationName)
        {
            return _contentManager.Load<AnimationData>(animationName);
        }
        public void Update(GameTime gameTime)
        {
            UpdateGameState(gameTime);
            _soundManager.PlaySoundtrack();
        }
        protected Texture2D LoadTexture(string textureName)
        {
            return _contentManager.Load<Texture2D>(textureName);
        }

        protected SpriteFont LoadFont(string fontName)
        {
            return _contentManager.Load<SpriteFont>(fontName);
        }

        protected SoundEffect LoadSound(string soundName)
        {
            return _contentManager.Load<SoundEffect>(soundName);
        }

        protected void NotifyEvent(BaseGameStateEvent gameEvent)
        {
            OnEventNotification?.Invoke(this, gameEvent);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnNotify(gameEvent);
            }

            _soundManager.OnNotify(gameEvent);
        }

        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }
        protected void AddGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected bool isGameObjectExist(BaseGameObject gameObject)
        {
            return _gameObjects.Contains(gameObject);
        }
        protected BaseGameObject GetGameObject(BaseGameObject gameObject) 
        {
            if (_gameObjects.Contains(gameObject))
            { 
                return _gameObjects.Find(x => x == gameObject);
            }
            return null;
        }

        protected BaseGameObject getScreenExist(string screenName)
        {
            BaseGameObject holder = _gameObjects.Find(x => x.getTextureName().Contains(screenName));
             
            return holder;
        }

        protected void RemoveGameObject(BaseGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
            {
                if (_debug)
                {
                    gameObject.RenderBoundingBoxes(spriteBatch);
                }

                gameObject.Render(spriteBatch);
            }
        }
    }
}
