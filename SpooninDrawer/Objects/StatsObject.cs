﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

namespace SpooninDrawer.Objects
{
    public class StatsObject : BaseTextObject
    {
        public const int ROLLING_SIZE = 60;
        private Queue<float> _rollingFPS = new Queue<float>();
        public float FPS { get; set; }
        public float MinFPS { get; private set; }
        public float MaxFPS { get; private set; }
        public float AverageFPS { get; private set; }
        public bool IsRunningSlowly { get; set; }
        public int NbUpdateCalled { get; set; }
        public int NbDrawCalled { get; set; }
        public double CurrentTotalTimeinSeconds { get; set; }
        public StatsObject(SpriteFont font) : base(font)
        {
            Text = "";
            _font = font;
            NbUpdateCalled = 0;
            NbDrawCalled = 0;
            zIndex = 99;
            Activate();
            
        }
        public void Update(GameTime gameTime, OrthographicCamera camera)
        {
            Position = camera.Position;
            CurrentTotalTimeinSeconds = gameTime.TotalGameTime.TotalSeconds;
            NbUpdateCalled++;
            FPS = 1.0f / (float)gameTime.ElapsedGameTime.
           TotalSeconds;
            _rollingFPS.Enqueue(FPS);

            if (_rollingFPS.Count > ROLLING_SIZE)
            {
                _rollingFPS.Dequeue();
                var sum = 0.0f;
                MaxFPS = int.MinValue;
                MinFPS = int.MaxValue;
                foreach (var fps in _rollingFPS.ToArray())
                {
                    sum += fps;
                    if (fps > MaxFPS)
                    {
                        MaxFPS = fps;
                    }
                    if (fps < MinFPS)
                    {
                        MinFPS = fps;
                    }
                }
                AverageFPS = sum / _rollingFPS.Count;
            }
            else
            {
                AverageFPS = FPS;
                MinFPS = FPS;
                MaxFPS = FPS;
            }
            Text = $"FPS: {FPS} \n" + 
                $"AvgFPS: {AverageFPS} \n" + 
                $"Running Slowly: {IsRunningSlowly}  \n" + 
                $"Nb Updates: {NbUpdateCalled}  \n" + 
                $"Nb Draws: {NbDrawCalled}  \n" + 
                $"Game Time since last update: {gameTime.ElapsedGameTime.TotalSeconds} \n" + 
                $"Total Game Time: {gameTime.TotalGameTime} \n" +
                $"Total Gameplay Time: {gameTime.TotalGameTime.TotalSeconds} \n"                ;
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            NbDrawCalled++;
            base.Render(spriteBatch);
        }
    }
}
