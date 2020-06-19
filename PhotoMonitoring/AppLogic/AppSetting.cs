using Newtonsoft.Json;
using PhotoMonitoring.Model;
using PhotoMonitoring.Model.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhotoMonitoring.AppLogic
{
    static class AppSetting
    {

        public static Settings loadSettings()
        {
            //ResultSettings result = new ResultSettings();
            Settings st = new Settings();
            string temp = string.Empty;

            if (File.Exists(Directory.GetCurrentDirectory() + "\\appsettings.json"))
            {

                using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\appsettings.json"))
                {
                    temp = sr.ReadToEnd();
                }
                st = JsonConvert.DeserializeObject<Settings>(temp);

                //result.code = 0;
                //result.info = "";
                //result.settings = st;
            }
            else
            {
                //result.code = -1;
                //result.info = "Файл не найлен";
                //result.settings = null;
                st = null;
            }

            return st;
        }

        public static ResultSettings saveSetting(Settings st)
        {
            ResultSettings result = new ResultSettings();
            string settingsJson = JsonConvert.SerializeObject(st);
            
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\appsettings.json"))
            {
                writer.Write(settingsJson);
            }

            result.code = 0;
            result.info = "";
            result.settings = st;

            return result;
        }


    }
}
