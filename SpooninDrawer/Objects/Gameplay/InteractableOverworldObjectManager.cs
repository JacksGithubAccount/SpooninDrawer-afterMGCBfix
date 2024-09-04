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
        public InteractableOverworldObject Drawer;
        private const string DrawerTexturePath = "Sprites/Animations/DrawerSpriteSheet";

        private const string DrawerAnimationIdle = "Animations/Drawer/idle";
        private const string DrawerAnimationInteract = "Animations/Drawer/Interact";
        public InteractableOverworldObjectManager()
        {

        }
        public void LoadContent(ContentManager content)
        {
            var DrawerIdleAnim = content.Load<AnimationData>(DrawerAnimationIdle);
            var DrawerInteractAnim = content.Load<AnimationData>(DrawerAnimationInteract);
            Drawer = new InteractableOverworldObject(1, "Drawer", DrawerTexturePath, content.Load<Texture2D>(DrawerTexturePath), DrawerIdleAnim, DrawerInteractAnim, DrawerIdleAnim, DrawerIdleAnim);
            Drawer.Position = new Vector2(0, 0);
            Drawer.zIndex = 99;
            Drawer.Collidable = true;
            Drawer.Activate();
        }
        public void DrawerInteract(Vector2 InteracterDirection)
        {
            Drawer.Interact(InteracterDirection, () => { Drawer.Interact(); });
        }
        public void Update(GameTime gametime)
        {
            Drawer.Update(gametime);
        }
        public void Render(SpriteBatch spriteBatch)
        {
            Drawer.Render(spriteBatch);
        }
    }
}
