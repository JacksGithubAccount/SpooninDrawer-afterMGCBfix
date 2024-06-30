using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public enum ItemType
    {
        KeyItem,
        Consumables,
        Materials,
        Weapon,
        Armor,
        Accessories
    }
    public interface InventoryItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
        public string TexturePath { get; set; }
        public Texture2D Texture2D { get; set; }
    }
}
