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
        private int id;
        private string name;
        private ItemType type;
        private string description;
    }
}
