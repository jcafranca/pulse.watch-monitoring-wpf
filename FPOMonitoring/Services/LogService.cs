using Newtonsoft.Json;
using SweetAlertSharp.Enums;
using SweetAlertSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FPOMonitoring.Data.Class;

namespace FPOMonitoring.Services
{
    public class LogService
    {
        public string SettingPath
        {
            get
            {
                var folder_path = $"{AppDomain.CurrentDomain.BaseDirectory}AppFiles";
                if (!System.IO.Directory.Exists(folder_path)) System.IO.Directory.CreateDirectory(folder_path);
                return System.IO.Path.Combine(folder_path, "Settings.json");
            }
        }
        public void ErrorLogToFile(string ErrorMsg, string StackTraceMsg, string errWhere)
        {
            try
            {
                string pth = System.AppDomain.CurrentDomain.BaseDirectory + "\\AppLog\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                if (!Directory.Exists(pth))
                {
                    Directory.CreateDirectory(pth);
                }

                if (!File.Exists(pth + "Log.info"))
                {
                    using (StreamWriter sWriter = File.CreateText(pth + "Log.info"))
                    {
                        sWriter.WriteLine("Time             : " + DateTime.Now.ToString("HH:mm:ss"));
                        sWriter.WriteLine("Error            : " + ErrorMsg.Trim('\0', ' ', '\n', '\r'));
                        sWriter.WriteLine("StackTrace       : " + StackTraceMsg.Trim('\0', ' ', '\n', '\r'));
                        sWriter.WriteLine("Error Occurred In : " + errWhere);
                        sWriter.WriteLine();
                        sWriter.WriteLine();
                    }
                }
                else
                {
                    using (StreamWriter sWriter = File.AppendText(pth + "Log.info"))
                    {
                        sWriter.WriteLine("Time             : " + DateTime.Now.ToString("HH:mm:ss"));
                        sWriter.WriteLine("Error            : " + ErrorMsg.Trim('\0', ' ', '\n', '\r'));
                        sWriter.WriteLine("StackTrace       : " + StackTraceMsg.Trim('\0', ' ', '\n', '\r'));
                        sWriter.WriteLine("Error Occurred In : " + errWhere);
                        sWriter.WriteLine();
                        sWriter.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it to another location if needed.
            }
        }
        public void Serialize(string path, object any)
        {
            if (any != null)
            {
                if (any is String s)
                {
                    var json = JsonConvert.SerializeObject(s);
                    System.IO.File.WriteAllText(path, json);
                } else if(any is DatabaseSettings d)
                {
                    var json = JsonConvert.SerializeObject(d);
                    System.IO.File.WriteAllText(path, json);
                } else if (any is TimezoneInfo tz)
                {
                    var json = JsonConvert.SerializeObject(tz);
                    System.IO.File.WriteAllText(path, json);
                }
            }
        }
        public object Deserialize(string path) 
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return (string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<object>(json)) ?? null;
            }
            return null;
        }

        public DatabaseSettings DeserializeSetting(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return (string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<DatabaseSettings>(json)) ?? null;
            }
            return null;
        }
    }
}
