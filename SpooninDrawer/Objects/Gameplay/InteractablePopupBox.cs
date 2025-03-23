using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using SpooninDrawer.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractablePopupBox : BaseGameObject
    {
        public BaseTextObject GameplayText { get; set; }
        public string Text { get { return GameplayText.Text; } set { GameplayText.Text = value; } }
        private string TextToDisplay;
        private List<string> WrappedText;
        public Vector2 MovePopupBoxUp = new Vector2(0, 0);
        private Vector2 TextPosition { get { return GameplayText.Position; } set { GameplayText.Position = value; } }
        private Vector2 BoxPosition { get { return Position; } set { Position = value; } }

        private int NextDialogLineCount = 0;
        private bool IsNextDialogBox = false;
        private int LinesInDialogBox = 2;
        public int WordWrapLength = 23;
        public override Vector2 Position
        {
            get { return Position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value + MovePopupBoxUp;
                if(GameplayText != null)
                    GameplayText.Position = value + new Vector2(5,5) + MovePopupBoxUp;
                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }
        //public new int zIndex { get { return zIndex; } set { zIndex = value; GameplayText.zIndex = value + 1; } }

        public Texture2D BoxTexture { get { return _texture; } set { _texture = value; } }
        public const string TexturePath = "Menu/InteractPopupBox";
        public double FadeAwayTime = 3;
        public double PopupTime = 0;
        public bool FadeAwayPopup = false;
        


        public InteractablePopupBox(BaseTextObject text, Vector2 position, Texture2D boxTexture)
        {
            GameplayText = text;
            Position = position;
            BoxTexture = boxTexture;
            //TextPosition = boxPosition + new Vector2(10, 10);
        }
        public InteractablePopupBox(GameplayText text, Vector2 position) : this(text, position, null) { }

        public void Activate(string text)
        {
            Text = "Added " + text + " to inventory";
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
            Text = text;
            WrappedText = WordWrapper.WordWrap(text, WordWrapLength);
            FitTextInBox();
        }
        public void ChangeText(string text, int wordWrapLenth)
        {
            ResetDialogBox();
            Text = text;
            WrappedText = WordWrapper.WordWrap(text, wordWrapLenth);
            FitTextInBox();
        }
        private void FitTextInBox()
        {
            if (WrappedText.Count >= LinesInDialogBox)
            {
                Text = WrappedText[NextDialogLineCount] + "\n" + WrappedText[NextDialogLineCount + 1];
                NextDialogLineCount += LinesInDialogBox;
            }
            else
            {
                Text = "";
                foreach (string WrappedTextItem in WrappedText)
                {
                    Text += WrappedTextItem + "\n";
                    NextDialogLineCount++;
                }
            }
        }
        public void ResetDialogBox()
        {
            TextToDisplay = "";
            Text = "";
            NextDialogLineCount = 0;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);   
            GameplayText.Render(spriteBatch);

        }
    }
}
