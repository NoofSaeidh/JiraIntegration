using JiraIntegration.Core;
using JiraIntegration.Core.Common;
using JiraIntegration.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JiraIntegration.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var config = Config.ReadAndSaveAsEncrypted(@"Creds.json"))
            {
                var jira = new JiraClient(config);

                jira.Test();
            }
        }


    }
}
