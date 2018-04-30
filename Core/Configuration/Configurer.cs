using JiraIntegration.Core.Common;
using JiraIntegration.Core.Constants;
using JiraIntegration.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Configuration
{
    public class Configurer : IDisposable
    {
        public const string AuthConfigFileName = ConfigConst.AuthConfigFile;
        public const string SettingsFileName = ConfigConst.SettingsFile;

        public Configurer(string configsFolder)
        {
            ConfigsFolder = configsFolder ?? throw new ArgumentNullException(nameof(configsFolder));
            if (!Directory.Exists(ConfigsFolder))
                throw new ArgumentException($"Folder {configsFolder} doesn't exist.", nameof(configsFolder));

        }
        public IAuthConfig AuthConfig { get; set; }
        public string AuthConfigPath => Path.Combine(ConfigsFolder, AuthConfigFileName);
        public string ConfigsFolder { get; }
        public bool Initialized => AuthConfig != null && Settings != null;
        public Settings Settings { get; set; }
        public string SettingsConfigPath => Path.Combine(ConfigsFolder, SettingsFileName);
        public void CheckInitialization()
        {
            if (Initialized) return;

            if (AuthConfig == null)
                throw new JiraIntegrationException($"{nameof(Configurer)} is not fully initialized. " +
                    $"{nameof(AuthConfig)} is null.");

            if (Settings == null)
            {
                Settings = new Settings
                {
                    Favorites = new List<PersistentIssue>()
                };
            }
        }

        void IDisposable.Dispose()
        {
            (AuthConfig as IDisposable)?.Dispose();
        }

        public virtual void ReadConfigs()
        {
            Settings = ReadJson<Settings>(SettingsConfigPath);
            
            if (File.Exists(AuthConfigPath))
                AuthConfig = Configuration.AuthConfig.ReadAndSaveAsEncrypted(AuthConfigPath);
        }

        public virtual void SaveConfigs()
        {
            if (AuthConfig != null)
            {
                SaveJson(AuthConfigPath, AuthConfig);
            }
            if (Settings != null)
            {
                SaveJson(SettingsConfigPath, Settings);
            }
        }
        internal static T ReadJson<T>(string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        internal static void SaveJson(string path, object value)
        {
            //todo: maybe should review this
            if (value == null) return;
            
            File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));
        }
    }
}
