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
        public Vector2 TextPosition { get { return GameplayText.Position; } set { GameplayText.Position = value; } }
        public Vector2 BoxPosition { get { return Position; } set { Position = value; } }
        public Texture2D BoxTexture { get { return _texture; } set { _texture = value; } }
        public const string TexturePath = "Menu/InteractPopupBox";
        public double FadeAwayTime = 3;
        private double PopupTime = 0;
        private bool FadeAwayPopup = false;
        private GameTime gameTime;

        public InteractablePopupBox(GameplayText text, Vector2 boxPosition, Texture2D boxTexture, GameTime gameTime)
        {
            GameplayText = text;
            BoxPosition = boxPosition;
            BoxTexture = boxTexture;
            TextPosition = boxPosition + new Vector2(10, 10);
            this.gameTime = gameTime;
        }
        public InteractablePopupBox(GameplayText text, Vector2 boxPosition, GameTime gameTime) : this(text, boxPosition, null, gameTime) { }

        public void Activate(string text)
        {
            FadeAwayPopup = true;
            PopupTime = gameTime.TotalGameTime.TotalSeconds;
            Text = "Added " + text + " to inventory" + PopupTime;
            Activate();
        }
        public override void Activate()
        {
            base.Activate();
            GameplayText.Activate();
        }
        public override void Deactivate()
        {
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
            if (FadeAwayPopup & gameTime.TotalGameTime.TotalSeconds > PopupTime + FadeAwayTime) 
            { 
                Deactivate();
            }
        }
    }
}
