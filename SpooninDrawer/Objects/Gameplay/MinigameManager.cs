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
        private int currentLeftHandonDrawerFrame = 0;
        private int currentRightHandFrame = 0;
        private OrthographicCamera camera;
        public const string LeftHand1 = "Minigame/SpooninDrawer/LeftHand1";
        public const string LeftHand2 = "Minigame/SpooninDrawer/LeftHand2";
        public const string LeftHand3 = "Minigame/SpooninDrawer/LeftHand3";
        public const string LeftHand4 = "Minigame/SpooninDrawer/LeftHand4";
        public const string blankTexture = "Minigame/SpooninDrawer/blank";
        public const string RightHand1 = "Minigame/SpooninDrawer/RightHand1";
        public const string RightHand2 = "Minigame/SpooninDrawer/RightHand2";
        private List<Vector2> DrawerOffsetForHand = new List<Vector2>();
        public MinigameManager(Vector2 playerPosition, OrthographicCamera camera, Resolution resolution)
        {
            this.camera = camera;
            DrawerFrames.Add(new MinigameSplashImage(0, resolution));//810,552
            DrawerFrames.Add(new MinigameSplashImage(1, resolution));//782,664
            DrawerFrames.Add(new MinigameSplashImage(2, resolution));//777,781
            DrawerFrames.Add(new MinigameSplashImage(3, resolution));//763,1076
            DrawerFrames.Add(new MinigameSplashImage(4, resolution));//gone

            LeftHandFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandFrames.Add(new MinigameSplashImage(blankTexture));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand2));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand3));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand4));

            RightHandFrames.Add(new MinigameSplashImage(RightHand1));
            RightHandFrames.Add(new MinigameSplashImage(RightHand2));

            DrawerOffsetForHand.Add(new Vector2(0,0));
            DrawerOffsetForHand.Add(new Vector2(-28, 110));
            DrawerOffsetForHand.Add(new Vector2(-33, 227));
            DrawerOffsetForHand.Add(new Vector2(-50, 522));
            DrawerOffsetForHand.Add(new Vector2(0, 0));

            foreach (var drawer in DrawerFrames) {
                drawer.zIndex = 15;
                drawer.Deactivate();
            }
            foreach (var leftHand in LeftHandFrames)
            {
                leftHand.zIndex = 16;
                leftHand.Deactivate();
            }
            foreach (var leftHand in LeftHandonDrawerFrames)
            {
                leftHand.zIndex = 16;
                leftHand.Deactivate();
            }
            foreach (var rightHand in RightHandFrames)
            {
                rightHand.zIndex = 16;
                rightHand.Deactivate();
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
            //if (rng == 0)
            //{
            //    BackwardLeftHandFrame();
            //}
            //else if (rng == 1)
            //{
            ForewardLeftHandFrame();
            rng = rngesus.Next(0, 2);
            if (rng == 0)
            {
                ForewardDrawerFrame();
                //hides left hand when drawer is at highest frame
                if (currentDrawerFrame >= DrawerFrames.Count - 1)
                {
                    DeactivateLeftHand();
                }

            }
            else if (rng == 1)
            {

                BackwardDrawerFrame();
            }

            LeftHandonDrawerFrames[currentLeftHandonDrawerFrame].Position += DrawerOffsetForHand[currentDrawerFrame];
            //}
        }
        public void RandomLeftHandonDrawerFrame() 
        {
            Random rngesus = new Random();
            currentLeftHandonDrawerFrame = rngesus.Next(0, LeftHandonDrawerFrames.Count);
            DeactivateLeftHand();
            LeftHandonDrawerFrames[currentLeftHandonDrawerFrame].Activate();
            RefreshFrame(LeftHandonDrawerFrames[currentLeftHandonDrawerFrame]);
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
            currentDrawerFrame = 1;
            StartLeftHandFrame();           
            StartRightHandFrame();           
        }
        public void StartLeftHandFrame()
        {
            DeactivateLeftHand();
            currentLeftHandFrame = 0;
            RefreshFrame(LeftHandFrames[currentLeftHandFrame]);
            LeftHandFrames[currentLeftHandFrame].Position += DrawerOffsetForHand[currentDrawerFrame];
        }
        public void StartRightHandFrame()
        {
            DeactivateRightHand();
            currentRightHandFrame = 0;
            RefreshFrame(RightHandFrames[currentRightHandFrame]);
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
            if (currentLeftHandFrame < LeftHandFrames.Count - 1) { 
                currentLeftHandFrame++;}
            if (currentLeftHandFrame >= LeftHandFrames.Count - 1)
                RandomLeftHandonDrawerFrame();        
            
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
            DeactivateLeftHandonDrawer();
        }
        public void DeactivateLeftHandonDrawer()
        {
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
