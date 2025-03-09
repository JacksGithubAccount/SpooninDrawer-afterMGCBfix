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
        public Item Spoon;
        List<BaseGameObject> InteractableItems;
        Item tempItem;

        public InteractableOverworldObject Drawer;
        private const string DrawerTexturePath = "Sprites/Animations/DrawerSpriteSheet";

        private const string DrawerAnimationIdle = "Animations/Drawer/idle";
        private const string DrawerAnimationInteract = "Animations/Drawer/Interact";

        public InteractableOverworldObject BlueGuyRoy;

        private const string BlueGuyRoyTexturePath = "background/NPC1";

        public CollidableGameObject Table;
        public CollidableGameObject Chair1;
        public CollidableGameObject Chair2;

        private const string TableTexturePath = "background/Table";
        private const string ChairTexturePath = "background/Chair";

        public InteractableManager()
        {
            Spoon = new Item(0, "Spoon", ItemType.KeyItem, "It's a spoon");
            Spoon.InventoryTexturePath = "Items/spoon";
            Spoon.OverworldTexturePath = Spoon.InventoryTexturePath;
            Spoon.OverworldPosition = new Vector2(710, 1050);
            Spoon.Interactable = true;
            Spoon.Activate();
            Spoon.zIndex = 7;
            InteractableItems = new List<BaseGameObject>();
        }
        public void LoadContent(ContentManager content)
        {
            Spoon.InventoryTexture2D = content.Load<Texture2D>(Spoon.InventoryTexturePath);
            Spoon.OverworldTexture2D = content.Load<Texture2D>(Spoon.OverworldTexturePath);
            Spoon.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(Spoon.OverworldPosition.X, Spoon.OverworldPosition.Y), Spoon.Width, Spoon.Height));

            var DrawerIdleAnim = content.Load<AnimationData>(DrawerAnimationIdle);
            var DrawerInteractAnim = content.Load<AnimationData>(DrawerAnimationInteract);
            Drawer = new InteractableOverworldObject(1, "Drawer", DrawerTexturePath, content.Load<Texture2D>(DrawerTexturePath), new Vector2(2750, 600), DrawerIdleAnim, DrawerInteractAnim, DrawerIdleAnim, DrawerIdleAnim);
            Drawer.setInteractions(() => { Drawer.Interact(); }, () => { Drawer.Interact(); }, () => { Drawer.Interact(); }, () => { Drawer.Interact(); });
            Drawer.zIndex = 5;
            Drawer.Collidable = true;
            Drawer.Interactable = true;
            Drawer.Activate();

            Texture2D TableTexture = content.Load<Texture2D>(TableTexturePath);
            Table = new CollidableGameObject(new Rectangle(70, 190, 300, 60), TableTexture);
            Table.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(50, 250), 230, 220));
            Table.Position = new Vector2(500, 1000);            
            Table.zIndex = 5;
            Table.Activate();

            Texture2D ChairTexture = content.Load<Texture2D>(ChairTexturePath);
            Chair1 = new CollidableGameObject(new Rectangle(22,63,28, 172), ChairTexture);
            Chair1.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(22, 185), 113, 50));
            Chair1.Position = new Vector2(500, 1050);
            Chair1.zIndex = 4;
            Chair1.Activate();
            Chair2 = new CollidableGameObject(new Rectangle(22, 63, 28, 172), ChairTexture);
            Chair2.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(22, 185), 113, 50));
            Chair2.Position = new Vector2(450, 1200);
            Chair2.zIndex = 4;
            Chair2.Activate();
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
            return Spoon;
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
            var interactable = (InteractableOverworldObject)InteractableItems[0];
            if (objectDirection == ObjectDirection.Left && interactable.State[0])
            {
                interactable.Interact();
            }
            if (interactable.State[1])
            {

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
            //Drawer.Render(spriteBatch);
        }
    }
}
