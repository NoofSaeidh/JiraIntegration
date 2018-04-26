using JiraIntegration.Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Configuration
{
    public interface IAuthConfig
    {
        string Password { get; }
        string Username { get; }
        string JiraAddress { get; }
    }

    public class AuthConfig : IAuthConfig, IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Encrypter _encrypter;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SecureString _encryterSecureString;

        protected AuthConfig()
        {
            _encryterSecureString = new SecureString();
            _encryterSecureString.AppendChar((char)80); // P
            _encryterSecureString.AppendChar((char)97); // a
            _encryterSecureString.AppendChar((char)115); // s
            _encryterSecureString.AppendChar((char)115); // s
            _encryterSecureString.AppendChar((char)119); // w
            _encryterSecureString.AppendChar((char)111); // o
            _encryterSecureString.AppendChar((char)114); // r
            _encryterSecureString.AppendChar((char)100); // d
            _encryterSecureString.AppendChar((char)50); // 2

            _encrypter = new Encrypter(_encryterSecureString);
        }

        [JsonConstructor]
        public AuthConfig(string username, string jiraAddress, string encryptedPassword) : this()
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            JiraAddress = jiraAddress ?? throw new ArgumentNullException(nameof(jiraAddress));
            EncryptedPassword = encryptedPassword ?? throw new ArgumentNullException(nameof(encryptedPassword));
        }

        [JsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Password
        {
            get
            {
                if (EncryptedPassword == null)
                    throw new InvalidOperationException($"{nameof(EncryptedPassword)} is not specified.");

                return _encrypter.Decrypt(EncryptedPassword, Username + JiraAddress);
            }
        }

        public string Username { get; private set; }

        public string JiraAddress { get; private set; }

        [JsonProperty(nameof(Password))]
        public string EncryptedPassword { get; private set; }

        public void Save(string file)
        {
            var value = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(file, value);
        }

        public static AuthConfig Read(string file)
        {
            var value = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<AuthConfig>(value);
        }

        public static AuthConfig ReadAndSaveAsEncrypted(string file)
        {
            var config = Read(file);
            if (config._encrypter.IsEncrypted(config.EncryptedPassword))
                return config;

            config.EncryptedPassword = config._encrypter.Encrypt(
                config.EncryptedPassword,
                config.Username + config.JiraAddress);

            config.Save(file);

            return config;
        }

        public static AuthConfig WithUnencryptedPassword(string jiraAddress, string username, string unencryptedPassword)
        {
            var config = new AuthConfig
            {
                JiraAddress = jiraAddress ?? throw new ArgumentNullException(nameof(jiraAddress)),
                Username = username ?? throw new ArgumentNullException(nameof(username)),
            };
            config.EncryptedPassword = config._encrypter
                .Encrypt(unencryptedPassword ?? throw new ArgumentNullException(nameof(unencryptedPassword))
                    , username + jiraAddress);

            return config;
        }

        void IDisposable.Dispose()
        {
            _encryterSecureString.Dispose();
        }
    }
}
