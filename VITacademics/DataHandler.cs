using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VITacademics
{
    public class DataHandler
    {
        private static IsolatedStorageSettings Settings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
        
        public void saveData(String key, String dat) 
        {
            if (Settings.Contains(key))
                Settings[key] = dat;
            else
                Settings.Add(key, dat);
            Settings.Save();
        }

        public void saveAttendance(String json) {
            json = json.Substring(6);
            saveData("ATTJSON", json);
        }

        public List<Subject> loadSubjects() {
            List<Subject> Temp = new List<Subject>();
            try
            {
                String json = ((string)Settings["ATTJSON"]);
                System.Diagnostics.Debug.WriteLine(json);
                //READ JSON
                JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(json));
                JArray root = JArray.Load(reader);
                foreach (JObject j in root)
                {
                    Subject s = new Subject();
                    s.code = ((string)j["code"]);
                    s.title = ((string)j["title"]);
                    s.type = ((string)j["type"]);
                    s.slot = ((string)j["slot"]);
                    s.attended = ((int)j["attended"]);
                    s.conducted = ((int)j["conducted"]);
                    s.percentage = ((string)j["percentage"]);
                    s.regdate = ((string)j["regdate"]);
                    s.classnbr = ((string)j["classnbr"]);
                    JArray details = (JArray)j["details"];
                    
                    s.attendance = new List<DayByDay>();
                    for (int i = 0; i < details.Count; ) {s.attendance.Add(new DayByDay(details[i].ToString(), details[i+1].ToString())); i += 2; }
                    Temp.Add(s);
                }
            }
            catch (Exception e) { Console.Out.WriteLine(e.Message.ToString()); }
            return Temp;
        }

        public void setCampus(bool isVellore) {
            if (isVellore)
                saveData("isVellore", "1");
            else
                saveData("isVellore", "0");

        }
        public string getCap() {
            try
            {
                return ((string)Settings["CAPTCHA"]);
            }
            catch (Exception e) { return "";}
        }
        public bool isVellore() {
            if ((int)Settings["isVellore"] == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public string getDob() {
            try
            {
            return ((string)Settings["DOB"]);
            }
            catch (Exception e) { return ""; }
        }
        
        public string getReg() {
            try
            {
                return ((string)Settings["REGNO"]);
            }
            catch (Exception e) { return ""; }
        }

        public void saveReg(String reg) {
            saveData("REGNO", reg);
        }

        public void saveDob(String dob) {
            saveData("DOB", dob);
        }
    }
}
