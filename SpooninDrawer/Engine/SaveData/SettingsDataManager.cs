using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpooninDrawer.Engine.Input;
using SpooninDrawer.Engine.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.SaveData
{
    public class SettingsDataManager
    {
        private const string SettingsTextFileName = "Settings.txt";
        private const string ScreenSettingsText = "[ScreenSettings]";
        private const string ResolutionText = "[Resolution]";
        private const string ControlsText = "[Controls]";
        private const string VolumeBGMText = "[VolumeBGM]";
        private const string VolumeSEText = "[VolumeSE]";
        private const char SettingsDelimiterText = ':';
        private const string Keyboard = "Keyboard";
        private const string Mouse = "Mouse";

        public const string FullScreenText = "FullScreen";
        public const string WindowText = "Window";
        public const string BorderlessText = "Borderless";
        private SettingsData data;

        public SettingsDataManager() { }
        public SettingsDataManager(SettingsData data) { this.data = data; }

        public void CreateFile()
        {
            SaveSettingsData();

        }
        public void SaveSettingsData()
        {
            FileStream fs = File.Open(SettingsTextFileName, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(ScreenSettingsText + SettingsDelimiterText + data.ScreenSettingsValue);

                sw.WriteLine(ResolutionText + SettingsDelimiterText + data.ResolutionValue.ToString());

                sw.WriteLine(VolumeBGMText + SettingsDelimiterText + data.VolumeBGMValue);

                sw.WriteLine(VolumeSEText + SettingsDelimiterText + data.VolumeSEValue);

                sw.WriteLine(ControlsText + SettingsDelimiterText);
                foreach (ActionKey key in data.KeyboardControls)
                {
                    sw.WriteLine(Keyboard + SettingsDelimiterText + key.action.ToString() + SettingsDelimiterText + key.key.ToString());
                }
                foreach (ActionClick click in data.MouseControls)
                {
                    sw.WriteLine(Mouse + SettingsDelimiterText + click.action.ToString() + SettingsDelimiterText + click.click.ToString());
                }
            }
        }
        public SettingsData LoadSettingsData(SettingsData data)
        {
            if (File.Exists(SettingsTextFileName))
            {
                string line = "";
                using (StreamReader sr = new StreamReader(SettingsTextFileName))
                {
                    while ((line = sr.ReadLine()) is not null)
                    {
                        string[] splitLine = line.Split(SettingsDelimiterText);
                        if (splitLine[0] == ScreenSettingsText) { data.ScreenSettingsValue = splitLine[1]; }
                        else if (splitLine[0] == ResolutionText)
                        {
                            if (splitLine[1] == Resolution.x1080.ToString())
                            {
                                data.ResolutionValue = Resolution.x1080;
                            }
                            else if (splitLine[1] == Resolution.x720.ToString())
                            {
                                data.ResolutionValue = Resolution.x720;
                            }
                        }
                        else if (splitLine[0] == VolumeBGMText)
                        {
                            try
                            {
                                data.VolumeBGMValue = float.Parse(splitLine[1]);
                            }
                            catch
                            {
                                data.VolumeBGMValue = 0.5f;
                            }
                        }
                        else if (splitLine[0] == VolumeSEText)
                        {
                            try
                            {
                                data.VolumeSEValue = float.Parse(splitLine[1]);
                            }
                            catch
                            {
                                data.VolumeSEValue = 0.5f;
                            }
                        }
                        else
                        {
                            if (splitLine[0] == Keyboard)
                            {
                                foreach (ActionKey key in data.KeyboardControls)
                                {
                                    if (splitLine[1] == key.action.ToString())
                                    {
                                        Enum.TryParse(splitLine[2], false, out key.key);
                                    }
                                }
                            }
                            else if ((splitLine[0] == Mouse))
                            {
                                foreach (ActionClick click in data.MouseControls)
                                {
                                    if (splitLine[1] == click.action.ToString())
                                    {
                                        Enum.TryParse(splitLine[2], false, out click.click);
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return data;
        }
        public bool DoesSettingsDataTextExist()
        {
            return File.Exists(SettingsTextFileName);

        }
    }
}
