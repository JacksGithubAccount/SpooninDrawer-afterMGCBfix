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
        public const string GROUND = "ground";
        public const string BUILDINGS = "buildings";
        public const string OBJECTS = "objects";
        public const string EVENTS = "events";

        public const int TILE_SIZE = 128;

        private OrthographicCamera _camera;
        private Texture2D _groundTexture;
        private Texture2D _buildingTexture;
        private Texture2D _backgroundRectangle;
        private Texture2D _eventRectangle;
        private bool _cameraDrag;
        private GameEditorTileData _draggedTile;
        private int _mouseX;
        private int _mouseY;
        private List<Level> _levels = new List<Level>();

        public Dictionary<string, TextureAtlas> Atlas { get; private set; }
        public Dictionary<string, GameObject> GameObjects { get; private set; }
        public List<GameEditorEvent> LevelEvents { get; private set; }

        public string CurrentElementName { get; set; }
        public string CurrentLayer { get; set; }
        public int CurrentLevel { get; set; }

        public event EventHandler<EventArgs> OnInitialized;
        public event EventHandler<EventSelectedArgs> OnEventSelected;
        public event EventHandler<EventArgs> OnEventDeselected;

        internal IGraphicsDeviceService _graphicsDeviceService;

        protected override void Initialize()
        {
            base.Initialize();
            GraphicsDevice GraphicsDevice = _graphicsDeviceService.GraphicsDevice;
            _backgroundRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _backgroundRectangle.SetData(new[] { Color.CadetBlue });
            var viewportAdapter = new DefaultViewportAdapter(Editor.GraphicsDevice);
            _camera = new OrthographicCamera(viewportAdapter);
            ResetCameraPosition();
            _draggedTile = null;
            OnInitialized(this, EventArgs.Empty);

            //atlas loading stuff
            Atlas = new Dictionary<string, TextureAtlas>();
            var groundTiles = GetGroundTiles();
            var buildingTiles = GetBuildingTiles();
            var objectTiles = GetGameObjects();

            var groundAtlas = new TextureAtlas(GROUND, _groundTexture, groundTiles);

            Atlas.Add(GROUND, groundAtlas);
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

        private void ResetCameraPosition()
        {
            _camera.Position = new Vector2(0, Level.LEVEL_LENGTH * TILE_SIZE - ClientSize.Height);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Middle)
            {
                _cameraDrag = false;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_cameraDrag)
            {
                _camera.Move(new Vector2(_mouseX - e.X, _mouseY - e.Y));
            }
            _mouseX = e.X;
            _mouseY = e.Y;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Middle)
            {
                _cameraDrag = true;
            }
        }
        private Dictionary<string, Rectangle> GetGroundTiles()
        {
            return new Dictionary<string, Rectangle>
            {
                { "sand", new Rectangle(0, 0, 128, 128) },
                { "beach_tm_02_grass", new Rectangle(128, 0, 128, 128) },
                { "beach_tm_02", new Rectangle(256, 0, 128, 128) },
                { "beach_tm_01_grass", new Rectangle(384, 0, 128, 128) },
                { "beach_tm_01", new Rectangle(512, 0, 128, 128) },
                { "beach_tl_grass", new Rectangle(640, 0, 128, 128) },
                { "beach_tl", new Rectangle(768, 0, 128, 128) },
                { "beach_rm_05_grass", new Rectangle(896, 0, 128, 128) },


                { "grass", new Rectangle(0, 128, 128, 128) },
                { "beach_rm_05", new Rectangle(128, 128, 128, 128) },
                { "beach_rm_01-22", new Rectangle(256, 128, 128, 128) },
                { "beach_rm_01_grass-22", new Rectangle(384, 128, 128, 128) },
                { "beach_rm_01_grass", new Rectangle(512, 128, 128, 128) },
                { "beach_r_up_diagonal_grass", new Rectangle(768, 128, 128, 128) },
                { "beach_r_up_diagonal", new Rectangle(896, 128, 128, 128) },

                { "beach_tr_grass", new Rectangle(0, 256, 128, 128) },
                { "beach_rm_04_grass", new Rectangle(128, 256, 128, 128) },
                { "beach_r_down_diagonal_neighbour_grass", new Rectangle(256, 256, 128, 128) },
                { "beach_lm_04_grass", new Rectangle(384, 256, 128, 128) },
                { "beach_lm_04", new Rectangle(512, 256, 128, 128) },
                { "beach_lm_03_grass", new Rectangle(640, 256, 128, 128) },
                { "beach_lm_03", new Rectangle(768, 256, 128, 128) },
                { "beach_lm_02_grass", new Rectangle(896, 256, 128, 128) },

                { "beach_tr", new Rectangle(0, 384, 128, 128) },
                { "beach_rm_04", new Rectangle(128, 384, 128, 128) },
                { "beach_r_down_diagonal_neighbour", new Rectangle(256, 384, 128, 128) },
                { "beach_lm_02", new Rectangle(384, 384, 128, 128) },
                { "beach_l_up_diagonal_grass", new Rectangle(512, 384, 128, 128) },
                { "beach_l_up_diagonal", new Rectangle(640, 384, 128, 128) },
                { "beach_br_grass", new Rectangle(768, 384, 128, 128) },
                { "beach_br", new Rectangle(896, 384, 128, 128) },

                { "beach_tm_04_grass", new Rectangle(0, 512, 128, 128) },
                { "beach_rm_03_grass", new Rectangle(128, 512, 128, 128) },
                { "beach_r_down_diagonal_grass", new Rectangle(256, 512, 128, 128) },
                { "beach_lm_01_grass", new Rectangle(384, 512, 128, 128) },
                { "beach_bm_05_grass", new Rectangle(512, 512, 128, 128) },
                { "beach_bm_03_grass", new Rectangle(640, 512, 128, 128) },
                { "beach_bm_03", new Rectangle(768, 512, 128, 128) },
                { "beach_bm_02_grass", new Rectangle(896, 512, 128, 128) },

                { "beach_tm_04", new Rectangle(0, 640, 128, 128) },
                { "beach_rm_03", new Rectangle(128, 640, 128, 128) },
                { "beach_r_down_diagonal", new Rectangle(256, 640, 128, 128) },
                { "beach_lm_01", new Rectangle(384, 640, 128, 128) },
                { "beach_bm_05", new Rectangle(512, 640, 128, 128) },
                { "beach_bm_02", new Rectangle(640, 640, 128, 128) },
                { "beach_bl_grass", new Rectangle(768, 640, 128, 128) },
                { "beach_bl", new Rectangle(896, 640, 128, 128) },

                { "beach_tm_03_grass", new Rectangle(0, 768, 128, 128) },
                { "beach_rm_02_grass", new Rectangle(128, 768, 128, 128) },
                { "beach_r_diagonal_neighbour_grass", new Rectangle(256, 768, 128, 128) },
                { "beach_l_up_diagonal_neighbour_grass", new Rectangle(384, 768, 128, 128) },
                { "beach_bm_04_grass", new Rectangle(512, 768, 128, 128) },
                { "beach_bm_01_grass", new Rectangle(640, 768, 128, 128) },

                { "beach_tm_03", new Rectangle(0, 896, 128, 128) },
                { "beach_rm_02", new Rectangle(128, 896, 128, 128) },
                { "beach_r_diagonal_neighbour", new Rectangle(256, 896, 128, 128) },
                { "beach_l_up_diagonal_neighbour", new Rectangle(384, 896, 128, 128) },
                { "beach_bm_04", new Rectangle(512, 896, 128, 128) },
                { "beach_bm_01", new Rectangle(640, 896, 128, 128) },
            };
        }
    }
}