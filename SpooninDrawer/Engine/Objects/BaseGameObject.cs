using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpooninDrawer.Engine.States;

namespace SpooninDrawer.Engine.Objects
{
    public class BaseGameObject
    {
        protected Texture2D _texture;
        protected Texture2D _boundingBoxTexture;

        protected Vector2 _position = Vector2.One;
        protected List<BoundingBox> _boundingBoxes = new List<BoundingBox>();

        public int zIndex;
        public event EventHandler<BaseGameStateEvent> OnObjectChanged;

        public bool Destroyed { get; private set; }
        public bool Active { get; protected set; }
        public bool Interactable { get; set; }
        public float Angle { get; set; }
        public Vector2 Direction { get; set; }

        public virtual int Width { get { return _texture.Width; } }
        public virtual int Height { get { return _texture.Height; } }
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                var deltaX = value.X - _position.X;
                var deltaY = value.Y - _position.Y;
                _position = value;

                foreach (var bb in _boundingBoxes)
                {
                    bb.Position = new Vector2(bb.Position.X + deltaX, bb.Position.Y + deltaY);
                }
            }
        }
        public string getTextureName()
        {
            if (_texture != null)
                return _texture.ToString();
            else
                return "Lmao null error";
        }

        public List<BoundingBox> BoundingBoxes
        {
            get
            {
                return _boundingBoxes;
            }
        }
        public BaseGameObject() { Initialize(); }
        public BaseGameObject(Texture2D texture)
        {
            _texture = texture;
            Initialize();
        }

        public virtual void Initialize()
        {
            Angle = 0.0f;
            Direction = new Vector2(0, 0);
            Position = Vector2.One;
            Interactable = false;
        }

        public virtual void OnNotify(BaseGameStateEvent gameEvent) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
        }

        public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (!Active)
            {
                return;
            }
            if (_boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (var bb in _boundingBoxes)
            {
                spriteBatch.Draw(_boundingBoxTexture, bb.Rectangle, Color.Red);
            }
        }

        public void Destroy()
        {
            Destroyed = true;
        }
        public virtual void Activate()
        {
            Active = true;
        }

        public virtual void Deactivate()
        {
            Active = false;
        }

        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }

        public void AddBoundingBox(BoundingBox bb)
        {
            _boundingBoxes.Add(bb);
        }
        protected Vector2 CalculateDirection(float angleOffset = 0.0f)
        {
            Direction = new Vector2((float)Math.Cos(Angle - angleOffset), (float)Math.Sin(Angle - angleOffset));
            Direction.Normalize();

            return Direction;
        }
        private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        {
            _boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            _boundingBoxTexture.SetData<Color>(new Color[] { Color.White });
        }
    }
}
