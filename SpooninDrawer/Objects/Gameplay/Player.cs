using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class Player
    {
        public List<ItemSlot> Inventory;
        public Player() { Inventory = new List<ItemSlot>(); } 
    }
}
