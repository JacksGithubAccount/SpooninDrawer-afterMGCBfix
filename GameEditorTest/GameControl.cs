using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameEditor
{
    public class GameControl : MonoGameControl
    {
        private Texture2D _backgroundRectangle;
        private OrthographicCamera _camera;
        private bool _cameraDrag;
        protected override void Initialize()
        {
            base.Initialize();
            _backgroundRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _backgroundRectangle.SetData(new[] { Color.CadetBlue });
            var viewportAdapter = new DefaultViewportAdapter(Editor.graphics);
            _camera = new OrthographicCamera(viewportAdapter);
            ResetCameraPosition();
            _draggedTile = null;
            OnInitialized(this, EventArgs.Empty);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw()
        {
            var tower = Editor.Content.Load<Texture2D>(@"Sprites\Tower");
            Editor.spriteBatch.Draw(
             tower, new Rectangle(0, 0, tower.Width, tower.Height),
            Color.White);
            base.Draw();
        }
    }
}