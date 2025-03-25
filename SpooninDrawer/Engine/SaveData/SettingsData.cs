using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.SaveData
{
    public class SettingsData
    {
        public string ScreenSettingsValue = "Window";
        public Resolution ResolutionValue = Resolution.x1080;
        public float VolumeBGMValue = 0.5f;
        public float VolumeSEValue = 0.5f;
        public List<ActionClick> MouseControls = new List<ActionClick>
            {
                new ActionClick(Click.LeftClick, Actions.Confirm),
                new ActionClick(Click.RightClick, Actions.Cancel),
                new ActionClick(Click.MiddleClick, Actions.OpenMenu),
                new ActionClick(Click.None, Actions.MoveUp),
                new ActionClick(Click.None, Actions.MoveDown),
                new ActionClick(Click.None, Actions.MoveLeft),
                new ActionClick(Click.None, Actions.MoveRight),
                new ActionClick(Click.None, Actions.Interact),
                new ActionClick(Click.None, Actions.Attack),
                new ActionClick(Click.None, Actions.OpenMenu),
                new ActionClick(Click.None, Actions.Pause)
            };
        public List<ActionKey> KeyboardControls = new List<ActionKey>
            {
                new ActionKey(Keys.A, Actions.MoveLeft),
                new ActionKey(Keys.D, Actions.MoveRight),
                new ActionKey(Keys.W, Actions.MoveUp),
                new ActionKey(Keys.S, Actions.MoveDown),
                new ActionKey(Keys.Z, Actions.Confirm),
                new ActionKey(Keys.Z, Actions.Interact),
                new ActionKey(Keys.X, Actions.Cancel),
                new ActionKey(Keys.Z, Actions.Attack),
                new ActionKey(Keys.C, Actions.OpenMenu),
                new ActionKey(Keys.P, Actions.Pause)
            };

    }
}
