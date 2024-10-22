using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    enum ObjectDirection
    {
        Up, Down, Left, Right, None
    }
    public class InteractableManager
    {
        Item item;
        List<BaseGameObject> InteractableItems;
        Item tempItem;

        public InteractableOverworldObject Drawer;
        private const string DrawerTexturePath = "Sprites/Animations/DrawerSpriteSheet";

        private const string DrawerAnimationIdle = "Animations/Drawer/idle";
        private const string DrawerAnimationInteract = "Animations/Drawer/Interact";

        public InteractableManager()
        {
            item = new Item(0, "Spoon", ItemType.KeyItem, "It's a spoon");
            item.InventoryTexturePath = "Items/spoon";
            item.OverworldTexturePath = item.InventoryTexturePath;
            item.OverworldPosition = new Vector2(120, 120);
            item.Interactable = true;
            item.Activate();
            item.zIndex = 5;
            InteractableItems = new List<BaseGameObject>();

        }
        public void LoadContent(ContentManager content)
        {
            item.InventoryTexture2D = content.Load<Texture2D>(item.InventoryTexturePath);
            item.OverworldTexture2D = content.Load<Texture2D>(item.OverworldTexturePath);
            item.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(item.OverworldPosition.X, item.OverworldPosition.Y), item.Width, item.Height));

            var DrawerIdleAnim = content.Load<AnimationData>(DrawerAnimationIdle);
            var DrawerInteractAnim = content.Load<AnimationData>(DrawerAnimationInteract);
            Drawer = new InteractableOverworldObject(1, "Drawer", DrawerTexturePath, content.Load<Texture2D>(DrawerTexturePath), new Vector2(200, 200), DrawerIdleAnim, DrawerInteractAnim, DrawerIdleAnim, DrawerIdleAnim);
            Drawer.setInteractions(() => { Drawer.Interact(); }, () => { Drawer.Interact(); }, () => { Drawer.Interact(); }, () => { Drawer.Interact(); });
            Drawer.zIndex = 99;
            Drawer.Collidable = true;
            Drawer.Interactable = true;
            Drawer.Activate();
        }
        public void AddToInventory(Item item, int amount, Player player)
        {
            if (player.Inventory.Exists(x => (x.item == item)))
            {
                player.Inventory.Find(x => (x.item == item)).quantity += amount;
            }
            else
            {
                player.Inventory.Add(new ItemSlot(item, amount));
            }

        }
        public Item AddToInventory(Player player)
        {
            if (InteractableItems[0].GetType() == typeof(Item))
            {
                Item tempItem = (Item)InteractableItems[0];
                if (player.Inventory.Exists(x => (x.item == tempItem)))
                {
                    player.Inventory.Find(x => (x.item == tempItem)).quantity += 1;
                }
                else
                {
                    player.Inventory.Add(new ItemSlot(tempItem, 1));
                }
                return tempItem;
            }
            else
                return null;
        }
        public Item GetItem()
        {
            return item;
        }
        public BaseGameObject GetInteractable()
        {
            if (!IsInteractableEmpty())
            {
                return InteractableItems[0];
            }
            else
                return null;
        }
        public void AddInteractableItem(BaseGameObject item)
        {
            if (item.GetType() == typeof(Item))
            {
                try
                {
                    if (!InteractableItems.Contains((Item)item))
                        InteractableItems.Add((Item)item);
                }
                catch { }
            }
            else if (item.GetType() == typeof(InteractableOverworldObject))
            {
                if (!InteractableItems.Contains((InteractableOverworldObject)item))
                    InteractableItems.Add((InteractableOverworldObject)item);
            }
        }
        public void RemoveInteractableItem(BaseGameObject item)
        {
            item.Deactivate();
            InteractableItems.Remove(item);
        }
        public void ClearInteractables()
        {

            InteractableItems.Clear();
        }
        public bool IsInteractableEmpty()
        {
            return InteractableItems.Count == 0;
        }
        public void InteractWithObject(Vector2 InteracterPosition)
        {
            ObjectDirection objectDirection = GetInteractionDirection(InteracterPosition);
            var temp = (InteractableOverworldObject)InteractableItems[0];
            if (objectDirection == ObjectDirection.Down)
            {
                temp.Interact();
            }
        }
        private ObjectDirection GetInteractionDirection(Vector2 InteractorPosition)
        {
            if (InteractorPosition.X > InteractableItems[0].Position.X && InteractorPosition.X < InteractableItems[0].Position.X + InteractableItems[0].Width)
            {
                if (InteractorPosition.Y < InteractableItems[0].Position.Y)
                {
                    //above
                    return ObjectDirection.Up;
                }
                else if (InteractorPosition.Y > InteractableItems[0].Position.Y + InteractableItems[0].Height)
                {
                    //below
                    return ObjectDirection.Down;
                }

            }
            else if (InteractorPosition.Y > InteractableItems[0].Position.Y && InteractorPosition.Y < InteractableItems[0].Position.Y + InteractableItems[0].Height)
            {
                if (InteractorPosition.X < InteractableItems[0].Position.X)
                {
                    //left
                    return ObjectDirection.Left;
                }
                else if (InteractorPosition.X > InteractableItems[0].Position.X + InteractableItems[0].Width)
                {
                    //right
                    return ObjectDirection.Right;
                }
            }

            return ObjectDirection.None;
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
