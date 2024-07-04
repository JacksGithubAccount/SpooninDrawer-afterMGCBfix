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
        public GameplayText GameplayText {  get; set; }
        public string Text { get { return GameplayText.Text; } set { GameplayText.Text = value; } }
        public Vector2 TextPosition { get { return GameplayText.Position; } set { GameplayText.Position = value; } }
        public Vector2 BoxPosition { get { return Position; } set { Position = value;  } }
        public Texture2D BoxTexture { get { return _texture; } set { _texture = value; } }
    }
}
