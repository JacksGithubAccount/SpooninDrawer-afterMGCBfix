using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractableOverworldObjectManager
    {
        private List<InteractableOverworldObject> interactableOverworldObjects = new List<InteractableOverworldObject>();
        InteractableOverworldObject Drawer;
        public InteractableOverworldObjectManager()
        {
            
        }
        public void LoadContent(ContentManager content)
        {
            Drawer = new InteractableOverworldObject(1, "Drawer", "OverworldObjects/Drawer", content.Load<Texture2D>(Drawer.TexturePath));
            Drawer.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(Drawer.Position.X, Drawer.Position.Y), Drawer.Width, Drawer.Height));
        }
    }
}
