using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class DialogBox : BaseGameObject, iBaseScreen
    {
        enum titleCommands
        {
            Next,
            Auto,
            Log,
            Skip
        }
        public GameplayText GameplayText { get; set; }
        public string Text { get { return GameplayText.Text; } set { GameplayText.Text = value; } }
        public Vector2 MovePopupBoxUp = new Vector2(0, 0);
        private Vector2 TextPosition { get { return GameplayText.Position; } set { GameplayText.Position = value; } }
        private Vector2 BoxPosition { get { return Position; } set { Position = value; } }
        private Resolution DisplayResolution;

        public override Vector2 Position
        {
            get { return Position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value + MovePopupBoxUp;
                if (GameplayText != null)
                    GameplayText.Position = value + new Vector2(5, 5) + MovePopupBoxUp;
                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }

        public Texture2D BoxTexture { get { return _texture; } set { _texture = value; } }
        public const string TexturePath1080 = "Menu/DialogBox1080";
        public const string TexturePath720 = "Menu/DialogBox720";
        public string TexturePath;
        public double FadeAwayTime = 3;
        public double PopupTime = 0;
        public bool FadeAwayPopup = false;

        public DialogBox(GameplayText text, Vector2 position, Texture2D boxTexture, Resolution resolution)
        {
            DisplayResolution = resolution;
            GameplayText = text;
            Position = position;
            BoxTexture = boxTexture;

            if (DisplayResolution == Resolution.x1080)
            {
                TexturePath = TexturePath1080;

                //button locations
            }
            else if(DisplayResolution == Resolution.x720)
            {
                TexturePath = TexturePath720;
            }
        }
        public void Initialize()
        {
        }
        public void Initialize(Resolution resolution)
        {
            DisplayResolution = resolution;
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            GameplayText.Render(spriteBatch);

        }
    }
