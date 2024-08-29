using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine2D.PipelineExtensions;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Engine.Objects.Animations;
using Animation = SpooninDrawer.Engine.Objects.Animations.Animation;

namespace SpooninDrawer.Objects.Gameplay
{
    public class InteractableOverworldObject : BaseGameObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TexturePath { get; set; }

        private Animation IdleAnimation = new Animation(false);
        private Animation InteractAnimation = new Animation(false);
        private Animation FinishAnimation = new Animation(false);
        private Animation EndingAnimation = new Animation(false);

        public InteractableOverworldObject(int ID, string Name, string texturePath, Texture2D texture, AnimationData idle, AnimationData interact, AnimationData finish, AnimationData end)
        {
            _texture = texture;
            this.ID = ID;
            this.Name = Name;
            TexturePath = texturePath;
            IdleAnimation = new Animation(idle);
            InteractAnimation = new Animation(interact);
            FinishAnimation = new Animation(finish);
            EndingAnimation = new Animation(end);
        }
    }
}
