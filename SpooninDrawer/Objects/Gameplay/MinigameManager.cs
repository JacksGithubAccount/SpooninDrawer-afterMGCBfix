using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Objects.Minigame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Objects.Gameplay
{
    public class MinigameManager
    {
        public List<MinigameSplashImage> DrawerFrames = new List<MinigameSplashImage>();
        public List<MinigameSplashImage> LeftHandFrames = new List<MinigameSplashImage>();
        public List<MinigameSplashImage> LeftHandonDrawerFrames = new List<MinigameSplashImage>();
        public List<MinigameSplashImage> RightHandFrames = new List<MinigameSplashImage>();
        private int currentDrawerFrame = 0;
        private int currentLeftHandFrame = 0;
        private int currentRightHandFrame = 0;
        private OrthographicCamera camera;
        public const string LeftHand1 = "Minigame/SpooninDrawer/LeftHand1";
        public MinigameManager(Vector2 playerPosition, OrthographicCamera camera, Resolution resolution)
        {
            this.camera = camera;
            DrawerFrames.Add(new MinigameSplashImage(0, resolution));
            DrawerFrames.Add(new MinigameSplashImage(1, resolution));
            DrawerFrames.Add(new MinigameSplashImage(2, resolution));
            DrawerFrames.Add(new MinigameSplashImage(3, resolution));
            DrawerFrames.Add(new MinigameSplashImage(4, resolution));

            LeftHandFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));

            foreach (var drawer in DrawerFrames) {
                drawer.zIndex = 15;
                drawer.Deactivate();
            }
            foreach (var leftHand in LeftHandFrames)
            {
                leftHand.zIndex = 16;
                leftHand.Deactivate();
            }
        }
        private void RefreshFrame(MinigameSplashImage frame)
        {
            frame.Position = camera.Position;
            frame.Activate();
        }
        public void TrueRandomDrawerFrame()
        {
            Random rng = new Random();
            int rngesus = rng.Next(0, DrawerFrames.Count);
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
        public void NextRandomFrame()
        {
            Random rngesus = new Random();
            int rng = rngesus.Next(0, 3);
            switch (rng)
            {
                case 0:
                    RandomLeftHandFrame();
                    break;
                case 1:
                    RandomRightHandFrame();
                    break;
                default:
                    break;
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
        public void RandomLeftHandFrame()
        {
            Random rngesus = new Random();
            int rng = rngesus.Next(0, 2);
            if (rng == 0)
                ForewardLeftHandFrame();
            else if (rng == 1)
                BackwardLeftHandFrame();
        }
        public void RandomLeftHandonDrawerFrame() 
        {
            Random rngesus = new Random();
            int rng = rngesus.Next(0, LeftHandonDrawerFrames.Count);
            DeactivateLeftHand();
            //aligns hand to drawer based on frame
            switch (currentDrawerFrame)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4: 
                    break;
                default:
                    break;
            }
            LeftHandonDrawerFrames[rng].Activate();
        }
        public void RandomRightHandFrame()
        {
            Random rngesus = new Random();
            int rng = rngesus.Next(0, 2);
            if (rng == 0)
                ForewardRightHandFrame();
            else if (rng == 1)
                BackwardRightHandFrame();
        }
        public void StartDrawerFrame()
        {
            DeactivateDrawer();
            RefreshFrame(DrawerFrames[1]);
            StartLeftHandFrame();
            //StartRightHandFrame();
        }
        public void StartLeftHandFrame()
        {
            DeactivateLeftHand();
            RefreshFrame(LeftHandFrames[0]);
        }
        public void StartRightHandFrame()
        {
            DeactivateRightHand();
            RefreshFrame(RightHandFrames[0]);
        }
        public void ForewardDrawerFrame()
        {
            DeactivateDrawer();
            if(currentDrawerFrame < DrawerFrames.Count - 1) 
                currentDrawerFrame++;
            RefreshFrame(DrawerFrames[currentDrawerFrame]);
        }
        public void ForewardLeftHandFrame()
        {
            DeactivateLeftHand();
            if (currentLeftHandFrame < LeftHandFrames.Count - 1)
                currentLeftHandFrame++;
            else if (currentLeftHandFrame >= LeftHandFrames.Count - 1)
            {
                RandomDrawerFrame();
                RandomLeftHandonDrawerFrame();
            }
            RefreshFrame(LeftHandFrames[currentLeftHandFrame]);
        }
        public void ForewardRightHandFrame()
        {
            DeactivateRightHand();
            if (currentRightHandFrame < RightHandFrames.Count - 1)
                currentRightHandFrame++;
            RefreshFrame(RightHandFrames[currentRightHandFrame]);
        }
        public void BackwardDrawerFrame()
        {
            DeactivateDrawer();
            if(currentDrawerFrame > 0)
                currentDrawerFrame--;
            RefreshFrame(DrawerFrames[currentDrawerFrame]);
        }
        public void BackwardLeftHandFrame()
        {
            DeactivateLeftHand();
            if (currentLeftHandFrame > 0)
                currentLeftHandFrame--;
            RefreshFrame(LeftHandFrames[currentLeftHandFrame]);
        }
        public void BackwardRightHandFrame()
        {
            DeactivateRightHand();
            if (currentRightHandFrame > 0)
                currentRightHandFrame--;
            RefreshFrame(RightHandFrames[currentRightHandFrame]);
        }

        public void DeactivateDrawer()
        {
            foreach (var drawer in DrawerFrames)
            {
                drawer.Deactivate();
            }

        }
        public void DeactivateLeftHand()
        {
            foreach (var lefthand in LeftHandFrames)
            {
                lefthand.Deactivate();
            }
            foreach (var lefties in LeftHandonDrawerFrames)
            {
            lefties.Deactivate(); 
            }        
        }
        public void DeactivateRightHand()
        {
            foreach (var righthand in RightHandFrames)
            {
                righthand.Deactivate();
            }
        }        
            public void DeactivateAll()
        {
            DeactivateDrawer();
            DeactivateLeftHand();
            DeactivateRightHand();
        }
    }
}
