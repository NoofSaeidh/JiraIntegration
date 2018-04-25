using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Core.Common
{
    public interface IEncrypter
    {
        string Encrypt(string text);
        string Decrypt(string encryptedTest);
        bool IsEncrypted(string text);
    }
}
