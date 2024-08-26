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
        public InteractableOverworldObjectManager() 
        {
            InteractableOverworldObject Drawer = new InteractableOverworldObject(1, "Drawer");
        }
        public void LoadContent(ContentManager content)
        {
            item.InventoryTexture2D = content.Load<Texture2D>(item.InventoryTexturePath);
            item.OverworldTexture2D = content.Load<Texture2D>(item.OverworldTexturePath);
            item.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(item.OverworldPosition.X, item.OverworldPosition.Y), item.Width, item.Height));

        }
    }
}
