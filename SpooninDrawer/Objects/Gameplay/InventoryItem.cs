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
    public class InventoryItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public ItemType type { get; set; }
        public string description { get; set; }
    }
}
