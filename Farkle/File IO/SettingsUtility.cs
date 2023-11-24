using Farkle.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Farkle.File_IO
{
    public static class SettingsUtility
    {
        private static readonly string SETTINGS_FILE_NAME = Environment.CurrentDirectory + "/Settings.txt";
        
        public static bool SaveSettings(string settingName, FarkleRuleSet ruleSet)
        {
            try
            {
                SavedFarkleSetting savableFarkleRuleSet = new SavedFarkleSetting()
                {
                    SettingName = settingName,
                    RuleSet = ruleSet
                };

                LoadAllSettings(out List<SavedFarkleSetting> settings);
                if (settings == null)
                    settings = new List<SavedFarkleSetting>();

                if (settings.Any(s => s.SettingName == settingName))
                {
                    var setting = settings.First(s => s.SettingName == settingName);
                    setting.RuleSet = ruleSet;
                }
                else
                {
                    settings.Add(savableFarkleRuleSet);
                }

                string json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(SETTINGS_FILE_NAME, json);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n" + ex);
                return false;
            }
        }

        public static bool LoadAllSettings(out List<SavedFarkleSetting> settings)
        {
            settings = new List<SavedFarkleSetting>();
            try
            {
                bool settingsNewlyCreated = false;  
                if (!File.Exists(SETTINGS_FILE_NAME))
                {
                    File.Create(SETTINGS_FILE_NAME);
                    settingsNewlyCreated = true;
                }

                if (!settingsNewlyCreated)
                {
                    using (FileStream fs = File.OpenRead(SETTINGS_FILE_NAME))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string data = reader.ReadToEnd();
                        settings = JsonConvert.DeserializeObject<List<SavedFarkleSetting>>(data) ?? new List<SavedFarkleSetting>();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n" + ex);
                return false;
            }
        }

        public static bool DeleteSetting(string settingName)
        {
            try
            {
                if (LoadAllSettings(out List<SavedFarkleSetting> settings) && settings != null)
                {
                    settings.RemoveAll(s => s.SettingName == settingName);
                    string json = JsonConvert.SerializeObject(settings);
                    File.WriteAllText(SETTINGS_FILE_NAME, json);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n" + ex);
                return false;
            }
        }
    }
}
