using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class ItemManager
    {
        InventoryItem inventoryItem;
        OverworldItem overworldItem;
        public ItemManager()
        {
            inventoryItem.ID = 0;
            inventoryItem.Name = "Spoon";
            inventoryItem.Description = "Spoon";
            inventoryItem.Type = ItemType.KeyItem;
            inventoryItem.TexturePath = "insert path here";

        }
        public void LoadContent(ContentManager content, InventoryItem item)
        {
            item.Texture2D = content.Load<Texture2D>(item.TexturePath);
        }
        public void LoadOverworldItem(OverworldItem item)
        {

        }
    }
}
