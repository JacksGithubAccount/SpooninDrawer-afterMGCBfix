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
    public enum MinigameState
    {
        Normal,
        DrawerTooIn,
        DrawerTooOut,
        ArmInWay,
        DrawerInWay,
        SpoonInDrawer
    }
    public class MinigameManager
    {
        public MinigameState currentState;
        public List<List<MinigameSplashImage>> DrawerFrames = new List<List<MinigameSplashImage>>();
        public List<MinigameSplashImage> LeftHandFrames = new List<MinigameSplashImage>();
        public List<MinigameSplashImage> LeftHandonDrawerFrames = new List<MinigameSplashImage>();
        public List<List<MinigameSplashImage>> RightHandFrames = new List<List<MinigameSplashImage>>();
        public List<MinigameSplashImage> SpoonList = new List<MinigameSplashImage>();
        private int currentDrawerFrame = 0;
        private int currentLeftHandFrame = 0;
        private int currentLeftHandonDrawerFrame = 0;
        private int currentRightHandFrame = 0;
        private OrthographicCamera camera;
        public const string DrawerShelf3 = "Minigame/SpooninDrawer/DrawerShelf31080";
        public const string LeftHand1 = "Minigame/SpooninDrawer/LeftHand1";
        public const string LeftHand2 = "Minigame/SpooninDrawer/LeftHand2";
        public const string LeftHand3 = "Minigame/SpooninDrawer/LeftHand3";
        public const string LeftHand4 = "Minigame/SpooninDrawer/LeftHand4";
        public const string blankTexture = "Minigame/SpooninDrawer/blank";
        public const string RightHand1 = "Minigame/SpooninDrawer/RightHand1";
        public const string RightHand2 = "Minigame/SpooninDrawer/RightHand2";
        public const string RightHand3 = "Minigame/SpooninDrawer/RightHand3";
        public const string RightHand4 = "Minigame/SpooninDrawer/RightHand4";
        public const string RightArm4 = "Minigame/SpooninDrawer/RightArm4";
        public const string RightSpoon4 = "Minigame/SpooninDrawer/RightSpoon4";
        public const string RightHand4Open = "Minigame/SpooninDrawer/RightHand4Open";
        public const string Spoon = "Minigame/SpooninDrawer/Spoon";
        public const string RightHandLeaving = "Minigame/SpooninDrawer/RightHandLeaving";

        private List<MinigameSplashImage> Drawer0List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> Drawer1List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> Drawer2List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> Drawer3List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> Drawer4List = new List<MinigameSplashImage>();
        

        private List<MinigameSplashImage> RightHand1List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> RightHand2List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> RightHand3List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> RightHand4List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> RightHand5List = new List<MinigameSplashImage>();
        private List<MinigameSplashImage> RightHand6List = new List<MinigameSplashImage>();

        private List<Vector2> DrawerOffsetForHand = new List<Vector2>();

        private bool SpoonDropped = false;
        public MinigameManager(Vector2 playerPosition, OrthographicCamera camera, Resolution resolution)
        {
            currentState = MinigameState.Normal;
            this.camera = camera;
            Drawer0List.Add(new MinigameSplashImage(0, resolution));//810,552
            Drawer1List.Add(new MinigameSplashImage(1, resolution));//782,664
            Drawer2List.Add(new MinigameSplashImage(2, resolution));//777,781
            Drawer3List.Add(new MinigameSplashImage(3, resolution));//763,1076
            Drawer3List.Add(new MinigameSplashImage(DrawerShelf3));
            Drawer4List.Add(new MinigameSplashImage(4, resolution));//gone

            DrawerFrames.Add(Drawer0List);
            DrawerFrames.Add(Drawer1List);
            DrawerFrames.Add(Drawer2List);
            DrawerFrames.Add(Drawer3List);
            DrawerFrames.Add(Drawer4List);

            LeftHandFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandFrames.Add(new MinigameSplashImage(blankTexture));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand1));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand2));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand3));
            LeftHandonDrawerFrames.Add(new MinigameSplashImage(LeftHand4));

            SpoonList.Add(new MinigameSplashImage(Spoon));

            RightHand1List.Add(new MinigameSplashImage(RightHand1));
            RightHand2List.Add(new MinigameSplashImage(RightHand2));
            RightHand3List.Add(new MinigameSplashImage(RightHand3));
            RightHand4List.Add(new MinigameSplashImage(RightHand4));
            RightHand4List.Add(new MinigameSplashImage(RightArm4));
            RightHand4List.Add(new MinigameSplashImage(RightSpoon4));
            RightHand5List.Add(new MinigameSplashImage(RightHand4Open));
            RightHand5List.Add(new MinigameSplashImage(RightArm4));
            RightHand6List.Add(new MinigameSplashImage(RightHandLeaving));

            RightHandFrames.Add(RightHand1List);
            RightHandFrames.Add(RightHand2List);
            RightHandFrames.Add(RightHand3List);
            RightHandFrames.Add(RightHand4List);
            RightHandFrames.Add(RightHand5List);
            RightHandFrames.Add(RightHand6List);

            DrawerOffsetForHand.Add(new Vector2(0,0));
            DrawerOffsetForHand.Add(new Vector2(-28, 110));
            DrawerOffsetForHand.Add(new Vector2(-33, 227));
            DrawerOffsetForHand.Add(new Vector2(-50, 522));
            DrawerOffsetForHand.Add(new Vector2(0, 0));

            foreach (var frame in DrawerFrames) {
                foreach (var drawer in frame)
                {
                    drawer.zIndex = 15;
                    drawer.Deactivate();
                }
            }
            //makes the drawer front draw in front of hand
            DrawerFrames[3][1].zIndex = 17;
            foreach (var leftHand in LeftHandFrames)
            {
                leftHand.zIndex = 18;
                leftHand.Deactivate();
            }
            foreach (var leftHand in LeftHandonDrawerFrames)
            {
                leftHand.zIndex = 18;
                leftHand.Deactivate();
            }
            foreach (var right in RightHandFrames)
            {
                foreach (var rightHand in right)
                {
                    rightHand.zIndex = 18;
                    rightHand.Deactivate();
                }
            }
            //sets spoon to draw over hand
            RightHand4List[0].zIndex = 15;
            RightHand4List[2].zIndex = 16;
            RightHand5List[0].zIndex = 15;
            foreach (var spoon in SpoonList)
            {
                spoon.zIndex = 16;
                spoon.Deactivate();
            }
        }
        private void RefreshFrame(MinigameSplashImage frame)
        {
            frame.Position = camera.Position;
            frame.Activate();
        }
        private void RefreshFrame(List<MinigameSplashImage> frameList)
        {
            foreach (var frame in frameList)
            {
                RefreshFrame(frame);
            }
        }
        public void TrueRandomDrawerFrame()
        {
            Random rng = new Random();
            int rngesus = rng.Next(0, DrawerFrames.Count);
            for (int i = 0; i < DrawerFrames.Count; i++) {
                foreach (var frame in DrawerFrames[i])
                {
                    if (i == rngesus)
                    {
                        frame.Position = camera.Position;
                        frame.Activate();
                    }
                    else
                    {
                        frame.Deactivate();
                    }
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
            if(SpoonDropped && currentDrawerFrame == 3)
            {
                RefreshFrame(SpoonList);
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
            foreach (var drawerFrame in DrawerFrames[1])
                RefreshFrame(drawerFrame);
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
            foreach (var frame in RightHandFrames[currentRightHandFrame])
                RefreshFrame(frame);
        }
        public void ForewardDrawerFrame()
        {
            DeactivateDrawer();
            if(currentDrawerFrame < DrawerFrames.Count - 1) 
                currentDrawerFrame++;
            if(currentDrawerFrame == 5)
                currentState = MinigameState.DrawerTooOut;
            else if(currentState == MinigameState.DrawerTooOut)
                currentState = MinigameState.Normal;
            foreach (var frame in DrawerFrames[currentDrawerFrame])
                RefreshFrame(frame);
            if (currentDrawerFrame == 2 || currentDrawerFrame == 4)
                DeactivateSpoon();
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
            {                
                if (currentDrawerFrame != 3 && currentRightHandFrame == 3)
                {
                    //stops right hand from dropping spoon if drawer isnt open
                }
                else
                {
                    currentRightHandFrame++;                    
                }
            }
            foreach (var frame in RightHandFrames[currentRightHandFrame])
                RefreshFrame(frame);
            if(currentRightHandFrame == 4)
            {
                RefreshFrame(SpoonList);
                SpoonDropped = true;
            }
        }
        public void BackwardDrawerFrame()
        {
            DeactivateDrawer();
            if(currentDrawerFrame > 0)
                currentDrawerFrame--;
            foreach (var frame in DrawerFrames[currentDrawerFrame])
                RefreshFrame(frame);
            if(currentDrawerFrame == 2 || currentDrawerFrame == 4)
                DeactivateSpoon();
            if(currentDrawerFrame == 1 && SpoonDropped)
            {
                //spoon is in drawer
            }
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
            {
                if (SpoonDropped && currentDrawerFrame != 3 && currentRightHandFrame <= 4) { }
                else
                    currentRightHandFrame--;
            }
            foreach (var frame in RightHandFrames[currentRightHandFrame])
                RefreshFrame(frame);
            if (currentRightHandFrame == 3 && currentDrawerFrame == 3)
            {
                DeactivateSpoon();
                SpoonDropped = false;
            }
        }

        public void DeactivateDrawer()
        {
            foreach (var frame in DrawerFrames)
            {
                foreach(var drawer in frame)
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
            foreach (var right in RightHandFrames)
            {
                foreach (var righthand in right)
                {
                    righthand.Deactivate();
                }                
            }
        }   
        public void DeactivateSpoon()
        {
            foreach(var spoon in SpoonList)
            {
                spoon.Deactivate();
            }
        }
            public void DeactivateAll()
        {
            DeactivateDrawer();
            DeactivateLeftHand();
            DeactivateRightHand();
            DeactivateSpoon();
        }
    }
}
