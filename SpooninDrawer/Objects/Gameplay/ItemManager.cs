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
        Item item;
        public ItemManager()
        {
            item.ID = 0;
            item.Name = "Spoon";
            item.Description = "Spoon";
            item.Type = ItemType.KeyItem;
            item.InventoryTexturePath = "Content/Items/Spoon";
            item.OverworldTexturePath = item.InventoryTexturePath;

        }
        public void LoadContent(ContentManager content, Item item)
        {
            item.InventoryTexture2D = content.Load<Texture2D>(item.InventoryTexturePath);
            item.OverworldTexture2D = content.Load<Texture2D>(item.OverworldTexturePath);

        }
    }
}
