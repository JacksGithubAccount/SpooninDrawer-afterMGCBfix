using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class ItemSlot
    {
        public Item item;
        public int quantity;
        public ItemSlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}
