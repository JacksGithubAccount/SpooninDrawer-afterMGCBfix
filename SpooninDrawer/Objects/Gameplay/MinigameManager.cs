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
        private int currentDrawerFrame = 0;
        private OrthographicCamera camera;
        public MinigameManager(Vector2 playerPosition, OrthographicCamera camera, Resolution resolution) 
        {
            this.camera = camera;
            DrawerFrames.Add(new Drawer(0, resolution));
            DrawerFrames.Add(new Drawer(1, resolution));
            DrawerFrames.Add(new Drawer(2, resolution));
            DrawerFrames.Add(new Drawer(3, resolution));
            DrawerFrames.Add(new Drawer(4, resolution));

            foreach (var drawer in DrawerFrames) {
                drawer.zIndex = 15;
                drawer.Deactivate();
            }
        }
        public void TrueRandomDrawerFrame()
        {
            Random rng = new Random();
            int rngesus = rng.Next(0,DrawerFrames.Count);
            for (int i = 0; i < DrawerFrames.Count; i++) {
                if (i == rngesus) 
                {
                    DrawerFrames[i].Position = camera.Position;
                    DrawerFrames[i].Activate();
                }
                else
                {
                    DrawerFrames[i].Deactivate();
                }
            }
        }
        public void RandomDrawerFrame()
        {
            Random rngesus = new Random();
            int rng = rngesus.Next(0, 2);
            if (rng == 0)
                ForewardDrawerFrame();
            else if (rng == 1)
                BackwardDrawerFrame();
        }
        public void StartDrawerFrame()
        {
            Deactivate();
            DrawerFrames[1].Position = camera.Position;
            DrawerFrames[1].Activate();
        }
        public void ForewardDrawerFrame()
        {
            Deactivate();
            if(currentDrawerFrame < DrawerFrames.Count - 1) 
                currentDrawerFrame++;
            DrawerFrames[currentDrawerFrame].Position = camera.Position;
            DrawerFrames[currentDrawerFrame].Activate();
        }
        public void BackwardDrawerFrame()
        {
            Deactivate();
            if(currentDrawerFrame > 0)
                currentDrawerFrame--;
            DrawerFrames[currentDrawerFrame].Position = camera.Position;
            DrawerFrames[currentDrawerFrame].Activate();
        }

        public void Deactivate()
        {
            foreach (var drawer in DrawerFrames)
            {
                drawer.Deactivate();
            }
        }
    }
}
