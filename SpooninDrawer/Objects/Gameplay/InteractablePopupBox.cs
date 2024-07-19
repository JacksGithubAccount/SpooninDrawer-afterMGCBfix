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
            base.Activate();
            GameplayText.Activate();
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
        }
    }
}
