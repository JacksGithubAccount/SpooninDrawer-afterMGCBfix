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
        public string ScreenSettingsValue = "";
        public Resolution ResolutionValue;
        public string VolumeBGMValue = "";
        public string VolumeSEValue = "";
        public List<ActionClick> MouseControls;
        public List<ActionKey> KeyboardControls;
    }
}
