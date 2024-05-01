using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Objects;
using SpooninDrawer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Input
{
    public class MousePositionHandler
    {
        private MouseState mouseState;
        private BaseScreen screen;
        private Vector2 screenPosition;
        public MousePositionHandler(BaseScreen screen)
        {
            mouseState = Mouse.GetState();
            this.screen = screen;
            screenPosition = new Vector2(0, 0);
        }
        public void SetScreen(BaseScreen screen)
        {
            this.screen = screen;
        }
        public Vector2 GetScreenPosition() { return screenPosition; }
        public bool IsMouseOverButton()
        {
            mouseState = Mouse.GetState();
            if (screen.ButtonRectangles != null)
            {
                for (int i = 0; i < screen.ButtonRectangles.Length; i++)
                {
                    foreach (Rectangle rect in screen.ButtonRectangles[i])
                    {
                        if (rect.Intersects(mouseState.Position))
                            return true;
                    };
                }
            }
            return false;
        }
        public Vector2 GetButtonUnderMouse()
        {
            for (int i = 0; i < screen.ButtonRectangles.Length; i++) 
            {
                foreach (Rectangle rect in screen.ButtonRectangles[i])
                {
                    if (rect.Intersects(mouseState.Position))
                    {
                        int j = Array.FindIndex(screen.ButtonRectangles[i], x => x == rect);
                        return screenPosition = new Vector2(i, j);
                    }                        
                };
            }            
            return screenPosition;
        }
    }
}
