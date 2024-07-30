using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using SpooninDrawer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.States;
using Microsoft.Xna.Framework;
using SpooninDrawer.States.Gameplay;
using SpooninDrawer.Engine.Input;
using Microsoft.Xna.Framework.Content;

namespace SpooninDrawer.States.Dev
{
    public class TestCameraState : BaseGameState
    {
        private const float CAMERA_SPEED = 10.0f;

        private const string PlayerFighter = "Sprites/Animations/PlayerSpriteSheet";
        private const string PlayerAnimationTurnLeft = "Animations/Player/left_walk";
        private const string PlayerAnimationTurnRight = "Animations/Player/right_walk";
        private const string PlayerAnimationIdle = "Animations/Player/idle";
        private PlayerSprite _playerSprite;
        private OrthographicCamera _camera;
        public override void LoadContent(ContentManager content)
        {
            var viewportAdapter = new DefaultViewportAdapter(_graphicsDevice);
            //_camera = new OrthographicCamera(viewportAdapter);
            //_playerSprite = new PlayerSprite(LoadTexture(PlayerFighter), LoadAnimation(PlayerAnimationTurnLeft), LoadAnimation(PlayerAnimationTurnRight), LoadAnimation(PlayerAnimationIdle));
            var turnLeftAnimation = LoadAnimation(PlayerAnimationTurnLeft);
            var turnRightAnimation = LoadAnimation(PlayerAnimationTurnRight);
            var idelAnimation = LoadAnimation(PlayerAnimationIdle);
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter), turnLeftAnimation, turnRightAnimation, idelAnimation);
            _playerSprite.blank = blankTexture;
            _playerSprite.Position = new Vector2(0, 0);
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            if(_playerSprite != null)
                _playerSprite.Render(spriteBatch);
        }
        public override void UpdateGameState(GameTime gameTime)
        {
            if (_playerSprite != null && _camera != null)
            {
                _playerSprite.Update(gameTime);
                _camera.Position = _playerSprite.Position - _camera.Origin;
            }
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

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.GameExit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is GameplayInputCommand.PlayerMoveLeft)
                {
                    //_playerSprite.MoveLeft();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveRight)
                {
                    //_playerSprite.MoveRight();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveUp)
                {
                    //_playerSprite.MoveUp();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerMoveDown)
                {
                    //_playerSprite.MoveDown();
                    KeepPlayerInBounds();
                }

                if (cmd is GameplayInputCommand.PlayerStopsMoving)
                {
                    _playerSprite.StopMoving();
                }

                if (cmd is GameplayInputCommand.PlayerAction)
                {
                    //mc action
                }
            });
        }
        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }
    }
}
