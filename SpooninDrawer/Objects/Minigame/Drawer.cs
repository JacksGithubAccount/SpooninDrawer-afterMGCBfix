using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Minigame
{
    public class Drawer : BaseGameObject
    {
        public const string TexturePathDrawer01080 = "Minigame/SpooninDrawer/drawer01080";
        public const string TexturePathDrawer11080 = "Minigame/SpooninDrawer/drawer11080";
        public const string TexturePathDrawer21080 = "Minigame/SpooninDrawer/drawer21080";
        public const string TexturePathDrawer31080 = "Minigame/SpooninDrawer/drawer31080";
        public const string TexturePathDrawer41080 = "Minigame/SpooninDrawer/drawer41080";
        public const string TexturePathDrawer0720 = "Minigame/SpooninDrawer/drawer0720";
        public const string TexturePathDrawer1720 = "Minigame/SpooninDrawer/drawer1720";
        public const string TexturePathDrawer2720 = "Minigame/SpooninDrawer/drawer2720";
        public const string TexturePathDrawer3720 = "Minigame/SpooninDrawer/drawer3720";
        public const string TexturePathDrawer4720 = "Minigame/SpooninDrawer/drawer4720";
        public string TexturePath;
        private Resolution DisplayResolution;

        public Drawer(int DrawerImageNumber, Resolution resolution)
        {
            Position = new Vector2 (0, 0);
            DisplayResolution = resolution;

            if (DisplayResolution == Resolution.x1080)
            {
                switch (DrawerImageNumber)
                {                    
                    case 0:
                        TexturePath = TexturePathDrawer01080;
                        break;
                    case 1:
                        TexturePath = TexturePathDrawer11080;
                        break;
                    case 2:
                        TexturePath = TexturePathDrawer21080;
                        break;
                    case 3:
                        TexturePath = TexturePathDrawer31080;
                        break;
                    case 4:
                        TexturePath = TexturePathDrawer41080;
                        break;
                    default:
                        break;
                }
                
            }
            else if (DisplayResolution == Resolution.x720)
            {
                switch (DrawerImageNumber)
                {
                    case 0:
                        TexturePath = TexturePathDrawer0720;
                        break;
                    case 1:
                        TexturePath = TexturePathDrawer1720;
                        break;
                    case 2:
                        TexturePath = TexturePathDrawer2720;
                        break;
                    case 3:
                        TexturePath = TexturePathDrawer3720;
                        break;
                    case 4:
                        TexturePath = TexturePathDrawer4720;
                        break;
                    default:
                        break;
                }
            }
        }
        public void SetTexture(Texture2D texture) { 
            _texture = texture;
        }

    }
}
