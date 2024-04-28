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
        public MousePositionHandler(MouseState mouseState, BaseScreen screen)
        {
            this.mouseState = mouseState;
            this.screen = screen;
        }
        public void SetScreen(BaseScreen screen)
        {
            this.screen = screen;
        }
        public bool IsMouseOverButton()
        {
            for (int i = 0; i < screen.ButtonRectangles.Length; i++)
            {
                foreach (Rectangle rect in screen.ButtonRectangles[i])
                {
                    return rect.Intersects(mouseState.Position);
                };
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
                        return new Vector2(i, j);
                    }                        
                };
            }            
            return new Vector2(0,0);
        }
    }
}
