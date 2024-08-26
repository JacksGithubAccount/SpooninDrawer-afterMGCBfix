using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpooninDrawer.Engine.Objects;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractableOverworldObject : BaseGameObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }

        public InteractableOverworldObject(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
