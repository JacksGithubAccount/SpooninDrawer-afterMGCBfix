using Microsoft.Xna.Framework.Content;
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
            inventoryItem.id = 0;
            inventoryItem.name = "Spoon";
            inventoryItem.description = "Spoon";
            inventoryItem.type = ItemType.KeyItem;

        }
        public void LoadContent(ContentManager content)
        {

        }
        public void LoadOverworldItem(OverworldItem item)
        {

        }
    }
}
