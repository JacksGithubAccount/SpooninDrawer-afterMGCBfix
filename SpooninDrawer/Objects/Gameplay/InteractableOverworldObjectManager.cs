using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects.Animations;
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

        private const string DrawerAnimationIdle = "Animations/Drawer/idle";
        public InteractableOverworldObjectManager()
        {
            
        }
        public void LoadContent(ContentManager content)
        {
            var DrawerIdleAnim = content.Load<AnimationData>(DrawerAnimationIdle);
            Drawer = new InteractableOverworldObject(1, "Drawer", "OverworldObjects/Drawer", content.Load<Texture2D>(Drawer.TexturePath), DrawerIdleAnim, DrawerIdleAnim, DrawerIdleAnim, DrawerIdleAnim);
            Drawer.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(Drawer.Position.X, Drawer.Position.Y), Drawer.Width, Drawer.Height));
        }
    }
}
