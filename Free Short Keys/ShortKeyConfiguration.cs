using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Free_Short_Keys
{
    public static class ShortKeyConfiguration
    {
        private const string DefaultConfigurationFileName = "default-configuration.json";
        private const string CategoryFileExtension = "-category.json";

        private static Keylogger keylogger = null;

        public static List<ShortKey> GetShortKeys()
        {
            List<ShortKey> result = new List<ShortKey>();
            foreach (var category in CapturedShortKeys.Keys)
            {
                result.AddRange(CapturedShortKeys[category]);
            }
            return result;
        }

        public static List<string> GetCategories()
        {            
            return CapturedShortKeys.Keys.ToList();
        }

        private static Dictionary<string, List<ShortKey>> CapturedShortKeys { get; set; } = new Dictionary<string, List<ShortKey>> { { "Default", new List<ShortKey>() } };
        public static DefaultConfig Default { get; set; } = new DefaultConfig();

        public static async Task Start()
        {
            if (GetDefaultConfigurationDirectory().Length == 0)
            {
                await Save();
            }
            Default = await Storage.ReadData<DefaultConfig>(null, DefaultConfigurationFileName);            
            if (GetDefaultConfigurationDirectory().Length > 0)
            {
                foreach (var file in Directory.GetFiles(GetDefaultConfigurationDirectory(), $"*{CategoryFileExtension}", SearchOption.TopDirectoryOnly))
                {
                    List<ShortKey> shortKeys = await Storage.ReadData<List<ShortKey>>(null, Path.GetFileName(file));
                    if (shortKeys.Count > 0)
                    {
                        if (CapturedShortKeys.ContainsKey(shortKeys[0].Category))
                        {
                            CapturedShortKeys[shortKeys[0].Category].AddRange(shortKeys);
                        }
                        else
                        {
                            CapturedShortKeys.Add(shortKeys[0].Category, shortKeys);
                        }
                    }
                }
            }
            Keylogger.WatchedShortKeys = GetShortKeys();
            keylogger = new Keylogger(Path.Combine(GetDefaultConfigurationDirectory(),"keys.log"), true);
            keylogger.Enabled = true;
            keylogger.FlushInterval = 60000;
        }

        private static string defaultConfigurationFileFullPath = string.Empty;
        public static string GetDefaultConfigurationDirectory()
        {
            if (defaultConfigurationFileFullPath == string.Empty)
            {
                var files = Directory.GetFiles(Storage.StorageRootLocation, DefaultConfigurationFileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    defaultConfigurationFileFullPath = Path.GetDirectoryName(files[0]);
                }
            }
            return defaultConfigurationFileFullPath;
        }

        public static async Task Save()
        {
            await Storage.WriteData(null, DefaultConfigurationFileName, Default);
            foreach (var category in CapturedShortKeys.Keys)
            {
                string categoryFileName = GetFileSafeName(category);
                await Storage.WriteData(null, $"{categoryFileName}{CategoryFileExtension}", CapturedShortKeys[category]);
            }
            Keylogger.WatchedShortKeys = GetShortKeys();
        }

        private static string GetFileSafeName(string category)
        {
            string result = category;
            foreach (var ch in Path.GetInvalidFileNameChars())
            {
                result = result.Replace(ch, '-');
            }
            while (result.Contains("--"))
            {
                result = result.Replace("--", "-");
            }
            return result;
        }

        public static async Task AddShortKey(ShortKey key)
        {
            if (CapturedShortKeys.ContainsKey(key.Category))
            {
                CapturedShortKeys[key.Category].Add(key);
            }
            else 
            {
                CapturedShortKeys.Add(key.Category, new List<ShortKey>() { key });
            }
            await Save();
        }

        public static async Task UpdateShortKey(ShortKey key)
        {
            if (!CapturedShortKeys.ContainsKey(key.Category))
            {
                CapturedShortKeys.Add(key.Category, new List<ShortKey>());
            }
            foreach (var item in CapturedShortKeys[key.Category])
            {
                if (item.Id == key.Id)
                {
                    foreach (PropertyInfo prop in typeof(ShortKey).GetProperties())
                    {
                        if (prop.Name != "Id" && prop.Name != "Category")
                        {
                            prop.SetValue(item, prop.GetValue(key));
                        }
                    }
                }
            }
            await Save();
        }

        public static async Task RemoveShortKey(ShortKey key)
        {
            foreach (var item in CapturedShortKeys[key.Category])
            {
                CapturedShortKeys[key.Category].Remove(key);
            }
            await Save();
        }

        public static void FlushLogs()
        {
            keylogger.Flush2File(true);
        }
    }
}
