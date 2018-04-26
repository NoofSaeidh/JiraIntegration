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
            using (var configClient = new ConfigClient(AppDomain.CurrentDomain.BaseDirectory))
            {
                configClient.ReadConfigs();
                var jira = new JiraClient(configClient);

                jira.Test();
            }
        }


    }
}
