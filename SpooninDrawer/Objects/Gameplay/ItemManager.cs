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
    public class ItemManager
    {
        Item item;
        List<Item> InteractableItems;
        Item tempItem;
        public ItemManager()
        {
            item = new Item(0, "Spoon", ItemType.KeyItem, "It's a spoon");
            item.InventoryTexturePath = "Items/spoon";
            item.OverworldTexturePath = item.InventoryTexturePath;
            item.OverworldPosition = new Vector2(120, 120);
            item.Interactable = true;
            item.Activate();
            item.zIndex = 5;
            InteractableItems = new List<Item>();

        }
        public void LoadContent(ContentManager content)
        {
            item.InventoryTexture2D = content.Load<Texture2D>(item.InventoryTexturePath);
            item.OverworldTexture2D = content.Load<Texture2D>(item.OverworldTexturePath);
            item.AddBoundingBox(new Engine.Objects.BoundingBox(new Vector2(item.OverworldPosition.X, item.OverworldPosition.Y), item.Width, item.Height));

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
            Item tempItem = InteractableItems[0];
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
        public Item GetItem()
        {
            return item;
        }
        public void AddInteractableItem(Item item)
        {
            if(!InteractableItems.Contains(item))
                InteractableItems.Add(item);
        }
        public void AddInteractableItem(BaseGameObject item)
        {
            try
            {
                if (!InteractableItems.Contains((Item)item))
                    InteractableItems.Add((Item)item);
            }catch { }         
        }
        public void RemoveInteractableItem(Item item)
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
    }
}
