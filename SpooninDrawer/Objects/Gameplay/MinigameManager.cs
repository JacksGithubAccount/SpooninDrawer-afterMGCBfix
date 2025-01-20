using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Minigame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class MinigameManager
    {
        public List<Drawer> DrawerFrames = new List<Drawer>();
        public MinigameManager(Vector2 playerPosition, OrthographicCamera camera, Resolution resolution) 
        {
            DrawerFrames.Add(new Drawer(1, resolution));
            DrawerFrames.Add(new Drawer(2, resolution));
            DrawerFrames.Add(new Drawer(3, resolution));

            foreach (var drawer in DrawerFrames) {
                drawer.zIndex = 15;
                drawer.Deactivate();
            }
        }
        public void Activate()
        {
            Random rng = new Random();
            int rngesus = rng.Next(0,DrawerFrames.Count);
            for (int i = 0; i < DrawerFrames.Count; i++) {
                if (i == rngesus) 
                {
                    DrawerFrames[i].Activate();
                }
                else
                {
                    DrawerFrames[i].Deactivate();
                }
            }
        }
    }
}
