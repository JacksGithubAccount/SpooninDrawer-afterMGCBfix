using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractableOverworldObject : BaseGameObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }

        public InteractableOverworldObject(int ID, string Name,string texturePath, Texture2D texture)
        {
            texture = _texture;
            this.ID = ID;
            this.Name = Name;
        }
    }
}
