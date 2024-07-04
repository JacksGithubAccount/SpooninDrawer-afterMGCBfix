using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    public class Item : BaseGameObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
        public string InventoryTexturePath { get; set; }
        public string OverworldTexturePath { get; set; }
        public Texture2D InventoryTexture2D { get; set; }
        public Texture2D OverworldTexture2D { get { return _texture; } set { _texture = value; } }
        public Vector2 InventoryPosition { get; set; }
        public Vector2 OverworldPosition { get { return _position; } set { _position = value; } }

        public Item(int id, string name, ItemType itemType, string description)
        {
            ID = id;
            Name = name;
            Type = itemType;
            Description = description;
        }
    }
}
