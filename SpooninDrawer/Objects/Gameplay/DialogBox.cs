using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Screens;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class DialogBox : BaseGameObject
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
        private string TextToDisplay;
        private bool IsDisplayText = false;
        private int TextDisplayPositionInString = 0;
        private float TextDisplaySpeed = 2;
        private float TextDisplaySpeedHolder = 0;
        public List<string> TextLog;
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
                    GameplayText.Position = value + new Vector2(25, 25) + MovePopupBoxUp;
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
        private const int WordWrapLength = 183;

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
            else if (DisplayResolution == Resolution.x720)
            {
                TexturePath = TexturePath720;
            }
            
            Text = "";
            ChangeText(Text);
        }
        public DialogBox(GameplayText text, Vector2 position, Resolution resolution) : this(text, position, null, resolution) { }

        public void Activate(string text)
        {
            //Text = "Added " + text + " to inventory";
            Activate();
        }
        public void Activate(string text, bool IsFadeAway)
        {
            FadeAwayPopup = IsFadeAway;
            Activate(text);
        }
        public override void Activate()
        {
            base.Activate();
            GameplayText.Activate();
        }
        public override void Deactivate()
        {
            FadeAwayPopup = false;
            base.Deactivate();
            GameplayText.Deactivate();
        }
        public void ChangeText(string text)
        {
            ResetDialogBox();
            List<string> WrappedText = WordWrapper.WordWrap(text, WordWrapLength);            
            foreach (string WrappedTextItem in WrappedText) 
            {
                TextToDisplay += WrappedTextItem + "\n";
                IsDisplayText = true;
            }
        }
        private void ResetDialogBox()
        {
            TextToDisplay = "";
            Text = "";
            TextDisplayPositionInString = 0;
            TextDisplaySpeedHolder = 0;
        }
        public void Update()
        {
            //controls text display
            if (IsDisplayText) {
                if (TextDisplaySpeed >= 1)
                {
                    for (int i = 0; i < TextDisplaySpeed; i++)
                    {
                        if(TextDisplayPositionInString + i < TextToDisplay.Length - 1)
                            Text += TextToDisplay.ElementAt(TextDisplayPositionInString + i);
                    }
                }
                else
                {
                    if (TextDisplaySpeedHolder % 1 == 0)
                    {
                        Text += TextToDisplay.ElementAt(TextDisplayPositionInString);
                    }
                }
                //checks for out of bounds position in string
                if (TextToDisplay.Length - 1 != TextDisplayPositionInString)
                {
                    TextDisplaySpeedHolder += TextDisplaySpeed;
                    TextDisplayPositionInString = (int)TextDisplaySpeedHolder;
                }
            }
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            GameplayText.Render(spriteBatch);
        }
    }
}
