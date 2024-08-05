using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractablePopupBox : BaseGameObject
    {
        public GameplayText GameplayText { get; set; }
        public string Text { get { return GameplayText.Text; } set { GameplayText.Text = value; } }
        private Vector2 TextPosition { get { return GameplayText.Position; } set { GameplayText.Position = value; } }
        private Vector2 BoxPosition { get { return Position; } set { Position = value; } }
        public override Vector2 Position
        {
            get { return _position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;
                if(GameplayText != null)
                    GameplayText.Position = value;
                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }

        public Texture2D BoxTexture { get { return _texture; } set { _texture = value; } }
        public const string TexturePath = "Menu/InteractPopupBox";
        public double FadeAwayTime = 3;
        public double PopupTime = 0;
        public bool FadeAwayPopup = false;

        public InteractablePopupBox(GameplayText text, Vector2 boxPosition, Texture2D boxTexture)
        {
            GameplayText = text;
            BoxPosition = boxPosition;
            BoxTexture = boxTexture;
            TextPosition = boxPosition + new Vector2(10, 10);
        }
        public InteractablePopupBox(GameplayText text, Vector2 boxPosition) : this(text, boxPosition, null) { }

        public void Activate(string text)
        {
            Text = "Added " + text + " to inventory";
            Activate();
        }
        public void Activate(string text, GameTime gameTime)
        {
            FadeAwayPopup = true;            
            Text = "Added " + text + " to inventory";
            Activate();
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
            Text = text;
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);   
            GameplayText.Render(spriteBatch);

        }
    }
}
